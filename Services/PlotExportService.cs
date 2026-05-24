using System.Drawing.Imaging;
using OxyPlot;
using OxyPlot.WindowsForms;
using modified_structure_analysis.Models;

namespace modified_structure_analysis.Services;

public static class PlotExportService
{
    public static void Export(PlotModel model, string filePath, GraphExportOptions options)
    {
        string? originalTitle = null;
        if (options.Title != null)
        {
            originalTitle = model.Title;
            model.Title = options.Title;
        }

        try
        {
            switch (options.Format)
            {
                case GraphExportFormat.Png:
                    ExportToPng(model, filePath, options.Width, options.Height, options.Dpi, options.Grayscale);
                    break;
                case GraphExportFormat.Jpeg:
                    ExportToJpeg(model, filePath, options.Width, options.Height, options.Dpi, options.JpegQuality, options.Grayscale);
                    break;
                case GraphExportFormat.Svg:
                    ExportToSvg(model, filePath, options.Width, options.Height);
                    break;
                case GraphExportFormat.Pdf:
                    ExportToPdf(model, filePath, options.Width, options.Height);
                    break;
            }
        }
        finally
        {
            if (originalTitle != null)
                model.Title = originalTitle;
        }
    }

    public static void ExportToPng(PlotModel model, string path, int width, int height, int dpi = 96, bool grayscale = false)
    {
        using var bmp = RenderToBitmap(model, width, height);
        if (grayscale) ConvertToGrayscale(bmp);
        bmp.SetResolution(dpi, dpi);
        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
    }

    public static void ExportToJpeg(PlotModel model, string path, int width, int height, int dpi = 96, int quality = 90, bool grayscale = false)
    {
        using var bmp = RenderToBitmap(model, width, height);
        if (grayscale) ConvertToGrayscale(bmp);
        bmp.SetResolution(dpi, dpi);

        var encoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
        var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
        encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        bmp.Save(path, encoder, encoderParams);
    }

    public static void ExportToSvg(PlotModel model, string path, int width, int height)
    {
        using var stream = File.Create(path);
        OxyPlot.SvgExporter.Export(model, stream, width, height, true, null, false);
    }

    public static void ExportToPdf(PlotModel model, string path, int width, int height)
    {
        using var stream = File.Create(path);
        var exporter = new PdfExporter { Width = width, Height = height };
        exporter.Export(model, stream);
    }

    private static Bitmap RenderToBitmap(PlotModel model, int width, int height)
    {
        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using var g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        var rc = new GraphicsRenderContext();
        rc.SetGraphicsTarget(g);
        ((IPlotModel)model).Update(true);
        ((IPlotModel)model).Render(rc, new OxyRect(0, 0, width, height));

        return bmp;
    }

    private static void ConvertToGrayscale(Bitmap bmp)
    {
        var attr = new ImageAttributes();
        var matrix = new ColorMatrix(new[]
        {
            new[] { 0.299f, 0.299f, 0.299f, 0f, 0f },
            new[] { 0.587f, 0.587f, 0.587f, 0f, 0f },
            new[] { 0.114f, 0.114f, 0.114f, 0f, 0f },
            new[] { 0f,     0f,     0f,     1f, 0f },
            new[] { 0f,     0f,     0f,     0f, 1f }
        });
        attr.SetColorMatrix(matrix);

        using var g = Graphics.FromImage(bmp);
        g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height),
            0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attr);
    }
}
