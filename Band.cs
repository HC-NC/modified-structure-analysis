using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modified_structure_analysis
{
    public class Band
    {
        private string _name;
        private int _originalWidth;
        private int _originalHeight;

        private List<float> _values;
        private List<float> _normalizeValues;
        private Dictionary<(int x, int y), float> _spatialData;
        private float[]? _pixelValues;
        private float[]? _normalizedPixelValues;
        private float _noDataValue = float.NaN;

        private float _sum = 0;
        private int _count = 0;
        private float _minimum = float.MaxValue;
        private float _maximum = float.MinValue;
        private float _mean;
        private float _sigma;
        private float _variance = 0;
        private float _skewness = 0;
        private float _kurtosis = 0;

        private float _kernelC;
        private float _normalizeKernelC;
        private KernelType _kernelType = KernelType.Epanechnikov;

        private GeoTransform? _geoTransform;

        [DisplayName("Name")]
        [Description("Band name")]
        [Category("_")]
        public string Name => _name;

        [DisplayName("Values")]
        [Description("Sample values")]
        [Category("_")]
        public ReadOnlyCollection<float> Values => _values.AsReadOnly();

        [DisplayName("Sum")]
        [Description("Sum of sample value")]
        [Category("Statistics")]
        public float Sum => _sum;

        [DisplayName("Count")]
        [Description("Count of sample value")]
        [Category("Statistics")]
        public int Count => _count;

        [DisplayName("Minimum")]
        [Description("Minimum sample value")]
        [Category("Statistics")]
        public float Minimum => _minimum;

        [DisplayName("Maximum")]
        [Description("Maximum sample value")]
        [Category("Statistics")]
        public float Maximum => _maximum;

        [DisplayName("Mean")]
        [Description("Mean of sample value")]
        [Category("Statistics")]
        public float Mean => _mean;

        [DisplayName("Standard deviation")]
        [Description("Standard deviation of sample values")]
        [Category("Statistics")]
        public float Sigma => _sigma;

        [DisplayName("Variance")]
        [Description("Variance of sample values")]
        [Category("Statistics")]
        public float Variance => _variance;

        [DisplayName("Skewness")]
        [Description("Skewness of sample values")]
        [Category("Statistics")]
        public float Skewness => _skewness;

        [DisplayName("Kurtosis")]
        [Description("Kurtosis of sample values")]
        [Category("Statistics")]
        public float Kurtosis => _kurtosis;

        public float KernelC => _kernelC;
        public float NormalizeKernelC => _normalizeKernelC;

        [DisplayName("Width")]
        [Description("Original image width")]
        [Category("Spatial")]
        public int OriginalWidth => _originalWidth;

        [DisplayName("Height")]
        [Description("Original image height")]
        [Category("Spatial")]
        public int OriginalHeight => _originalHeight;

        [DisplayName("Geo Transform")]
        [Description("Georeferencing information")]
        [Category("Spatial")]
        public GeoTransform? GeoTransform => _geoTransform;

        [Browsable(false)]
        public Dictionary<(int x, int y), float> SpatialData => _spatialData;

        [DisplayName("Kernel Type")]
        [Description("Kernel function type for density estimation")]
        [Category("Kernel")]
        public KernelType KernelType
        {
            get => _kernelType;
            set
            {
                _kernelType = value;
                _kernelC = (float)KernelFunctions.GetDefaultBandwidth(_sigma, _count);
                _normalizeKernelC = _kernelC / (_maximum - _minimum);
            }
        }

        public Band(string name)
        {
            _name = name;

            _values = new List<float>();
            _normalizeValues = new List<float>();
            _spatialData = new Dictionary<(int x, int y), float>();
        }

        public void AddValue(float value)
        {
            _values.Add(value);

            _sum += value;

            _minimum = MathF.Min(value, _minimum);
            _maximum = MathF.Max(value, _maximum);
        }

        public void AddValueAtPosition(int x, int y, float value)
        {
            AddValue(value);
            _spatialData[(x, y)] = value;
        }

        public void SetGeoTransform(GeoTransform transform)
        {
            _geoTransform = transform;
        }

        public void SetDimensions(int width, int height)
        {
            _originalWidth = width;
            _originalHeight = height;
            _pixelValues = new float[width * height];
            _normalizedPixelValues = new float[width * height];
            for (int i = 0; i < _pixelValues.Length; i++)
            {
                _pixelValues[i] = _noDataValue;
                _normalizedPixelValues[i] = 0;
            }
        }

        public void SetPixelValue(int x, int y, float value)
        {
            int idx = y * _originalWidth + x;
            if (idx >= 0 && idx < _pixelValues!.Length)
            {
                _pixelValues[idx] = value;
                if (!float.IsNaN(value))
                {
                    _sum += value;
                    _minimum = MathF.Min(value, _minimum);
                    _maximum = MathF.Max(value, _maximum);
                }
            }
        }

        public float GetPixelValue(int i)
        {
            if (_pixelValues == null || i < 0 || i >= _pixelValues.Length)
                return _noDataValue;
            return _pixelValues[i];
        }

        public float GetNormalizedPixelValue(int i)
        {
            if (_pixelValues == null || i < 0 || i >= _pixelValues.Length)
                return 0;

            if (float.IsNaN(_pixelValues[i]))
                return 0;

            if (_normalizedPixelValues![i] == 0 && _maximum != _minimum)
            {
                _normalizedPixelValues[i] = (_pixelValues[i] - _minimum) / (_maximum - _minimum);
            }
            return _normalizedPixelValues[i];
        }

        public override string ToString()
        {
            return Name;
        }

        public void Normalize()
        {
            _normalizeValues.Clear();

            foreach (var v in _values)
                _normalizeValues.Add(Normalize(v));
        }

        public float Normalize(float v)
        {
            return (v - _minimum) / (_maximum - _minimum);
        }

        public void CalculateStatistics()
        {
            if (_pixelValues == null)
                return;

            _count = 0;
            _sum = 0;
            _minimum = float.MaxValue;
            _maximum = float.MinValue;

            foreach (float v in _pixelValues)
            {
                if (!float.IsNaN(v))
                {
                    _count++;
                    _sum += v;
                    _minimum = MathF.Min(v, _minimum);
                    _maximum = MathF.Max(v, _maximum);
                }
            }

            if (_count == 0)
                return;

            _mean = _sum / _count;

            foreach (float v in _pixelValues)
            {
                if (!float.IsNaN(v))
                {
                    _variance += MathF.Pow(v - _mean, 2);
                    _skewness += MathF.Pow(v - _mean, 3);
                    _kurtosis += MathF.Pow(v - _mean, 4);
                }
            }

            _variance /= _count - 1;

            _sigma = MathF.Sqrt(_variance);

            if (_sigma > 0)
            {
                _skewness /= _count * MathF.Pow(_sigma, 3);
                _kurtosis = _kurtosis / (_count * MathF.Pow(_sigma, 4)) - 3;
            }

            _kernelC = (float)KernelFunctions.GetDefaultBandwidth(_sigma, _count);
            _normalizeKernelC = _kernelC / (_maximum - _minimum);
        }

        public double GetKernelDensityEstimate(float v)
        {
            if (_count == 0)
                return 0;

            double result = 0;
            for (int i = 0; i < _pixelValues!.Length; i++)
            {
                float pv = GetNormalizedValue(i);
                if (!float.IsNaN(_pixelValues[i]))
                    result += KernelFunctions.GetKernel(_kernelType, (v - pv) / _normalizeKernelC);
            }
            return result / (_count * _normalizeKernelC);
        }

        public float GetValue(int i)
        {
            return GetPixelValue(i);
        }

        public float GetNormalizedValue(int i)
        {
            return GetNormalizedPixelValue(i);
        }
    }
}
