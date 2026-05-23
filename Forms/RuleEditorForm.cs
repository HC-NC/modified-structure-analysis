namespace modified_structure_analysis
{
    public partial class RuleEditorForm : Form
    {
        private List<Band> _bands;
        private ClassificationRule _rule;
        private ClassificationRule _oldRule;
        private bool _isEdit;
        private bool _isSecondStage;
        private ClassStatistics[]? _classStats;

        public ClassificationRule Rule => _rule;

        public RuleEditorForm(List<Band> bands, ClassificationRule? existingRule = null,
            bool isSecondStage = false, ClassStatistics[]? classStats = null)
        {
            InitializeComponent();

            _bands = bands;
            _isEdit = existingRule != null;
            _isSecondStage = isSecondStage;
            _classStats = classStats;
            _rule = existingRule ?? new ClassificationRule();
            _oldRule = (ClassificationRule)_rule.Clone();

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

        private void AddCondition_Click(object? sender, EventArgs e)
        {
            var condForm = new ConditionEditorForm(_bands, null, _isSecondStage, _classStats);
            if (condForm.ShowDialog() == DialogResult.OK && condForm.ResultCondition != null)
            {
                _rule.Conditions.Add(condForm.ResultCondition);
                UpdateConditionsList();
                _conditionsListBox.SelectedIndex = _conditionsListBox.Items.Count - 1;
            }
        }

        private void EditCondition_Click(object? sender, EventArgs e)
        {
            if (_conditionsListBox.SelectedIndex < 0) return;

            int index = _conditionsListBox.SelectedIndex;

            var selectedCond = _rule.Conditions[index];
            var condForm = new ConditionEditorForm(_bands, selectedCond, _isSecondStage, _classStats);
            if (condForm.ShowDialog() == DialogResult.OK && condForm.ResultCondition != null)
            {
                _rule.Conditions[index] = condForm.ResultCondition;
                UpdateConditionsList();
                _conditionsListBox.SelectedIndex = index;
            }
        }

        private void CloneCondition_Click(object sender, EventArgs e)
        {
            if (_conditionsListBox.SelectedIndex < 0) return;

            int index = _conditionsListBox.SelectedIndex;

            Condition condition = (Condition)_rule.Conditions[index].Clone();
            _rule.Conditions.Insert(index + 1, condition);
            UpdateConditionsList();
            _conditionsListBox.SelectedIndex = index + 1;
        }

        private void RemoveCondition_Click(object? sender, EventArgs e)
        {
            if (_conditionsListBox.SelectedIndex < 0) return;
            int index = _conditionsListBox.SelectedIndex;
            _rule.Conditions.RemoveAt(index);
            UpdateConditionsList();
            if (_rule.Conditions.Count == 0)
                return;

            if (index >= _conditionsListBox.Items.Count)
                index = _conditionsListBox.Items.Count - 1;

            _conditionsListBox.SelectedIndex = index;
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
            if (sender is Button btn && btn.DialogResult == DialogResult.Cancel)
                _rule = _oldRule;

            Close();
        }

        private void contextMenuStrip1_LocationChanged(object sender, EventArgs e)
        {
            int y = _conditionsListBox.PointToClient(contextMenuStrip1.PointToScreen(new Point(0, 0))).Y;
            int index = Math.Clamp(y / _conditionsListBox.ItemHeight, 0, _conditionsListBox.Items.Count - 1);

            _conditionsListBox.SelectedIndex = index;
        }
    }
}
