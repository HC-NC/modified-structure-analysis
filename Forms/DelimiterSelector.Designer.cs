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
            lbl = new Label();
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
            // lbl
            // 
            lbl.Dock = DockStyle.Top;
            lbl.Location = new Point(0, 0);
            lbl.Name = "lbl";
            lbl.Size = new Size(278, 23);
            lbl.TabIndex = 0;
            lbl.Text = "Select column delimiter:";
            // 
            // rbTab
            // 
            rbTab.AutoSize = true;
            rbTab.Checked = true;
            rbTab.Location = new Point(7, 7);
            rbTab.Name = "rbTab";
            rbTab.Size = new Size(64, 29);
            rbTab.TabIndex = 1;
            rbTab.TabStop = true;
            rbTab.Text = "Tab";
            // 
            // rbComma
            // 
            rbComma.AutoSize = true;
            rbComma.Location = new Point(7, 42);
            rbComma.Name = "rbComma";
            rbComma.Size = new Size(119, 29);
            rbComma.TabIndex = 2;
            rbComma.Text = "Comma (,)";
            // 
            // rbSemicolon
            // 
            rbSemicolon.AutoSize = true;
            rbSemicolon.Location = new Point(7, 77);
            rbSemicolon.Name = "rbSemicolon";
            rbSemicolon.Size = new Size(139, 29);
            rbSemicolon.TabIndex = 3;
            rbSemicolon.Text = "Semicolon (;)";
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(111, 7);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 34);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(192, 7);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 34);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Controls.Add(btnOk);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(0, 136);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(4);
            flowLayoutPanel1.Size = new Size(278, 48);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(rbTab);
            flowLayoutPanel2.Controls.Add(rbComma);
            flowLayoutPanel2.Controls.Add(rbSemicolon);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(0, 23);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(4);
            flowLayoutPanel2.Size = new Size(278, 113);
            flowLayoutPanel2.TabIndex = 7;
            // 
            // DelimiterSelector
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(278, 184);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(lbl);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "DelimiterSelector";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Delimiter";
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

        private Label lbl;
        private RadioButton rbTab;
        private RadioButton rbComma;
        private RadioButton rbSemicolon;
        private Button btnOk;
        private Button btnCancel;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}