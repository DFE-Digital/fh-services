using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

public class AddUserSessionCommandValidator : AbstractValidator<AddUserSessionCommand>
{
    public const int EmailMaxLength = 255;
    public const int SidMaxLength = 255;
    
    public AddUserSessionCommandValidator()
    {
        RuleFor(v => v.Email)
            .MaximumLength(EmailMaxLength)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Sid)
            .MaximumLength(SidMaxLength)
            .NotNull()
            .NotEmpty();
    }
}