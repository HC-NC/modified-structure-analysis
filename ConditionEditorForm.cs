using System.Drawing;

namespace modified_structure_analysis;

public class CompareTarget
{
    public DensityType DensityType { get; set; } = DensityType.Single;
    public int SingleBandIndex { get; set; }
    public List<int> BandIndices { get; set; } = new();
    public double? ConstantValue { get; set; }
    public bool IsConstant => ConstantValue.HasValue;

    public string ToDisplayString(List<Band> bands)
    {
        if (ConstantValue.HasValue)
            return ConstantValue.Value.ToString("F4");

        string bandName = "";
        if (DensityType == DensityType.Single && SingleBandIndex >= 0 && SingleBandIndex < bands.Count)
            bandName = bands[SingleBandIndex].Name;
        else if (BandIndices.Count > 0)
            bandName = string.Join(", ", BandIndices.Where(i => i >= 0 && i < bands.Count).Select(i => bands[i].Name));

        return DensityType switch
        {
            DensityType.Single => $"p({bandName})",
            DensityType.Product => $"Π({bandName})",
            DensityType.Multivariate => $"p({bandName})",
            _ => bandName
        };
    }
}

public class Condition
{
    public DensityType LeftDensityType { get; set; } = DensityType.Single;
    public int LeftSingleBandIndex { get; set; }
    public List<int> LeftBandIndices { get; set; } = new();

    public CompareTarget RightSide { get; set; } = new();

    public ComparisonOperator Operator { get; set; } = ComparisonOperator.GreaterThan;
}

public class ConditionEditorForm : Form
{
    private List<Band> _bands;
    private Condition? _condition;

    private ListBox _leftBandsListBox;
    private ComboBox _leftDensityTypeComboBox;
    private ComboBox _operatorComboBox;

    private ListBox _rightBandsListBox;
    private ComboBox _rightDensityTypeComboBox;
    private TextBox _rightConstantTextBox;
    private RadioButton _rightConstantRadio;
    private RadioButton _rightDensityRadio;

    public Condition? ResultCondition { get; private set; }

    public ConditionEditorForm(List<Band> bands, Condition? existingCondition)
    {
        _bands = bands;
        _condition = existingCondition;

        InitializeUI();
        LoadBands();

        if (_condition != null)
            LoadCondition();
    }

    private void InitializeUI()
    {
        Text = _condition == null ? "Add Condition" : "Edit Condition";
        Width = 500;
        Height = 480;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        int top = 20;
        int left = 20;
        int labelW = 100;
        int controlW = 180;

        var lblLeft = new Label { Left = left, Top = top, Width = 450, Text = "Left Side (density to evaluate):", Font = new Font(Font, FontStyle.Bold) };
        top += 25;

        _leftDensityTypeComboBox = new ComboBox { Left = left, Top = top, Width = 150 };
        _leftDensityTypeComboBox.Items.AddRange(new string[] { "Single", "Product", "Multivariate" });
        _leftDensityTypeComboBox.SelectedIndex = 0;
        _leftDensityTypeComboBox.SelectedIndexChanged += (s, e) => UpdateLeftVisibility();

        var lblLeftBands = new Label { Left = left + 160, Top = top + 3, Width = 80, Text = "Bands:" };
        _leftBandsListBox = new ListBox { Left = left + 240, Top = top, Width = 180, Height = 100, SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended };
        top += 105;

        _operatorComboBox = new ComboBox { Left = left, Top = top, Width = 60 };
        _operatorComboBox.Items.AddRange(new string[] { ">", "<", ">=", "<=", "==" });
        _operatorComboBox.SelectedIndex = 0;
        top += 35;

        var lblRight = new Label { Left = left, Top = top, Width = 450, Text = "Right Side:", Font = new Font(Font, FontStyle.Bold) };
        top += 25;

        _rightConstantRadio = new RadioButton { Left = left, Top = top, Width = 80, Text = "Constant", Checked = true };
        _rightConstantRadio.CheckedChanged += (s, e) => UpdateRightVisibility();

        _rightDensityRadio = new RadioButton { Left = left + 90, Top = top, Width = 80, Text = "Density" };
        top += 25;

        var lblRightConst = new Label { Left = left, Top = top, Width = labelW, Text = "Value:" };
        _rightConstantTextBox = new TextBox { Left = left + labelW, Top = top, Width = 80, Text = "0.5" };
        top += 30;

        _rightDensityTypeComboBox = new ComboBox { Left = left, Top = top, Width = 150 };
        _rightDensityTypeComboBox.Items.AddRange(new string[] { "Single", "Product", "Multivariate" });
        _rightDensityTypeComboBox.SelectedIndex = 0;
        _rightDensityTypeComboBox.SelectedIndexChanged += (s, e) => UpdateRightVisibility();

        var lblRightBands = new Label { Left = left + 160, Top = top + 3, Width = 80, Text = "Bands:" };
        _rightBandsListBox = new ListBox { Left = left + 240, Top = top, Width = 180, Height = 80, SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended };
        top += 85;

        var btnOk = new Button { Left = 180, Top = top, Width = 80, Text = "OK" };
        btnOk.Click += Ok_Click;

        var btnCancel = new Button { Left = 270, Top = top, Width = 80, Text = "Cancel" };
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.AddRange(new Control[] {
            lblLeft, _leftDensityTypeComboBox, lblLeftBands, _leftBandsListBox,
            _operatorComboBox, lblRight, _rightConstantRadio, _rightDensityRadio,
            lblRightConst, _rightConstantTextBox, _rightDensityTypeComboBox, lblRightBands, _rightBandsListBox,
            btnOk, btnCancel
        });

        UpdateLeftVisibility();
        UpdateRightVisibility();
    }

    private void LoadBands()
    {
        _leftBandsListBox.Items.Clear();
        _rightBandsListBox.Items.Clear();

        for (int i = 0; i < _bands.Count; i++)
        {
            _leftBandsListBox.Items.Add($"{i}: {_bands[i].Name}");
            _rightBandsListBox.Items.Add($"{i}: {_bands[i].Name}");
        }
    }

    private void UpdateLeftVisibility()
    {
        bool isSingle = _leftDensityTypeComboBox.SelectedIndex == 0;
        _leftBandsListBox.Enabled = !isSingle;
    }

    private void UpdateRightVisibility()
    {
        bool isConstant = _rightConstantRadio.Checked;
        _rightConstantTextBox.Visible = isConstant;
        _rightDensityTypeComboBox.Visible = !isConstant;
        _rightBandsListBox.Visible = !isConstant;

        bool isRightSingle = !isConstant && _rightDensityTypeComboBox.SelectedIndex == 0;
        if (isRightSingle)
        {
            _rightBandsListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
        }
        else
        {
            _rightBandsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        }
    }

    private void LoadCondition()
    {
        _leftDensityTypeComboBox.SelectedIndex = (int)_condition.LeftDensityType;

        if (_condition.LeftDensityType != DensityType.Single)
        {
            foreach (var idx in _condition.LeftBandIndices)
            {
                if (idx >= 0 && idx < _leftBandsListBox.Items.Count)
                    _leftBandsListBox.SetSelected(idx, true);
            }
        }
        else
        {
            if (_condition.LeftSingleBandIndex >= 0)
                _leftBandsListBox.SetSelected(_condition.LeftSingleBandIndex, true);
        }

        _operatorComboBox.SelectedIndex = (int)_condition.Operator;

        if (_condition.RightSide.IsConstant)
        {
            _rightConstantRadio.Checked = true;
            _rightConstantTextBox.Text = _condition.RightSide.ConstantValue?.ToString() ?? "0.5";
        }
        else
        {
            _rightDensityRadio.Checked = true;
            _rightDensityTypeComboBox.SelectedIndex = (int)_condition.RightSide.DensityType;

            if (_condition.RightSide.DensityType == DensityType.Single)
            {
                if (_condition.RightSide.SingleBandIndex >= 0)
                    _rightBandsListBox.SetSelected(_condition.RightSide.SingleBandIndex, true);
            }
            else
            {
                foreach (var idx in _condition.RightSide.BandIndices)
                {
                    if (idx >= 0 && idx < _rightBandsListBox.Items.Count)
                        _rightBandsListBox.SetSelected(idx, true);
                }
            }
        }
        UpdateRightVisibility();
    }

    private void Ok_Click(object? sender, EventArgs e)
    {
        ResultCondition = new Condition
        {
            LeftDensityType = (DensityType)_leftDensityTypeComboBox.SelectedIndex,
            Operator = (ComparisonOperator)_operatorComboBox.SelectedIndex
        };

        if (ResultCondition.LeftDensityType == DensityType.Single)
        {
            if (_leftBandsListBox.SelectedIndex >= 0)
                ResultCondition.LeftSingleBandIndex = _leftBandsListBox.SelectedIndex;
        }
        else
        {
            ResultCondition.LeftBandIndices.AddRange(_leftBandsListBox.SelectedIndices.Cast<int>());
        }

        ResultCondition.RightSide = new CompareTarget();

        if (_rightConstantRadio.Checked)
        {
            if (!double.TryParse(_rightConstantTextBox.Text, out double val))
                val = 0.0;
            ResultCondition.RightSide.ConstantValue = val;
        }
        else
        {
            ResultCondition.RightSide.DensityType = (DensityType)_rightDensityTypeComboBox.SelectedIndex;

            if (ResultCondition.RightSide.DensityType == DensityType.Single)
            {
                if (_rightBandsListBox.SelectedIndex >= 0)
                    ResultCondition.RightSide.SingleBandIndex = _rightBandsListBox.SelectedIndex;
            }
            else
            {
                ResultCondition.RightSide.BandIndices.AddRange(_rightBandsListBox.SelectedIndices.Cast<int>());
            }
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}

public class ConditionDisplayItem
{
    public Condition Condition { get; }
    public string Display { get; }

    public ConditionDisplayItem(Condition cond, List<Band> bands)
    {
        Condition = cond;
        string leftStr = GetDensityString(cond.LeftDensityType, cond.LeftSingleBandIndex, cond.LeftBandIndices, bands);
        string rightStr = cond.RightSide.ToDisplayString(bands);
        string opStr = cond.Operator switch
        {
            ComparisonOperator.GreaterThan => ">",
            ComparisonOperator.LessThan => "<",
            ComparisonOperator.GreaterOrEqual => ">=",
            ComparisonOperator.LessOrEqual => "<=",
            ComparisonOperator.Equal => "==",
            _ => "?"
        };
        Display = $"{leftStr} {opStr} {rightStr}";
    }

    private string GetDensityString(DensityType type, int singleIdx, List<int> indices, List<Band> bands)
    {
        if (type == DensityType.Single && singleIdx >= 0 && singleIdx < bands.Count)
            return $"p({bands[singleIdx].Name})";

        var names = indices.Where(i => i >= 0 && i < bands.Count).Select(i => $"{i}: {bands[i].Name}");
        string nameStr = string.Join(", ", names);

        return type switch
        {
            DensityType.Product => $"Π({nameStr})",
            DensityType.Multivariate => $"p({nameStr})",
            _ => nameStr
        };
    }
}