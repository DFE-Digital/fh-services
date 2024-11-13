using FamilyHubs.Referral.Data.Entities;

namespace FamilyHubs.Referral.Data.Repository;

public static class ReferralSeedData
{
    public static IReadOnlyCollection<Role> SeedRoles()
    {
        return new List<Role>()
        {
            new Role
            {
                Id = 1,
                Name = "DfeAdmin",
                Description = "DfE Administrator"
            },
            new Role
            {
                Id = 2,
                Name = "LaManager",
                Description = "Local Authority Manager"
            },
            new Role
            {
                Id = 3,
                Name = "VcsManager",
                Description = "VCS Manager"
            },
            new Role
            {
                Id = 4,
                Name = "LaProfessional",
                Description = "Local Authority Professional"
            },
            new Role
            {
                Id = 5,
                Name = "VcsProfessional",
                Description = "VCS Professional"
            },
            new Role
            {
                Id = 6,
                Name = "LaDualRole",
                Description = "Local Authority Dual Role"
            },
            new Role
            {
                Id = 7,
                Name = "VcsDualRole",
                Description = "VCS Dual Role"
            },
        };
    }

    public static IReadOnlyCollection<Status> SeedStatuses()
    {
        return new HashSet<Status>()
        {
            new Status()
            {
                Id = 1,
                Name = "New",
                SortOrder = 0,
                SecondrySortOrder = 1,
            },
            new Status()
            {
                Id = 2,
                Name = "Opened",
                SortOrder = 1,
                SecondrySortOrder = 1,
            },
            new Status()
            {
                Id = 3,
                Name = "Accepted",
                SortOrder = 2,
                SecondrySortOrder = 2,
            },
            new Status()
            {
                Id = 4,
                Name = "Declined",
                SortOrder = 3,
                SecondrySortOrder = 0,
            },
        };
    }

    public static IReadOnlyCollection<Entities.Referral> SeedReferral(bool testing = false)
    {
        List<Data.Entities.Referral> listReferrals = new()
        {
            new Data.Entities.Referral
            {
                ReferrerTelephone = "0121 555 7777",
                ReasonForSupport = "Reason For Support",
                EngageWithFamily = "Engage With Family",
                Recipient = new Recipient
                {
                    Name = "Test User",
                    Email = "TestUser@email.com",
                    Telephone = "078873456",
                    TextPhone = "078873456",
                    AddressLine1 = "Address Line 1",
                    AddressLine2 = "Address Line 2",
                    TownOrCity = "Birmingham",
                    County = "Country",
                    PostCode = "B30 2TV"
                },
                UserAccount = new UserAccount
                {
                    Id = 5,
                    EmailAddress = "Joe.Professional@email.com",
                    Name = "Joe Professional",
                    PhoneNumber = "011 222 3333",
                    Team = "Social Work team North"
                },
                Status = new Status
                {
                    Id = 1,
                    Name = "New",
                    SortOrder = 1,
                    SecondrySortOrder = 0,
                },
                ReferralService = new Data.Entities.ReferralService
                {
                    Id = 1,
                    Name = "Test Service",
                    Description = "Test Service Description",
                    Url = "www.TestService.com",
                    OrganizationId = 1,
                    Organisation = new Organisation
                    {
                        Id = 1,
                        Name = "Test Organisation",
                        Description = "Test Organisation Description",
                    }
                }
            },

            new Data.Entities.Referral
            {
                ReferrerTelephone = "0121 555 7777",
                ReasonForSupport = "Reason For Support 2",
                EngageWithFamily = "Engage With Family 2",
                Recipient = new Recipient
                {
                    Name = "Test User 2",
                    Email = "TestUser2@email.com",
                    Telephone = "078873457",
                    TextPhone = "078873457",
                    AddressLine1 = "User 2 Address Line 1",
                    AddressLine2 = "User 2 Address Line 2",
                    TownOrCity = "Birmingham",
                    County = "Country",
                    PostCode = "B31 2TV"
                },
                UserAccount = new UserAccount
                {
                    Id = 5,
                    EmailAddress = "Joe.Professional@email.com",
                    Name = "Joe Professional",
                    PhoneNumber = "011 222 3333",
                    Team = "Social Work team North"
                },
                Status = new Status
                {
                    Id = 1,
                    Name = "Opened",
                    SortOrder = 1,
                    SecondrySortOrder = 1,
                },
                ReferralService = new Data.Entities.ReferralService
                {
                    Id = 1,
                    Name = "Test Service",
                    Description = "Test Service Description",
                    OrganizationId = 1,
                    Organisation = new Organisation
                    {
                        Id = 1,
                        Name = "Test Organisation",
                        Description = "Test Organisation Description",
                    }
                }
            }
        };

        if (!testing)
        {
            listReferrals.RemoveAt(1);
        }

        return listReferrals;

    }
}
