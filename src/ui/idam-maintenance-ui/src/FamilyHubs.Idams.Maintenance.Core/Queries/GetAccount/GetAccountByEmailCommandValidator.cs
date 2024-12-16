using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;

// ReSharper disable once UnusedType.Global - Mediator
[ExcludeFromCodeCoverage]
public class GetAccountByEmailCommandValidator : AbstractValidator<GetAccountByEmailCommand>
{
    public GetAccountByEmailCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotNull()
            .NotEmpty();
    }
}