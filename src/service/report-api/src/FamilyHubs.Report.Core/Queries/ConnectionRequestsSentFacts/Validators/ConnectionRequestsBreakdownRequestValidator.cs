using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class ConnectionRequestsBreakdownRequestValidator : AbstractValidator<ConnectionRequestsBreakdownRequest>
{
    public ConnectionRequestsBreakdownRequestValidator()
    {
        RuleFor(v => v.Date)
            .NotEmpty();
    }
}
