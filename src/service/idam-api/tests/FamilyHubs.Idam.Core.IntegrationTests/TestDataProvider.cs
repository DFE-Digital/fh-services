using FamilyHubs.Idam.Core.Services;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;

namespace FamilyHubs.Idam.Core.IntegrationTests;

public static class TestDataProvider
{
    public static AccountClaim GetSingleTestAccountClaim()
    {
        return new AccountClaim
        {
            AccountId = 1,
            Name = FamilyHubsClaimTypes.OrganisationId,
            Value = "1"
        };
    }

    public static Task<List<OrganisationDto>?> GetOrganisations()
    {
        var organisations = new List<OrganisationDto>();
        organisations.Add(new OrganisationDto
        {
            Id= 1,
            AdminAreaCode = "E12345",
            Name = "TestOrganisation",
            OrganisationType = OrganisationType.LA,
            Description = "Description"
        });

        organisations.Add(new OrganisationDto
        {
            Id = 2,
            AdminAreaCode = "E12346",
            Name = "TestOrganisationFilter",
            OrganisationType = OrganisationType.LA,
            Description = "Description"
        });

#pragma warning disable CS8619 
        return Task.FromResult(organisations);
#pragma warning restore CS8619 
    }
}