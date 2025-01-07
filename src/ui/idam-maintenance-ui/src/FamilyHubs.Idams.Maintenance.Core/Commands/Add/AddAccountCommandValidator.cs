using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
{
    public const int NameMaxLength = 255;
    
    public AddAccountCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(NameMaxLength)
            .NotNull()
            .NotEmpty();
    }
}