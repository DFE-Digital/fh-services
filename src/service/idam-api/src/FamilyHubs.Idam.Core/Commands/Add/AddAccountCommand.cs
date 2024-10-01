using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Add;

public class AddAccountCommand : IRequest<string>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<AccountClaim> Claims { get; set; } = new List<AccountClaim>();
}

public class AddAccountCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<AddAccountCommandHandler> logger)
    : IRequestHandler<AddAccountCommand, string>
{
    public async Task<string> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);

        if (account is not null)
        {
            logger.LogWarning("Account {Email} already exists", request.Email);
            throw new AlreadyExistsException("Account Already exists");
        }

        var entity = new Account 
        { 
            Name = request.Name, 
            Email = request.Email.ToLower(), 
            PhoneNumber = request.PhoneNumber,
            Status = AccountStatus.Active , 
            Claims = request.Claims 
        };

        await dbContext.Accounts.AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Account {Id} saved to DB", entity.Id);

        return entity.Email;
    }
}