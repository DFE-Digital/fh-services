using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.ReferralUi.UnitTests.Core.ApiClients;

public static class ClientHelper
{
    public static List<TaxonomyDto> GetTaxonomies()
    {
        var activity = new TaxonomyDto { Id = 1, Name = "Activities, clubs and groups", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = null };
        var support = new TaxonomyDto { Id = 2, Name = "Family support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = null };
        var health = new TaxonomyDto { Id = 3, Name = "Health", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = null };
        var earlyYear = new TaxonomyDto { Id = 4, Name = "Pregnancy, birth and early years", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = null };
        var send = new TaxonomyDto { Id = 5, Name = "Special educational needs and disabilities (SEND)", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = null };
        var transport = new TaxonomyDto { Id = 6, Name = "Transport", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = null };

        var taxonomies = new List<TaxonomyDto>
        {
            new() { Name = "Activities", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },
            new() { Name = "Before and after school clubs", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },
            new() { Name = "Holiday clubs and schemes", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },
            new() { Name = "Music, arts and dance", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },
            new() { Name = "Parent, baby and toddler groups", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },
            new() { Name = "Pre-school playgroup", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },
            new() { Name = "Sports and recreation", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = activity.Id },

            new() { Name = "Bullying and cyber bullying", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Debt and welfare advice", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Domestic abuse", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Intensive targeted family support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Money, benefits and housing", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Parenting support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Reducing parental conflict", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Separating and separated parent support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Stopping smoking", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Substance misuse (including alcohol and drug)", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Targeted youth support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },
            new() { Name = "Youth justice services", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = support.Id },

            new() { Name = "Hearing and sight", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = health.Id },
            new() { Name = "Mental health, social and emotional support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = health.Id },
            new() { Name = "Nutrition and weight management", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = health.Id },
            new() { Name = "Oral health", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = health.Id },
            new() { Name = "Public health services", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = health.Id },

            new() { Name = "Birth registration", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = earlyYear.Id },
            new() { Name = "Early years language and learning", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = earlyYear.Id },
            new() { Name = "Health visiting", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = earlyYear.Id },
            new() { Name = "Infant feeding support (including breastfeeding)", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = earlyYear.Id },
            new() { Name = "Midwife and maternity", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = earlyYear.Id },
            new() { Name = "Perinatal mental health support (pregnancy to one year post birth)", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = earlyYear.Id },

            new() { Name = "Autistic Spectrum Disorder (ASD)", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Breaks and respite", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Early years support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Groups for parents and carers of children with SEND", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Hearing impairment", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Learning difficulties and disabilities", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Multi-sensory impairment", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Other difficulties or disabilities", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Physical disabilities", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Social, emotional and mental health support", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Speech, language and communication needs", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },
            new() { Name = "Visual impairment", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = send.Id },

            new() { Name = "Community transport", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = transport.Id },

        };

        var parentTaxonomies = new List<TaxonomyDto>
        {
            activity,
            support,
            health,
            earlyYear,
            send,
            transport,
        };

        taxonomies.AddRange(parentTaxonomies);

        return taxonomies;
    }

    public static ReferralDto GetReferralDto()
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
                UserAccountRoles = new List<UserAccountRoleDto>()
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
                OrganisationDto = new ReferralService.Shared.Dto.OrganisationDto
                {
                    Id = 2,
                    ReferralServiceId = 2,
                    Name = "Organisation",
                    Description = "Organisation Description",
                }
            }
        };
    }

    public static ServiceDto GetTestCountyCouncilServicesDto()
    {
        var service = new ServiceDto
        {
            OrganisationId = 1,
            ServiceType = ServiceType.InformationSharing,
            Status = ServiceStatusType.Active,
            Name = "Unit Test Service",
            Description = "Unit Test Service Description",
            CanFamilyChooseDeliveryLocation = true,
            ServiceDeliveries = new List<ServiceDeliveryDto>
            {
                new()
                {
                    Name = AttendingType.Online,
                }
            },
            Eligibilities = new List<EligibilityDto>
            {
                new()
                {
                    EligibilityType = null,
                    MinimumAge = 0,
                    MaximumAge = 13,
                }
            },
            Contacts = new List<ContactDto>
            {
                new()
                {
                    Name = "Contact",
                    Title = string.Empty,
                    Telephone = "01827 65777",
                    TextPhone = "01827 65777",
                    Url = "https://www.unittestservice.com",
                    Email = "support@unittestservice.com"
                }
            },
            CostOptions = new List<CostOptionDto>
            {
                new()
                {
                    Amount = 1,
                    Option = "paid",
                    AmountDescription = "£1 a session",
                }
            },
            Languages = new List<LanguageDto>
            {
                new()
                {
                    Name = "English",
                    Code = "en"
                }
            },
            ServiceAreas = new List<ServiceAreaDto>
            {
                new()
                {
                    ServiceAreaName = "National",
                    Extent = null,
                    Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                }
            },
            Locations = new List<LocationDto>
            {
                new()
                {
                    Name = "Test Location",
                    Description = "",
                    Latitude = 52.6312,
                    Longitude = -1.66526,
                    Address1 = "Some Lane",
                    City = ", Stathe, Tamworth, Staffordshire, ",
                    PostCode = "B77 3JN",
                    Country = "England",
                    StateProvince = "null",
                    LocationTypeCategory = LocationTypeCategory.FamilyHub,
                    LocationType = LocationType.Postal,
                    Contacts = new List<ContactDto>
                    {
                        new()
                        {
                            Name = "Contact",
                            Title = string.Empty,
                            TextPhone = "01827 65777",
                            Telephone = "01827 65777",
                            Url = "https://www.unittestservice.com",
                            Email = "support@unittestservice.com"
                        }
                    },
                    Schedules = new List<ScheduleDto>
                    {
                        new()
                        {
                            Description = "Description",
                            ValidFrom = new DateTime(2023, 1, 1).ToUniversalTime(),
                            ValidTo = new DateTime(2023, 1, 1).ToUniversalTime().AddHours(8),
                            ByDay = "byDay",
                            ByMonthDay = "byMonth",
                            DtStart = "dtStart",
                            Freq = null,
                            Interval = 1,
                            OpensAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                            ClosesAt = new DateTime(2023, 1, 1).ToUniversalTime().AddMonths(6)
                        }
                    }
                }
            },
            Taxonomies = new List<TaxonomyDto>
            {
                new()
                {
                    Id = 1,
                    Name = "Organisation",
                    TaxonomyType = TaxonomyType.ServiceCategory,
                    ParentId = null
                },
                new()
                {
                    Id = 2,
                    Name = "Support",
                    TaxonomyType = TaxonomyType.ServiceCategory,
                    ParentId = null
                },
                new()
                {
                    Id = 3,
                    Name = "Children",
                    TaxonomyType = TaxonomyType.ServiceCategory,
                    ParentId = null
                },
                new()
                {

                    Id = 4,
                    Name = "Long Term Health Conditions",
                    TaxonomyType = TaxonomyType.ServiceCategory,
                    ParentId = null
                }
            },
            Schedules = new List<ScheduleDto>
            {
                new()
                {
                    Description = "Description",
                    OpensAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                    ClosesAt = new DateTime(2023, 1, 1).ToUniversalTime().AddHours(8),
                    ByDay = "byDay1",
                    ByMonthDay = "byMonth",
                    DtStart = "dtStart",
                    Freq = null,
                    Interval = 1,
                    ValidTo = new DateTime(2023, 1, 1).ToUniversalTime(),
                    ValidFrom = new DateTime(2023, 1, 1).ToUniversalTime().AddMonths(6)
                }
            }
        };

        return service;
    }

    public static ServiceDirectory.Shared.Dto.OrganisationDto GetTestCountyCouncilWithoutAnyServices()
    {
        var testCountyCouncil = new ServiceDirectory.Shared.Dto.OrganisationDto
        {
            OrganisationType = OrganisationType.LA,
            Name = "Unit Test A County Council",
            Description = "Unit Test A County Council",
            AdminAreaCode = "XTEST",
            Uri = new Uri("https://www.unittesta.gov.uk/").ToString(),
            Url = "https://www.unittesta.gov.uk/"
        };

        return testCountyCouncil;
    }

    public static List<AccountDto> GetAccountList() 
    {
        return
        [
            new AccountDto
            {
                Id = 1,
                Name = "Test User1",
                Email = "TestUser1@email.com"
            },


            new AccountDto
            {
                Id = 2,
                Name = "Test User2",
                Email = "TestUser2@email.com"
            }
        ];
    }
}
