namespace FamilyHubs.ServiceDirectory.Core.Queries.Dsl.Condition;

public class OrCondition : FhQueryCondition
{
    private readonly FhQueryCondition[] _conditions;

    public OrCondition(params FhQueryCondition[] conditions)
    {
        _conditions = conditions;
    }

    public override FhParameter[] AllParameters() =>
        _conditions.Aggregate(
            (IEnumerable<FhParameter>) Array.Empty<FhParameter>(),
            (acc, next) => acc.Concat(next.AllParameters())
        ).ToArray();

    public override string Format() => $"({string.Join(" OR ", _conditions.Select(cond => cond.Format()))})";
}
