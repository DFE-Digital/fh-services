﻿using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectory.Data.Repository;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync(bool shouldRestDatabaseOnRestart)
    {
        if (shouldRestDatabaseOnRestart) 
            await _context.Database.EnsureDeletedAsync();

        if (!_context.Database.IsSqlServer())
        {
            await _context.Database.EnsureCreatedAsync();
            await _context.Database.ExecuteSqlRawAsync("UPDATE geometry_columns SET srid = 4326 WHERE f_table_name = 'locations';");
        }

        await SeedAsync();
    }

    private async Task SeedAsync()
    {
        var organisationSeedData = new OrganisationSeedData(_context);

        if (!await _context.Taxonomies.AnyAsync())
            await organisationSeedData.SeedTaxonomies();

        if (!await _context.Organisations.AnyAsync())
            await organisationSeedData.SeedOrganisations();
    }
}
