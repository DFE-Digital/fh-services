using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class LaConnectionRequestsBreakdownRequestValidator : AbstractValidator<LaConnectionRequestsBreakdownRequest>
{
    public LaConnectionRequestsBreakdownRequestValidator(IValidator<ConnectionRequestsBreakdownRequest> validator)
    {
        Include(validator);

        RuleFor(v => v.LaOrgId)
            .NotEmpty();
    }
}
