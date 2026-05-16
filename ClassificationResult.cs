using System.Drawing;

namespace modified_structure_analysis;

public class ClassificationResult
{
    public int Width { get; }
    public int Height { get; }
    public int[] ClassIndices { get; }
    public List<ClassificationRule> Rules { get; }
    public Color UndefinedColor { get; } = Color.Transparent;

    public int UndefinedClassIndex => -1;

    public ClassificationResult(int width, int height, List<ClassificationRule> rules)
    {
        Width = width;
        Height = height;
        Rules = rules;
        ClassIndices = new int[width * height];
        Array.Fill(ClassIndices, UndefinedClassIndex);
    }

    public void SetClass(int pixelIndex, int classIndex)
    {
        if (pixelIndex >= 0 && pixelIndex < ClassIndices.Length)
            ClassIndices[pixelIndex] = classIndex;
    }

    public int GetClass(int pixelIndex)
    {
        if (pixelIndex >= 0 && pixelIndex < ClassIndices.Length)
            return ClassIndices[pixelIndex];
        return UndefinedClassIndex;
    }

    public Color GetPixelColor(int pixelIndex)
    {
        int classIdx = GetClass(pixelIndex);
        if (classIdx < 0 || classIdx >= Rules.Count)
            return UndefinedColor;
        return Rules[classIdx].Color;
    }

    public Bitmap ToBitmap()
    {
        Bitmap bmp = new Bitmap(Width, Height);
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int idx = y * Width + x;
                bmp.SetPixel(x, y, GetPixelColor(idx));
            }
        }
        return bmp;
    }

    public Dictionary<int, int> GetClassStatistics()
    {
        var stats = new Dictionary<int, int>();
        for (int i = 0; i < Rules.Count; i++)
            stats[i] = 0;
        stats[UndefinedClassIndex] = 0;

        foreach (int classIdx in ClassIndices)
        {
            if (!stats.ContainsKey(classIdx))
                stats[classIdx] = 0;
            stats[classIdx]++;
        }

        return stats;
    }
}