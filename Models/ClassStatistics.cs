namespace modified_structure_analysis.Models;

using System.ComponentModel;
using Services;

public class ClassBandStatistics
{
    [Category("Raw"), DisplayName("Count")]
    public int Count { get; set; }

    [Category("Raw"), DisplayName("Sum")]
    public float Sum { get; set; }

    [Category("Raw"), DisplayName("Minimum")]
    public float Minimum { get; set; }

    [Category("Raw"), DisplayName("Maximum")]
    public float Maximum { get; set; }

    [Category("Raw"), DisplayName("Mean")]
    public float Mean { get; set; }

    [Category("Raw"), DisplayName("Sigma (σ)")]
    public float Sigma { get; set; }

    [Category("Raw"), DisplayName("Variance (σ²)")]
    public float Variance { get; set; }

    [Category("Raw"), DisplayName("Skewness")]
    public float Skewness { get; set; }

    [Category("Raw"), DisplayName("Kurtosis")]
    public float Kurtosis { get; set; }

    [Category("Raw"), DisplayName("KernelC (bandwidth)")]
    public float KernelC { get; set; }

    [Category("Normalized"), DisplayName("Normalize KernelC")]
    public float NormalizeKernelC { get; set; }

    [Browsable(false)]
    public float ZMin { get; set; }

    [Browsable(false)]
    public float ZMax { get; set; }

    [Browsable(false)]
    public float ZMean { get; set; }

    [Browsable(false)]
    public float ZSigma { get; set; }

    [Browsable(false)]
    public float ZKernelC { get; set; }

    [Browsable(false)]
    public float ZNormalizeKernelC { get; set; }

    [Browsable(false)]
    public bool HasValidZStats => ZMin < ZMax && !float.IsInfinity(ZMin);
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

        Parallel.For(0, totalClasses, (cls, state) =>
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

                if (count > 0)
                {
                    var classValues = new List<float>(count);
                    foreach (int px in classPixels[cls])
                    {
                        float v = band.GetValue(px);
                        if (!float.IsNaN(v))
                            classValues.Add(v);
                    }
                    bs.KernelC = (float)BandwidthOptimizer.Compute(classValues, bs.Sigma, count, min, max, band.KernelType);
                }
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
                if (pixelCount > 0 && zScoreCache != null)
                {
                    var zValues = new List<float>(pixelCount);
                    foreach (int px in classPixels[cls])
                    {
                        int cacheIdx = b * cachedPixelCount + px;
                        float z = zScoreCache[cacheIdx];
                        if (!float.IsNaN(z))
                            zValues.Add(z);
                    }
                    bs.ZKernelC = (float)BandwidthOptimizer.Compute(zValues, bs.ZSigma, pixelCount, bs.ZMin, bs.ZMax, band.KernelType);
                }
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
        });

        return stats;
    }
}
