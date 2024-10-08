using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Add;

public class AddClaimCommand : IRequest<long>
{
    public required long AccountId { get; set; }
    public required string Name { get; set; }
    public required string Value { get; set; }
}

public class AddClaimCommandHandler(ApplicationDbContext dbContext, ILogger<AddClaimCommandHandler> logger)
    : IRequestHandler<AddClaimCommand, long>
{
    public async Task<long> Handle(AddClaimCommand request, CancellationToken cancellationToken)
    {
        var accountClaim = await dbContext.AccountClaims
            .FirstOrDefaultAsync(r => r.AccountId == request.AccountId && r.Name == request.Name, cancellationToken);

        if (accountClaim is not null)
        {
            logger.LogWarning("Account claim {Claim} for AccountId:{AccountId} already exists", request.Name, request.AccountId);
            throw new AlreadyExistsException("Account claim Already exists");
        }

        var entity = new AccountClaim
        {
            AccountId = request.AccountId,
            Name = request.Name,
            Value = request.Value
        };

        await dbContext.AccountClaims.AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Account Claim {Claim} saved to DB", request.Name);

        return entity.AccountId;
    }
}