using System.Drawing.Imaging;
using System.Text;
using System.Text.Json;
using modified_structure_analysis.Models;
using OSGeo.GDAL;
using ModelsBand = modified_structure_analysis.Models.Band;

namespace modified_structure_analysis.Services;

public static class ClassificationExporter
{
    public static void Export(ClassificationResult result, ClassificationExportOptions options, List<ModelsBand>? bands = null)
    {
        switch (options.Format)
        {
            case ClassificationExportFormat.GeoTiff:
                ExportToGeoTiff(result, options.FilePath, options.ProjectionWkt, options.IncludeGeoTransform, bands);
                break;
            case ClassificationExportFormat.Png:
                ExportToPng(result, options.FilePath);
                break;
            case ClassificationExportFormat.PngWithWorldFile:
                ExportToPngWithWorldFile(result, options.FilePath, bands);
                break;
        }
    }

    public static void ExportToGeoTiff(ClassificationResult result, string path, string? projectionWkt, bool includeGeoTransform, List<ModelsBand>? bands = null)
    {
        int width = result.Width;
        int height = result.Height;

        var driver = Gdal.GetDriverByName("GTiff");
        if (driver == null)
            throw new InvalidOperationException("GTiff driver not available.");

        using var ds = driver.Create(path, width, height, 1, DataType.GDT_Byte,
            new[] { "COMPRESS=LZW", "TILED=YES" });

        if (includeGeoTransform && bands != null && bands.Count > 0)
        {
            var geoTransform = FindGeoTransform(bands);
            if (geoTransform != null)
                ds.SetGeoTransform(geoTransform.ToGdalArray());
        }

        if (!string.IsNullOrEmpty(projectionWkt))
            ds.SetProjection(projectionWkt);

        using var band = ds.GetRasterBand(1);
        byte[] rasterData = BuildRasterData(result, width, height);
        band.WriteRaster(0, 0, width, height, rasterData, width, height, 0, 0);
        band.FlushCache();

        WriteRasterAttributeTable(band, result);
    }

    private static void WriteRasterAttributeTable(OSGeo.GDAL.Band band, ClassificationResult result)
    {
        if (result.Palette == null || result.Palette.Length == 0)
            return;

        int classCount = result.Palette.Length;

        var rat = new RasterAttributeTable();
        rat.SetTableType(RATTableType.GRTT_THEMATIC);

        int col = 0;
        rat.CreateColumn("Value", RATFieldType.GFT_Integer, RATFieldUsage.GFU_MinMax);
        int valueCol = col++;

        rat.CreateColumn("Red", RATFieldType.GFT_Integer, RATFieldUsage.GFU_Red);
        int redCol = col++;

        rat.CreateColumn("Green", RATFieldType.GFT_Integer, RATFieldUsage.GFU_Green);
        int greenCol = col++;

        rat.CreateColumn("Blue", RATFieldType.GFT_Integer, RATFieldUsage.GFU_Blue);
        int blueCol = col++;

        rat.CreateColumn("Class", RATFieldType.GFT_String, RATFieldUsage.GFU_Name);
        int nameCol = col++;

        rat.SetRowCount(classCount);

        for (int i = 0; i < classCount; i++)
        {
            rat.SetValueAsInt(i, valueCol, i);
            rat.SetValueAsInt(i, redCol, result.Palette[i].R);
            rat.SetValueAsInt(i, greenCol, result.Palette[i].G);
            rat.SetValueAsInt(i, blueCol, result.Palette[i].B);
            rat.SetValueAsString(i, nameCol, $"Class {i}");
        }

        band.SetDefaultRAT(rat);
    }

    public static void ExportToPng(ClassificationResult result, string path)
    {
        using var bitmap = ResultRenderer.ToBitmap(result);
        bitmap.Save(path, ImageFormat.Png);
    }

    public static void ExportToPngWithWorldFile(ClassificationResult result, string path, List<ModelsBand>? bands = null)
    {
        ExportToPng(result, path);

        string worldFilePath = Path.ChangeExtension(path, ".pgw");
        WriteWorldFile(result, worldFilePath, bands);
    }

    public static void ExportStats(ClassificationResult result, StatsExportOptions options)
    {
        var stats = result.GetClassStatistics();
        int totalPixels = result.Width * result.Height;

        switch (options.Format)
        {
            case StatsExportFormat.Csv:
                ExportStatsToCsv(stats, totalPixels, options.FilePath);
                break;
            case StatsExportFormat.Txt:
                ExportStatsToTxt(stats, totalPixels, options.FilePath);
                break;
            case StatsExportFormat.Json:
                ExportStatsToJson(stats, totalPixels, options.FilePath);
                break;
        }
    }

    private static byte[] BuildRasterData(ClassificationResult result, int width, int height)
    {
        byte[] data = new byte[width * height];
        for (int i = 0; i < data.Length; i++)
        {
            int classIdx = result.ClassIndices[i];
            data[i] = classIdx >= 0 ? (byte)(classIdx % 256) : (byte)255;
        }
        return data;
    }

    private static GeoTransform? FindGeoTransform(List<ModelsBand> bands)
    {
        foreach (var band in bands)
        {
            if (band.GeoTransform != null)
                return band.GeoTransform;
        }
        return null;
    }

    private static void WriteWorldFile(ClassificationResult result, string path, List<ModelsBand>? bands)
    {
        var geo = bands != null ? FindGeoTransform(bands) : null;
        if (geo == null)
        {
            geo = new GeoTransform(0, 0, 1, -1);
        }

        // World file stores the CENTER of the top-left pixel, GDAL GeoTransform stores the CORNER.
        // Need to add half-pixel offset.
        double centerX = geo.OriginX + geo.PixelSizeX / 2.0;
        double centerY = geo.OriginY + geo.PixelSizeY / 2.0;
        double pixelSizeX = Math.Abs(geo.PixelSizeX);
        double pixelSizeY = -Math.Abs(geo.PixelSizeY);

        using var writer = new StreamWriter(path);
        writer.WriteLine(pixelSizeX.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
        writer.WriteLine("0");
        writer.WriteLine("0");
        writer.WriteLine(pixelSizeY.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
        writer.WriteLine(centerX.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
        writer.WriteLine(centerY.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
    }

    private static void ExportStatsToCsv(Dictionary<int, int> stats, int totalPixels, string path)
    {
        using var writer = new StreamWriter(path, false, Encoding.UTF8);
        writer.WriteLine("Class,Count,Percentage");
        foreach (var kv in stats.OrderBy(k => k.Key))
        {
            double pct = totalPixels > 0 ? (double)kv.Value / totalPixels * 100 : 0;
            string label = kv.Key >= 0 ? kv.Key.ToString() : "Undefined";
            writer.WriteLine($"{label},{kv.Value},{pct.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}");
        }
    }

    private static void ExportStatsToTxt(Dictionary<int, int> stats, int totalPixels, string path)
    {
        using var writer = new StreamWriter(path, false, Encoding.UTF8);
        writer.WriteLine("Classification Statistics");
        writer.WriteLine("========================");
        writer.WriteLine($"Total pixels: {totalPixels}");
        writer.WriteLine();
        writer.WriteLine("Class\t\tCount\t\tPercentage");
        foreach (var kv in stats.OrderBy(k => k.Key))
        {
            double pct = totalPixels > 0 ? (double)kv.Value / totalPixels * 100 : 0;
            string label = kv.Key >= 0 ? kv.Key.ToString() : "Undefined";
            writer.WriteLine($"{label}\t\t{kv.Value}\t\t{pct.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}%");
        }
    }

    private static void ExportStatsToJson(Dictionary<int, int> stats, int totalPixels, string path)
    {
        var data = new
        {
            totalPixels,
            classes = stats.OrderBy(k => k.Key).Select(kv => new
            {
                classId = kv.Key >= 0 ? kv.Key : -1,
                label = kv.Key >= 0 ? kv.Key.ToString() : "Undefined",
                count = kv.Value,
                percentage = totalPixels > 0 ? Math.Round((double)kv.Value / totalPixels * 100, 2) : 0
            })
        };

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json, Encoding.UTF8);
    }
}
