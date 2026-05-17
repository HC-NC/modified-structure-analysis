namespace modified_structure_analysis;

public class ClassificationEngine
{
    private List<Band> _bands;
    private List<ClassificationRule> _rules;

    private Dictionary<(int bandIndex, int pixelIndex), float> _singleDensityCache = new();
    private Dictionary<(List<int> bandIndices, int pixelIndex), float> _productDensityCache = new();
    private Dictionary<(List<int> bandIndices, int pixelIndex), float> _multivariateDensityCache = new();

    public ClassificationEngine(List<Band> bands, List<ClassificationRule> rules)
    {
        _bands = bands;
        _rules = rules;
    }

    public ClassificationResult Run()
    {
        int width = _bands[0].OriginalWidth;
        int height = _bands[0].OriginalHeight;

        var result = new ClassificationResult(width, height, _rules);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int pixelIndex = y * width + x;

                int? classIndex = EvaluatePixel(pixelIndex);

                if (classIndex.HasValue)
                    result.SetClass(pixelIndex, classIndex.Value);
            }
        }

        return result;
    }

    private int? EvaluatePixel(int pixelIndex)
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

        var key = (bandIndex, pixelIndex);
        if (_singleDensityCache.TryGetValue(key, out float cached))
            return cached;

        float density = _bands[bandIndex].GetNormalizedValue(pixelIndex);
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
                product *= _bands[idx].GetNormalizedValue(pixelIndex);
        }

        float result = (float)product;
        _productDensityCache[key] = result;
        return result;
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
            float result = _bands[bandIndices[0]].GetNormalizedValue(pixelIndex);
            _multivariateDensityCache[key] = result;
            return result;
        }

        double productBandwidths = 1.0;
        double sum = 0.0;
        int n = _bands[0].Count;

        foreach (int idx in bandIndices)
        {
            if (idx >= 0 && idx < _bands.Count)
            {
                productBandwidths *= _bands[idx].NormalizeKernelC;
            }
        }

        double normalization = n * productBandwidths;

        foreach (int idx in bandIndices)
        {
            if (idx < 0 || idx >= _bands.Count)
                continue;

            for (int j = 0; j < bandIndices.Count; j++)
            {
                if (j == idx || j >= bandIndices.Count)
                    continue;

                float xi = _bands[idx].GetNormalizedValue(pixelIndex);
                float xj = _bands[j].GetNormalizedValue(pixelIndex);

                float kernelValue = (float)KernelFunctions.GetEpanechnikov((xi - xj) / _bands[j].NormalizeKernelC);
                sum += kernelValue;
            }
        }

        float resultValue = (float)(sum / normalization);
        _multivariateDensityCache[key] = resultValue;
        return resultValue;
    }

    public void ClearCache()
    {
        _singleDensityCache.Clear();
        _productDensityCache.Clear();
        _multivariateDensityCache.Clear();
    }
}