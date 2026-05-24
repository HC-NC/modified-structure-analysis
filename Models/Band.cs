using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using modified_structure_analysis.Services;

namespace modified_structure_analysis.Models;

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
    private GeoTransform? _geoTransform;

    private float _sum;
    private int _count;
    private float _minimum = float.MaxValue;
    private float _maximum = float.MinValue;

    private float _mean;
    private float _sigma;
    private float _variance;
    private float _skewness;
    private float _kurtosis;
    private float _kernelC;
    private float _normalizeKernelC;
    private KernelType _kernelType = KernelType.Epanechnikov;

    [DisplayName("Name")]
    [Description("Band name")]
    [Category("_")]
    public string Name => _name;

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
        set => _kernelType = value;
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

    public void SetMinMax(float min, float max)
    {
        _minimum = min;
        _maximum = max;
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
        SetValueAt(idx, value);
    }

    public void SetValueAt(int index, float value)
    {
        if (index >= 0 && index < _pixelValues!.Length)
        {
            _pixelValues[index] = value;
            if (!float.IsNaN(value))
            {
                _sum += value;
                _minimum = MathF.Min(value, _minimum);
                _maximum = MathF.Max(value, _maximum);
                _count++;
            }
        }
    }

    public void SetStatistics(int count, float sum, float min, float max, float mean, float sigma, float variance, float skewness, float kurtosis, float kernelC, float normalizeKernelC)
    {
        _count = count;
        _sum = sum;
        _minimum = min;
        _maximum = max;
        _mean = mean;
        _sigma = sigma;
        _variance = variance;
        _skewness = skewness;
        _kurtosis = kurtosis;
        _kernelC = kernelC;
        _normalizeKernelC = normalizeKernelC;
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

    public float GetValue(int i)
    {
        return GetPixelValue(i);
    }

    public float GetNormalizedValue(int i)
    {
        return GetNormalizedPixelValue(i);
    }

    public float GetZScore(int pixelIndex)
    {
        float val = GetPixelValue(pixelIndex);
        if (float.IsNaN(val))
            return 0;
        if (_sigma == 0)
            return 0;
        return (val - _mean) / _sigma;
    }
}
