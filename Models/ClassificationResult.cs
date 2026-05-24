namespace modified_structure_analysis.Models;

public class ClassificationResult
{
    public int Width { get; }
    public int Height { get; }
    public int[] ClassIndices { get; }
    public List<ClassificationRule>? Rules { get; }
    public Color[]? Palette { get; }
    public Color UndefinedColor { get; } = Color.Transparent;

    public int UndefinedClassIndex => -1;

    public ClassificationResult(int width, int height, List<ClassificationRule> rules)
    {
        Width = width;
        Height = height;
        Rules = rules;
        Palette = null;
        ClassIndices = new int[width * height];
        Array.Fill(ClassIndices, UndefinedClassIndex);
    }

    public ClassificationResult(int width, int height, Color[] palette)
    {
        Width = width;
        Height = height;
        Rules = null;
        Palette = palette;
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

    public Dictionary<int, int> GetClassStatistics()
    {
        var stats = new Dictionary<int, int>();

        if (Palette != null)
        {
            for (int i = 0; i < Palette.Length; i++)
                stats[i] = 0;
        }
        else if (Rules != null)
        {
            for (int i = 0; i < Rules.Count; i++)
                stats[i] = 0;
        }
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
