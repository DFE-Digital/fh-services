using Ardalis.GuardClauses;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Delete;

public class DeleteAccountCommand : IRequest<bool>
{
    public required long AccountId { get; set; }
}

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
{
    private readonly IRepository _repository;
    private readonly ILogger<DeleteAccountCommandHandler> _logger;

    public DeleteAccountCommandHandler(IRepository repository, ILogger<DeleteAccountCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.Accounts
                .Where(r => r.Id == request.AccountId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning($"No account found for Id: {request.AccountId}");
                throw new NotFoundException(nameof(Account), request.AccountId.ToString());
            }

            _repository.Remove(entity);

            await _repository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Account with Id: {request.AccountId} deleted");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred deleting Account for Id: {AccountId}", request.AccountId);

            throw;
        }
    }
}