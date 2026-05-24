using modified_structure_analysis.Models;

namespace modified_structure_analysis.Forms;

public class ConditionDisplayItem
{
    public Condition Condition { get; }
    public string Display { get; }

    public ConditionDisplayItem(Condition cond, List<Band> bands)
    {
        Condition = cond;
        string leftStr = GetDensityString(cond.LeftDensityType, cond.LeftSingleBandIndex, cond.LeftBandIndices, bands);
        string rightStr = cond.RightSide.ToDisplayString(bands);
        string opStr = cond.Operator switch
        {
            ComparisonOperator.GreaterThan => ">",
            ComparisonOperator.LessThan => "<",
            ComparisonOperator.GreaterOrEqual => ">=",
            ComparisonOperator.LessOrEqual => "<=",
            ComparisonOperator.Equal => "==",
            _ => "?"
        };
        Display = $"{leftStr} {opStr} {rightStr}";
    }

    private string GetDensityString(DensityType type, int singleIdx, List<int> indices, List<Band> bands)
    {
        if (type == DensityType.Single && singleIdx >= 0 && singleIdx < bands.Count)
            return $"p({bands[singleIdx].Name})";

        if (type == DensityType.ChannelValue && singleIdx >= 0 && singleIdx < bands.Count)
            return $"v({bands[singleIdx].Name})";

        if (type == DensityType.ChannelZScore && singleIdx >= 0 && singleIdx < bands.Count)
            return $"z({bands[singleIdx].Name})";

        if (type == DensityType.ZScoreSingle && singleIdx >= 0 && singleIdx < bands.Count)
            return $"zs_p({bands[singleIdx].Name})";

        var names = indices.Where(i => i >= 0 && i < bands.Count).Select(i => $"{i}: {bands[i].Name}");
        string nameStr = string.Join(", ", names);

        return type switch
        {
            DensityType.Product => $"Π({nameStr})",
            DensityType.Multivariate => $"p({nameStr})",
            DensityType.ZScoreProduct => $"zs_Π({nameStr})",
            DensityType.ZScoreMultivariate => $"zs_p({nameStr})",
            _ => nameStr
        };
    }
}