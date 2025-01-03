using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

public class AddClaimCommandValidator : AbstractValidator<AddClaimCommand>
{
    public const int NameMaxLength = 255;
    public const int ValueMaxLength = 255;
    
    public AddClaimCommandValidator()
    {
        RuleFor(v => v.AccountId)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(NameMaxLength)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Value)
            .MaximumLength(ValueMaxLength)
            .NotNull()
            .NotEmpty();
    }
}