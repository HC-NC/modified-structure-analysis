namespace modified_structure_analysis.Services;

using Models;

public interface IDensitySource
{
    float GetValue(int bandIndex, int pixelIndex);
    float GetKernelC(int bandIndex);
    float GetMinValue(int bandIndex);
    float GetMaxValue(int bandIndex);
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
        _bands[bandIndex].GetValue(pixelIndex);

    public float GetKernelC(int bandIndex) =>
        _bands[bandIndex].KernelC;

    public float GetMinValue(int bandIndex) =>
        _bands[bandIndex].Minimum;

    public float GetMaxValue(int bandIndex) =>
        _bands[bandIndex].Maximum;

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
        _bands[bandIndex].GetValue(pixelIndex);

    public float GetKernelC(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.KernelC ?? 0.01f;
    }

    public float GetMinValue(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.Minimum ?? 0;
    }

    public float GetMaxValue(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.Maximum ?? 1;
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

    public float GetValue(int bandIndex, int pixelIndex) =>
        _bands[bandIndex].GetZScore(pixelIndex);

    public float GetKernelC(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.ZKernelC ?? 0.01f;
    }

    public float GetMinValue(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.ZMin ?? 0;
    }

    public float GetMaxValue(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.ZMax ?? 1;
    }

    public int GetCount(int bandIndex)
    {
        var bs = _classStats[_classIndex].Bands?[bandIndex];
        return bs?.Count ?? 0;
    }
}
