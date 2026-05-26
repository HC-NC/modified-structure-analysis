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
            _conditionsListBox.Dock = DockStyle.Fill;
            _conditionsListBox.Location = new Point(0, 33);
            _conditionsListBox.Name = "_conditionsListBox";
            _conditionsListBox.Size = new Size(778, 457);
            _conditionsListBox.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem1, editToolStripMenuItem1, cloneToolStripMenuItem, toolStripSeparator1, removeToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(149, 138);
            contextMenuStrip1.LocationChanged += contextMenuStrip1_LocationChanged;
            // 
            // addToolStripMenuItem1
            // 
            addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            addToolStripMenuItem1.Size = new Size(148, 32);
            addToolStripMenuItem1.Text = "Add";
            addToolStripMenuItem1.Click += AddCondition_Click;
            // 
            // editToolStripMenuItem1
            // 
            editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            editToolStripMenuItem1.Size = new Size(148, 32);
            editToolStripMenuItem1.Text = "Edit";
            editToolStripMenuItem1.Click += EditCondition_Click;
            // 
            // cloneToolStripMenuItem
            // 
            cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            cloneToolStripMenuItem.Size = new Size(148, 32);
            cloneToolStripMenuItem.Text = "Clone";
            cloneToolStripMenuItem.Click += CloneCondition_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(145, 6);
            // 
            // removeToolStripMenuItem1
            // 
            removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            removeToolStripMenuItem1.Size = new Size(148, 32);
            removeToolStripMenuItem1.Text = "Remove";
            removeToolStripMenuItem1.Click += RemoveCondition_Click;
            // 
            // _okBtn
            // 
            _okBtn.DialogResult = DialogResult.OK;
            _okBtn.Location = new Point(521, 7);
            _okBtn.Name = "_okBtn";
            _okBtn.Size = new Size(120, 40);
            _okBtn.TabIndex = 8;
            _okBtn.Text = "OK";
            _okBtn.Click += Close_Click;
            // 
            // _cancelBtn
            // 
            _cancelBtn.DialogResult = DialogResult.Cancel;
            _cancelBtn.Location = new Point(647, 7);
            _cancelBtn.Name = "_cancelBtn";
            _cancelBtn.Size = new Size(120, 40);
            _cancelBtn.TabIndex = 9;
            _cancelBtn.Text = "Cancel";
            _cancelBtn.Click += Close_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(_cancelBtn);
            flowLayoutPanel1.Controls.Add(_okBtn);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(0, 490);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(4);
            flowLayoutPanel1.Size = new Size(778, 54);
            flowLayoutPanel1.TabIndex = 10;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, cloneToolStripMenuItem1, removeToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(778, 33);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.ShortcutKeyDisplayString = "";
            addToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            addToolStripMenuItem.Size = new Size(62, 29);
            addToolStripMenuItem.Text = "Add";
            addToolStripMenuItem.Click += AddCondition_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            editToolStripMenuItem.Size = new Size(58, 29);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.Click += EditCondition_Click;
            // 
            // cloneToolStripMenuItem1
            // 
            cloneToolStripMenuItem1.Name = "cloneToolStripMenuItem1";
            cloneToolStripMenuItem1.ShortcutKeys = Keys.Control | Keys.D;
            cloneToolStripMenuItem1.Size = new Size(73, 29);
            cloneToolStripMenuItem1.Text = "Clone";
            cloneToolStripMenuItem1.Click += CloneCondition_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            removeToolStripMenuItem.Size = new Size(92, 29);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += RemoveCondition_Click;
            // 
            // RuleEditorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 544);
            Controls.Add(_conditionsListBox);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RuleEditorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "RuleEditorForm";
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