using System.Drawing.Imaging;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using modified_structure_analysis.Models;

namespace modified_structure_analysis.Services;

public static class PlotExportService
{
    public static void Export(PlotModel model, string filePath, GraphExportOptions options)
    {
        switch (options.Format)
        {
            case GraphExportFormat.Png:
                ExportToPng(model, filePath, options.Width, options.Height, options.Dpi);
                break;
            case GraphExportFormat.Jpeg:
                ExportToJpeg(model, filePath, options.Width, options.Height, options.Dpi, options.JpegQuality);
                break;
            case GraphExportFormat.Svg:
                ExportToSvg(model, filePath, options.Width, options.Height);
                break;
            case GraphExportFormat.Pdf:
                ExportToPdf(model, filePath, options.Width, options.Height);
                break;
        }
    }

    public static void ExportToPng(PlotModel model, string path, int width, int height, int dpi = 96)
    {
        using var bmp = RenderToBitmap(model, width, height);
        bmp.SetResolution(dpi, dpi);
        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
    }

    public static void ExportToJpeg(PlotModel model, string path, int width, int height, int dpi = 96, int quality = 90)
    {
        using var bmp = RenderToBitmap(model, width, height);
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
        var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using var g = System.Drawing.Graphics.FromImage(bmp);
        g.Clear(System.Drawing.Color.White);

        var rc = new GraphicsRenderContext();
        rc.SetGraphicsTarget(g);
        ((IPlotModel)model).Update(true);
        ((IPlotModel)model).Render(rc, new OxyRect(0, 0, width, height));

        return bmp;
    }
}
