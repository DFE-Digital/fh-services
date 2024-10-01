using FamilyHubs.Idam.Core.Models;
using FamilyHubs.Idam.Core.Services;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using FamilyHubs.SharedKernel.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Idam.Core.Queries.GetAccounts;

public class GetAccountsCommand : IRequest<PaginatedList<Account>?>
{
    public long? RequestedOrganisationId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? OrganisationName { get; set; }
    public bool IsLaUser { get; set; }
    public bool IsVcsUser { get; set; }
    public string SortBy { get; set; }

    public GetAccountsCommand(long? id, int? pageSize, int? pageNumber, string? userName, string? email, string? organisationName, bool? isLaUser, bool? isVcsUser, string? sortBy)
    {
        RequestedOrganisationId = id;
        PageSize = pageSize;
        PageNumber = pageNumber;
        UserName = userName;
        Email = email;
        OrganisationName = organisationName;
        IsLaUser = isLaUser ?? false;
        IsVcsUser = isVcsUser ?? false;
        SortBy = sortBy ?? "Name_Ascending";
    }
}

public class GetAccountsCommandHandler(
    ApplicationDbContext dbContext,
    IServiceDirectoryService serviceDirectoryService,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetAccountsCommand, PaginatedList<Account>?>
{
    private const string OrganisationNameClaim = "OrganisationName";

    public async Task<PaginatedList<Account>?> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
    {
        var authorisedOrganisations = await GetUserAuthorisedOrganisations();

        var (success, organisations) = await TryGetRequestedAssociatedOrganisations(request.RequestedOrganisationId, authorisedOrganisations);
        if(!success)
        {
            //  No organisations listed to return users from
            return null;
        }

        return await GetUsers(organisations, request);
    }

    private async Task<IUserOrganisationsFilter> GetUserAuthorisedOrganisations()
    {
        var user = httpContextAccessor.HttpContext?.GetFamilyHubsUser();

        if (!long.TryParse(user?.OrganisationId, out var organisationId))
        {
            throw new UnauthorizedAccessException("Invalid Bearer token, Organisation Claim is invalid");
        }

        if (organisationId == -1 && user.Role == RoleTypes.DfeAdmin)
        {
            return new UserOrganisationsFilterAdmin(serviceDirectoryService);// DfeAdmins can view users related to all organisations
        }

        // Returns only organisations that the current bearer token has access to
        return new UserOrganisationsFilterStandard(
            await serviceDirectoryService.GetOrganisationsByAssociatedId(organisationId)
        );
    }

    private static async Task<(bool, IUserOrganisationsFilter)> TryGetRequestedAssociatedOrganisations(long? requestedOrganisationId, IUserOrganisationsFilter authorisedOrganisations)
    {
        //  If no organisations return false
        if (!authorisedOrganisations.Any)
        {
            return (false, new UserOrganisationsFilterStandard());
        }

        //If no Id requested, return all authorised organisations
        if (!requestedOrganisationId.HasValue)
        {
            return (authorisedOrganisations.Any, authorisedOrganisations);
        }

        var organisations = await authorisedOrganisations.Requested(requestedOrganisationId.Value);
        return (organisations.Any, organisations);
    }

    private async Task<PaginatedList<Account>?> GetUsers(IUserOrganisationsFilter organisations, GetAccountsCommand request)
    {
        int pageNumber = request.PageNumber.GetValueOrDefault();
        int pageSize = request.PageSize.GetValueOrDefault();

        var accountsQuery = await CreateFilteredQuery(organisations, request);
        accountsQuery = SortQuery(accountsQuery, request);

        var totalCount = await accountsQuery.CountAsync();
        if(totalCount == 0)
            return null;

        if (pageNumber > 0)
        {
            //  Only paginate if page parameters passed in
            accountsQuery = accountsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        else
        {
            //  Returning all results on a single page
            pageNumber = 1;
            pageSize = totalCount;
        }

        var users = await accountsQuery.ToListAsync();
        await AttachOrganisationNameToUserClaims(users, organisations);
        var result = new PaginatedList<Account>(users, totalCount, pageNumber, pageSize);

        return result;
    }

    private async Task<IQueryable<Account>> CreateFilteredQuery(IUserOrganisationsFilter organisations, GetAccountsCommand request)
    {
        var accountsQuery = await organisations
            .Filter(dbContext.Accounts, request.OrganisationName);
            
        accountsQuery = accountsQuery.Where(x => x.Status == Data.Entities.AccountStatus.Active);

        if (request.IsLaUser != request.IsVcsUser) // If both values are the same dont filter
        {
            List<string> roles = request.IsLaUser
                ? [RoleTypes.LaProfessional, RoleTypes.LaManager, RoleTypes.LaDualRole]
                : [RoleTypes.VcsProfessional, RoleTypes.VcsManager, RoleTypes.VcsDualRole];

            accountsQuery = accountsQuery.Where(acc=>
                acc.Claims.Any(claim => claim.Name == FamilyHubsClaimTypes.Role && roles.Contains(claim.Value)));
        }

        if (!string.IsNullOrEmpty(request.UserName) || !string.IsNullOrEmpty(request.Email))
        {
            var accounts = FindAccounts(accountsQuery, request).Select(acc => acc.Id);

            accountsQuery = dbContext.Accounts.Where(x => accounts.Contains(x.Id));
        }

        return accountsQuery;
    }

    private static IEnumerable<Account> FindAccounts(IQueryable<Account> activeAccounts, GetAccountsCommand request)
    {
        const int pageSize = 1000;

        var pageNumber = 0;
        int currentCount;
        var accounts = new HashSet<Account>();

        do
        {
            var entities = activeAccounts
                .IgnoreAutoIncludes()
                .OrderBy(a => a.Id)
                .Skip(pageNumber*pageSize).Take(pageSize).ToList();

            pageNumber++;
            currentCount = entities.Count;

            if (!string.IsNullOrEmpty(request.UserName))
            {
                var results = entities.Where(x => x.Name.ToLower().Contains(request.UserName.ToLower()));
                accounts.UnionWith(results);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var results = entities.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
                accounts.UnionWith(results);
            }
        } while (currentCount >= pageSize);

        return accounts;
    }

    private IQueryable<Account> SortQuery(IQueryable<Account> accounts, GetAccountsCommand request)
    {
        return request.SortBy switch
        {
            "Name_Ascending" => accounts.OrderBy(a => a.Name),
            "Name_Descending" => accounts.OrderByDescending(a => a.Name),
            "Email_Ascending" => accounts.OrderBy(a => a.Email),
            "Email_Descending" => accounts.OrderByDescending(a => a.Email),
            "Organisation_Ascending" => accounts.OrderBy(a => a.Claims.First(claim => claim.Name == OrganisationNameClaim).Value),
            "Organisation_Descending" => accounts.OrderByDescending(a => a.Claims.First(claim => claim.Name == OrganisationNameClaim).Value),
            _ => throw new ArgumentException("Invalid parameter 'sortBy', value must be Name, Email or Organisation")
        };
    }

    private static async Task AttachOrganisationNameToUserClaims(IEnumerable<Account> accounts, IUserOrganisationsFilter organisations)
    {
        var users = accounts.Select(user => (user, user.Claims.First(y => y.Name == FamilyHubsClaimTypes.OrganisationId).Value)).ToList();
        var organisationMap = await organisations.MapFor(users.Select(u => u.Value));
        foreach (var (user, claimOrgId) in users)
        {
            var organisation = organisationMap[claimOrgId];
            user.Claims.Add(new AccountClaim { AccountId = user.Id, Name = OrganisationNameClaim, Value = organisation.Name });
        }
    }
}


