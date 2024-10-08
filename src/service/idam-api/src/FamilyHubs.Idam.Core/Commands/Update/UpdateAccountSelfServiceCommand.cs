using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Update;

public class UpdateAccountSelfServiceCommand : IRequest
{
    public required long AccountId { get; set; }
    public required string Name { get; set; }
}

public class UpdateAccountSelfServiceCommandHandler(
    ApplicationDbContext dbContext,
    IHttpContextAccessor httpContextAccessor,
    ILogger<UpdateAccountSelfServiceCommandHandler> logger)
    : IRequestHandler<UpdateAccountSelfServiceCommand>
{
    public async Task Handle(UpdateAccountSelfServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Accounts
            .FirstOrDefaultAsync(r => r.Id == request.AccountId, cancellationToken);

        if (entity is null)
        {
            throw new Ardalis.GuardClauses.NotFoundException(nameof(Account), request.AccountId.ToString());
        }

        if (!IsValidRequest(request))
        {
            throw new AuthorisationException("Current User is not authorised to update another user");
        }

        logger.LogInformation("Updating account {AccountId} in DB", request.AccountId);

        entity.Name = request.Name;

        dbContext.Accounts.Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private bool IsValidRequest(UpdateAccountSelfServiceCommand request)
    {
        // does account ID match bearer token ID
        var bearerAccountId = httpContextAccessor.HttpContext?.User.Claims.First(x => x.Type == "AccountId").Value;
        return request.AccountId.ToString() == bearerAccountId;
    }
}