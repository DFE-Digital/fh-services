using FluentValidation;

namespace FamilyHubs.Idam.Core.Commands.Add;

public class AddClaimCommandValidator : AbstractValidator<AddClaimCommand>
{
    public AddClaimCommandValidator()
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