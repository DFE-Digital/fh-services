using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;

// ReSharper disable once UnusedType.Global - Mediator
[ExcludeFromCodeCoverage]
public class GetAccountByIdCommandValidator : AbstractValidator<GetAccountByIdCommand>
{
    public GetAccountByIdCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty();
    }
}