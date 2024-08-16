using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FluentValidation;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Validators;

public class ConnectionRequestsRequestValidator : AbstractValidator<ConnectionRequestsRequest>
{
    public ConnectionRequestsRequestValidator()
    {
        RuleFor(v => v.Date)
            .NotEmpty();

        RuleFor(v => v.AmountOfDays)
            .NotNull();
    }
}
