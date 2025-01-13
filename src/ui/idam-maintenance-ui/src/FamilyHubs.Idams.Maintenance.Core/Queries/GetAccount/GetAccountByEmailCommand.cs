using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;

public class GetAccountByEmailCommand : IRequest<Account?>
{
    public GetAccountByEmailCommand(string email)
    {
        Email = email;
    }
    public string Email { get; }
}

public class GetAccountByEmailCommandHandler : IRequestHandler<GetAccountByEmailCommand, Account?>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetAccountByEmailCommandHandler> _logger;

    public GetAccountByEmailCommandHandler(IRepository repository, ILogger<GetAccountByEmailCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Account?> Handle(GetAccountByEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _repository.Accounts.FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);
            return account;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred Getting Account by email");
            throw;
        }
    }
}


