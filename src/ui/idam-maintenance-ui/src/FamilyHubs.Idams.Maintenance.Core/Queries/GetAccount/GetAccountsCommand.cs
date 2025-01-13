using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.SharedKernel.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;

public class GetAccountsCommand : IRequest<PaginatedList<Account>>
{
    public GetAccountsCommand(string? userName, string? email, long? organisationId, bool? isLaUser, bool? isVcsUser,
        string? sortBy, int pageNumer = 1, int pagesize = 10)
    {
        PageNumber = pageNumer;
        PageSize = pagesize;
        UserName = userName;
        Email = email;
        OrganisationId = organisationId;
        IsLaUser = isLaUser ?? false; // Treat NULL Db columns as effectively False for these two.
        IsVcsUser = isVcsUser ?? false;
        SortBy = sortBy;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public string? UserName { get; }
    public string? Email { get; }
    public long? OrganisationId { get; }
    public bool IsLaUser { get; }
    public bool IsVcsUser { get; }
    public string? SortBy { get; }
}

public class GetAccountsCommandHandler : IRequestHandler<GetAccountsCommand, PaginatedList<Account>>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetAccountsCommandHandler> _logger;

    public GetAccountsCommandHandler(IRepository repository, ILogger<GetAccountsCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PaginatedList<Account>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Account> query = _repository.Accounts;

            if (!string.IsNullOrEmpty(request.UserName) || !string.IsNullOrEmpty(request.Email))
            {
                query = await FilterByNameOrEmail(query, request.UserName, request.Email, cancellationToken);
            }

            if (request.OrganisationId != null)
            {
                query = query.Where(account => account.Claims.Any(claim =>
                    claim.Name == FamilyHubsClaimTypes.OrganisationId &&
                    claim.Value == request.OrganisationId.ToString()));
            }

            if (request.IsLaUser && request.IsVcsUser)
            {
                query = query.Where(account =>
                    account.Claims.Any(claim => RoleGroups.LaOrVcsProfessionalOrDualRole.Contains(claim.Value)));
            }
            else if (request.IsLaUser)
            {
                query = query.Where(account =>
                    account.Claims.Any(claim => RoleGroups.LaProfessionalOrDualRole.Contains(claim.Value)));
            }
            else if (request.IsVcsUser)
            {
                query = query.Where(account =>
                    account.Claims.Any(claim => RoleGroups.VcsProfessionalOrDualRole.Contains(claim.Value)));
            }

            int resultRowCount = await query.CountAsync(cancellationToken);

            List<Account> result = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<Account>(result, resultRowCount, request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error thrown in GetAccountCommand");
            throw;
        }
    }

    /*
     * Since the IDAM DB fields are all encrypted, we have to load the database into memory for decryption
     * before we can work on it.
     *
     * Since this has the potential to use too many resources (e.g., 10,000 users), we load and process the
     * accounts in batches, until we have gone through the database to find users by name or email.
     */
    private async Task<IQueryable<Account>> FilterByNameOrEmail(IQueryable<Account> query, string? userName, string? email, CancellationToken cancellationToken)
    {
        int rowCount = await _repository.Accounts.CountAsync(cancellationToken);
        int batch = 0;
        const int batchSize = 500;
        int batchTotal = (int)Math.Ceiling((double)rowCount / batchSize);

        List<long> batchResultList = new();

        while (batch < batchTotal)
        {
            IEnumerable<long> accountBatch = (await _repository.Accounts
                    .Skip(batchSize * batch)
                    .Take(batchSize)
                    .Select(account => new {account.Id, account.Name, account.Email})
                    .ToListAsync(cancellationToken))
                .Where(account =>
                    (!string.IsNullOrEmpty(userName) && account.Name.Contains(userName, StringComparison.InvariantCultureIgnoreCase))
                    ||
                    (!string.IsNullOrEmpty(email) && account.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase)))
                .Select(account => account.Id);

            batchResultList.AddRange(accountBatch);

            batch++;
        }

        return query.Where(account => batchResultList.Contains(account.Id));
    }
}
