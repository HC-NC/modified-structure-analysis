namespace modified_structure_analysis.Models;

public class CompareTarget : ICloneable
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
        if ((DensityType == DensityType.Single || DensityType == DensityType.ChannelValue
            || DensityType == DensityType.ChannelZScore || DensityType == DensityType.ZScoreSingle)
            && SingleBandIndex >= 0 && SingleBandIndex < bands.Count)
            bandName = bands[SingleBandIndex].Name;
        else if (BandIndices.Count > 0)
            bandName = string.Join(", ", BandIndices.Where(i => i >= 0 && i < bands.Count).Select(i => bands[i].Name));

        return DensityType switch
        {
            DensityType.Single => $"p({bandName})",
            DensityType.Product => $"Π({bandName})",
            DensityType.Multivariate => $"p({bandName})",
            DensityType.ChannelValue => $"v({bandName})",
            DensityType.ChannelZScore => $"z({bandName})",
            DensityType.ZScoreSingle => $"zs_p({bandName})",
            DensityType.ZScoreProduct => $"zs_Π({bandName})",
            DensityType.ZScoreMultivariate => $"zs_p({bandName})",
            _ => bandName
        };
    }

    public object Clone()
    {
        CompareTarget compareTarget = new CompareTarget();

        compareTarget.DensityType = DensityType;
        compareTarget.SingleBandIndex = SingleBandIndex;
        compareTarget.BandIndices = [.. BandIndices];
        compareTarget.ConstantValue = ConstantValue;
        
        return compareTarget;
    }
}
