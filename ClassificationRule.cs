using System.Drawing;

namespace modified_structure_analysis;

public enum ComparisonOperator
{
    GreaterThan,
    LessThan,
    GreaterOrEqual,
    LessOrEqual,
    Equal
}

public enum DensityType
{
    Single,
    Product,
    Multivariate
}

public class ClassificationRule
{
    public Color Color { get; set; } = Color.Red;
    public string Name { get; set; } = "";
    public List<Condition> Conditions { get; set; } = new();
    public bool IsEnabled { get; set; } = true;

    public string GenerateName()
    {
        if (Conditions.Count == 0)
            return "Empty Rule";

        return $"Rule with {Conditions.Count} condition(s)";
    }
}