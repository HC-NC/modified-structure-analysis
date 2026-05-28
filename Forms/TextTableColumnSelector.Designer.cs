namespace modified_structure_analysis.Forms
{
    partial class TextTableColumnSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextTableColumnSelector));
            dataGridView1 = new DataGridView();
            cancelButton = new Button();
            acceptButton = new Button();
            panel2 = new Panel();
            resolutionNumericUpDown = new NumericUpDown();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resolutionNumericUpDown).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            resources.ApplyResources(dataGridView1, "dataGridView1");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            acceptButton.DialogResult = DialogResult.OK;
            acceptButton.Name = "acceptButton";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(resolutionNumericUpDown);
            panel2.Controls.Add(label1);
            panel2.Name = "panel2";
            // 
            // resolutionNumericUpDown
            // 
            resources.ApplyResources(resolutionNumericUpDown, "resolutionNumericUpDown");
            resolutionNumericUpDown.DecimalPlaces = 3;
            resolutionNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            resolutionNumericUpDown.Name = "resolutionNumericUpDown";
            resolutionNumericUpDown.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(acceptButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // TextTableColumnSelector
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(flowLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextTableColumnSelector";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            Load += FormLoad;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resolutionNumericUpDown).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button cancelButton;
        private Button acceptButton;
        private Panel panel2;
        private NumericUpDown resolutionNumericUpDown;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}