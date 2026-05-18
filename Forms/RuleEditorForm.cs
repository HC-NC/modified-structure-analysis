namespace modified_structure_analysis
{
    public partial class RuleEditorForm : Form
    {
        private List<Band> _bands;
        private ClassificationRule _rule;
        private bool _isEdit;

        public ClassificationRule ResultRule { get; private set; }

        public RuleEditorForm(List<Band> bands, ClassificationRule? existingRule = null)
        {
            InitializeComponent();

            _bands = bands;
            _isEdit = existingRule != null;
            _rule = existingRule ?? new ClassificationRule();
            ResultRule = _rule;

            _colorBtn.BackColor = _rule.Color;

            Text = _isEdit ? "Edit Rule" : "Add Rule";
        }

        private void RuleEditorForm_Load(object sender, EventArgs e)
        {
            UpdateConditionsList();

            if (_rule.Conditions.Count == 0)
                AddCondition_Click(sender, e);
        }

        private void UpdateConditionsList()
        {
            _conditionsListBox.Items.Clear();
            foreach (var cond in _rule.Conditions)
            {
                _conditionsListBox.Items.Add(new ConditionDisplayItem(cond, _bands));
            }
        }

        private string GetRulePreview()
        {
            if (_rule.Conditions.Count == 0)
                return "No conditions";

            var parts = _rule.Conditions.Select(c => new ConditionDisplayItem(c, _bands).Display);
            return string.Join(" AND ", parts);
        }

        private void AddCondition_Click(object? sender, EventArgs e)
        {
            var condForm = new ConditionEditorForm(_bands, null);
            if (condForm.ShowDialog() == DialogResult.OK && condForm.ResultCondition != null)
            {
                _rule.Conditions.Add(condForm.ResultCondition);
                UpdateConditionsList();
            }
        }

        private void EditCondition_Click(object? sender, EventArgs e)
        {
            if (_conditionsListBox.SelectedIndex < 0) return;

            var selectedCond = _rule.Conditions[_conditionsListBox.SelectedIndex];
            var condForm = new ConditionEditorForm(_bands, selectedCond);
            if (condForm.ShowDialog() == DialogResult.OK && condForm.ResultCondition != null)
            {
                _rule.Conditions[_conditionsListBox.SelectedIndex] = condForm.ResultCondition;
                UpdateConditionsList();
            }
        }

        private void RemoveCondition_Click(object? sender, EventArgs e)
        {
            if (_conditionsListBox.SelectedIndex < 0) return;
            _rule.Conditions.RemoveAt(_conditionsListBox.SelectedIndex);
            UpdateConditionsList();
        }

        private void ColorBtn_Click(object? sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = _rule.Color;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _rule.Color = colorDialog.Color;
                    _colorBtn.BackColor = _rule.Color;
                }
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
