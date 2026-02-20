using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modified_structure_analysis
{
    public class Band
    {
        private string _name;

        private List<float> _values;

        private float _sum = 0;
        private int _count = 0;
        private float _minimum = float.MaxValue;
        private float _maximum = float.MinValue;
        private float _mean;
        private float _sigma;
        private float _variance = 0;
        private float _skewness = 0;
        private float _kurtosis = 0;

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

        public Band(string name)
        {
            _name = name;

            _values = new List<float>();
        }

        public void AddValue(float value)
        {
            _values.Add(value);

            _sum += value;

            _minimum = MathF.Min(value, _minimum);
            _maximum = MathF.Max(value, _maximum);
        }

        public override string ToString()
        {
            return Name;
        }

        public float Normalize(float v)
        {
            return (v - _minimum) / (_maximum - _minimum);
        }

        public void CalculateStatistics()
        {
            _count = _values.Count;
            _mean = _sum / _count;

            foreach (float v in _values)
            {
                _variance += MathF.Pow(v - _mean, 2);
                _skewness += MathF.Pow(v - _mean, 3);
                _kurtosis += MathF.Pow(v - _mean, 4);
            }

            _variance /= _count - 1;

            _sigma = MathF.Sqrt(_variance);

            _skewness /= _count * MathF.Pow(_sigma, 3);
            _kurtosis = _kurtosis / (_count * MathF.Pow(_sigma, 4)) - 3;
        }

        public float GetValue(int i)
        {
            return _values[i];
        }
    }
}
