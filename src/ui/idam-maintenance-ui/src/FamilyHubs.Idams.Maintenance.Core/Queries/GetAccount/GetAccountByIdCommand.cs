
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;

public class GetAccountByIdCommand : IRequest<Account?>
{
    public required long Id { get; set; }
}

public class GetAccountByIdCommandHandler : IRequestHandler<GetAccountByIdCommand, Account?>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetAccountByIdCommandHandler> _logger;

    public GetAccountByIdCommandHandler(IRepository repository, ILogger<GetAccountByIdCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Account?> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _repository.Accounts.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            return account;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred Getting Account for Id :{Id}", request.Id);
            throw;
        }
    }
}

