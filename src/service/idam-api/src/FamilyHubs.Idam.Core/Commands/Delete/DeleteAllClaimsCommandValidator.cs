using FluentValidation;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteAllClaimsCommandValidator : AbstractValidator<DeleteAllClaimsCommand>
{
    public DeleteAllClaimsCommandValidator()
    {
        RuleFor(v => v.AccountId)
            .NotNull()
            .NotEmpty();
    }
}