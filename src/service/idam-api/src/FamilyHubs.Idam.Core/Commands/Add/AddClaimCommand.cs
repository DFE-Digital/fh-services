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

public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand, long>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AddClaimCommandHandler> _logger;

    public AddClaimCommandHandler(ApplicationDbContext dbContext, ILogger<AddClaimCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<long> Handle(AddClaimCommand request, CancellationToken cancellationToken)
    {
        var accountClaim = await _dbContext.AccountClaims
            .FirstOrDefaultAsync(r => r.AccountId == request.AccountId && r.Name == request.Name, cancellationToken);

        if (accountClaim is not null)
        {
            _logger.LogWarning("Account claim {claim} for AccountId:{accountId} already exists", request.Name, request.AccountId);
            throw new AlreadyExistsException("Account claim Already exists");
        }
        
        try
        {
            var entity = new AccountClaim { AccountId = request.AccountId, Name = request.Name, Value = request.Value };

            await _dbContext.AccountClaims.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Account Claim {claim} saved to DB", request.Name);

            return entity.AccountId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating claim for Id:{Id}", request.AccountId);

            throw;
        }
    }
}