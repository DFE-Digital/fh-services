using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.SharedKernel.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccountClaims;

public class GetAccountClaimsByIdCommand : IRequest<List<AccountClaim>>
{
    public required long AccountId { get; set; }
}

public class GetAccountClaimsByEmailCommandHandler : IRequestHandler<GetAccountClaimsByIdCommand, List<AccountClaim>>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetAccountClaimsByEmailCommandHandler> _logger;

    public GetAccountClaimsByEmailCommandHandler(IRepository repository, ILogger<GetAccountClaimsByEmailCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<AccountClaim>> Handle(GetAccountClaimsByIdCommand request, CancellationToken cancellationToken)
    {
        long id = 0;

        try
        {
            var account = await _repository.Accounts.FirstOrDefaultAsync(r => r.Id == request.AccountId, cancellationToken);

            if (account == null)
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
            _logger.LogInformation("Returning {claimsCount} for {Id}", claims.Count, account.Id);

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
