namespace modified_structure_analysis.Forms
{
    partial class TwoImageViewForm
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
            splitContainer1 = new SplitContainer();
            viewport1 = new Viewport();
            viewport2 = new Viewport();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.ControlDark;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(viewport1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(viewport2);
            splitContainer1.Size = new Size(1258, 552);
            splitContainer1.SplitterDistance = 629;
            splitContainer1.TabIndex = 0;
            // 
            // viewport1
            // 
            viewport1.BackColor = SystemColors.Control;
            viewport1.Dock = DockStyle.Fill;
            viewport1.Location = new Point(0, 0);
            viewport1.Name = "viewport1";
            viewport1.Size = new Size(629, 552);
            viewport1.TabIndex = 0;
            // 
            // viewport2
            // 
            viewport2.BackColor = SystemColors.Control;
            viewport2.Dock = DockStyle.Fill;
            viewport2.Location = new Point(0, 0);
            viewport2.Name = "viewport2";
            viewport2.Size = new Size(625, 552);
            viewport2.TabIndex = 0;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 552);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1258, 32);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(179, 25);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // TwoImageViewForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 584);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            MinimizeBox = false;
            Name = "TwoImageViewForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "TwoImageViewForm";
            FormClosing += TwoImageViewForm_FormClosing;
            Load += TwoImageViewForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private Viewport viewport1;
        private Viewport viewport2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}