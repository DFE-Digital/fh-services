using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccountClaims;

// ReSharper disable once UnusedType.Global - Mediator
[ExcludeFromCodeCoverage]
public class GetAccountClaimsByIdCommandValidator : AbstractValidator<GetAccountClaimsByIdCommand>
{
    public GetAccountClaimsByIdCommandValidator()
    {
        RuleFor(v => v.AccountId)
            .GreaterThan(0);
    }
}