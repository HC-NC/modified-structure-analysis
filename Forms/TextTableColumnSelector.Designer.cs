namespace modified_structure_analysis
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
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            cancelButton = new Button();
            acceptButton = new Button();
            panel2 = new Panel();
            label1 = new Label();
            resolutionNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resolutionNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(378, 195);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(cancelButton);
            panel1.Controls.Add(acceptButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 195);
            panel1.Name = "panel1";
            panel1.Size = new Size(378, 49);
            panel1.TabIndex = 1;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(136, 6);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(112, 34);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // acceptButton
            // 
            acceptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            acceptButton.DialogResult = DialogResult.OK;
            acceptButton.Location = new Point(254, 6);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(112, 34);
            acceptButton.TabIndex = 0;
            acceptButton.Text = "Accept";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(resolutionNumericUpDown);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 158);
            panel2.Name = "panel2";
            panel2.Size = new Size(378, 37);
            panel2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 5);
            label1.Name = "label1";
            label1.Size = new Size(95, 25);
            label1.TabIndex = 0;
            label1.Text = "Resolution";
            // 
            // resolutionNumericUpDown
            // 
            resolutionNumericUpDown.DecimalPlaces = 3;
            resolutionNumericUpDown.Location = new Point(136, 3);
            resolutionNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            resolutionNumericUpDown.Name = "resolutionNumericUpDown";
            resolutionNumericUpDown.Size = new Size(230, 31);
            resolutionNumericUpDown.TabIndex = 1;
            resolutionNumericUpDown.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // TextTableColumnSelector
            // 
            AcceptButton = acceptButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(378, 244);
            Controls.Add(panel2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "TextTableColumnSelector";
            StartPosition = FormStartPosition.CenterParent;
            Text = "TextTableColumnSelector";
            Load += FormLoad;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resolutionNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Button cancelButton;
        private Button acceptButton;
        private Panel panel2;
        private NumericUpDown resolutionNumericUpDown;
        private Label label1;
    }
}