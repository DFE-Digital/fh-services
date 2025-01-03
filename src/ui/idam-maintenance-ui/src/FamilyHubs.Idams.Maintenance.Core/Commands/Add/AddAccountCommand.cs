using FamilyHubs.Idams.Maintenance.Core.Exceptions;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

public class AddAccountCommand : IRequest<string>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<AccountClaim> Claims { get; set; } = new List<AccountClaim>();
}

public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, string>
{
    private readonly IRepository _repository;
    private readonly ISender _sender;
    private readonly ILogger<AddAccountCommandHandler> _logger;

    public AddAccountCommandHandler(
        IRepository repository,
        ISender sender,
        ILogger<AddAccountCommandHandler> logger
    )
    {
        _repository = repository;
        _sender = sender;
        _logger = logger;
    }

    public async Task<string> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        if (await IfAccountExists(request.Email, cancellationToken))
        {
            _logger.LogWarning("Account {email} already exists", request.Email);
            throw new AlreadyExistsException("Account Already exists");
        }

        try
        {
            var entity = new Account
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Status = AccountStatus.Active,
                Claims = request.Claims
            };

            await _repository.AddAsync(entity, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Account {email} saved to DB", request.Email);

            _logger.LogInformation("Account {email} sending an event grid message", request.Email);
            SendEventGridMessageCommand sendEventGridMessageCommand = new(entity);
            _ = _sender.Send(sendEventGridMessageCommand, cancellationToken);
            _logger.LogInformation("Account {email} completed the event grid message", request.Email);


            return entity.Email;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating account for Email:{Email}", request.Email);
            throw;
        }
    }

    private async Task<bool> IfAccountExists(
        string email,
        CancellationToken cancellationToken
    )
    {
        return await _repository.Accounts.AnyAsync(account => account.Email == email, cancellationToken);
    }
}