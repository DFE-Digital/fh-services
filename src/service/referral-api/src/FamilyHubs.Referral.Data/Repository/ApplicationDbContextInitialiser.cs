using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Data.Repository;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync(bool isProduction, bool shouldRestDatabaseOnRestart)
    {
        if (!isProduction)
        {
            if (shouldRestDatabaseOnRestart)
                await _context.Database.EnsureDeletedAsync();

            if (_context.Database.IsSqlServer())
                await _context.Database.MigrateAsync();
            else
                await _context.Database.EnsureCreatedAsync();

            await SeedAsync();
        }
    }

    private async Task SeedAsync()
    {
        var statuses = ReferralSeedData.SeedStatuses();
        if (!await _context.Statuses.AnyAsync())
        {
            _context.Statuses.AddRange(statuses);

            await _context.SaveChangesAsync();
        }
        else
        {
            foreach (var seedStatus in statuses)
            {
                var dbStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.Name == seedStatus.Name);
                if (!seedStatus.Equals(dbStatus))
                { 
                    if (dbStatus == null)
                    {
                        dbStatus = seedStatus;
                    }
                    else
                    {
                        dbStatus.Name = seedStatus.Name;
                        dbStatus.SortOrder = seedStatus.SortOrder;
                        dbStatus.SecondrySortOrder = seedStatus.SecondrySortOrder;
                    }
                    
                    _context.Statuses.Update(dbStatus); 
                }
            }

            await _context.SaveChangesAsync();
        }

        var roles = ReferralSeedData.SeedRoles();
        if (!await _context.Roles.AnyAsync())
        {
            _context.Roles.AddRange(roles);

            await _context.SaveChangesAsync();
        }
        else
        {
            foreach (var seedRole in roles)
            {
                var dbRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == seedRole.Name);
                if (!seedRole.Equals(dbRole))
                {
                    if (dbRole == null)
                    {
                        dbRole = seedRole;
                    }
                    else
                    {
                        dbRole.Name = seedRole.Name;
                        dbRole.Description = seedRole.Description;
                        
                    }

                    _context.Roles.Update(dbRole);
                }
            }

            await _context.SaveChangesAsync();
        }
        

        if (_context.Database.IsSqlite() && !await _context.Referrals.AnyAsync())
        {
            var referrals = ReferralSeedData.SeedReferral();

            foreach (Entities.Referral referral in referrals)
            {
                var status = await _context.Statuses.SingleOrDefaultAsync(x => x.Name == referral.Status.Name);
                if (status != null)
                {
                    referral.Status = status;
                }
            }

            _context.Referrals.AddRange(referrals);

            await _context.SaveChangesAsync();
        }
    }
}
