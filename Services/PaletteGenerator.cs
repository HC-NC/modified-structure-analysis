using System.Drawing;

namespace modified_structure_analysis.Services;

public static class PaletteGenerator
{
    public static Color[] GenerateHSV(int count)
    {
        Color[] palette = new Color[count];
        for (int i = 0; i < count; i++)
        {
            double hue = (double)i / count * 360.0;
            palette[i] = ColorFromHSV(hue, 0.8, 0.9);
        }
        return palette;
    }

    public static Color[] GenerateSecondStage(Color[] firstStagePalette, int rulesCount)
    {
        if (rulesCount <= 1)
            return (Color[])firstStagePalette.Clone();

        int classCount = firstStagePalette.Length;
        Color[] palette = new Color[classCount * rulesCount];

        for (int c = 0; c < classCount; c++)
        {
            for (int r = 0; r < rulesCount; r++)
            {
                double fraction = (double)r / Math.Max(1, rulesCount - 1);
                double sat = 0.9 - fraction * 0.5;
                double val = 0.95 - fraction * 0.25;
                palette[c * rulesCount + r] = AdjustBrightness(firstStagePalette[c], sat, val);
            }
        }

        return palette;
    }

    private static Color AdjustBrightness(Color color, double targetSat, double targetVal)
    {
        double r = color.R / 255.0;
        double g = color.G / 255.0;
        double b = color.B / 255.0;

        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        double delta = max - min;

        double hue = 0;
        if (delta > 0)
        {
            if (max == r)
                hue = 60 * (((g - b) / delta) % 6);
            else if (max == g)
                hue = 60 * (((b - r) / delta) + 2);
            else
                hue = 60 * (((r - g) / delta) + 4);
        }
        if (hue < 0) hue += 360;

        return ColorFromHSV(hue, targetSat, targetVal);
    }

    private static Color ColorFromHSV(double hue, double saturation, double value)
    {
        int hi = (int)(hue / 60.0) % 6;
        double f = hue / 60.0 - hi;
        double p = value * (1.0 - saturation);
        double q = value * (1.0 - f * saturation);
        double t = value * (1.0 - (1.0 - f) * saturation);

        double r, g, b;
        switch (hi)
        {
            case 0: r = value; g = t; b = p; break;
            case 1: r = q; g = value; b = p; break;
            case 2: r = p; g = value; b = t; break;
            case 3: r = p; g = q; b = value; break;
            case 4: r = t; g = p; b = value; break;
            default: r = value; g = p; b = q; break;
        }

        return Color.FromArgb(
            (int)(r * 255),
            (int)(g * 255),
            (int)(b * 255)
        );
    }
}
