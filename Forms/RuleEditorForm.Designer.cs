namespace modified_structure_analysis.Forms
{
    partial class RuleEditorForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleEditorForm));
            _conditionsListBox = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            addToolStripMenuItem1 = new ToolStripMenuItem();
            editToolStripMenuItem1 = new ToolStripMenuItem();
            cloneToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            removeToolStripMenuItem1 = new ToolStripMenuItem();
            _okBtn = new Button();
            _cancelBtn = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            addToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cloneToolStripMenuItem1 = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _conditionsListBox
            // 
            _conditionsListBox.ContextMenuStrip = contextMenuStrip1;
            _conditionsListBox.DisplayMember = "Display";
            resources.ApplyResources(_conditionsListBox, "_conditionsListBox");
            _conditionsListBox.Name = "_conditionsListBox";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem1, editToolStripMenuItem1, cloneToolStripMenuItem, toolStripSeparator1, removeToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(contextMenuStrip1, "contextMenuStrip1");
            contextMenuStrip1.LocationChanged += contextMenuStrip1_LocationChanged;
            // 
            // addToolStripMenuItem1
            // 
            addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            resources.ApplyResources(addToolStripMenuItem1, "addToolStripMenuItem1");
            addToolStripMenuItem1.Click += AddCondition_Click;
            // 
            // editToolStripMenuItem1
            // 
            editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            resources.ApplyResources(editToolStripMenuItem1, "editToolStripMenuItem1");
            editToolStripMenuItem1.Click += EditCondition_Click;
            // 
            // cloneToolStripMenuItem
            // 
            cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            resources.ApplyResources(cloneToolStripMenuItem, "cloneToolStripMenuItem");
            cloneToolStripMenuItem.Click += CloneCondition_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // removeToolStripMenuItem1
            // 
            removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            resources.ApplyResources(removeToolStripMenuItem1, "removeToolStripMenuItem1");
            removeToolStripMenuItem1.Click += RemoveCondition_Click;
            // 
            // _okBtn
            // 
            _okBtn.DialogResult = DialogResult.OK;
            resources.ApplyResources(_okBtn, "_okBtn");
            _okBtn.Name = "_okBtn";
            _okBtn.Click += Close_Click;
            // 
            // _cancelBtn
            // 
            _cancelBtn.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(_cancelBtn, "_cancelBtn");
            _cancelBtn.Name = "_cancelBtn";
            _cancelBtn.Click += Close_Click;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(_cancelBtn);
            flowLayoutPanel1.Controls.Add(_okBtn);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, cloneToolStripMenuItem1, removeToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(addToolStripMenuItem, "addToolStripMenuItem");
            addToolStripMenuItem.Click += AddCondition_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
            editToolStripMenuItem.Click += EditCondition_Click;
            // 
            // cloneToolStripMenuItem1
            // 
            cloneToolStripMenuItem1.Name = "cloneToolStripMenuItem1";
            resources.ApplyResources(cloneToolStripMenuItem1, "cloneToolStripMenuItem1");
            cloneToolStripMenuItem1.Click += CloneCondition_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(removeToolStripMenuItem, "removeToolStripMenuItem");
            removeToolStripMenuItem.Click += RemoveCondition_Click;
            // 
            // RuleEditorForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_conditionsListBox);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RuleEditorForm";
            ShowInTaskbar = false;
            TopMost = true;
            Load += RuleEditorForm_Load;
            contextMenuStrip1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox _conditionsListBox;
        private Button _okBtn;
        private Button _cancelBtn;
        private FlowLayoutPanel flowLayoutPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem cloneToolStripMenuItem1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ToolStripMenuItem editToolStripMenuItem1;
        private ToolStripMenuItem cloneToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem removeToolStripMenuItem1;
    }
}