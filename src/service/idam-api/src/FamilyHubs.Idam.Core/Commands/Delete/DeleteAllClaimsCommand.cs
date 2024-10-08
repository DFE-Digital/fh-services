using Ardalis.GuardClauses;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteAllClaimsCommand : IRequest<bool>
{
    public required long AccountId { get; set; }
}

public class DeleteAllClaimsCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<DeleteAllClaimsCommandHandler> logger)
    : IRequestHandler<DeleteAllClaimsCommand, bool>
{
    public async Task<bool> Handle(DeleteAllClaimsCommand request, CancellationToken cancellationToken)
    {
        var entities = await dbContext.AccountClaims
            .Where(r => r.AccountId == request.AccountId)
            .ToListAsync(cancellationToken);

        if (entities is null or { Count: < 1 })
        {
            throw new NotFoundException(nameof(AccountClaim), request.AccountId.ToString());
        }

        logger.LogInformation("Deleting {ClaimsCount} claims for accountId:{AccountId}", entities.Count, request.AccountId);

        dbContext.AccountClaims.RemoveRange(entities);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}