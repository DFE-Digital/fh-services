using Ardalis.GuardClauses;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteAccountCommand : IRequest<bool>
{
    public required long AccountId { get; set; }
}

public class DeleteAccountCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteAccountCommandHandler> logger)
    : IRequestHandler<DeleteAccountCommand, bool>
{
    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Accounts
            .Where(r => r.Id == request.AccountId)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Account), request.AccountId.ToString());
        }

        logger.LogInformation("Deleting account with Id: {AccountId}", request.AccountId);

        dbContext.Accounts.Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}