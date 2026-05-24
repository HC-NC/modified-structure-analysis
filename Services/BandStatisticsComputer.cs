namespace modified_structure_analysis.Services;

using Models;

public static class BandStatisticsComputer
{
    public static void Compute(Band band)
    {
        int pixelCount = band.OriginalWidth * band.OriginalHeight;
        if (pixelCount == 0)
            return;

        int count = 0;
        float sum = 0;
        float min = float.MaxValue;
        float max = float.MinValue;

        for (int i = 0; i < pixelCount; i++)
        {
            float v = band.GetPixelValue(i);
            if (!float.IsNaN(v))
            {
                count++;
                sum += v;
                min = MathF.Min(v, min);
                max = MathF.Max(v, max);
            }
        }

        if (count == 0)
            return;

        float mean = sum / count;

        float variance = 0;
        float skewness = 0;
        float kurtosis = 0;

        for (int i = 0; i < pixelCount; i++)
        {
            float v = band.GetPixelValue(i);
            if (!float.IsNaN(v))
            {
                variance += MathF.Pow(v - mean, 2);
                skewness += MathF.Pow(v - mean, 3);
                kurtosis += MathF.Pow(v - mean, 4);
            }
        }

        variance /= count - 1;
        float sigma = MathF.Sqrt(variance);

        if (sigma > 0)
        {
            skewness /= count * MathF.Pow(sigma, 3);
            kurtosis = kurtosis / (count * MathF.Pow(sigma, 4)) - 3;
        }

        float kernelC = (float)KernelFunctions.GetDefaultBandwidth(sigma, count);
        float normalizeKernelC = kernelC / (max - min);

        band.SetStatistics(count, sum, min, max, mean, sigma, variance, skewness, kurtosis, kernelC, normalizeKernelC);
    }
}
