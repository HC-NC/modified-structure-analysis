using System.ComponentModel;
using System.Linq;

using modified_structure_analysis.Models;
using modified_structure_analysis.Properties;

namespace modified_structure_analysis.Forms
{
    public partial class ConditionEditorForm : Form
    {
        private List<Band> _bands;
        private Condition? _condition;
        private bool _isSecondStage;
        private HashSet<int> _zScoreValidBands = new();

        private List<DensityType> _leftDensityTypes = new();
        private List<DensityType> _rightDensityTypes = new();

        public Condition? ResultCondition { get; private set; }

        public ConditionEditorForm(List<Band> bands, Condition? existingCondition,
            bool isSecondStage = false, ClassStatistics[]? classStats = null)
        {
            _bands = bands;
            _condition = existingCondition;
            _isSecondStage = isSecondStage;

            if (classStats != null)
            {
                for (int b = 0; b < bands.Count; b++)
                {
                    bool hasStats = false;
                    foreach (var cs in classStats)
                    {
                        var bs = cs.Bands?[b];
                        if (bs != null && bs.Count > 0 && bs.ZMax > bs.ZMin)
                        {
                            hasStats = true;
                            break;
                        }
                    }
                    if (hasStats)
                        _zScoreValidBands.Add(b);
                }
            }

            InitializeComponent();

            Text = _condition == null ? Resources.Add_Condition : Resources.Edit_Condition;

            PopulateDensityTypes();
        }

        private static bool IsZScoreType(DensityType type)
        {
            return type is DensityType.ZScoreSingle
                or DensityType.ZScoreProduct
                or DensityType.ZScoreMultivariate;
        }

        private void PopulateDensityTypes()
        {
            _leftDensityTypes.Clear();
            _rightDensityTypes.Clear();
            _leftDensityTypeComboBox.Items.Clear();
            _rightDensityTypeComboBox.Items.Clear();

            (DensityType type, string label)[] items;

            if (_isSecondStage)
            {
                items = [
                    (DensityType.ChannelValue, Resources.ChannelValue),
                    (DensityType.ChannelZScore, Resources.ChannelZScore),
                    (DensityType.Single, Resources.Single),
                    (DensityType.Product, Resources.ProductDensity),
                    (DensityType.Multivariate, Resources.Multivariate),
                    (DensityType.ZScoreSingle, Resources.ZScoreSingle),
                    (DensityType.ZScoreProduct, Resources.ZScoreProduct),
                    (DensityType.ZScoreMultivariate, Resources.ZScoreMultivariate)
                ];
            }
            else
            {
                items = [
                    (DensityType.ChannelValue, Resources.ChannelValue),
                    (DensityType.ChannelZScore, Resources.ChannelZScore),
                    (DensityType.Single, Resources.Single),
                    (DensityType.Product, Resources.ProductDensity),
                    (DensityType.Multivariate, Resources.Multivariate)
                ];
            }

            foreach (var (type, label) in items)
            {
                _leftDensityTypes.Add(type);
                _leftDensityTypeComboBox.Items.Add(label);
                _rightDensityTypes.Add(type);
                _rightDensityTypeComboBox.Items.Add(label);
            }
        }

        private static bool IsSingleBandType(DensityType type)
        {
            return type is DensityType.ChannelValue or DensityType.ChannelZScore
                or DensityType.Single or DensityType.ZScoreSingle;
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

            DensityType type = _leftDensityTypes[idx];
            bool isSingle = IsSingleBandType(type);
            bool isZScore = IsZScoreType(type);

            if (isZScore && _leftBandsListBox.Items.Count == _bands.Count)
            {
                RebuildBandsList(_leftBandsListBox, isZScore);
            }
            else if (!isZScore && _leftBandsListBox.Items.Count != _bands.Count)
            {
                RebuildBandsList(_leftBandsListBox, isZScore);
            }

            _leftBandsListBox.SelectionMode = isSingle
                ? SelectionMode.One
                : SelectionMode.MultiExtended;
        }

        private void UpdateRightVisibility(object sender, EventArgs e)
        {
            bool isConstant = _rightConstantRadio.Checked;

            splitContainer2.Panel1Collapsed = isConstant;
            splitContainer2.Panel2Collapsed = !isConstant;

            int idx = _rightDensityTypeComboBox.SelectedIndex;
            if (idx < 0 || idx >= _rightDensityTypes.Count) return;

            DensityType type = _rightDensityTypes[idx];
            bool isSingle = !isConstant && IsSingleBandType(type);
            bool isZScore = !isConstant && IsZScoreType(type);

            if (isZScore && _rightBandsListBox.Items.Count == _bands.Count)
            {
                RebuildBandsList(_rightBandsListBox, isZScore);
            }
            else if (!isZScore && _rightBandsListBox.Items.Count != _bands.Count)
            {
                RebuildBandsList(_rightBandsListBox, isZScore);
            }

            _rightBandsListBox.SelectionMode = isSingle
                ? SelectionMode.One
                : SelectionMode.MultiExtended;
        }

        private void RebuildBandsList(ListBox listBox, bool isZScore)
        {
            listBox.Items.Clear();

            if (isZScore)
            {
                foreach (int bi in _zScoreValidBands.OrderBy(b => b))
                    listBox.Items.Add(_bands[bi]);
            }
            else
            {
                foreach (var band in _bands)
                    listBox.Items.Add(band);
            }
        }

        private List<int> GetSelectedBandIndices(ListBox listBox)
        {
            return listBox.SelectedItems.Cast<Band>()
                .Select(b => _bands.IndexOf(b))
                .Where(i => i >= 0)
                .ToList();
        }

        private int GetSingleSelectedBandIndex(ListBox listBox)
        {
            if (listBox.SelectedItem is Band band)
                return _bands.IndexOf(band);
            return -1;
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
                if (_condition.LeftSingleBandIndex >= 0
                    && _condition.LeftSingleBandIndex < _bands.Count)
                {
                    var target = _bands[_condition.LeftSingleBandIndex];
                    int listIdx = _leftBandsListBox.Items.IndexOf(target);
                    if (listIdx >= 0)
                        _leftBandsListBox.SetSelected(listIdx, true);
                }
            }
            else
            {
                foreach (var bi in _condition.LeftBandIndices)
                {
                    if (bi >= 0 && bi < _bands.Count)
                    {
                        var target = _bands[bi];
                        int listIdx = _leftBandsListBox.Items.IndexOf(target);
                        if (listIdx >= 0)
                            _leftBandsListBox.SetSelected(listIdx, true);
                    }
                }
            }

            _operatorComboBox.SelectedIndex = (int)_condition.Operator;

            if (_condition.RightSide.IsConstant)
            {
                _rightConstantRadio.Checked = true;
                _rightConstantTextBox.Text = _condition.RightSide.ConstantValue?.ToString() ?? "0";
            }
            else
            {
                _rightDensityRadio.Checked = true;

                int rightIdx = _rightDensityTypes.IndexOf(_condition.RightSide.DensityType);
                if (rightIdx >= 0)
                    _rightDensityTypeComboBox.SelectedIndex = rightIdx;

                if (IsSingleBandType(_condition.RightSide.DensityType))
                {
                    if (_condition.RightSide.SingleBandIndex >= 0
                        && _condition.RightSide.SingleBandIndex < _bands.Count)
                    {
                        var target = _bands[_condition.RightSide.SingleBandIndex];
                        int listIdx = _rightBandsListBox.Items.IndexOf(target);
                        if (listIdx >= 0)
                            _rightBandsListBox.SetSelected(listIdx, true);
                    }
                }
                else
                {
                    foreach (var bi in _condition.RightSide.BandIndices)
                    {
                        if (bi >= 0 && bi < _bands.Count)
                        {
                            var target = _bands[bi];
                            int listIdx = _rightBandsListBox.Items.IndexOf(target);
                            if (listIdx >= 0)
                                _rightBandsListBox.SetSelected(listIdx, true);
                        }
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
                int bandIdx = GetSingleSelectedBandIndex(_leftBandsListBox);
                if (bandIdx >= 0)
                    ResultCondition.LeftSingleBandIndex = bandIdx;
            }
            else
            {
                ResultCondition.LeftBandIndices.AddRange(GetSelectedBandIndices(_leftBandsListBox));
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
                        int bandIdx = GetSingleSelectedBandIndex(_rightBandsListBox);
                        if (bandIdx >= 0)
                            ResultCondition.RightSide.SingleBandIndex = bandIdx;
                    }
                    else
                    {
                        ResultCondition.RightSide.BandIndices.AddRange(
                            GetSelectedBandIndices(_rightBandsListBox));
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
