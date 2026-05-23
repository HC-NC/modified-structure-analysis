using System.ComponentModel;
using System.Linq;

namespace modified_structure_analysis
{
    public partial class ConditionEditorForm : Form
    {
        private List<Band> _bands;
        private Condition? _condition;
        private bool _isSecondStage;

        private List<DensityType> _leftDensityTypes = new();
        private List<DensityType> _rightDensityTypes = new();

        public Condition? ResultCondition { get; private set; }

        public ConditionEditorForm(List<Band> bands, Condition? existingCondition, bool isSecondStage = false)
        {
            _bands = bands;
            _condition = existingCondition;
            _isSecondStage = isSecondStage;

            InitializeComponent();

            Text = _condition == null ? "Add Condition" : "Edit Condition";

            PopulateDensityTypes();
        }

        private void PopulateDensityTypes()
        {
            _leftDensityTypes.Clear();
            _rightDensityTypes.Clear();
            _leftDensityTypeComboBox.Items.Clear();
            _rightDensityTypeComboBox.Items.Clear();

            var commonTypes = new (DensityType type, string label)[]
            {
                (DensityType.ChannelValue, "ChannelValue (v)"),
                (DensityType.ChannelZScore, "ChannelZScore (z)"),
                (DensityType.Single, "Single (p)"),
                (DensityType.Product, "Product (Π)"),
                (DensityType.Multivariate, "Multivariate (p)")
            };

            var secondStageTypes = new (DensityType type, string label)[]
            {
                (DensityType.ZScoreSingle, "ZScore Single (zs_p)"),
                (DensityType.ZScoreProduct, "ZScore Product (zs_Π)"),
                (DensityType.ZScoreMultivariate, "ZScore Multivariate (zs_p)")
            };

            foreach (var (type, label) in commonTypes)
            {
                _leftDensityTypes.Add(type);
                _leftDensityTypeComboBox.Items.Add(label);
                _rightDensityTypes.Add(type);
                _rightDensityTypeComboBox.Items.Add(label);
            }

            if (_isSecondStage)
            {
                foreach (var (type, label) in secondStageTypes)
                {
                    _leftDensityTypes.Add(type);
                    _leftDensityTypeComboBox.Items.Add(label);
                    _rightDensityTypes.Add(type);
                    _rightDensityTypeComboBox.Items.Add(label);
                }
            }
        }

        private static bool IsSingleBandType(DensityType type)
        {
            return type is DensityType.ChannelValue or DensityType.ChannelZScore
                or DensityType.Single or DensityType.ZScoreSingle;
        }

        private static bool IsDensityType(DensityType type)
        {
            return type is DensityType.Single or DensityType.Product or DensityType.Multivariate
                or DensityType.ZScoreSingle or DensityType.ZScoreProduct or DensityType.ZScoreMultivariate;
        }

        private void ConditionEditorForm_Load(object sender, EventArgs e)
        {
            LoadBands();
            LoadCondition();

            UpdateLeftVisibility(sender, e);
            UpdateRightVisibility(sender, e);
        }

        private void LoadBands()
        {
            _leftBandsListBox.Items.Clear();
            _rightBandsListBox.Items.Clear();

            _leftBandsListBox.Items.AddRange(_bands.ToArray());
            _rightBandsListBox.Items.AddRange(_bands.ToArray());
        }

        private void UpdateLeftVisibility(object sender, EventArgs e)
        {
            int idx = _leftDensityTypeComboBox.SelectedIndex;
            if (idx < 0 || idx >= _leftDensityTypes.Count) return;

            bool isSingle = IsSingleBandType(_leftDensityTypes[idx]);

            if (isSingle)
                _leftBandsListBox.SelectionMode = SelectionMode.One;
            else
                _leftBandsListBox.SelectionMode = SelectionMode.MultiExtended;
        }

        private void UpdateRightVisibility(object sender, EventArgs e)
        {
            bool isConstant = _rightConstantRadio.Checked;

            splitContainer2.Panel1Collapsed = isConstant;
            splitContainer2.Panel2Collapsed = !isConstant;

            int idx = _rightDensityTypeComboBox.SelectedIndex;
            if (idx < 0 || idx >= _rightDensityTypes.Count) return;

            bool isRightSingle = !isConstant && IsSingleBandType(_rightDensityTypes[idx]);

            if (isRightSingle)
                _rightBandsListBox.SelectionMode = SelectionMode.One;
            else
                _rightBandsListBox.SelectionMode = SelectionMode.MultiExtended;
        }

        private void LoadCondition()
        {
            if (_condition == null)
            {
                _leftDensityTypeComboBox.SelectedIndex = 0;
                _rightDensityTypeComboBox.SelectedIndex = 0;
                _operatorComboBox.SelectedIndex = 0;

                return;
            }

            int leftIdx = _leftDensityTypes.IndexOf(_condition.LeftDensityType);
            if (leftIdx >= 0)
                _leftDensityTypeComboBox.SelectedIndex = leftIdx;

            if (IsSingleBandType(_condition.LeftDensityType))
            {
                if (_condition.LeftSingleBandIndex >= 0)
                    _leftBandsListBox.SetSelected(_condition.LeftSingleBandIndex, true);
            }
            else
            {
                foreach (var idx in _condition.LeftBandIndices)
                {
                    if (idx >= 0 && idx < _leftBandsListBox.Items.Count)
                        _leftBandsListBox.SetSelected(idx, true);
                }
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

                int rightIdx = _rightDensityTypes.IndexOf(_condition.RightSide.DensityType);
                if (rightIdx >= 0)
                    _rightDensityTypeComboBox.SelectedIndex = rightIdx;

                if (IsSingleBandType(_condition.RightSide.DensityType))
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
        }

        private void Ok_Click(object? sender, EventArgs e)
        {
            int leftIdx = _leftDensityTypeComboBox.SelectedIndex;
            if (leftIdx < 0 || leftIdx >= _leftDensityTypes.Count)
            {
                Close();
                return;
            }

            ResultCondition = new Condition
            {
                LeftDensityType = _leftDensityTypes[leftIdx],
                Operator = (ComparisonOperator)_operatorComboBox.SelectedIndex
            };

            if (IsSingleBandType(ResultCondition.LeftDensityType))
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
                int rightIdx = _rightDensityTypeComboBox.SelectedIndex;
                if (rightIdx >= 0 && rightIdx < _rightDensityTypes.Count)
                {
                    ResultCondition.RightSide.DensityType = _rightDensityTypes[rightIdx];

                    if (IsSingleBandType(ResultCondition.RightSide.DensityType))
                    {
                        if (_rightBandsListBox.SelectedIndex >= 0)
                            ResultCondition.RightSide.SingleBandIndex = _rightBandsListBox.SelectedIndex;
                    }
                    else
                    {
                        ResultCondition.RightSide.BandIndices.AddRange(_rightBandsListBox.SelectedIndices.Cast<int>());
                    }
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _rightConstantTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!double.TryParse(_rightConstantTextBox.Text, out double v))
                _rightConstantTextBox.Undo();
        }
    }
}
