using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class OrgConnectionRequestsBreakdownRequestValidator : AbstractValidator<OrgConnectionRequestsBreakdownRequest>
{
    public OrgConnectionRequestsBreakdownRequestValidator(IValidator<ConnectionRequestsBreakdownRequest> validator)
    {
        Include(validator);

        RuleFor(v => v.OrgId)
            .NotEmpty();
    }
}
