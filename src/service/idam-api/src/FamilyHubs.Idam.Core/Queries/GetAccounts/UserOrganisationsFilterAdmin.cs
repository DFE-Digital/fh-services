using FamilyHubs.Idam.Core.Services;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;

namespace FamilyHubs.Idam.Core.Queries.GetAccounts;

internal class UserOrganisationsFilterAdmin(IServiceDirectoryService serviceDirectoryService) : IUserOrganisationsFilter
{
    private List<OrganisationDto> _organisationCache = [];

    public bool Any => true;
    
    public async Task<IQueryable<Account>> Filter(IQueryable<Account> accountsQuery, string? organisationName)
    {
        if (string.IsNullOrEmpty(organisationName))
        {
            return accountsQuery.Where(acc => acc.Claims.Any(claim => claim.Name == FamilyHubsClaimTypes.OrganisationId && claim.Value != "-1"));
        }

        var orgsByName = await serviceDirectoryService.GetOrganisationsByName(organisationName);
        if (orgsByName != null) _organisationCache = orgsByName;

        var organisationIds = _organisationCache.Select(x => x.Id.ToString());
        return accountsQuery.Where(acc => acc.Claims.Any(claim => claim.Name == FamilyHubsClaimTypes.OrganisationId && organisationIds.Contains(claim.Value)));
    }

    public async Task<IUserOrganisationsFilter> Requested(long requestedOrganisationId)
    {
        var orgsById = await serviceDirectoryService.GetOrganisationsByIds(new List<long> { requestedOrganisationId });
        return new UserOrganisationsFilterStandard(orgsById);
    }

    public async Task<Dictionary<string, OrganisationDto>> MapFor(IEnumerable<string> orgIds)
    {
        var unknownIds = orgIds
            .Select(id => long.TryParse(id, out var o) ? o : -1)
            .Except(_organisationCache.Select(o => o.Id));
        var orgsById = await serviceDirectoryService.GetOrganisationsByIds(unknownIds) ?? new List<OrganisationDto>();

        return _organisationCache.Union(orgsById).ToDictionary(o => o.Id.ToString());
    }
}