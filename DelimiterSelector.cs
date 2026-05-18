namespace modified_structure_analysis;

public enum DelimiterType
{
    Tab,
    Comma,
    Semicolon,
    Space
}

public class DelimiterSelector : Form
{
    public DelimiterType SelectedDelimiter { get; private set; } = DelimiterType.Tab;

    public DelimiterSelector()
    {
        Text = "Select Delimiter";
        Width = 300;
        Height = 180;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var lbl = new Label { Left = 20, Top = 20, Width = 250, Text = "Select column delimiter:" };

        var rbTab = new RadioButton { Left = 20, Top = 50, Width = 250, Text = "Tab", Checked = true };
        var rbComma = new RadioButton { Left = 20, Top = 75, Width = 250, Text = "Comma (,)" };
        var rbSemicolon = new RadioButton { Left = 20, Top = 100, Width = 250, Text = "Semicolon (;)" };

        var btnOk = new Button { Text = "OK", Left = 100, Top = 130, Width = 80 };
        btnOk.Click += (s, e) =>
        {
            if (rbTab.Checked) SelectedDelimiter = DelimiterType.Tab;
            else if (rbComma.Checked) SelectedDelimiter = DelimiterType.Comma;
            else if (rbSemicolon.Checked) SelectedDelimiter = DelimiterType.Semicolon;
            DialogResult = DialogResult.OK;
        };

        var btnCancel = new Button { Text = "Cancel", Left = 190, Top = 130, Width = 80 };
        btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

        Controls.AddRange(new Control[] { lbl, rbTab, rbComma, rbSemicolon, btnOk, btnCancel });
    }
}