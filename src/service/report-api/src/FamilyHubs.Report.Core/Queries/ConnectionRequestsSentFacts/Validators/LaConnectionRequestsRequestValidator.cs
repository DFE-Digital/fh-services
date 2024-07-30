using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class LaConnectionRequestsRequestValidator : AbstractValidator<LaConnectionRequestsRequest>
{
    public LaConnectionRequestsRequestValidator(IValidator<ConnectionRequestsRequest> validator)
    {
        Include(validator);

        RuleFor(v => v.LaOrgId)
            .NotEmpty();
    }
}
