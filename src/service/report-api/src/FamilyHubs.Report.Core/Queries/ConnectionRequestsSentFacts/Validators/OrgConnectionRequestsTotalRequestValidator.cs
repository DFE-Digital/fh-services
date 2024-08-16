using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class OrgConnectionRequestsTotalRequestValidator : AbstractValidator<OrgConnectionRequestsTotalRequest>
{
    public OrgConnectionRequestsTotalRequestValidator()
    {
        RuleFor(v => v.OrgId)
            .NotEmpty();
    }
}
