namespace modified_structure_analysis.Services;

using Models;

public static class BandStatisticsComputer
{
    public static void Compute(Band band)
    {
        int pixelCount = band.OriginalWidth * band.OriginalHeight;
        if (pixelCount == 0)
            return;

        int count = band.Count;
        float sum = band.Sum;
        float min = band.Minimum;
        float max = band.Maximum;
        float mean = band.Mean;

        if (count == 0 || sum == 0 || min == float.MaxValue || max == float.MinValue)
        {
            for (int i = 0; i < pixelCount; i++)
            {
                float v = band.GetValue(i);
                if (!float.IsNaN(v))
                {
                    count++;
                    sum += v;
                    if (v < min) min = v;
                    if (v > max) max = v;
                }
            }



            if (count == 0)
                return;
        }

        if (mean == 0)
            mean = sum / count;

        float variance = 0;
        float skewness = 0;
        float kurtosis = 0;

        for (int i = 0; i < pixelCount; i++)
        {
            float v = band.GetValue(i);
            if (!float.IsNaN(v))
            {
                float d = v - mean;
                float d2 = d * d;
                variance += d2;
                skewness += d2 * d;
                kurtosis += d2 * d2;
            }
        }

        variance /= count - 1;
        float stdev = MathF.Sqrt(variance);

        if (stdev > 0)
        {
            float sigma3 = stdev * stdev * stdev;
            float sigma4 = sigma3 * stdev;
            skewness /= count * sigma3;
            kurtosis = kurtosis / (count * sigma4) - 3;
        }

        float kernelC = (float)KernelFunctions.GetDefaultBandwidth(stdev, count);
        float normalizeKernelC = kernelC / (max - min);

        band.SetStatistics(count, sum, min, max, mean, stdev, variance, skewness, kurtosis, kernelC, normalizeKernelC);
    }
}
