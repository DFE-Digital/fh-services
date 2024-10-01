using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;

namespace FamilyHubs.Referral.Core.Commands;

public abstract class BaseUserAccountHandler
{
    protected readonly ApplicationDbContext _context;

    protected BaseUserAccountHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    protected async Task<UserAccount> AttachExistingUserAccountRoles(UserAccount entity, CancellationToken cancellationToken)
    {
        if (entity.UserAccountRoles == null)
        {
            return entity;
        }
        for (int i = 0; i < entity.UserAccountRoles.Count; i++)
        {
            UserAccountRole? dbUserAccountRole = _context.UserAccountRoles.SingleOrDefault(x => x.UserAccountId == entity.UserAccountRoles[i].UserAccountId && (x.RoleId == entity.UserAccountRoles[i].RoleId || x.Role.Name == entity.UserAccountRoles[i].Role.Name));
            if (dbUserAccountRole == null)
            {
                _context.UserAccountRoles.Add(entity.UserAccountRoles[i]);
                await _context.SaveChangesAsync(cancellationToken);
            }

            entity.UserAccountRoles[i] = _context.UserAccountRoles.Single(x => x.UserAccountId == entity.UserAccountRoles[i].UserAccountId && x.RoleId == entity.UserAccountRoles[i].RoleId);
        }

        return entity;
    }

    protected async Task<UserAccount> AttachExistingOrgansiation(UserAccount entity, CancellationToken cancellationToken)
    {
        if (entity.OrganisationUserAccounts == null)
        {
            return entity;
        }

        var withIndexes = entity.OrganisationUserAccounts.Select((x, i) => (i, x)).ToList();
        foreach (var (idx, userAccountOrganisation) in withIndexes)
        {
            var organisation = _context.Organisations.SingleOrDefault(x => x.Id == userAccountOrganisation.Organisation.Id);

            if (organisation == null)
            {
                if (string.IsNullOrEmpty(userAccountOrganisation.Organisation.Name))
                {
                    entity.OrganisationUserAccounts.RemoveAt(idx);
                    continue;
                }
                _context.Organisations.Add(userAccountOrganisation.Organisation);
                await _context.SaveChangesAsync(cancellationToken);
            }

            userAccountOrganisation.Organisation = _context.Organisations.Single(x => x.Id == userAccountOrganisation.Organisation.Id);
        }

        return entity;
    }

    protected async Task<UserAccount> AttachExistingService(UserAccount entity, CancellationToken cancellationToken)
    {
        if (entity.ServiceUserAccounts == null)
        {
            return entity;
        }
        foreach (UserAccountService serviceUserAccount in entity.ServiceUserAccounts)
        {
            Organisation? organisation = _context.Organisations.SingleOrDefault(x => x.Id == serviceUserAccount.ReferralService.Id);

            if (organisation == null)
            {
                _context.ReferralServices.Add(serviceUserAccount.ReferralService);
                await _context.SaveChangesAsync(cancellationToken);
            }

            serviceUserAccount.ReferralService = _context.ReferralServices.Single(x => x.Id == serviceUserAccount.ReferralService.Id);
        }

        return entity;
    }
}
