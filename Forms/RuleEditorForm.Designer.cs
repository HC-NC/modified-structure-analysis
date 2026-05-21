namespace modified_structure_analysis
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
            lblColor = new Label();
            _colorBtn = new Button();
            _okBtn = new Button();
            _cancelBtn = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            addToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cloneToolStripMenuItem1 = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            contextMenuStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // _conditionsListBox
            // 
            _conditionsListBox.ContextMenuStrip = contextMenuStrip1;
            _conditionsListBox.DisplayMember = "Display";
            _conditionsListBox.Dock = DockStyle.Fill;
            _conditionsListBox.Location = new Point(0, 33);
            _conditionsListBox.Name = "_conditionsListBox";
            _conditionsListBox.Size = new Size(778, 451);
            _conditionsListBox.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem1, editToolStripMenuItem1, cloneToolStripMenuItem, toolStripSeparator1, removeToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(241, 171);
            contextMenuStrip1.LocationChanged += contextMenuStrip1_LocationChanged;
            // 
            // addToolStripMenuItem1
            // 
            addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            addToolStripMenuItem1.Size = new Size(240, 32);
            addToolStripMenuItem1.Text = "Add";
            addToolStripMenuItem1.Click += AddCondition_Click;
            // 
            // editToolStripMenuItem1
            // 
            editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            editToolStripMenuItem1.Size = new Size(240, 32);
            editToolStripMenuItem1.Text = "Edit";
            editToolStripMenuItem1.Click += EditCondition_Click;
            // 
            // cloneToolStripMenuItem
            // 
            cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            cloneToolStripMenuItem.Size = new Size(240, 32);
            cloneToolStripMenuItem.Text = "Clone";
            cloneToolStripMenuItem.Click += CloneCondition_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(237, 6);
            // 
            // removeToolStripMenuItem1
            // 
            removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            removeToolStripMenuItem1.Size = new Size(240, 32);
            removeToolStripMenuItem1.Text = "Remove";
            removeToolStripMenuItem1.Click += RemoveCondition_Click;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Location = new Point(9, 12);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(64, 25);
            lblColor.TabIndex = 5;
            lblColor.Text = "Color: ";
            // 
            // _colorBtn
            // 
            _colorBtn.Location = new Point(79, 7);
            _colorBtn.Margin = new Padding(4);
            _colorBtn.Name = "_colorBtn";
            _colorBtn.Size = new Size(100, 40);
            _colorBtn.TabIndex = 6;
            _colorBtn.Click += ColorBtn_Click;
            // 
            // _okBtn
            // 
            _okBtn.DialogResult = DialogResult.OK;
            _okBtn.Location = new Point(326, 7);
            _okBtn.Name = "_okBtn";
            _okBtn.Size = new Size(120, 40);
            _okBtn.TabIndex = 8;
            _okBtn.Text = "OK";
            _okBtn.Click += Close_Click;
            // 
            // _cancelBtn
            // 
            _cancelBtn.DialogResult = DialogResult.Cancel;
            _cancelBtn.Location = new Point(452, 7);
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
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(192, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(4);
            flowLayoutPanel1.Size = new Size(583, 54);
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 484);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(778, 60);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(_colorBtn);
            panel1.Controls.Add(lblColor);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(183, 51);
            panel1.TabIndex = 11;
            // 
            // RuleEditorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 544);
            Controls.Add(_conditionsListBox);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RuleEditorForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "RuleEditorForm";
            Load += RuleEditorForm_Load;
            contextMenuStrip1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox _conditionsListBox;
        private Button _colorBtn;
        private Button _okBtn;
        private Button _cancelBtn;
        private Label lblColor;
        private FlowLayoutPanel flowLayoutPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private ToolStripMenuItem cloneToolStripMenuItem1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ToolStripMenuItem editToolStripMenuItem1;
        private ToolStripMenuItem cloneToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem removeToolStripMenuItem1;
    }
}