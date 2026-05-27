namespace modified_structure_analysis.Models;

using Services;

public class ClassBandStatistics
{
    public int Count { get; set; }
    public float Sum { get; set; }
    public float Minimum { get; set; }
    public float Maximum { get; set; }
    public float Mean { get; set; }
    public float Sigma { get; set; }
    public float Variance { get; set; }
    public float Skewness { get; set; }
    public float Kurtosis { get; set; }
    public float KernelC { get; set; }
    public float NormalizeKernelC { get; set; }

    public float ZMin { get; set; }
    public float ZMax { get; set; }
    public float ZMean { get; set; }
    public float ZSigma { get; set; }
    public float ZKernelC { get; set; }
    public float ZNormalizeKernelC { get; set; }
}

public class ClassStatistics
{
    public int ClassIndex { get; set; }
    public int PixelCount { get; set; }
    public int[]? PixelIndices { get; set; }
    public ClassBandStatistics[]? Bands { get; set; }

    public static ClassStatistics[] ComputeFromResult(
        ClassificationResult result,
        List<Band> bands,
        float[]? zScoreCache,
        int cachedPixelCount,
        int width,
        int height)
    {
        int totalPixels = width * height;
        int totalClasses = result.Palette?.Length ?? result.Rules?.Count ?? 0;

        if (totalClasses == 0)
            return [];

        int[] classCounts = new int[totalClasses];
        List<int>[] classPixels = new List<int>[totalClasses];
        for (int i = 0; i < totalClasses; i++)
            classPixels[i] = new List<int>();

        for (int i = 0; i < totalPixels; i++)
        {
            int cls = result.ClassIndices[i];
            if (cls >= 0 && cls < totalClasses)
            {
                classCounts[cls]++;
                classPixels[cls].Add(i);
            }
        }

        ClassStatistics[] stats = new ClassStatistics[totalClasses];

        for (int cls = 0; cls < totalClasses; cls++)
        {
            int pixelCount = classCounts[cls];
            ClassBandStatistics[] bandStats = new ClassBandStatistics[bands.Count];

            for (int b = 0; b < bands.Count; b++)
            {
                var bs = new ClassBandStatistics();
                Band band = bands[b];

                float sum = 0;
                float min = float.MaxValue;
                float max = float.MinValue;
                int count = 0;

                float zSum = 0;
                float zMin = float.MaxValue;
                float zMax = float.MinValue;

                foreach (int px in classPixels[cls])
                {
                    float v = band.GetValue(px);
                    if (!float.IsNaN(v))
                    {
                        sum += v;
                        min = MathF.Min(min, v);
                        max = MathF.Max(max, v);
                        count++;
                    }

                    if (zScoreCache != null && b < bands.Count)
                    {
                        int cacheIdx = b * cachedPixelCount + px;
                        float z = zScoreCache[cacheIdx];
                        if (!float.IsNaN(z))
                        {
                            zSum += z;
                            zMin = MathF.Min(zMin, z);
                            zMax = MathF.Max(zMax, z);
                        }
                    }
                }

                bs.Count = count;
                bs.Sum = sum;
                bs.Minimum = count > 0 ? min : 0;
                bs.Maximum = count > 0 ? max : 0;
                bs.Mean = count > 0 ? sum / count : 0;

                float variance = 0;
                float skewness = 0;
                float kurtosis = 0;

                foreach (int px in classPixels[cls])
                {
                    float v = band.GetValue(px);
                    if (!float.IsNaN(v))
                    {
                        variance += MathF.Pow(v - bs.Mean, 2);
                        skewness += MathF.Pow(v - bs.Mean, 3);
                        kurtosis += MathF.Pow(v - bs.Mean, 4);
                    }
                }

                bs.Variance = count > 1 ? variance / (count - 1) : 0;
                bs.Sigma = MathF.Sqrt(bs.Variance);
                if (bs.Sigma > 0 && count > 0)
                {
                    bs.Skewness = skewness / (count * MathF.Pow(bs.Sigma, 3));
                    bs.Kurtosis = kurtosis / (count * MathF.Pow(bs.Sigma, 4)) - 3;
                }

                bs.KernelC = count > 0
                    ? (float)KernelFunctions.GetDefaultBandwidth(bs.Sigma, count)
                    : 0;
                bs.NormalizeKernelC = bs.Maximum > bs.Minimum
                    ? bs.KernelC / (bs.Maximum - bs.Minimum)
                    : 0.01f;

                bs.ZMin = zMin;
                bs.ZMax = zMax;
                bs.ZMean = pixelCount > 0 && zScoreCache != null ? zSum / pixelCount : 0;

                float zVariance = 0;
                foreach (int px in classPixels[cls])
                {
                    if (zScoreCache != null && b < bands.Count)
                    {
                        int cacheIdx = b * cachedPixelCount + px;
                        float z = zScoreCache[cacheIdx];
                        if (!float.IsNaN(z))
                            zVariance += MathF.Pow(z - bs.ZMean, 2);
                    }
                }

                bs.ZSigma = pixelCount > 1 ? MathF.Sqrt(zVariance / (pixelCount - 1)) : 0;
                bs.ZKernelC = pixelCount > 0
                    ? (float)KernelFunctions.GetDefaultBandwidth(bs.ZSigma, pixelCount)
                    : 0;
                bs.ZNormalizeKernelC = bs.ZMax > bs.ZMin
                    ? bs.ZKernelC / (bs.ZMax - bs.ZMin)
                    : 0.01f;

                bandStats[b] = bs;
            }

            stats[cls] = new ClassStatistics
            {
                ClassIndex = cls,
                PixelCount = pixelCount,
                PixelIndices = classPixels[cls].ToArray(),
                Bands = bandStats
            };
        }

        return stats;
    }
}
