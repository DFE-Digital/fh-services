
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Support;

public static class TestOrganisations
{
    public static readonly OrganisationDto Organisation1 = new()
    {
        Id = 100,
        Name = "Tower Hamlets",
        Description = "Tower Hamlets Council",
        AdminAreaCode = "E09000030",
        OrganisationType = OrganisationType.LA
    };
    
    public static readonly OrganisationDto Organisation2 = new()
    {
        Id = 101,
        Name = "Suffolk County",
        Description = "Suffolk County Council",
        AdminAreaCode = "E10000029",
        OrganisationType = OrganisationType.LA
    };
}