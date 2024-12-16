using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

// ReSharper disable once UnusedType.Global - Mediator
[ExcludeFromCodeCoverage]
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