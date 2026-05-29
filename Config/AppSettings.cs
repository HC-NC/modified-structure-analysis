using System.Globalization;
using modified_structure_analysis.Services;
using System.Text.Json;

namespace modified_structure_analysis.Config;

public enum HistogramBinsRule
{
    Sturges,
    BrooksCarruther,
    HeinholdHeide,
    Custom
}

public enum BandwidthMethod
{
    RuleOfThumb,
    LeastSquaresCrossValidation,
    DirectPlugIn,
    LeaveOneOutLikelihood
}

public enum ViewportInterpolation
{
    NearestNeighbor,
    Bilinear,
    Bicubic,
    HighQualityBilinear,
    HighQualityBicubic
}

public sealed class AppSettings
{
    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "ModifiedStructureAnalysis",
        "settings.json");

    public static AppSettings Instance { get; private set; } = Load();

    // ── General ────────────────────────────────────────────────
    public KernelType DefaultKernelType { get; set; } = KernelType.Epanechnikov;
    public BandwidthMethod BandwidthMethod { get; set; } = BandwidthMethod.RuleOfThumb;
    public string Language { get; set; } = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    // ── Histogram ──────────────────────────────────────────────
    public HistogramBinsRule HistogramBinsRule { get; set; } = HistogramBinsRule.BrooksCarruther;
    public int HistogramCustomBins { get; set; } = 50;
    public bool HistogramShowCumulative { get; set; } = true;
    public string HistogramBarColor { get; set; } = "#4CAF50";
    public string HistogramLineColor { get; set; } = "#F44336";

    // ── Viewport ───────────────────────────────────────────────
    public int DefaultRedBand { get; set; }
    public int DefaultGreenBand { get; set; } = 1;
    public int DefaultBlueBand { get; set; } = 2;
    public ViewportInterpolation Interpolation { get; set; } = ViewportInterpolation.NearestNeighbor;

    // ── Graph ──────────────────────────────────────────────────
    public bool GraphShowAxisLabels { get; set; } = true;
    public bool GraphShowLegend { get; set; } = true;

    // ── Classification ─────────────────────────────────────────
    public float UndefinedThreshold { get; set; }

    // ── Helpers ────────────────────────────────────────────────
    public int GetHistogramBinCount(int pixelCount)
    {
        return HistogramBinsRule switch
        {
            Config.HistogramBinsRule.Sturges => (int)(Math.Log(pixelCount) / Math.Log(2) + 1),
            Config.HistogramBinsRule.BrooksCarruther => (int)(5 * Math.Log(pixelCount)),
            Config.HistogramBinsRule.HeinholdHeide => (int)Math.Sqrt(pixelCount),
            Config.HistogramBinsRule.Custom => HistogramCustomBins,
            _ => (int)(5 * Math.Log(pixelCount))
        };
    }

    public void Save()
    {
        var dir = Path.GetDirectoryName(FilePath);
        if (dir != null) Directory.CreateDirectory(dir);
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    private static AppSettings Load()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch
        {
        }
        return new AppSettings();
    }
}
