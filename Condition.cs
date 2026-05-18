namespace modified_structure_analysis;

public class Condition
{
    public DensityType LeftDensityType { get; set; } = DensityType.Single;
    public int LeftSingleBandIndex { get; set; }
    public List<int> LeftBandIndices { get; set; } = new();

    public CompareTarget RightSide { get; set; } = new();

    public ComparisonOperator Operator { get; set; } = ComparisonOperator.GreaterThan;
}
