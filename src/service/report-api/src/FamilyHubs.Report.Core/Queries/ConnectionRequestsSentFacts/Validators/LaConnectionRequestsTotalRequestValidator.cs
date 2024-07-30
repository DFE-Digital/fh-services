using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class LaConnectionRequestsTotalRequestValidator : AbstractValidator<LaConnectionRequestsTotalRequest>
{
    public LaConnectionRequestsTotalRequestValidator()
    {
        RuleFor(v => v.LaOrgId)
            .NotEmpty();
    }
}
