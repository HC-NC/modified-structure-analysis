namespace modified_structure_analysis.Forms
{
    partial class BandSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BandSelectionForm));
            listBox1 = new ListBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            cancelButton = new Button();
            okButton = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // listBox1
            // 
            resources.ApplyResources(listBox1, "listBox1");
            listBox1.FormattingEnabled = true;
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
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
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            resources.ApplyResources(okButton, "okButton");
            okButton.Name = "okButton";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // BandSelectionForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanel1);
            Controls.Add(listBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BandSelectionForm";
            ShowInTaskbar = false;
            TopMost = true;
            Load += BandSelectionForm_Load;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button cancelButton;
        private Button okButton;
    }
}