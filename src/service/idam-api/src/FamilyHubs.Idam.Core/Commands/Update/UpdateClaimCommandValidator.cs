using FluentValidation;

namespace FamilyHubs.Idam.Core.Commands.Update;

public class UpdateClaimCommandValidator : AbstractValidator<UpdateClaimCommand>
{
    public UpdateClaimCommandValidator()
    {
        RuleFor(v => v.AccountId)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(255)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Value)
            .MaximumLength(255)
            .NotNull()
            .NotEmpty();
    }
}