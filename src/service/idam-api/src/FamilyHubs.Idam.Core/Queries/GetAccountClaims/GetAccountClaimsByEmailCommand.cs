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

public class GetAccountClaimsByEmailCommandHandler : IRequestHandler<GetAccountClaimsByEmailCommand, List<AccountClaim>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAccountClaimsByEmailCommandHandler> _logger;

    public GetAccountClaimsByEmailCommandHandler(ApplicationDbContext dbContext, ILogger<GetAccountClaimsByEmailCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<AccountClaim>> Handle(GetAccountClaimsByEmailCommand request, CancellationToken cancellationToken)
    {
        long id = 0;

        try
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);

            if (account is null)
            {
                _logger.LogWarning("No account found for requested email");
                return new List<AccountClaim>();
            }

            id = account.Id;

            var accountClaims = account.Claims;

            accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.FullName, account.Name));
            accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.PhoneNumber, account.PhoneNumber));
            accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.AccountStatus, account.Status.ToString()));
            accountClaims.Add(BuildClaim(account.Id, "Email", account.Email));
            accountClaims.Add(BuildClaim(account.Id, "OpenId", account.OpenId));
            accountClaims.Add(BuildClaim(account.Id, FamilyHubsClaimTypes.AccountId, account.Id.ToString()));

            var claims = accountClaims.ToList();
            _logger.LogInformation("Returning {claimsCount} for account Id {Id}", claims.Count, account.Id);

            return claims;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred Getting claims for AccountId:{Id}", id);

            throw;
        }
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