namespace modified_structure_analysis.Forms
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            cancelButton = new Button();
            okButton = new Button();
            dataGridView1 = new DataGridView();
            CheckBoxColumn = new DataGridViewCheckBoxColumn();
            TextBoxColumn = new DataGridViewTextBoxColumn();
            ImageColumn = new DataGridViewImageColumn();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(okButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            resources.ApplyResources(okButton, "okButton");
            okButton.Name = "okButton";
            okButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { CheckBoxColumn, TextBoxColumn, ImageColumn });
            resources.ApplyResources(dataGridView1, "dataGridView1");
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            // 
            // CheckBoxColumn
            // 
            CheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(CheckBoxColumn, "CheckBoxColumn");
            CheckBoxColumn.Name = "CheckBoxColumn";
            CheckBoxColumn.ReadOnly = true;
            // 
            // TextBoxColumn
            // 
            TextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(TextBoxColumn, "TextBoxColumn");
            TextBoxColumn.Name = "TextBoxColumn";
            TextBoxColumn.ReadOnly = true;
            // 
            // ImageColumn
            // 
            ImageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(ImageColumn, "ImageColumn");
            ImageColumn.Name = "ImageColumn";
            ImageColumn.ReadOnly = true;
            // 
            // FilterForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridView1);
            Controls.Add(flowLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FilterForm";
            ShowIcon = false;
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button cancelButton;
        private Button okButton;
        private DataGridView dataGridView1;
        private DataGridViewCheckBoxColumn CheckBoxColumn;
        private DataGridViewTextBoxColumn TextBoxColumn;
        private DataGridViewImageColumn ImageColumn;
    }
}