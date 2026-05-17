using System.Collections.Concurrent;

namespace modified_structure_analysis;

public class ClassificationEngine
{
    private List<Band> _bands;
    private List<ClassificationRule> _rules;
    private int _width;
    private int _height;

    private const int SingleValueCacheSteps = 1000;
    private ConcurrentDictionary<(int bandIndex, int valueIndex), float> _singleDensityCache = new();
    private ConcurrentDictionary<(List<int> bandIndices, int pixelIndex), float> _productDensityCache = new();
    private ConcurrentDictionary<(List<int> bandIndices, int pixelIndex), float> _multivariateDensityCache = new();

    public ClassificationEngine(List<Band> bands, List<ClassificationRule> rules)
    {
        _bands = bands;
        _rules = rules;

        if (_bands.Count > 0 && _bands[0].OriginalWidth > 0 && _bands[0].OriginalHeight > 0)
        {
            _width = _bands[0].OriginalWidth;
            _height = _bands[0].OriginalHeight;
        }
    }

    public ClassificationResult Run()
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

    public int? EvaluatePixel(int pixelIndex)
    {
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
            _ => 0
        };
    }

    private float GetSingleDensity(int bandIndex, int pixelIndex)
    {
        if (bandIndex < 0 || bandIndex >= _bands.Count)
            return 0;

        float normalizedValue = _bands[bandIndex].GetNormalizedValue(pixelIndex);
        int valueIndex = (int)(normalizedValue * SingleValueCacheSteps);

        var key = (bandIndex, valueIndex);
        if (_singleDensityCache.TryGetValue(key, out float cached))
            return cached;

        float density = (float)_bands[bandIndex].GetKernelDensityEstimate(normalizedValue);
        _singleDensityCache[key] = density;
        return density;
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
                float normalizedValue = _bands[idx].GetNormalizedValue(pixelIndex);
                double density = _bands[idx].GetKernelDensityEstimate(normalizedValue);
                product *= density;
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

        if (bandIndices.Count == 1 && bandIndices[0] >= 0 && bandIndices[0] < _bands.Count)
        {
            float normalizedValue = _bands[bandIndices[0]].GetNormalizedValue(pixelIndex);
            float mvResult = (float)_bands[bandIndices[0]].GetKernelDensityEstimate(normalizedValue);
            _multivariateDensityCache[key] = mvResult;
            return mvResult;
        }

        int n = _bands[0].OriginalWidth * _bands[0].OriginalHeight;

        double productBandwidths = 1.0;
        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
                productBandwidths *= _bands[idx].NormalizeKernelC;
        }

        double normalization = n * productBandwidths;

        double sum = 0.0;

        for (int i = 0; i < n; i++)
        {
            double kernelProduct = 1;

            foreach (int idx in bandIndices)
            {
                if (idx < 0 || idx >= _bands.Count) continue;

                float xi = _bands[idx].GetNormalizedValue(pixelIndex);
                float xj = _bands[idx].GetNormalizedValue(i);
                kernelProduct *= KernelFunctions.GetKernel(_bands[idx].KernelType, (xi - xj) / _bands[idx].NormalizeKernelC);
            }

            sum += kernelProduct;
        }

        float mvSumResult = (float)(sum / normalization);
        _multivariateDensityCache[key] = mvSumResult;
        return mvSumResult;
    }

    public void ClearCache()
    {
        _singleDensityCache.Clear();
        _productDensityCache.Clear();
        _multivariateDensityCache.Clear();
    }
}