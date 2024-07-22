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

public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, long>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateClaimCommandHandler> _logger;

    public UpdateClaimCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateClaimCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<long> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.AccountClaims
            .FirstOrDefaultAsync(r => r.AccountId == request.AccountId, cancellationToken);

        if (entity is null)
        {
            _logger.LogWarning("Account claim {claim} for AccountId:{accountId} not found", request.Name, request.AccountId);
            throw new NotFoundException(nameof(AccountClaim), request.AccountId.ToString());
        }

        try
        {
            entity.Name = request.Name;
            entity.Value = request.Value;

            _dbContext.AccountClaims.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Account Claim {claim} updated in DB", request.Name);

            return entity.AccountId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred updating claim for Id:{Id}", request.AccountId);
            throw;
        }
    }
}