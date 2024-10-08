using Ardalis.GuardClauses;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteClaimCommand : IRequest<bool>
{
    public required long AccountId { get; set; }
    public required string Name { get; set; }
}

public class DeleteClaimCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteClaimCommandHandler> logger)
    : IRequestHandler<DeleteClaimCommand, bool>
{
    public async Task<bool> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.AccountClaims
            .FirstOrDefaultAsync(r => r.AccountId == request.AccountId && r.Name == request.Name, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(AccountClaim), request.AccountId.ToString());
        }

        logger.LogInformation("Removing account claim {Claim} from DB", request.Name);

        dbContext.AccountClaims.Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}