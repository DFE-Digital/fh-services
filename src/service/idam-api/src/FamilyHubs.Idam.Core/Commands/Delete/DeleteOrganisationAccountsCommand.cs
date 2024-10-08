using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteOrganisationAccountsCommand : IRequest<bool>
{
    public required long OrganisationId { get; set; }
}

public class DeleteOrganisationAccountsCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<DeleteOrganisationAccountsCommandHandler> logger)
    : IRequestHandler<DeleteOrganisationAccountsCommand, bool>
{
    public async Task<bool> Handle(DeleteOrganisationAccountsCommand request, CancellationToken cancellationToken)
    {
        var accountIds = await dbContext.AccountClaims
            .Where(r => r.Name == "OrganisationId" && r.Value == request.OrganisationId.ToString())
            .Select(c => c.AccountId).ToListAsync(cancellationToken: cancellationToken);

        if (!accountIds.Any())
        {
            logger.LogInformation("No account claims found for organisation Id: {OrganisationId}", request.OrganisationId);
            return true;
        }                

        var accounts = await dbContext.Accounts
            .Where(r => accountIds.Contains(r.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        if (!accounts.Any())
        {
            logger.LogWarning("No accounts with ids {AccountIds} found for organisation Id: {OrganisationId}", string.Join(",", accountIds), request.OrganisationId);
            throw new NotFoundException("Accounts not found");
        }

        logger.LogInformation("Deleting accounts for Organisation Id: {OrganisationId}", request.OrganisationId);

        dbContext.Accounts.RemoveRange(accounts);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}