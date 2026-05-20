namespace modified_structure_analysis
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
            _leftDensityTypeComboBox = new ComboBox();
            _leftBandsListBox = new ListBox();
            _operatorComboBox = new ComboBox();
            _rightConstantRadio = new RadioButton();
            _rightDensityRadio = new RadioButton();
            lblRightConst = new Label();
            _rightConstantTextBox = new TextBox();
            _rightDensityTypeComboBox = new ComboBox();
            _rightBandsListBox = new ListBox();
            btnOk = new Button();
            btnCancel = new Button();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox2 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox4 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
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
            // _leftDensityTypeComboBox
            // 
            _leftDensityTypeComboBox.Dock = DockStyle.Top;
            _leftDensityTypeComboBox.Items.AddRange(new object[] { "Single", "Product", "Multivariate" });
            _leftDensityTypeComboBox.Location = new Point(3, 27);
            _leftDensityTypeComboBox.Name = "_leftDensityTypeComboBox";
            _leftDensityTypeComboBox.Size = new Size(283, 33);
            _leftDensityTypeComboBox.TabIndex = 1;
            _leftDensityTypeComboBox.SelectedIndexChanged += UpdateLeftVisibility;
            // 
            // _leftBandsListBox
            // 
            _leftBandsListBox.Dock = DockStyle.Fill;
            _leftBandsListBox.Location = new Point(3, 27);
            _leftBandsListBox.Name = "_leftBandsListBox";
            _leftBandsListBox.Size = new Size(277, 289);
            _leftBandsListBox.TabIndex = 3;
            // 
            // _operatorComboBox
            // 
            _operatorComboBox.Items.AddRange(new object[] { ">", "<", ">=", "<=", "==" });
            _operatorComboBox.Location = new Point(92, 12);
            _operatorComboBox.Margin = new Padding(0, 12, 8, 8);
            _operatorComboBox.Name = "_operatorComboBox";
            _operatorComboBox.Size = new Size(121, 33);
            _operatorComboBox.TabIndex = 4;
            // 
            // _rightConstantRadio
            // 
            _rightConstantRadio.AutoSize = true;
            _rightConstantRadio.Location = new Point(105, 3);
            _rightConstantRadio.Name = "_rightConstantRadio";
            _rightConstantRadio.Size = new Size(108, 29);
            _rightConstantRadio.TabIndex = 6;
            _rightConstantRadio.Text = "Constant";
            _rightConstantRadio.CheckedChanged += UpdateRightVisibility;
            // 
            // _rightDensityRadio
            // 
            _rightDensityRadio.AutoSize = true;
            _rightDensityRadio.Checked = true;
            _rightDensityRadio.Location = new Point(3, 3);
            _rightDensityRadio.Name = "_rightDensityRadio";
            _rightDensityRadio.Size = new Size(96, 29);
            _rightDensityRadio.TabIndex = 7;
            _rightDensityRadio.TabStop = true;
            _rightDensityRadio.Text = "Density";
            // 
            // lblRightConst
            // 
            lblRightConst.AutoSize = true;
            lblRightConst.Dock = DockStyle.Fill;
            lblRightConst.Location = new Point(3, 6);
            lblRightConst.Margin = new Padding(3, 6, 3, 0);
            lblRightConst.Name = "lblRightConst";
            lblRightConst.Size = new Size(58, 151);
            lblRightConst.TabIndex = 8;
            lblRightConst.Text = "Value:";
            // 
            // _rightConstantTextBox
            // 
            _rightConstantTextBox.Dock = DockStyle.Fill;
            _rightConstantTextBox.Location = new Point(67, 3);
            _rightConstantTextBox.Name = "_rightConstantTextBox";
            _rightConstantTextBox.Size = new Size(209, 31);
            _rightConstantTextBox.TabIndex = 9;
            _rightConstantTextBox.Text = "0,5";
            _rightConstantTextBox.Validating += _rightConstantTextBox_Validating;
            // 
            // _rightDensityTypeComboBox
            // 
            _rightDensityTypeComboBox.Dock = DockStyle.Top;
            _rightDensityTypeComboBox.Items.AddRange(new object[] { "Single", "Product", "Multivariate" });
            _rightDensityTypeComboBox.Location = new Point(0, 0);
            _rightDensityTypeComboBox.Name = "_rightDensityTypeComboBox";
            _rightDensityTypeComboBox.Size = new Size(279, 33);
            _rightDensityTypeComboBox.TabIndex = 10;
            _rightDensityTypeComboBox.SelectedIndexChanged += UpdateRightVisibility;
            // 
            // _rightBandsListBox
            // 
            _rightBandsListBox.Dock = DockStyle.Fill;
            _rightBandsListBox.Location = new Point(3, 27);
            _rightBandsListBox.Name = "_rightBandsListBox";
            _rightBandsListBox.Size = new Size(273, 93);
            _rightBandsListBox.TabIndex = 12;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(89, 8);
            btnOk.Margin = new Padding(0, 8, 4, 8);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(120, 40);
            btnOk.TabIndex = 13;
            btnOk.Text = "OK";
            btnOk.Click += Ok_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(217, 8);
            btnCancel.Margin = new Padding(4, 8, 8, 8);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(578, 382);
            splitContainer1.SplitterDistance = 289;
            splitContainer1.TabIndex = 15;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(_leftDensityTypeComboBox);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(289, 382);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Left Side:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(_leftBandsListBox);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(3, 60);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(283, 319);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Bands:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(splitContainer2);
            groupBox2.Controls.Add(flowLayoutPanel2);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(285, 382);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Right Side:";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(3, 62);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox4);
            splitContainer2.Panel1.Controls.Add(_rightDensityTypeComboBox);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tableLayoutPanel1);
            splitContainer2.Size = new Size(279, 317);
            splitContainer2.SplitterDistance = 156;
            splitContainer2.TabIndex = 1;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(_rightBandsListBox);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 33);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(279, 123);
            groupBox4.TabIndex = 11;
            groupBox4.TabStop = false;
            groupBox4.Text = "Bands:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(lblRightConst, 0, 0);
            tableLayoutPanel1.Controls.Add(_rightConstantTextBox, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(279, 157);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(_rightDensityRadio);
            flowLayoutPanel2.Controls.Add(_rightConstantRadio);
            flowLayoutPanel2.Dock = DockStyle.Top;
            flowLayoutPanel2.Location = new Point(3, 27);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(279, 35);
            flowLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Controls.Add(btnOk);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(230, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(345, 56);
            flowLayoutPanel1.TabIndex = 16;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(flowLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 382);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(578, 62);
            tableLayoutPanel2.TabIndex = 17;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(label1);
            flowLayoutPanel3.Controls.Add(_operatorComboBox);
            flowLayoutPanel3.Location = new Point(3, 3);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(221, 56);
            flowLayoutPanel3.TabIndex = 15;
            // 
            // label1
            // 
            label1.Location = new Point(4, 8);
            label1.Margin = new Padding(4, 8, 0, 8);
            label1.Name = "label1";
            label1.Size = new Size(88, 40);
            label1.TabIndex = 5;
            label1.Text = "Operator:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConditionEditorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 444);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel2);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(600, 400);
            Name = "ConditionEditorForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ConditionEditorForm";
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