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

    private string? _sourceFilePath;
    private int _sourceBandIndex;
    private bool _isGdalSource;

    private List<float>? _values;
    private Dictionary<(int x, int y), float>? _spatialData;
    private float[]? _pixelValues;
    private float[]? _normalizedPixelValues;
    private float _noDataValue = float.NaN;
    private double _gdalNoDataValue;
    private bool _hasGdalNoData;
    private GeoTransform? _geoTransform;

    private float _sum = 0;
    private int _count = 0;
    private float _minimum = float.MaxValue;
    private float _maximum = float.MinValue;

    private float _mean = 0;
    private float _stdev = 0;
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
    public float StDev => _stdev;

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
    }

    public void AddValue(float value)
    {
        _values ??= new List<float>();
        _values.Add(value);

        _sum += value;

        _minimum = MathF.Min(value, _minimum);
        _maximum = MathF.Max(value, _maximum);
    }

    public void AddValueAtPosition(int x, int y, float value)
    {
        AddValue(value);
        _spatialData ??= new Dictionary<(int x, int y), float>();
        _spatialData[(x, y)] = value;
    }

    public void SetStats(float min, float max, float mean, float stdev)
    {
        _minimum = min;
        _maximum = max;
        _mean = mean;
        _stdev = stdev;
    }

    public void SetGeoTransform(GeoTransform transform)
    {
        _geoTransform = transform;
    }

    public void SetSource(string filePath, int bandIndex, double noDataValue, bool hasNoData)
    {
        _sourceFilePath = filePath;
        _sourceBandIndex = bandIndex;
        _isGdalSource = true;
        _gdalNoDataValue = noDataValue;
        _hasGdalNoData = hasNoData;
    }

    public bool CanReload => _isGdalSource && _sourceFilePath != null && File.Exists(_sourceFilePath);

    public void EnsurePixelValuesLoaded()
    {
        if (_pixelValues != null || !_isGdalSource || _sourceFilePath == null)
            return;

        using var ds = OSGeo.GDAL.Gdal.Open(_sourceFilePath, OSGeo.GDAL.Access.GA_ReadOnly);
        if (ds == null) return;
        using var gdalBand = ds.GetRasterBand(_sourceBandIndex);

        _pixelValues = new float[_originalWidth * _originalHeight];
        gdalBand.ReadRaster(0, 0, _originalWidth, _originalHeight, _pixelValues, _originalWidth, _originalHeight, 0, 0);

        _normalizedPixelValues = new float[_originalWidth * _originalHeight];
        Array.Fill(_normalizedPixelValues, float.NaN);

        for (int idx = 0; idx < _pixelValues.Length; idx++)
        {
            float v = _pixelValues[idx];
            if (_hasGdalNoData && (v == (float)_gdalNoDataValue || double.IsNaN(v)))
            {
                _pixelValues[idx] = float.NaN;
            }
        }

        _count = 0;
        _sum = 0;
        _minimum = float.MaxValue;
        _maximum = float.MinValue;
        for (int idx = 0; idx < _pixelValues.Length; idx++)
        {
            float v = _pixelValues[idx];
            if (!float.IsNaN(v))
            {
                _count++;
                _sum += v;
                if (v < _minimum) _minimum = v;
                if (v > _maximum) _maximum = v;
            }
        }
    }

    public void SetDimensions(int width, int height)
    {
        _originalWidth = width;
        _originalHeight = height;
    }

    public void ClearRawData()
    {
        _values?.Clear();
        _values?.TrimExcess();
        _spatialData?.Clear();
    }

    public bool HasPixelData => _pixelValues != null;

    public void UnloadPixelData()
    {
        _pixelValues = null;
        _normalizedPixelValues = null;
    }

    public void SetPixelValue(int x, int y, float value)
    {
        int idx = y * _originalWidth + x;
        SetValueAt(idx, value);
    }

    public void SetValueAt(int index, float value)
    {
        if (_pixelValues == null)
        {
            if (_originalWidth <= 0 || _originalHeight <= 0) return;
            _pixelValues = new float[_originalWidth * _originalHeight];
            Array.Fill(_pixelValues, _noDataValue);
            _normalizedPixelValues = new float[_originalWidth * _originalHeight];
            Array.Fill(_normalizedPixelValues, float.NaN);
        }

        if (index < 0 || index >= _pixelValues.Length) return;

        _pixelValues[index] = value;
        if (!float.IsNaN(value))
        {
            _sum += value;
            _minimum = MathF.Min(value, _minimum);
            _maximum = MathF.Max(value, _maximum);
            _count++;
        }
    }

    public void SetStatistics(int count, float sum, float min, float max, float mean, float sigma, float variance, float skewness, float kurtosis, float kernelC, float normalizeKernelC)
    {
        _count = count;
        _sum = sum;
        _minimum = min;
        _maximum = max;
        _mean = mean;
        _stdev = sigma;
        _variance = variance;
        _skewness = skewness;
        _kurtosis = kurtosis;
        _kernelC = kernelC;
        _normalizeKernelC = normalizeKernelC;
    }

    public float GetValue(int i)
    {
        if (_pixelValues == null && CanReload)
            EnsurePixelValuesLoaded();

        if (_pixelValues == null || i < 0 || i >= _pixelValues.Length)
            return _noDataValue;
        return _pixelValues[i];
    }

    public float GetNormalizedValue(int i)
    {
        if (_pixelValues == null && CanReload)
            EnsurePixelValuesLoaded();

        if (_pixelValues == null || i < 0 || i >= _pixelValues.Length)
            return 0;

        if (float.IsNaN(_pixelValues[i]))
            return 0;

        if (_normalizedPixelValues != null && float.IsNaN(_normalizedPixelValues[i]) && _maximum != _minimum)
        {
            _normalizedPixelValues[i] = (_pixelValues[i] - _minimum) / (_maximum - _minimum);
        }
        return _normalizedPixelValues?[i] ?? 0;
    }

    public override string ToString()
    {
        return Name;
    }

    public float Normalize(float v)
    {
        return (v - _minimum) / (_maximum - _minimum);
    }

    public float GetZScore(int pixelIndex)
    {
        float val = GetValue(pixelIndex);
        if (float.IsNaN(val))
            return 0;
        if (_stdev == 0)
            return 0;
        return (val - _mean) / _stdev;
    }
}
