namespace modified_structure_analysis.Forms
{
    partial class ConditionEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionEditorForm));
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            _leftBandsListBox = new ListBox();
            _leftDensityTypeComboBox = new ComboBox();
            groupBox2 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox4 = new GroupBox();
            _rightBandsListBox = new ListBox();
            _rightDensityTypeComboBox = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblRightConst = new Label();
            _rightConstantTextBox = new TextBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            _rightDensityRadio = new RadioButton();
            _rightConstantRadio = new RadioButton();
            _operatorComboBox = new ComboBox();
            btnOk = new Button();
            btnCancel = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(_leftDensityTypeComboBox);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(_leftBandsListBox);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // _leftBandsListBox
            // 
            resources.ApplyResources(_leftBandsListBox, "_leftBandsListBox");
            _leftBandsListBox.Name = "_leftBandsListBox";
            // 
            // _leftDensityTypeComboBox
            // 
            resources.ApplyResources(_leftDensityTypeComboBox, "_leftDensityTypeComboBox");
            _leftDensityTypeComboBox.Name = "_leftDensityTypeComboBox";
            _leftDensityTypeComboBox.SelectedIndexChanged += UpdateLeftVisibility;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(splitContainer2);
            groupBox2.Controls.Add(flowLayoutPanel2);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox4);
            splitContainer2.Panel1.Controls.Add(_rightDensityTypeComboBox);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tableLayoutPanel1);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(_rightBandsListBox);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // _rightBandsListBox
            // 
            resources.ApplyResources(_rightBandsListBox, "_rightBandsListBox");
            _rightBandsListBox.Name = "_rightBandsListBox";
            // 
            // _rightDensityTypeComboBox
            // 
            resources.ApplyResources(_rightDensityTypeComboBox, "_rightDensityTypeComboBox");
            _rightDensityTypeComboBox.Name = "_rightDensityTypeComboBox";
            _rightDensityTypeComboBox.SelectedIndexChanged += UpdateRightVisibility;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(lblRightConst, 0, 0);
            tableLayoutPanel1.Controls.Add(_rightConstantTextBox, 1, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblRightConst
            // 
            resources.ApplyResources(lblRightConst, "lblRightConst");
            lblRightConst.Name = "lblRightConst";
            // 
            // _rightConstantTextBox
            // 
            resources.ApplyResources(_rightConstantTextBox, "_rightConstantTextBox");
            _rightConstantTextBox.Name = "_rightConstantTextBox";
            _rightConstantTextBox.Validating += _rightConstantTextBox_Validating;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(_rightDensityRadio);
            flowLayoutPanel2.Controls.Add(_rightConstantRadio);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // _rightDensityRadio
            // 
            resources.ApplyResources(_rightDensityRadio, "_rightDensityRadio");
            _rightDensityRadio.Checked = true;
            _rightDensityRadio.Name = "_rightDensityRadio";
            _rightDensityRadio.TabStop = true;
            // 
            // _rightConstantRadio
            // 
            resources.ApplyResources(_rightConstantRadio, "_rightConstantRadio");
            _rightConstantRadio.Name = "_rightConstantRadio";
            _rightConstantRadio.CheckedChanged += UpdateRightVisibility;
            // 
            // _operatorComboBox
            // 
            _operatorComboBox.Items.AddRange(new object[] { resources.GetString("_operatorComboBox.Items"), resources.GetString("_operatorComboBox.Items1"), resources.GetString("_operatorComboBox.Items2"), resources.GetString("_operatorComboBox.Items3"), resources.GetString("_operatorComboBox.Items4") });
            resources.ApplyResources(_operatorComboBox, "_operatorComboBox");
            _operatorComboBox.Name = "_operatorComboBox";
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            resources.ApplyResources(btnOk, "btnOk");
            btnOk.Name = "btnOk";
            btnOk.Click += Ok_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Controls.Add(btnOk);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(flowLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(label1);
            flowLayoutPanel3.Controls.Add(_operatorComboBox);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // ConditionEditorForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConditionEditorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            Load += ConditionEditorForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private ListBox _leftBandsListBox;
        private ComboBox _leftDensityTypeComboBox;
        private ComboBox _operatorComboBox;

        private ListBox _rightBandsListBox;
        private ComboBox _rightDensityTypeComboBox;
        private TextBox _rightConstantTextBox;
        private RadioButton _rightConstantRadio;
        private RadioButton _rightDensityRadio;

        #endregion
        private Label lblRightConst;
        private Button btnOk;
        private Button btnCancel;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox3;
        private SplitContainer splitContainer2;
        private FlowLayoutPanel flowLayoutPanel2;
        private GroupBox groupBox4;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label label1;
    }
}