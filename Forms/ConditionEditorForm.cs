using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace modified_structure_analysis
{
    public partial class ConditionEditorForm : Form
    {
        private List<Band> _bands;
        private Condition? _condition;

        public Condition? ResultCondition { get; private set; }

        public ConditionEditorForm(List<Band> bands, Condition? existingCondition)
        {
            _bands = bands;
            _condition = existingCondition;

            InitializeComponent();

            Text = _condition == null ? "Add Condition" : "Edit Condition";
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

            for (int i = 0; i < _bands.Count; i++)
            {
                _leftBandsListBox.Items.Add($"{i}: {_bands[i].Name}");
                _rightBandsListBox.Items.Add($"{i}: {_bands[i].Name}");
            }
        }

        private void UpdateLeftVisibility(object sender, EventArgs e)
        {
            bool isSingle = _leftDensityTypeComboBox.SelectedIndex == 0;

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

            bool isRightSingle = !isConstant && _rightDensityTypeComboBox.SelectedIndex == 0;

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
