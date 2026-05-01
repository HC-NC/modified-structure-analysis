using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;

namespace modified_structure_analysis;

public class AxisSettings
{
    public double? Minimum { get; set; }
    public double? Maximum { get; set; }
    public bool IsLogarithmic { get; set; } = false;
    public string Title { get; set; } = "";

    public void ApplyToAxis(Axis axis)
    {
        if (Minimum.HasValue)
            axis.Minimum = Minimum.Value;
        if (Maximum.HasValue)
            axis.Maximum = Maximum.Value;
    }

    public void Reset()
    {
        Minimum = null;
        Maximum = null;
        IsLogarithmic = false;
    }
}

public class LegendSettings
{
    public bool IsVisible { get; set; } = true;
    public LegendPosition Position { get; set; } = LegendPosition.RightTop;
    public LegendPlacement Placement { get; set; } = LegendPlacement.Inside;
}

public class GridSettings
{
    public bool IsVisible { get; set; } = true;
    public bool IsMajorGridVisible { get; set; } = true;
    public bool IsMinorGridVisible { get; set; } = false;
}

public class PlotSettings
{
    public AxisSettings XAxis { get; set; } = new();
    public AxisSettings YAxis { get; set; } = new();
    public LegendSettings Legend { get; set; } = new();
    public GridSettings Grid { get; set; } = new();

    public void ResetAxes()
    {
        XAxis.Reset();
        YAxis.Reset();
    }

    public void ResetAll()
    {
        ResetAxes();
        Legend.IsVisible = true;
        Legend.Position = LegendPosition.RightTop;
        Grid.IsVisible = true;
    }
}