using System.Drawing;

namespace modified_structure_analysis;

public class RuleEditorForm : Form
{
    private List<Band> _bands;
    private ClassificationRule _rule;
    private bool _isEdit;

    private ListBox _conditionsListBox;
    private Button _addConditionBtn;
    private Button _removeConditionBtn;
    private Button _editConditionBtn;
    private Button _colorBtn;
    private Button _okBtn;
    private Button _cancelBtn;

    public ClassificationRule ResultRule { get; private set; }

    public RuleEditorForm(List<Band> bands, ClassificationRule? existingRule = null)
    {
        _bands = bands;
        _isEdit = existingRule != null;
        _rule = existingRule ?? new ClassificationRule();
        ResultRule = _rule;

        InitializeUI();
        UpdateConditionsList();
    }

    private void InitializeUI()
    {
        Text = _isEdit ? "Edit Rule" : "Add Rule";
        Width = 500;
        Height = 400;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var lblConditions = new Label { Left = 20, Top = 20, Width = 200, Text = "Conditions:" };

        _conditionsListBox = new ListBox { Left = 20, Top = 45, Width = 350, Height = 150 };
        _conditionsListBox.DisplayMember = "Display";

        _addConditionBtn = new Button { Left = 380, Top = 45, Width = 80, Text = "Add" };
        _addConditionBtn.Click += AddCondition_Click;

        _removeConditionBtn = new Button { Left = 380, Top = 75, Width = 80, Text = "Remove" };
        _removeConditionBtn.Click += RemoveCondition_Click;

        _editConditionBtn = new Button { Left = 380, Top = 105, Width = 80, Text = "Edit" };
        _editConditionBtn.Click += EditCondition_Click;

        var lblColor = new Label { Left = 20, Top = 210, Width = 100, Text = "Class Color:" };
        _colorBtn = new Button { Left = 120, Top = 208, Width = 80, Height = 25, BackColor = _rule.Color, Text = "Color" };
        _colorBtn.Click += ColorBtn_Click;

        var lblPreview = new Label { Left = 20, Top = 250, Width = 400, Text = $"Preview: {GetRulePreview()}" };

        _okBtn = new Button { Left = 200, Top = 310, Width = 80, Text = "OK" };
        _okBtn.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };

        _cancelBtn = new Button { Left = 290, Top = 310, Width = 80, Text = "Cancel" };
        _cancelBtn.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.AddRange(new Control[] {
            lblConditions, _conditionsListBox, _addConditionBtn, _removeConditionBtn, _editConditionBtn,
            lblColor, _colorBtn, lblPreview, _okBtn, _cancelBtn
        });
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
}