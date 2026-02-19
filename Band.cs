using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modified_structure_analysis
{
    public class Band
    {
        private string _name;

        private List<float> _values;

        private float _minimum = float.MaxValue;
        private float _maximum = float.MinValue;

        [DisplayName("Name")]
        [Description("Band name")]
        [Category("_")]
        public string Name => _name;

        [DisplayName("Values")]
        [Description("Sample values")]
        [Category("_")]
        public ReadOnlyCollection<float> Values => _values.AsReadOnly();

        [DisplayName("Minimum")]
        [Description("Minimum sample value")]
        [Category("Statistics")]
        public float Minimum => _minimum;

        [DisplayName("Maximum")]
        [Description("Maximum sample value")]
        [Category("Statistics")]
        public float Maximum => _maximum;

        public Band(string name)
        {
            _name = name;

            _values = new List<float>();
        }

        public void AddValue(float value)
        {
            _values.Add(value);

            _minimum = Math.Min(value, _minimum);
            _maximum = Math.Max(value, _maximum);
        }

        public override string ToString()
        {
            return Name;
        }

        public float Normalize(float v)
        {
            return (v - _minimum) / (_maximum - _minimum);
        }
    }
}
