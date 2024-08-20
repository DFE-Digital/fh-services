using FluentValidation;

namespace FamilyHubs.Idam.Core.Queries.GetAccountClaims;

public class GetAccountClaimsByEmailCommandValidator : AbstractValidator<GetAccountClaimsByEmailCommand>
{
    public GetAccountClaimsByEmailCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotNull()
            .NotEmpty();
    }
}