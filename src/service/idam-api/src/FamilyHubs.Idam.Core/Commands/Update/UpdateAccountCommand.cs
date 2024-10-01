using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Update;

public class UpdateAccountCommand : IRequest
{
    public required long AccountId { get; set; }
    public required string Email { get; set; }
}

public class UpdateAccountCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateAccountCommandHandler> logger)
    : IRequestHandler<UpdateAccountCommand>
{
    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Accounts
            .FirstOrDefaultAsync(r => r.Id == request.AccountId, cancellationToken);

        if (entity is null)
        {
            throw new Ardalis.GuardClauses.NotFoundException(nameof(Account), request.AccountId.ToString());
        }

        logger.LogInformation("Updating account {AccountId} in DB", request.AccountId);

        entity.Email = request.Email.ToLower();

        dbContext.Accounts.Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}