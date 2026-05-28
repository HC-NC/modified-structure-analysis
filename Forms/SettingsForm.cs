using System.Globalization;
using modified_structure_analysis.Config;
using modified_structure_analysis.Services;

namespace modified_structure_analysis.Forms;

public partial class SettingsForm : Form
{
    private readonly AppSettings _settings = AppSettings.Instance;

    public SettingsForm()
    {
        InitializeComponent();
    }

    private void SettingsForm_Load(object? sender, EventArgs e)
    {
        _defaultKernelCombo.Items.Clear();
        _defaultKernelCombo.Items.AddRange(Enum.GetNames<KernelType>());
        _defaultKernelCombo.SelectedItem = _settings.DefaultKernelType.ToString();

        _bandwidthMethodCombo.Items.Clear();
        _bandwidthMethodCombo.Items.AddRange(new object[]
        {
            "Rule of Thumb (fast)",
            "Least-Squares Cross-Validation",
            "Direct Plug-In (Sheather–Jones)",
            "Adaptive (Abramson)"
        });
        _bandwidthMethodCombo.SelectedIndex = (int)_settings.BandwidthMethod;

        _binsRuleCombo.Items.Clear();
        _binsRuleCombo.Items.AddRange(Enum.GetNames<HistogramBinsRule>());
        _binsRuleCombo.SelectedItem = _settings.HistogramBinsRule.ToString();
        _customBinsNud.Enabled = _settings.HistogramBinsRule == HistogramBinsRule.Custom;

        _interpolationCombo.Items.Clear();
        _interpolationCombo.Items.AddRange(Enum.GetNames<ViewportInterpolation>());
        _interpolationCombo.SelectedItem = _settings.Interpolation.ToString();

        FillLanguageCombo();
        int langIdx = _languageCombo.FindStringExact(_settings.Language);
        _languageCombo.SelectedIndex = langIdx >= 0 ? langIdx : 0;

        _customBinsNud.Value = _settings.HistogramCustomBins;
        _showCumulativeCheck.Checked = _settings.HistogramShowCumulative;

        SetColorButton(_barColorSwatch, _barColorTextBox, _settings.HistogramBarColor);
        SetColorButton(_lineColorSwatch, _lineColorTextBox, _settings.HistogramLineColor);

        _redBandNud.Value = _settings.DefaultRedBand;
        _greenBandNud.Value = _settings.DefaultGreenBand;
        _blueBandNud.Value = _settings.DefaultBlueBand;

        _showAxisLabelsCheck.Checked = _settings.GraphShowAxisLabels;
        _showLegendCheck.Checked = _settings.GraphShowLegend;
    }

    private void FillLanguageCombo()
    {
        var languages = new List<string> { "" };
        var exeDir = AppDomain.CurrentDomain.BaseDirectory;
        foreach (var dir in Directory.GetDirectories(exeDir))
        {
            var name = Path.GetFileName(dir);
            try
            {
                CultureInfo.GetCultureInfo(name);
                if (Directory.GetFiles(dir, "*.resources.dll").Length > 0)
                    languages.Add(name);
            }
            catch { }
        }
        languages.Add("en-US");
        languages.Add("ru-RU");

        _languageCombo.Items.Clear();
        foreach (var lang in languages.Distinct().OrderBy(x => x))
            _languageCombo.Items.Add(lang);
    }

    private static void SetColorButton(Button swatch, TextBox text, string hex)
    {
        var color = ColorTranslator.FromHtml(hex);
        swatch.BackColor = color;
        swatch.Tag = hex;
        text.Text = hex;
    }

    private void _binsRuleCombo_SelectedIndexChanged(object? sender, EventArgs e)
    {
        _customBinsNud.Enabled = _binsRuleCombo.SelectedItem?.ToString() == nameof(HistogramBinsRule.Custom);
    }

    private void _barColorPickBtn_Click(object? sender, EventArgs e)
    {
        PickColor(_barColorSwatch, _barColorTextBox);
    }

    private void _lineColorPickBtn_Click(object? sender, EventArgs e)
    {
        PickColor(_lineColorSwatch, _lineColorTextBox);
    }

    private static void PickColor(Button swatch, TextBox text)
    {
        using var dlg = new ColorDialog();
        dlg.Color = swatch.BackColor;
        dlg.FullOpen = true;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            var hex = ColorTranslator.ToHtml(dlg.Color);
            swatch.BackColor = dlg.Color;
            swatch.Tag = hex;
            text.Text = hex;
        }
    }

    private void _okButton_Click(object? sender, EventArgs e)
    {
        _settings.DefaultKernelType = Enum.Parse<KernelType>((string)_defaultKernelCombo.SelectedItem!);
        _settings.BandwidthMethod = (BandwidthMethod)_bandwidthMethodCombo.SelectedIndex;
        _settings.Language = (string)_languageCombo.SelectedItem!;

        _settings.HistogramBinsRule = Enum.Parse<HistogramBinsRule>((string)_binsRuleCombo.SelectedItem!);
        _settings.HistogramCustomBins = (int)_customBinsNud.Value;
        _settings.HistogramShowCumulative = _showCumulativeCheck.Checked;
        _settings.HistogramBarColor = (string)_barColorSwatch.Tag!;
        _settings.HistogramLineColor = (string)_lineColorSwatch.Tag!;

        _settings.DefaultRedBand = (int)_redBandNud.Value;
        _settings.DefaultGreenBand = (int)_greenBandNud.Value;
        _settings.DefaultBlueBand = (int)_blueBandNud.Value;
        _settings.Interpolation = Enum.Parse<ViewportInterpolation>((string)_interpolationCombo.SelectedItem!);

        _settings.GraphShowAxisLabels = _showAxisLabelsCheck.Checked;
        _settings.GraphShowLegend = _showLegendCheck.Checked;

        _settings.Save();

        if (!string.IsNullOrEmpty(_settings.Language))
        {
            var culture = new CultureInfo(_settings.Language);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        Close();
    }

    private void _cancelButton_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
