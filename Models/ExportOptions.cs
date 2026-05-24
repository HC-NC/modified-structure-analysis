namespace modified_structure_analysis.Models;

public enum GraphExportFormat
{
    Png,
    Jpeg,
    Svg,
    Pdf
}

public enum ClassificationExportFormat
{
    GeoTiff,
    Png,
    PngWithWorldFile
}

public enum StatsExportFormat
{
    Csv,
    Txt,
    Json
}

public class GraphExportOptions
{
    public GraphExportFormat Format { get; set; } = GraphExportFormat.Png;
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public int Dpi { get; set; } = 150;
    public int JpegQuality { get; set; } = 90;
}

public class ClassificationExportOptions
{
    public string FilePath { get; set; } = "";
    public ClassificationExportFormat Format { get; set; } = ClassificationExportFormat.GeoTiff;
    public bool IncludeGeoTransform { get; set; } = true;
    public string? ProjectionWkt { get; set; }
}

public class StatsExportOptions
{
    public string FilePath { get; set; } = "";
    public StatsExportFormat Format { get; set; } = StatsExportFormat.Csv;
}
