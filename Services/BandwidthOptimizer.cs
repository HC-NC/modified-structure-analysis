using modified_structure_analysis.Config;

namespace modified_structure_analysis.Services;

public static class BandwidthOptimizer
{
    private const int MaxSamples = 2000;

    public static double ComputeDefault(double sigma, int count)
    {
        const double autoBetta = 0.951889;
        return autoBetta * sigma * Math.Pow(count, -1.0 / 5.0);
    }

    public static double Compute(IReadOnlyList<float> values, double sigma, int count,
        double dataMin, double dataMax, KernelType? kernelType = null,
        IProgress<int>? progress = null)
    {
        return AppSettings.Instance.BandwidthMethod switch
        {
            BandwidthMethod.LeastSquaresCrossValidation =>
                ComputeLscv(values, count, dataMin, dataMax, progress),
            BandwidthMethod.DirectPlugIn =>
                ComputeDirectPlugIn(values, sigma, count, progress),
            BandwidthMethod.LeaveOneOutLikelihood =>
                ComputeLeaveOneOutLikelihood(values, count, kernelType, progress),
            _ => ComputeDefault(sigma, count)
        };
    }

    private static List<float> Subsample(IReadOnlyList<float> values, int max)
    {
        if (values.Count <= max)
            return [.. values];

        var sampled = new List<float>(max);
        double step = (double)(values.Count - 1) / (max - 1);
        for (int i = 0; i < max; i++)
            sampled.Add(values[(int)(i * step)]);
        return sampled;
    }

    // ── Least-Squares Cross-Validation ─────────────────────────

    private static double ComputeLscv(IReadOnlyList<float> values, int n,
        double min, double max, IProgress<int>? progress)
    {
        var sample = Subsample(values, Math.Min(n, MaxSamples));
        int m = sample.Count;

        double h0 = ComputeDefault(StdDev(sample), m);
        double hMin = Math.Max(h0 * 0.1, 1e-8);
        double hMax = h0 * 10;
        const int steps = 50;

        double bestH = h0;
        double bestCv = double.MaxValue;

        for (int i = 0; i <= steps; i++)
        {
            double h = hMin + (hMax - hMin) * i / steps;
            // CV(h) = R(K)/(nh) + 1/(n²h) ΣᵢΣⱼ [K*K − 2K]((xᵢ−xⱼ)/h)
            // Using Gaussian reference kernel
            double sum = 0;
            for (int a = 0; a < m; a++)
            {
                for (int b = 0; b < m; b++)
                {
                    double d = (sample[a] - sample[b]) / h;
                    double d2 = d * d;
                    // K*K(v) = exp(−v²/4) / √(4π)
                    double kk = Math.Exp(-d2 / 4) / Math.Sqrt(4 * Math.PI);
                    // K(v)   = exp(−v²/2) / √(2π)
                    double k = Math.Exp(-d2 / 2) / Math.Sqrt(2 * Math.PI);
                    sum += kk - 2 * k;
                }
            }

            double rk = 1 / (2 * Math.Sqrt(Math.PI));
            double cv = rk / (m * h) + sum / (m * m * h);

            if (cv < bestCv)
            {
                bestCv = cv;
                bestH = h;
            }

            progress?.Report(i * 100 / steps);
        }

        return bestH;
    }

    // ── Direct Plug-In (Sheather–Jones) ────────────────────────

    private static double ComputeDirectPlugIn(IReadOnlyList<float> values,
        double sigma, int n, IProgress<int>? progress)
    {
        var sample = Subsample(values, Math.Min(n, MaxSamples));
        int m = sample.Count;
        double h0 = ComputeDefault(StdDev(sample), m);
        progress?.Report(20);

        // Estimate ψ₄ using Gaussian 4th-derivative kernel
        double psi4 = 0;
        for (int i = 0; i < m; i++)
            for (int j = i + 1; j < m; j++)
            {
                double d = (sample[i] - sample[j]) / h0;
                psi4 += G4(d);
            }
        psi4 = 2.0 / (m * (m - 1) * Math.Pow(h0, 5)) * psi4;
        progress?.Report(60);

        // Clamp ψ₄ to avoid degenerate results
        if (psi4 <= 0 || double.IsNaN(psi4) || double.IsInfinity(psi4))
            psi4 = 0.01 / Math.Pow(sigma, 5);

        // Epanechnikov constants: R(K) = 3/5, σ²(K) = 1/5
        double hSJ = Math.Pow(0.6 / (0.2 * m * psi4), 0.2);
        progress?.Report(80);

        // Sanity check — fall back to rule of thumb if unreasonable
        if (double.IsNaN(hSJ) || double.IsInfinity(hSJ) || hSJ <= 0 || hSJ > h0 * 100)
            hSJ = h0;

        progress?.Report(100);
        return hSJ;
    }

    // ── Leave-One-Out Likelihood (classification) ─────────────

    private static double ComputeLeaveOneOutLikelihood(IReadOnlyList<float> values,
        int n, KernelType? kernelType, IProgress<int>? progress)
    {
        var sample = Subsample(values, Math.Min(n, MaxSamples));
        int m = sample.Count;
        KernelType kt = kernelType ?? AppSettings.Instance.DefaultKernelType;

        double h0 = ComputeDefault(StdDev(sample), m);
        double hMin = Math.Max(h0 * 0.1, 1e-8);
        double hMax = h0 * 10;
        const int steps = 50;

        double bestH = h0;
        double bestNll = double.MaxValue;

        for (int i = 0; i <= steps; i++)
        {
            double h = hMin + (hMax - hMin) * i / steps;
            double nll = 0;

            for (int t = 0; t < m; t++)
            {
                double density = 0;
                for (int s = 0; s < m; s++)
                {
                    if (s == t) continue;
                    double u = (sample[t] - sample[s]) / h;
                    density += KernelFunctions.GetKernel(kt, u);
                }
                density /= (m - 1) * h;

                if (density > 1e-10)
                    nll -= Math.Log(density);
                else
                    nll += 100;
            }

            if (nll < bestNll)
            {
                bestNll = nll;
                bestH = h;
            }

            progress?.Report(i * 100 / steps);
        }

        return bestH;
    }

    // 4th derivative of standard Gaussian: φ⁽⁴⁾(x) = (x⁴ − 6x² + 3)·φ(x)
    private static double G4(double x)
    {
        double phi = Math.Exp(-x * x / 2) / Math.Sqrt(2 * Math.PI);
        return (x * x * x * x - 6 * x * x + 3) * phi;
    }

    private static double StdDev(IReadOnlyList<float> values)
    {
        double mean = 0;
        for (int i = 0; i < values.Count; i++)
            mean += values[i];
        mean /= values.Count;

        double variance = 0;
        for (int i = 0; i < values.Count; i++)
            variance += (values[i] - mean) * (values[i] - mean);
        return Math.Sqrt(variance / (values.Count - 1));
    }
}
