using AutoMapper;
using FamilyHubs.Referral.Core;
using FamilyHubs.ReferralService.Shared.Dto;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.Referral.UnitTests;

public abstract class BaseUnitTest<T>
{
    protected readonly IMapper Mapper;
    protected readonly ILogger<T> Logger;

    protected BaseUnitTest()
    {
        Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMappingProfiles())));
        Logger = Substitute.For<ILogger<T>>();
    }

    protected static ReferralDto GetReferralDto()
    {
        return new ReferralDto
        {
            Id = 2,
            ReasonForSupport = "Reason For Support",
            EngageWithFamily = "Engage With Family",
            RecipientDto = new RecipientDto
            {
                Id = 2,
                Name = "Joe Blogs",
                Email = "JoeBlog@email.com",
                Telephone = "078123456",
                TextPhone = "078123456",
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                TownOrCity = "Town or City",
                County = "County",
                PostCode = "B30 2TV"
            },
            ReferralUserAccountDto = new UserAccountDto
            {
                Id = 2,
                EmailAddress = "Bob.Referrer@email.com",
                Name = "Bob Referrer",
                PhoneNumber = "1234567890",
                Team = "Team",
                UserAccountRoles = new List<UserAccountRoleDto>
                {
                    new()
                    {
                        UserAccount = new UserAccountDto
                        {
                            EmailAddress = "Bob.Referrer@email.com",
                        },
                        Role = new RoleDto
                        {
                            Name = "VcsProfessional"
                        }
                    }
                },
                ServiceUserAccounts = new List<UserAccountServiceDto>(),
                OrganisationUserAccounts = new List<UserAccountOrganisationDto>(),
            },
            Status = new ReferralStatusDto
            {
                Id = 1,
                Name = "New",
                SortOrder = 0
            },
            ReferralServiceDto = new ReferralServiceDto
            {
                Id = 2,
                Name = "Service",
                Description = "Service Description",
                Url = "www.service.com",
                OrganisationDto = new OrganisationDto
                {
                    Id = 2,
                    Name = "Organisation",
                    Description = "Organisation Description",
                }
            }
        };
    }
}