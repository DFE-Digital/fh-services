using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

// ReSharper disable once UnusedType.Global - Mediator
[ExcludeFromCodeCoverage]
public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
{
    public AddAccountCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(255)
            .NotNull()
            .NotEmpty();
    }
}