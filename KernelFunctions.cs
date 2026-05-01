namespace modified_structure_analysis
{
    public enum KernelType
    {
        Uniform,
        Triangular,
        Epanechnikov,
        Quartic,
        Triweight,
        Tricube,
        Gaussian,
        Cosine,
        Logistic,
        Sigmoid
    }

    public static class KernelFunctions
    {
        public static double GetKernel(KernelType type, double u)
        {
            return type switch
            {
                KernelType.Uniform => GetUniform(u),
                KernelType.Triangular => GetTriangular(u),
                KernelType.Epanechnikov => GetEpanechnikov(u),
                KernelType.Quartic => GetQuartic(u),
                KernelType.Triweight => GetTriweight(u),
                KernelType.Tricube => GetTricube(u),
                KernelType.Gaussian => GetGaussian(u),
                KernelType.Cosine => GetCosine(u),
                KernelType.Logistic => GetLogistic(u),
                KernelType.Sigmoid => GetSigmoidFunction(u),
                _ => GetEpanechnikov(u)
            };
        }

        public static double GetUniform(double u)
        {
            return Math.Abs(u) <= 1 ? 0.5 : 0;
        }

        public static double GetTriangular(double u)
        {
            return Math.Abs(u) <= 1 ? 1 - Math.Abs(u) : 0;
        }

        public static double GetEpanechnikov(double u)
        {
            return Math.Abs(u) <= 1 ? 0.75 * (1 - Math.Pow(u, 2)) : 0;
        }

        public static double GetQuartic(double u)
        {
            return Math.Abs(u) <= 1 ? 0.9375 * Math.Pow(1 - Math.Pow(u, 2), 2) : 0;
        }

        public static double GetTriweight(double u)
        {
            return Math.Abs(u) <= 1 ? 1.09375 * Math.Pow(1 - Math.Pow(u, 2), 3) : 0;
        }

        public static double GetTricube(double u)
        {
            return Math.Abs(u) <= 1 ? 0.8641975308641976 * Math.Pow(1 - Math.Pow(Math.Abs(u), 3), 3) : 0;
        }

        public static double GetGaussian(double u)
        {
            return 0.3989422804014327 * Math.Exp(-Math.Pow(u, 2) / 2);
        }

        public static double GetCosine(double u)
        {
            return Math.Abs(u) <= 1 ? Math.PI / 4 * Math.Cos(Math.PI / 2 * u) : 0;
        }

        public static double GetLogistic(double u)
        {
            return 1 / (Math.Exp(u) + 2 + Math.Exp(-u));
        }

        public static double GetSigmoidFunction(double u)
        {
            return 2 / Math.PI * 1 / (Math.Exp(u) + Math.Exp(-u));
        }

        public static double GetDefaultBandwidth(float sigma, int count)
        {
            const float AUTO_BETTA = 0.951889f;
            return AUTO_BETTA * sigma * Math.Pow(count, -1f / 5f);
        }
    }
}