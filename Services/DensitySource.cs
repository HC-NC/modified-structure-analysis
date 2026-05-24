namespace modified_structure_analysis.Services;

using Models;

public interface IDensitySource
{
    float GetValue(int bandIndex, int pixelIndex);
    float GetKernelC(int bandIndex);
    int GetCount(int bandIndex);
    int ClassId { get; }
}

public class GlobalDensitySource : IDensitySource
{
    private readonly List<Band> _bands;

    public GlobalDensitySource(List<Band> bands)
    {
        _bands = bands;
    }

    public int ClassId => -1;

    public float GetValue(int bandIndex, int pixelIndex) =>
        _bands[bandIndex].GetNormalizedValue(pixelIndex);

    public float GetKernelC(int bandIndex) =>
        _bands[bandIndex].NormalizeKernelC;

    public int GetCount(int bandIndex) =>
        _bands[bandIndex].Count;
}

public class PerClassRegularDensitySource : IDensitySource
{
    private readonly List<Band> _bands;
    private readonly ClassStatistics[] _classStats;
    private readonly int _classIndex;

    public PerClassRegularDensitySource(List<Band> bands, ClassStatistics[] classStats, int classIndex)
    {
        _bands = bands;
        _classStats = classStats;
        _classIndex = classIndex;
    }

    public int ClassId => _classIndex;

    public float GetValue(int bandIndex, int pixelIndex) =>
        _bands[bandIndex].GetNormalizedValue(pixelIndex);

    public float GetKernelC(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.NormalizeKernelC ?? 0.01f;
    }

    public int GetCount(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.Count ?? 0;
    }
}

public class ZScoreDensitySource : IDensitySource
{
    private readonly List<Band> _bands;
    private readonly ClassStatistics[] _classStats;
    private readonly int _classIndex;

    public ZScoreDensitySource(List<Band> bands, ClassStatistics[] classStats, int classIndex)
    {
        _bands = bands;
        _classStats = classStats;
        _classIndex = classIndex;
    }

    public int ClassId => _classIndex;

    public float GetValue(int bandIndex, int pixelIndex)
    {
        float z = _bands[bandIndex].GetZScore(pixelIndex);
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        if (bs == null || bs.ZMax <= bs.ZMin)
            return 0;
        return Math.Clamp((z - bs.ZMin) / (bs.ZMax - bs.ZMin), 0, 1);
    }

    public float GetKernelC(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.ZNormalizeKernelC ?? 0.01f;
    }

    public int GetCount(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.Count ?? 0;
    }
}
