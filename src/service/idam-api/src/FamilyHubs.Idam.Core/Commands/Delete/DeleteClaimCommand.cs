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

public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteClaimCommandHandler> _logger;

    public DeleteClaimCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteClaimCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.AccountClaims
                .FirstOrDefaultAsync(r => r.AccountId == request.AccountId && r.Name == request.Name, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("Account claim {claim} for AccountId:{accountId} not found", request.Name, request.AccountId);
                throw new NotFoundException(nameof(AccountClaim), request.AccountId.ToString());
            }

            _dbContext.AccountClaims.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Account Claim {claim} removed from DB", request.Name);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred deleting Claim for Id: {AccountId}", request.AccountId);

            throw;
        }
    }
}