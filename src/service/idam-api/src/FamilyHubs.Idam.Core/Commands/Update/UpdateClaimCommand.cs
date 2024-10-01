using Ardalis.GuardClauses;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Update;

public class UpdateClaimCommand : IRequest<long>
{
    public required long AccountId { get; set; }
    public required string Name { get; set; }
    public required string Value { get; set; }
}

public class UpdateClaimCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateClaimCommandHandler> logger)
    : IRequestHandler<UpdateClaimCommand, long>
{
    public async Task<long> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.AccountClaims
            .FirstOrDefaultAsync(r => r.AccountId == request.AccountId, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(AccountClaim), request.AccountId.ToString());
        }

        logger.LogInformation("Updating account claim {Claim} for Id {Id} in DB", request.Name, request.AccountId);

        entity.Name = request.Name;
        entity.Value = request.Value;

        dbContext.AccountClaims.Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.AccountId;
    }
}