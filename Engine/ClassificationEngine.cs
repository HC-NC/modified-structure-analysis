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

    private ConcurrentDictionary<(int bandIndex, int valueIndex), float> _singleDensityCache = new();
    private ConcurrentDictionary<(int bandIndex, int classIndex, int valueIndex), float> _singleDensityPerClassCache = new();
    private ConcurrentDictionary<(List<int> bandIndices, int pixelIndex), float> _productDensityCache = new();
    private ConcurrentDictionary<(List<int> bandIndices, int pixelIndex), float> _multivariateDensityCache = new();

    private int[]? _sampleIndices;
    private int _sampleSize = 0;
    private bool _sampleInitialized = false;

    private float[]? _zScoreCache;
    private int _cachedBandCount;
    private int _cachedPixelCount;

    private int[]? _firstStageClassIndexes;
    private ClassStatistics[]? _classStats;

    private ConcurrentDictionary<(int bandIndex, int classIndex, int valueIndex), float> _zScoreSingleDensityCache = new();
    private ConcurrentDictionary<(List<int> bandIndices, int pixelIndex), float> _zScoreProductDensityCache = new();
    private ConcurrentDictionary<(List<int> bandIndices, int pixelIndex), float> _zScoreMultivariateDensityCache = new();

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

    private float EvaluateLeftSide(Condition condition, int pixelIndex)
    {
        return condition.LeftDensityType switch
        {
            DensityType.Single => GetSingleDensity(condition.LeftSingleBandIndex, pixelIndex),
            DensityType.Product => GetProductDensity(condition.LeftBandIndices, pixelIndex),
            DensityType.Multivariate => GetMultivariateDensity(condition.LeftBandIndices, pixelIndex),
            DensityType.ChannelValue => GetChannelValue(condition.LeftSingleBandIndex, pixelIndex),
            DensityType.ChannelZScore => GetChannelZScore(condition.LeftSingleBandIndex, pixelIndex),
            DensityType.ZScoreSingle => GetZScoreSingleDensity(condition.LeftSingleBandIndex, pixelIndex),
            DensityType.ZScoreProduct => GetZScoreProductDensity(condition.LeftBandIndices, pixelIndex),
            DensityType.ZScoreMultivariate => GetZScoreMultivariateDensity(condition.LeftBandIndices, pixelIndex),
            _ => 0
        };
    }

    private float EvaluateRightSide(CompareTarget rightSide, int pixelIndex)
    {
        if (rightSide.IsConstant)
            return (float)(rightSide.ConstantValue ?? 0);

        return rightSide.DensityType switch
        {
            DensityType.Single => GetSingleDensity(rightSide.SingleBandIndex, pixelIndex),
            DensityType.Product => GetProductDensity(rightSide.BandIndices, pixelIndex),
            DensityType.Multivariate => GetMultivariateDensity(rightSide.BandIndices, pixelIndex),
            DensityType.ChannelValue => GetChannelValue(rightSide.SingleBandIndex, pixelIndex),
            DensityType.ChannelZScore => GetChannelZScore(rightSide.SingleBandIndex, pixelIndex),
            DensityType.ZScoreSingle => GetZScoreSingleDensity(rightSide.SingleBandIndex, pixelIndex),
            DensityType.ZScoreProduct => GetZScoreProductDensity(rightSide.BandIndices, pixelIndex),
            DensityType.ZScoreMultivariate => GetZScoreMultivariateDensity(rightSide.BandIndices, pixelIndex),
            _ => 0
        };
    }

    private float GetChannelValue(int bandIndex, int pixelIndex)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;
        return _bands[bandIndex].GetPixelValue(pixelIndex);
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

    private float GetSingleDensity(int bandIndex, int pixelIndex)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;

        float normalizedValue = _bands[bandIndex].GetNormalizedValue(pixelIndex);
        int valueIndex = (int)(normalizedValue * _singleCacheSteps);

        if (_firstStageClassIndexes != null && _classStats != null)
        {
            int cls = _firstStageClassIndexes[pixelIndex];
            if (cls < 0 || cls >= _classStats.Length)
                return 0;

            var bandStats = _classStats[cls].Bands?[bandIndex];
            if (bandStats == null || bandStats.Count == 0 || bandStats.Maximum <= bandStats.Minimum)
                return 0;

            var perClassKey = (bandIndex, cls, valueIndex);
            if (_singleDensityPerClassCache.TryGetValue(perClassKey, out float cached))
                return cached;

            double density = 0.0;
            float kernelC = bandStats.NormalizeKernelC;
            KernelType kernelType = _bands[bandIndex].KernelType;
            int[] classPixels = _classStats[cls].PixelIndices!;

            foreach (int px in classPixels)
            {
                float pxValue = _bands[bandIndex].GetNormalizedValue(px);
                density += KernelFunctions.GetKernel(kernelType, (normalizedValue - pxValue) / kernelC);
            }

            density /= (bandStats.Count * kernelC);
            float result = (float)density;
            _singleDensityPerClassCache[perClassKey] = result;
            return result;
        }

        var key = (bandIndex, valueIndex);
        if (_singleDensityCache.TryGetValue(key, out float globalCached))
            return globalCached;

        float globalDensity = (float)BandKdeEstimator.Estimate(_bands[bandIndex], normalizedValue);
        _singleDensityCache[key] = globalDensity;
        return globalDensity;
    }

    private float GetProductDensity(List<int> bandIndices, int pixelIndex)
    {
        if (bandIndices.Count == 0)
            return 0;

        var key = (bandIndices, pixelIndex);
        if (_productDensityCache.TryGetValue(key, out float cached))
            return cached;

        double product = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
            {
                float singleDensity = GetSingleDensity(idx, pixelIndex);
                product *= singleDensity;
            }
        }

        float prodResult = (float)product;
        _productDensityCache[key] = prodResult;
        return prodResult;
    }

    private float GetMultivariateDensity(List<int> bandIndices, int pixelIndex)
    {
        if (bandIndices.Count == 0)
            return 0;

        var key = (bandIndices, pixelIndex);
        if (_multivariateDensityCache.TryGetValue(key, out float cached))
            return cached;

        if (_firstStageClassIndexes != null && _classStats != null)
        {
            int cls = _firstStageClassIndexes[pixelIndex];
            if (cls < 0 || cls >= _classStats.Length)
                return 0;

            int[] classPixels = _classStats[cls].PixelIndices!;
            int classCount = classPixels.Length;

            if (bandIndices.Count == 1 && bandIndices[0] >= 0 && bandIndices[0] < _bands.Count)
            {
                float singleResult = GetSingleDensity(bandIndices[0], pixelIndex);
                _multivariateDensityCache[key] = singleResult;
                return singleResult;
            }

            double productBandwidths = 1.0;
            foreach (int idx in bandIndices)
            {
                if (idx >= 0 && idx < _bands.Count && _classStats[cls].Bands != null)
                {
                    var bs = _classStats[cls].Bands[idx];
                    productBandwidths *= bs.NormalizeKernelC;
                }
            }

            double sum = 0.0;
            foreach (int samplePx in classPixels)
            {
                double kernelProduct = 1.0;

                foreach (int idx in bandIndices)
                {
                    if (idx < 0 || idx >= _bands.Count) continue;

                    var bandStats = _classStats[cls].Bands?[idx];
                    if (bandStats == null || bandStats.Maximum <= bandStats.Minimum)
                    {
                        kernelProduct = 0;
                        break;
                    }

                    float xi = _bands[idx].GetNormalizedValue(pixelIndex);
                    float xj = _bands[idx].GetNormalizedValue(samplePx);

                    kernelProduct *= KernelFunctions.GetKernel(
                        _bands[idx].KernelType,
                        (xi - xj) / bandStats.NormalizeKernelC);
                }

                sum += kernelProduct;
            }

            double normalization = classCount * productBandwidths;
            float mvResult = normalization > 0 ? (float)(sum / normalization) : 0;
            _multivariateDensityCache[key] = mvResult;
            return mvResult;
        }

        if (bandIndices.Count == 1 && bandIndices[0] >= 0 && bandIndices[0] < _bands.Count)
        {
            float normalizedValue = _bands[bandIndices[0]].GetNormalizedValue(pixelIndex);
            float mvResult = (float)BandKdeEstimator.Estimate(_bands[bandIndices[0]], normalizedValue);
            _multivariateDensityCache[key] = mvResult;
            return mvResult;
        }

        int totalPixels = _width * _height;
        double n = totalPixels;

        if (!_sampleInitialized)
        {
            InitializeSampleIndices(totalPixels);
            _sampleInitialized = true;
        }

        double globalProductBandwidths = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
                globalProductBandwidths *= _bands[idx].NormalizeKernelC;
        }

        double globalSum = 0.0;
        foreach (int sampleIdx in _sampleIndices)
        {
            double kernelProduct = 1;

            foreach (int idx in bandIndices)
            {
                if (idx < 0 || idx >= _bands.Count) continue;

                float xi = _bands[idx].GetNormalizedValue(pixelIndex);
                float xj = _bands[idx].GetNormalizedValue(sampleIdx);
                kernelProduct *= KernelFunctions.GetKernel(_bands[idx].KernelType, (xi - xj) / _bands[idx].NormalizeKernelC);
            }

            globalSum += kernelProduct;
        }

        double sampleNormalization = (double)_sampleSize * globalProductBandwidths;
        float mvSumResult = (float)(globalSum / sampleNormalization);
        _multivariateDensityCache[key] = mvSumResult;
        return mvSumResult;
    }

    private float GetZScoreSingleDensity(int bandIndex, int pixelIndex)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;
        if (_firstStageClassIndexes == null || _classStats == null)
            return 0;

        int cls = _firstStageClassIndexes[pixelIndex];
        if (cls < 0 || cls >= _classStats.Length)
            return 0;

        var bandStats = _classStats[cls].Bands?[bandIndex];
        if (bandStats == null || bandStats.Count == 0 || bandStats.ZMax <= bandStats.ZMin)
            return 0;

        float z = GetChannelZScore(bandIndex, pixelIndex);
        float normalizedZ = (z - bandStats.ZMin) / (bandStats.ZMax - bandStats.ZMin);
        normalizedZ = Math.Clamp(normalizedZ, 0, 1);

        int valueIndex = (int)(normalizedZ * _singleCacheSteps);
        var key = (bandIndex, cls, valueIndex);

        if (_zScoreSingleDensityCache.TryGetValue(key, out float cached))
            return cached;

        double density = 0.0;
        float zNormKernelC = bandStats.ZNormalizeKernelC;
        KernelType kernelType = _bands[bandIndex].KernelType;
        int[] classPixels = _classStats[cls].PixelIndices!;

        foreach (int px in classPixels)
        {
            float pxZ = GetChannelZScore(bandIndex, px);
            float pxNormZ = (pxZ - bandStats.ZMin) / (bandStats.ZMax - bandStats.ZMin);
            pxNormZ = Math.Clamp(pxNormZ, 0, 1);

            density += KernelFunctions.GetKernel(kernelType, (normalizedZ - pxNormZ) / zNormKernelC);
        }

        density /= (bandStats.Count * zNormKernelC);
        float result = (float)density;
        _zScoreSingleDensityCache[key] = result;
        return result;
    }

    private float GetZScoreProductDensity(List<int> bandIndices, int pixelIndex)
    {
        if (bandIndices.Count == 0)
            return 0;
        if (_firstStageClassIndexes == null || _classStats == null)
            return 0;

        var key = (bandIndices, pixelIndex);
        if (_zScoreProductDensityCache.TryGetValue(key, out float cached))
            return cached;

        double product = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
            {
                float singleDensity = GetZScoreSingleDensity(idx, pixelIndex);
                product *= singleDensity;
            }
        }

        float result = (float)product;
        _zScoreProductDensityCache[key] = result;
        return result;
    }

    private float GetZScoreMultivariateDensity(List<int> bandIndices, int pixelIndex)
    {
        if (bandIndices.Count == 0)
            return 0;
        if (_firstStageClassIndexes == null || _classStats == null)
            return 0;

        var key = (bandIndices, pixelIndex);
        if (_zScoreMultivariateDensityCache.TryGetValue(key, out float cached))
            return cached;

        if (bandIndices.Count == 1 && bandIndices[0] >= 0 && bandIndices[0] < _bands.Count)
        {
            float singleResult = GetZScoreSingleDensity(bandIndices[0], pixelIndex);
            _zScoreMultivariateDensityCache[key] = singleResult;
            return singleResult;
        }

        int cls = _firstStageClassIndexes[pixelIndex];
        if (cls < 0 || cls >= _classStats!.Length)
            return 0;

        int[] classPixels = _classStats[cls].PixelIndices!;
        int classCount = classPixels.Length;

        double productBandwidths = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count && _classStats[cls].Bands != null)
            {
                var bs = _classStats[cls].Bands[idx];
                productBandwidths *= bs.ZNormalizeKernelC;
            }
        }

        double sum = 0.0;
        foreach (int samplePx in classPixels)
        {
            double kernelProduct = 1.0;

            foreach (int idx in bandIndices)
            {
                if (idx < 0 || idx >= _bands.Count) continue;

                var bandStats = _classStats[cls].Bands?[idx];
                if (bandStats == null || bandStats.ZMax <= bandStats.ZMin)
                {
                    kernelProduct = 0;
                    break;
                }

                float xi = GetChannelZScore(idx, pixelIndex);
                float xj = GetChannelZScore(idx, samplePx);

                float xiNorm = (xi - bandStats.ZMin) / (bandStats.ZMax - bandStats.ZMin);
                float xjNorm = (xj - bandStats.ZMin) / (bandStats.ZMax - bandStats.ZMin);

                kernelProduct *= KernelFunctions.GetKernel(
                    _bands[idx].KernelType,
                    (xiNorm - xjNorm) / bandStats.ZNormalizeKernelC);
            }

            sum += kernelProduct;
        }

        double normalization = classCount * productBandwidths;
        float mvResult = normalization > 0 ? (float)(sum / normalization) : 0;
        _zScoreMultivariateDensityCache[key] = mvResult;
        return mvResult;
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
        _singleDensityPerClassCache.Clear();
        _productDensityCache.Clear();
        _multivariateDensityCache.Clear();
        _zScoreSingleDensityCache.Clear();
        _zScoreProductDensityCache.Clear();
        _zScoreMultivariateDensityCache.Clear();
        _sampleInitialized = false;
        _zScoreCache = null;
    }
}
