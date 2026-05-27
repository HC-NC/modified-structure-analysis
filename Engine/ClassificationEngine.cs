using System.Collections.Concurrent;
using modified_structure_analysis.Models;
using modified_structure_analysis.Services;

namespace modified_structure_analysis.Engine;

public class ClassificationEngine
{
    private List<Band> _bands;
    private List<ClassificationRule> _rules;
    private int _width;
    private int _height;

    private int _singleCacheSteps;

    private ConcurrentDictionary<(int bandIndex, int classId, int valueIndex), float> _singleDensityCache = new();
    private ConcurrentDictionary<(int classId, List<int> bandIndices, int pixelIndex), float> _multivariateDensityCache = new();

    private int[]? _sampleIndices;
    private int _sampleSize = 0;
    private bool _sampleInitialized = false;

    private float[]? _zScoreCache;
    private int _cachedBandCount;
    private int _cachedPixelCount;

    private int[]? _firstStageClassIndexes;
    private ClassStatistics[]? _classStats;

    public ClassificationMode Mode { get; set; } = ClassificationMode.RulePerClass;

    public float[]? ZScoreCache => _zScoreCache;
    public int CachedBandCount => _cachedBandCount;
    public int CachedPixelCount => _cachedPixelCount;

    public ClassificationEngine(List<Band> bands, List<ClassificationRule> rules)
    {
        _bands = bands;
        _rules = rules;

        if (_bands.Count > 0 && _bands[0].OriginalWidth > 0 && _bands[0].OriginalHeight > 0)
        {
            _width = _bands[0].OriginalWidth;
            _height = _bands[0].OriginalHeight;
        }

        float avgKernelC = 0;
        foreach (var band in _bands)
            avgKernelC += band.NormalizeKernelC;
        avgKernelC = _bands.Count > 0 ? avgKernelC / _bands.Count : 0.01f;

        _singleCacheSteps = Math.Clamp((int)(500 / avgKernelC), 100, 5000);

        int totalPixels = _width * _height;
        _sampleSize = Math.Min(totalPixels, Math.Max(2000, (int)(Math.Sqrt(totalPixels) * 4)));
    }

    public ClassificationResult Run()
    {
        if (Mode == ClassificationMode.DirectCheck)
            return RunDirectCheck();
        return RunRulePerClass();
    }

    private ClassificationResult RunRulePerClass()
    {
        var classificationResult = new ClassificationResult(_width, _height, _rules);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                int pixelIndex = y * _width + x;
                int? classIndex = EvaluatePixel(pixelIndex);
                if (classIndex.HasValue)
                    classificationResult.SetClass(pixelIndex, classIndex.Value);
            }
        }

        return classificationResult;
    }

    private ClassificationResult RunDirectCheck()
    {
        int pixelCount = _width * _height;
        int bandCount = _bands.Count;

        _zScoreCache = new float[bandCount * pixelCount];
        Array.Fill(_zScoreCache, float.NaN);
        _cachedBandCount = bandCount;
        _cachedPixelCount = pixelCount;

        int classCount = 1 << _rules.Count;
        Color[] palette = PaletteGenerator.GenerateHSV(classCount);
        var result = new ClassificationResult(_width, _height, palette);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                int pixelIndex = y * _width + x;

                int classIndex = 0;
                for (int ruleIdx = 0; ruleIdx < _rules.Count; ruleIdx++)
                {
                    var rule = _rules[ruleIdx];
                    if (!rule.IsEnabled)
                        continue;

                    bool conditionMet = true;
                    foreach (var condition in rule.Conditions)
                    {
                        if (!EvaluateCondition(condition, pixelIndex))
                        {
                            conditionMet = false;
                            break;
                        }
                    }

                    if (conditionMet)
                        classIndex |= (1 << ruleIdx);
                }

                result.SetClass(pixelIndex, classIndex);
            }
        }

        return result;
    }

    public ClassificationResult RunSecondStage(int[] firstStageClassIndices, int firstStageClassCount, Color[]? firstStagePalette = null)
    {
        _firstStageClassIndexes = firstStageClassIndices;

        int ruleCount = _rules.Count;
        int totalClassCount = firstStageClassCount * ruleCount;
        Color[] palette = firstStagePalette != null
            ? PaletteGenerator.GenerateSecondStage(firstStagePalette, ruleCount)
            : PaletteGenerator.GenerateHSV(totalClassCount);
        var result = new ClassificationResult(_width, _height, palette);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                int pixelIndex = y * _width + x;
                int firstClass = firstStageClassIndices[pixelIndex];

                if (firstClass < 0 || firstClass >= firstStageClassCount)
                    continue;

                int? secondClass = EvaluatePixel(pixelIndex);
                if (!secondClass.HasValue)
                    continue;

                int finalClass = firstClass * ruleCount + secondClass.Value;
                result.SetClass(pixelIndex, finalClass);
            }
        }

        return result;
    }

    public int? EvaluatePixel(int pixelIndex)
    {
        if (_bands.Count > 0 && _bands[0].OriginalWidth * _bands[0].OriginalHeight > pixelIndex)
        {
            for (int b = 0; b < _bands.Count; b++)
            {
                if (float.IsNaN(_bands[b].GetValue(pixelIndex)))
                    return null;
            }
        }

        if (Mode == ClassificationMode.DirectCheck)
        {
            int classIndex = 0;
            for (int ruleIdx = 0; ruleIdx < _rules.Count; ruleIdx++)
            {
                var rule = _rules[ruleIdx];
                if (!rule.IsEnabled)
                    continue;

                bool conditionMet = true;
                foreach (var condition in rule.Conditions)
                {
                    if (!EvaluateCondition(condition, pixelIndex))
                    {
                        conditionMet = false;
                        break;
                    }
                }

                if (conditionMet)
                    classIndex |= (1 << ruleIdx);
            }
            return classIndex;
        }

        for (int ruleIndex = 0; ruleIndex < _rules.Count; ruleIndex++)
        {
            var rule = _rules[ruleIndex];
            if (!rule.IsEnabled)
                continue;

            bool allConditionsMet = true;
            foreach (var condition in rule.Conditions)
            {
                if (!EvaluateCondition(condition, pixelIndex))
                {
                    allConditionsMet = false;
                    break;
                }
            }

            if (allConditionsMet)
                return ruleIndex;
        }

        return null;
    }

    private bool EvaluateCondition(Condition condition, int pixelIndex)
    {
        float leftValue = EvaluateLeftSide(condition, pixelIndex);
        float rightValue = EvaluateRightSide(condition.RightSide, pixelIndex);

        return condition.Operator switch
        {
            ComparisonOperator.GreaterThan => leftValue > rightValue,
            ComparisonOperator.LessThan => leftValue < rightValue,
            ComparisonOperator.GreaterOrEqual => leftValue >= rightValue,
            ComparisonOperator.LessOrEqual => leftValue <= rightValue,
            ComparisonOperator.Equal => Math.Abs(leftValue - rightValue) < 1e-6,
            _ => false
        };
    }

    private bool IsZScoreType(DensityType type) =>
        type is DensityType.ZScoreSingle or DensityType.ZScoreProduct or DensityType.ZScoreMultivariate;

    private IDensitySource CreateDensitySource(int pixelIndex, DensityType type)
    {
        if (IsZScoreType(type))
        {
            int cls = _firstStageClassIndexes![pixelIndex];
            return new ZScoreDensitySource(_bands, _classStats!, cls);
        }

        if (_classStats != null)
        {
            int cls = _firstStageClassIndexes![pixelIndex];
            return new PerClassRegularDensitySource(_bands, _classStats, cls);
        }

        return new GlobalDensitySource(_bands);
    }

    private float EvaluateLeftSide(Condition condition, int pixelIndex)
    {
        return condition.LeftDensityType switch
        {
            DensityType.ChannelValue => GetChannelValue(condition.LeftSingleBandIndex, pixelIndex),
            DensityType.ChannelZScore => GetChannelZScore(condition.LeftSingleBandIndex, pixelIndex),

            DensityType.Single => GetSingleDensity(condition.LeftSingleBandIndex, pixelIndex, CreateDensitySource(pixelIndex, condition.LeftDensityType)),
            DensityType.Product => GetProductDensity(condition.LeftBandIndices, pixelIndex, CreateDensitySource(pixelIndex, condition.LeftDensityType)),
            DensityType.Multivariate => GetMultivariateDensity(condition.LeftBandIndices, pixelIndex, CreateDensitySource(pixelIndex, condition.LeftDensityType)),

            DensityType.ZScoreSingle => GetSingleDensity(condition.LeftSingleBandIndex, pixelIndex, CreateDensitySource(pixelIndex, condition.LeftDensityType)),
            DensityType.ZScoreProduct => GetProductDensity(condition.LeftBandIndices, pixelIndex, CreateDensitySource(pixelIndex, condition.LeftDensityType)),
            DensityType.ZScoreMultivariate => GetMultivariateDensity(condition.LeftBandIndices, pixelIndex, CreateDensitySource(pixelIndex, condition.LeftDensityType)),

            _ => 0
        };
    }

    private float EvaluateRightSide(CompareTarget rightSide, int pixelIndex)
    {
        if (rightSide.IsConstant)
            return (float)(rightSide.ConstantValue ?? 0);

        return rightSide.DensityType switch
        {
            DensityType.ChannelValue => GetChannelValue(rightSide.SingleBandIndex, pixelIndex),
            DensityType.ChannelZScore => GetChannelZScore(rightSide.SingleBandIndex, pixelIndex),

            DensityType.Single => GetSingleDensity(rightSide.SingleBandIndex, pixelIndex, CreateDensitySource(pixelIndex, rightSide.DensityType)),
            DensityType.Product => GetProductDensity(rightSide.BandIndices, pixelIndex, CreateDensitySource(pixelIndex, rightSide.DensityType)),
            DensityType.Multivariate => GetMultivariateDensity(rightSide.BandIndices, pixelIndex, CreateDensitySource(pixelIndex, rightSide.DensityType)),

            DensityType.ZScoreSingle => GetSingleDensity(rightSide.SingleBandIndex, pixelIndex, CreateDensitySource(pixelIndex, rightSide.DensityType)),
            DensityType.ZScoreProduct => GetProductDensity(rightSide.BandIndices, pixelIndex, CreateDensitySource(pixelIndex, rightSide.DensityType)),
            DensityType.ZScoreMultivariate => GetMultivariateDensity(rightSide.BandIndices, pixelIndex, CreateDensitySource(pixelIndex, rightSide.DensityType)),

            _ => 0
        };
    }

    private float GetChannelValue(int bandIndex, int pixelIndex)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;
        return _bands[bandIndex].GetValue(pixelIndex);
    }

    private float GetChannelZScore(int bandIndex, int pixelIndex)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;

        if (_zScoreCache != null && bandIndex < _cachedBandCount && pixelIndex < _cachedPixelCount)
        {
            int cacheIdx = bandIndex * _cachedPixelCount + pixelIndex;
            if (!float.IsNaN(_zScoreCache[cacheIdx]))
                return _zScoreCache[cacheIdx];
        }

        float zScore = _bands[bandIndex].GetZScore(pixelIndex);

        if (_zScoreCache != null && bandIndex < _cachedBandCount && pixelIndex < _cachedPixelCount)
        {
            int cacheIdx = bandIndex * _cachedPixelCount + pixelIndex;
            _zScoreCache[cacheIdx] = zScore;
        }

        return zScore;
    }

    private float GetSingleDensity(int bandIndex, int pixelIndex, IDensitySource source)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;

        float value = source.GetValue(bandIndex, pixelIndex);
        if (float.IsNaN(value))
            return 0;

        float min = source.GetMinValue(bandIndex);
        float max = source.GetMaxValue(bandIndex);
        float range = max - min;
        int valueIndex = range > 0
            ? (int)((value - min) / range * _singleCacheSteps)
            : 0;
        var key = (bandIndex, source.ClassId, valueIndex);

        if (_singleDensityCache.TryGetValue(key, out float cached))
            return cached;

        float kernelC = source.GetKernelC(bandIndex);
        if (kernelC <= 0)
            return 0;

        KernelType kernelType = _bands[bandIndex].KernelType;
        double density = 0;

        if (source.ClassId >= 0)
        {
            int[] classPixels = _classStats![source.ClassId].PixelIndices!;
            int count = source.GetCount(bandIndex);
            if (count == 0)
                return 0;

            foreach (int px in classPixels)
            {
                float pv = source.GetValue(bandIndex, px);
                if (float.IsNaN(pv)) continue;
                density += KernelFunctions.GetKernel(kernelType, (value - pv) / kernelC);
            }
            density /= (count * kernelC);
        }
        else
        {
            int totalPixels = _width * _height;
            int count = 0;
            for (int i = 0; i < totalPixels; i++)
            {
                float pv = source.GetValue(bandIndex, i);
                if (float.IsNaN(pv)) continue;
                density += KernelFunctions.GetKernel(kernelType, (value - pv) / kernelC);
                count++;
            }
            if (count == 0)
                return 0;
            density /= (count * kernelC);
        }

        float result = (float)density;
        _singleDensityCache[key] = result;
        return result;
    }

    private float GetProductDensity(List<int> bandIndices, int pixelIndex, IDensitySource source)
    {
        if (bandIndices.Count == 0)
            return 0;

        double product = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
                product *= GetSingleDensity(idx, pixelIndex, source);
        }

        return (float)product;
    }

    private float GetMultivariateDensity(List<int> bandIndices, int pixelIndex, IDensitySource source)
    {
        if (bandIndices.Count == 0)
            return 0;

        var key = (source.ClassId, bandIndices, pixelIndex);
        if (_multivariateDensityCache.TryGetValue(key, out float cached))
            return cached;

        if (bandIndices.Count == 1 && bandIndices[0] >= 0 && bandIndices[0] < _bands.Count)
        {
            float singleResult = GetSingleDensity(bandIndices[0], pixelIndex, source);
            _multivariateDensityCache[key] = singleResult;
            return singleResult;
        }

        if (source.ClassId >= 0)
        {
            int cls = source.ClassId;
            int[] classPixels = _classStats![cls].PixelIndices!;
            int classCount = classPixels.Length;
            if (classCount == 0) return 0;

            double productBandwidths = 1.0;
            foreach (int idx in bandIndices)
            {
                if (idx >= 0 && idx < _bands.Count)
                    productBandwidths *= source.GetKernelC(idx);
            }

            double sum = 0.0;
            foreach (int samplePx in classPixels)
            {
                double kernelProduct = 1.0;

                foreach (int idx in bandIndices)
                {
                    if (idx < 0 || idx >= _bands.Count) continue;

                    float xi = source.GetValue(idx, pixelIndex);
                    if (float.IsNaN(xi)) { kernelProduct = 0; break; }
                    float xj = source.GetValue(idx, samplePx);
                    if (float.IsNaN(xj)) continue;
                    float kc = source.GetKernelC(idx);

                    kernelProduct *= KernelFunctions.GetKernel(
                        _bands[idx].KernelType,
                        (xi - xj) / kc);
                }

                sum += kernelProduct;
            }

            double normalization = classCount * productBandwidths;
            float mvResult = normalization > 0 ? (float)(sum / normalization) : 0;
            _multivariateDensityCache[key] = mvResult;
            return mvResult;
        }

        int totalPixels = _width * _height;

        if (!_sampleInitialized)
        {
            InitializeSampleIndices(totalPixels);
            _sampleInitialized = true;
        }

        double globalProductBandwidths = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
                globalProductBandwidths *= source.GetKernelC(idx);
        }

        double globalSum = 0.0;
        foreach (int sampleIdx in _sampleIndices)
        {
            double kernelProduct = 1;

            foreach (int idx in bandIndices)
            {
                if (idx < 0 || idx >= _bands.Count) continue;

                float xi = source.GetValue(idx, pixelIndex);
                if (float.IsNaN(xi)) { kernelProduct = 0; break; }
                float xj = source.GetValue(idx, sampleIdx);
                if (float.IsNaN(xj)) continue;
                float kc = source.GetKernelC(idx);

                kernelProduct *= KernelFunctions.GetKernel(
                    _bands[idx].KernelType,
                    (xi - xj) / kc);
            }

            globalSum += kernelProduct;
        }

        double sampleNormalization = (double)_sampleSize * globalProductBandwidths;
        float mvSumResult = (float)(globalSum / sampleNormalization);
        _multivariateDensityCache[key] = mvSumResult;
        return mvSumResult;
    }

    private void InitializeSampleIndices(int totalPixels)
    {
        _sampleIndices = new int[_sampleSize];

        if (_sampleSize == totalPixels)
        {
            for (int i = 0; i < _sampleSize; i++)
                _sampleIndices[i] = i;
        }
        else
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var indices = new HashSet<int>();
            while (indices.Count < _sampleSize)
            {
                indices.Add(random.Next(totalPixels));
            }
            indices.CopyTo(_sampleIndices);
        }

        _sampleInitialized = true;
    }

    public void UseZScoreCache(float[] cache, int bandCount, int pixelCount)
    {
        _zScoreCache = cache;
        _cachedBandCount = bandCount;
        _cachedPixelCount = pixelCount;
    }

    public void SetFirstStageContext(int[] classIndices, ClassStatistics[] classStats)
    {
        _firstStageClassIndexes = classIndices;
        _classStats = classStats;
    }

    public void EnsureZScoreCache(int bandCount, int pixelCount)
    {
        if (_zScoreCache == null || _cachedBandCount != bandCount || _cachedPixelCount != pixelCount)
        {
            _zScoreCache = new float[bandCount * pixelCount];
            Array.Fill(_zScoreCache, float.NaN);
            _cachedBandCount = bandCount;
            _cachedPixelCount = pixelCount;
        }
    }

    public void ReshuffleSamples()
    {
        int totalPixels = _width * _height;
        if (_sampleSize == totalPixels) return;

        var random = new Random(Guid.NewGuid().GetHashCode());
        for (int i = _sampleSize - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (_sampleIndices[i], _sampleIndices[j]) = (_sampleIndices[j], _sampleIndices[i]);
        }
    }

    public void ClearCache()
    {
        _singleDensityCache.Clear();
        _multivariateDensityCache.Clear();
        _sampleInitialized = false;
        _zScoreCache = null;
    }
}
