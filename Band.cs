namespace modified_structure_analysis
{
    public class Band
    {
        private string _name;

        private List<float> _values;

        private float _minimum;
        private float _maximum;

        public string Name => _name;

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
    }
}
