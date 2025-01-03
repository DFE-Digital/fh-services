using FamilyHubs.Idams.Maintenance.Core.Exceptions;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

public class AddClaimCommand : IRequest<long>
{
    public required long AccountId { get; set; }
    public required string Name { get; set; }
    public required string Value { get; set; }
}

public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand, long>
{
    private readonly IRepository _repository;
    private readonly ILogger<AddClaimCommandHandler> _logger;

    public AddClaimCommandHandler(IRepository repository, ILogger<AddClaimCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<long> Handle(AddClaimCommand request, CancellationToken cancellationToken)
    {
        var accountClaim = await _repository.AccountClaims
            .FirstOrDefaultAsync(r => r.AccountId == request.AccountId && r.Name == request.Name, cancellationToken);

        if (accountClaim is not null)
        {
            _logger.LogWarning("Account claim {claim} for AccountId:{accountId} already exists", request.Name, request.AccountId);
            throw new AlreadyExistsException("Account claim Already exists");
        }
        
        try
        {
            var entity = new AccountClaim { AccountId = request.AccountId, Name = request.Name, Value = request.Value };

            await _repository.AddAsync(entity, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);
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