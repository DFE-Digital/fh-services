using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Queries.GetAccount;

public class GetAccountCommand : IRequest<Account?>
{
    public required string Email { get; set; }
}

public class GetAccountCommandHandler(ApplicationDbContext dbContext, ILogger<GetAccountCommandHandler> logger)
    : IRequestHandler<GetAccountCommand, Account?>
{
    public async Task<Account?> Handle(GetAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts.FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);
        if (account is null)
        {
            logger.LogWarning("No account found for requested email");
        }

        return account;
    }
}

