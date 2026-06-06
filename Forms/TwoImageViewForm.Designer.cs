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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwoImageViewForm));
            splitContainer1 = new SplitContainer();
            viewport1 = new Viewport();
            viewport2 = new Viewport();
            statusStrip1 = new StatusStrip();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.ControlDark;
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(viewport1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(viewport2);
            // 
            // viewport1
            // 
            viewport1.BackColor = SystemColors.Control;
            resources.ApplyResources(viewport1, "viewport1");
            viewport1.Name = "viewport1";
            // 
            // viewport2
            // 
            viewport2.BackColor = SystemColors.Control;
            resources.ApplyResources(viewport2, "viewport2");
            viewport2.Name = "viewport2";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // TwoImageViewForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            MinimizeBox = false;
            Name = "TwoImageViewForm";
            ShowInTaskbar = false;
            TopMost = true;
            FormClosing += TwoImageViewForm_FormClosing;
            Load += TwoImageViewForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private Viewport viewport1;
        private Viewport viewport2;
        private StatusStrip statusStrip1;
    }
}