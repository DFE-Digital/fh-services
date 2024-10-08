using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using FamilyHubs.SharedKernel.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Queries.GetAccountClaims;

public class GetAccountClaimsByEmailCommand : IRequest<List<AccountClaim>>
{
    public required string Email { get; set; }
}

public class GetAccountClaimsByEmailCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<GetAccountClaimsByEmailCommandHandler> logger)
    : IRequestHandler<GetAccountClaimsByEmailCommand, List<AccountClaim>>
{
    public async Task<List<AccountClaim>> Handle(GetAccountClaimsByEmailCommand request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts.FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);

        if (account is null)
        {
            logger.LogWarning("No account found for requested email");
            return [];
        }

        logger.LogInformation("Fetching claims for account Id {Id}", account.Id);

        var accountClaims = account.Claims;

        accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.FullName, account.Name));
        accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.PhoneNumber, account.PhoneNumber));
        accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.AccountStatus, account.Status.ToString()));
        accountClaims.Add(BuildClaim(account.Id, "Email", account.Email));
        accountClaims.Add(BuildClaim(account.Id, "OpenId", account.OpenId));
        accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.AccountId, account.Id.ToString()));

        var claims = accountClaims.ToList();
        logger.LogInformation("Returning {ClaimsCount} for account Id {Id}", claims.Count, account.Id);

        return claims;
    }

    private static AccountClaim BuildClaim(long id, string claimName, string? claimValue)
    {
        return new AccountClaim
        {
            AccountId = id,
            Name = claimName,
            Value = claimValue ?? string.Empty
        };
    }
}