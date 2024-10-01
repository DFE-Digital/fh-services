using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Queries.GetAccount;

public class GetAccountByIdCommand : IRequest<Account?>
{
    public required long Id { get; set; }
}

public class GetAccountByIdCommandHandler(ApplicationDbContext dbContext, ILogger<GetAccountByIdCommandHandler> logger)
    : IRequestHandler<GetAccountByIdCommand, Account?>
{
    public async Task<Account?> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting account for Id :{Id}", request.Id);

        var account = await dbContext.Accounts.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (account is null)
        {
            logger.LogWarning("No account found for requested email");
        }

        return account;
    }
}

