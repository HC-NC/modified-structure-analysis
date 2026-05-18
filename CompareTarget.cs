namespace modified_structure_analysis;

public class CompareTarget
{
    public DensityType DensityType { get; set; } = DensityType.Single;
    public int SingleBandIndex { get; set; }
    public List<int> BandIndices { get; set; } = new();
    public double? ConstantValue { get; set; }
    public bool IsConstant => ConstantValue.HasValue;

    public string ToDisplayString(List<Band> bands)
    {
        if (ConstantValue.HasValue)
            return ConstantValue.Value.ToString("F4");

        string bandName = "";
        if (DensityType == DensityType.Single && SingleBandIndex >= 0 && SingleBandIndex < bands.Count)
            bandName = bands[SingleBandIndex].Name;
        else if (BandIndices.Count > 0)
            bandName = string.Join(", ", BandIndices.Where(i => i >= 0 && i < bands.Count).Select(i => bands[i].Name));

        return DensityType switch
        {
            DensityType.Single => $"p({bandName})",
            DensityType.Product => $"Π({bandName})",
            DensityType.Multivariate => $"p({bandName})",
            _ => bandName
        };
    }
}
