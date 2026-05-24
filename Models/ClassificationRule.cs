using System.Drawing;

namespace modified_structure_analysis.Models;

public enum ComparisonOperator
{
    GreaterThan,
    LessThan,
    GreaterOrEqual,
    LessOrEqual,
    Equal
}

public enum ClassificationMode
{
    RulePerClass,
    DirectCheck
}

public enum DensityType
{
    ChannelValue,
    ChannelZScore,
    Single,
    Product,
    Multivariate,
    ZScoreSingle,
    ZScoreProduct,
    ZScoreMultivariate
}

public class ClassificationRule : ICloneable
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

    public object Clone()
    {
        ClassificationRule classificationRule = new ClassificationRule();

        classificationRule.Color = Color;
        classificationRule.Name = Name;

        List<Condition> conditions = new();
        
        foreach (Condition condition in Conditions)
            conditions.Add((Condition)condition.Clone());

        classificationRule.Conditions = conditions;
        classificationRule.IsEnabled = IsEnabled;

        return classificationRule;
    }
}