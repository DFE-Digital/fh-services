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

public class DeleteAllClaimsCommandHandler : IRequestHandler<DeleteAllClaimsCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteAllClaimsCommandHandler> _logger;

    public DeleteAllClaimsCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteAllClaimsCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAllClaimsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.AccountClaims
                .Where(r => r.AccountId == request.AccountId)
                .ToListAsync(cancellationToken);

            if (entities is null or { Count: < 1 })
            {
                _logger.LogWarning("Np Account claims for accountId:{accountId} found", request.AccountId);
                throw new NotFoundException(nameof(AccountClaim), request.AccountId.ToString());
            }

            _dbContext.AccountClaims.RemoveRange(entities);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("{claimsCount} claims deleted for accountId:{accountId}", entities.Count, request.AccountId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred deleting All Claims for Id: {accountId}", request.AccountId);

            throw;
        }
    }
}