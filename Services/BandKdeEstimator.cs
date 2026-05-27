namespace modified_structure_analysis.Services;

using Models;

public static class BandKdeEstimator
{
    public static double Estimate(Band band, float normalizedValue)
    {
        if (band.Count == 0)
            return 0;

        double result = 0;
        int totalPixels = band.OriginalWidth * band.OriginalHeight;
        for (int i = 0; i < totalPixels; i++)
        {
            float pv = band.GetNormalizedValue(i);
            if (!float.IsNaN(band.GetValue(i)))
                result += KernelFunctions.GetKernel(band.KernelType, (normalizedValue - pv) / band.NormalizeKernelC);
        }
        return result / (band.Count * band.NormalizeKernelC);
    }
}
