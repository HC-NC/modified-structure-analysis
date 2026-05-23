using System.Drawing;

namespace modified_structure_analysis;

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
