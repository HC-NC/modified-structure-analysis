namespace modified_structure_analysis.Services;

using Models;

public static class ResultRenderer
{
    public static Color GetPixelColor(ClassificationResult result, int pixelIndex)
    {
        int classIdx = result.GetClass(pixelIndex);
        if (result.Palette != null)
        {
            if (classIdx >= 0 && classIdx < result.Palette.Length)
                return result.Palette[classIdx];
            return result.UndefinedColor;
        }
        if (classIdx < 0 || result.Rules == null || classIdx >= result.Rules.Count)
            return result.UndefinedColor;
        return result.Rules[classIdx].Color;
    }

    public static Bitmap ToBitmap(ClassificationResult result)
    {
        Bitmap bmp = new Bitmap(result.Width, result.Height);
        for (int y = 0; y < result.Height; y++)
        {
            for (int x = 0; x < result.Width; x++)
            {
                int idx = y * result.Width + x;
                bmp.SetPixel(x, y, GetPixelColor(result, idx));
            }
        }
        return bmp;
    }
}
