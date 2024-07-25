using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class OrgConnectionRequestsRequestValidator : AbstractValidator<OrgConnectionRequestsRequest>
{
    public OrgConnectionRequestsRequestValidator(IValidator<ConnectionRequestsRequest> validator)
    {
        Include(validator);

        RuleFor(v => v.OrgId)
            .NotEmpty();
    }
}
