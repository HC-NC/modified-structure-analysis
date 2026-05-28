namespace modified_structure_analysis.Forms
{
    partial class DelimiterSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelimiterSelector));
            rbTab = new RadioButton();
            rbComma = new RadioButton();
            rbSemicolon = new RadioButton();
            btnOk = new Button();
            btnCancel = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // rbTab
            // 
            resources.ApplyResources(rbTab, "rbTab");
            rbTab.Checked = true;
            rbTab.Name = "rbTab";
            rbTab.TabStop = true;
            // 
            // rbComma
            // 
            resources.ApplyResources(rbComma, "rbComma");
            rbComma.Name = "rbComma";
            // 
            // rbSemicolon
            // 
            resources.ApplyResources(rbSemicolon, "rbSemicolon");
            rbSemicolon.Name = "rbSemicolon";
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            resources.ApplyResources(btnOk, "btnOk");
            btnOk.Name = "btnOk";
            btnOk.Click += btnOk_Click;
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
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(rbTab);
            flowLayoutPanel2.Controls.Add(rbComma);
            flowLayoutPanel2.Controls.Add(rbSemicolon);
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // DelimiterSelector
            // 
            AcceptButton = btnOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DelimiterSelector";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private RadioButton rbTab;
        private RadioButton rbComma;
        private RadioButton rbSemicolon;
        private Button btnOk;
        private Button btnCancel;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}