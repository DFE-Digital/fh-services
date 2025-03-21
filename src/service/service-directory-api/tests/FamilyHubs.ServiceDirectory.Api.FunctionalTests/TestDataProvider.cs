﻿using System.Diagnostics;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FamilyHubs.ServiceDirectory.Core.Helper;
using FamilyHubs.ServiceDirectory.Data.Entities.ManyToMany;
using NetTopologySuite.Geometries;
using Location = FamilyHubs.ServiceDirectory.Data.Entities.Location;

namespace FamilyHubs.ServiceDirectory.Api.FunctionalTests;

public static class TestDataProvider
{
    public static OrganisationDetailsDto GetTestCountyCouncilRecord()
    {
        var bristolCountyCouncil = new OrganisationDetailsDto
        {
            AdminAreaCode = "E06000023",
            OrganisationType = OrganisationType.LA,
            Name = "Test County Council",
            Description = "Test County Council",
            Url = new Uri("https://www.test.gov.uk/").ToString(),
            Uri = "https://www.test.gov.uk/",
            Services = new List<ServiceDto>
            {
                new ServiceDto
                {
                    ServiceType = ServiceType.InformationSharing,
                    Name = "Test Organisation for Children with Tracheostomies",
                    Description = @"Test Organisation for for Children with Tracheostomies is a national self help group operating as a registered charity and is run by parents of children with a tracheostomy and by people who sympathise with the needs of such families. ACT as an organisation is non profit making, it links groups and individual members throughout Great Britain and Northern Ireland.",
                    ServiceDeliveries = new List<ServiceDeliveryDto>
                    {
                        new ServiceDeliveryDto
                        {
                            Name = AttendingType.Online,
                        }
                    },
                    Eligibilities = new List<EligibilityDto>
                    {
                        new EligibilityDto
                        {
                            MaximumAge = 13,
                            MinimumAge = 0
                        }
                    },
                    Contacts = new List<ContactDto>
                    {
                        new ContactDto
                        {
                            Name = "Service",
                            Title = string.Empty,
                            Telephone = "01827 65780",
                            TextPhone = "01827 65780",
                            Url = "www.testservice.com",
                            Email = "support@testservice.com"
                        }
                    },
                    Languages = new List<LanguageDto>
                    {
                        new LanguageDto
                        {
                            Name = "English",
                            Code = "en"
                        }
                    },
                    ServiceAreas = new List<ServiceAreaDto>
                    {
                        new ServiceAreaDto
                        {
                            Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                            ServiceAreaName = "National",
                            Extent = null
                        }
                    },
                    Locations = new List<LocationDto>
                    {
                        new LocationDto
                        {
                            LocationTypeCategory = LocationTypeCategory.FamilyHub,
                            Name = "Test Location Name",
                            Latitude = 52.6312,
                            Longitude = -1.66526,
                            Address1 = "75 Sheepcote Lane",
                            City = ", Stathe, Tamworth, Staffordshire, ",
                            PostCode = "B77 3JN",
                            StateProvince = "",
                            Country = "England",
                            LocationType= LocationType.Postal
                        }
                    },
                    Taxonomies = new List<TaxonomyDto>
                    {
                        new TaxonomyDto
                        {
                            Name = "Organisation",
                            TaxonomyType = TaxonomyType.ServiceCategory
                        },
                        new TaxonomyDto
                        {
                            Name = "Support",
                            TaxonomyType = TaxonomyType.ServiceCategory
                        },
                        new TaxonomyDto
                        {
                            Name = "Children",
                            TaxonomyType = TaxonomyType.ServiceCategory
                        },
                        new TaxonomyDto
                        {
                            Name = "Long Term Health Conditions",
                            TaxonomyType = TaxonomyType.ServiceCategory
                        }
                    }
                }
            }
        };

        return bristolCountyCouncil;
    }

    public static ServiceDto GetTestCountyCouncilServicesCreateRecord(long parentId)
    {
        var service = new ServiceDto
        {
            OrganisationId = 1,
            ServiceType = ServiceType.InformationSharing,
            Name = "Test Organisation for Children with Tracheostomies",
            Description = @"Test1 Organisation for for Children with Tracheostomies is a national self help group operating as a registered charity and is run by parents of children with a tracheostomy and by people who sympathise with the needs of such families. ACT as an organisation is non profit making, it links groups and individual members throughout Great Britain and Northern Ireland.",
            ServiceDeliveries = new List<ServiceDeliveryDto>
            {
                new ServiceDeliveryDto
                {
                    Name = AttendingType.Online,
                }
            },
            Eligibilities = new List<EligibilityDto>
            {
                new EligibilityDto
                {
                    MaximumAge = 13,
                    MinimumAge = 0
                }
            },
            Contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    Name =  "Service",
                    Title = string.Empty,
                    Telephone = "01827 65770",
                    TextPhone = "01827 65770",
                    Url = "https://www.testservice1.com",
                    Email = "support@testservice1.com"
                }
            },
            Languages = new List<LanguageDto>
            {
                new LanguageDto
                {
                    Name = "English",
                    Code = "en"
                }
            },
            ServiceAreas = new List<ServiceAreaDto>
            {
                new ServiceAreaDto
                {
                    ServiceAreaName = "National",
                    Extent = null,
                    Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                }
            },
            Locations = new List<LocationDto>
            {
                new LocationDto
                {
                    Name = "Test",
                    Description = "",
                    Latitude = 52.6312,
                    Longitude = -1.66526,
                    Address1 = "76 Sheepcote Lane",
                    City = ", Stathe, Tamworth, Staffordshire, ",
                    PostCode = "B77 3JN",
                    Country = "England",
                    StateProvince = "null",
                    LocationTypeCategory = LocationTypeCategory.FamilyHub,
                    LocationType= LocationType.Postal
                }
            }
        };

        return service;
    }

    public static IList<Service> SeedSalfordService(long organisationId)
    {
        return new List<Service>
        {
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.FamilyExperience,
                Name = "Baby Social at Ordsall Neighbourhood Centre",
                Description = "This session is for babies non mobile aged from birth to twelve months. Each week we will introduce you to one of our five to thrive key messages and a fun activity you can do at home with your baby. It will also give you the opportunity to connect with other parents and share your experiences.",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                        ServiceId = 0
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 1,
                        MinimumAge = 0,
                        ServiceId = 0
                    }
                },
                CostOptions = new List<CostOption>
                {
                    new CostOption
                    {
                        Option = "Session",
                        Amount = 2.5m,
                        AmountDescription = "AmountDescription",
                        ServiceId = 0
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                        ServiceId = 0
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "Local",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                        ServiceId = 0
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Ordsall Neighbourhood Centre",
                            Description = "2, Robert Hall Street M5 3LT",
                            Latitude = 53.474103227856105D,
                            Longitude = -2.2721559641660787D,
                            GeoPoint = new Point(-2.2721559641660787D, 53.474103227856105D) { SRID = GeoPoint.WGS84 },
                            Address1 = "2, Robert Hall Street",
                            City = "Ordsall",
                            PostCode = "M5 3LT",
                            Country = "United Kingdom",
                            StateProvince = "Salford",
                            LocationType= LocationType.Postal,
                            Schedules = new List<Schedule>
                            {
                                new Schedule
                                {
                                    Description = "Friday 1.30pm - 2.30pm",
                                    ByDay = "FR"
                                }
                            },
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "",
                                    Name = "Broughton Hub",
                                    Telephone = "0161 778 0601",
                                    TextPhone = "0161 778 0601",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        },
                        Schedules = new List<Schedule>
                        {
                            new Schedule
                            {
                                Description = "Friday 1.30pm - 2.30pm",
                                ByDay = "SU"
                            }
                        }
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "Infant feeding support",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 1
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.FamilyExperience,
                Name = "Oakwood Academy",
                Description = "Oakwood Academy is a special school for pupils aged 9-18 years who have a range of moderate and/or complex learning difficulties. The school has Visual Arts, Technology and Sports Specialist status. \r\n\r\nAdmissions to Oakwood Academy are controlled by Salford Local Authority. We are unable to accept direct requests for placement from parents or carers or other local authorities. Pupils who attend Oakwood Academy have an Educational, Health and Care Plan which outlines the area of need and what provision and resources are needed to support the pupil. \r\n\r\nIn rare cases, a child may be admitted on an assessment placement to determine what the pupil's needs are and whether their needs can be met at Oakwood Academy. ",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                        ServiceId = 0
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 10,
                        MinimumAge = 4,
                        ServiceId = 0
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                        ServiceId = 0
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "Local",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                        ServiceId = 0
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Oakwood Academy",
                            Description = "",
                            Latitude = 53.493505779578605D,
                            Longitude = -2.336084327089324D,
                            GeoPoint = new Point(-2.2721559641660787D, 53.474103227856105D) { SRID = GeoPoint.WGS84 },
                            Address1 = "Chatsworth Road",
                            City = "Eccles",
                            PostCode = "M30 9DY",
                            Country = "United Kingdom",
                            StateProvince = "Manchester",
                            LocationType= LocationType.Postal,
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Ms",
                                    Name = "Kate Berry",
                                    Telephone = "01619212880",
                                    TextPhone = "01619212880",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        }
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "Early years support",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 2
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.FamilyExperience,
                Name = "Central Family Hub",
                Description = "Family Hub",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                        ServiceId = 0,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 25,
                        MinimumAge = 0,
                        ServiceId = 0,
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                        ServiceId = 0
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "Local",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                        ServiceId = 0,
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.FamilyHub,
                            Name = "Central Family Hub",
                            Description = "Broughton Hub",
                            Latitude = 53.507025D,
                            Longitude = -2.259764D,
                            GeoPoint = new Point(-2.259764D, 53.507025D) { SRID = GeoPoint.WGS84 },
                            Address1 = "50 Rigby Street",
                            City = "Manchester",
                            PostCode = "M7 4BQ",
                            Country = "United Kingdom",
                            StateProvince = "Salford",
                            LocationType= LocationType.Postal,
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Ms",
                                    Name = "Kate Berry",
                                    Telephone = "0161 778 0601",
                                    TextPhone = "0161 778 0601",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        }
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.FamilyExperience,
                Name = "North Family Hub",
                Description = "Family Hub",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                        ServiceId = 0
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 25,
                        MinimumAge = 0,
                        ServiceId = 0
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                        ServiceId = 0
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "Local",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                        ServiceId = 0
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.FamilyHub,
                            Name = "North Family Hub",
                            Description = "Swinton Gateway",
                            Longitude = 53.5124278D,
                            Latitude = -2.342044D,
                            GeoPoint = new Point(-2.342044D, 53.5124278D) { SRID = GeoPoint.WGS84 },
                            Address1 = "100 Chorley Road",
                            City = "Manchester",
                            PostCode = "M27 6BP",
                            Country = "United Kingdom",
                            StateProvince = "Salford",
                            LocationType= LocationType.Postal,
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Ms",
                                    Name = "Kate Berry",
                                    Telephone = "0161 778 0495",
                                    TextPhone = "0161 778 0495",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        }
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.FamilyExperience,
                Name = "South Family Hub",
                Description = "Family Hub",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                        ServiceId = 0,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 25,
                        MinimumAge = 0,
                        ServiceId = 0,
                    }
                },
                Fundings = new List<Funding>
                {
                    new Funding
                    {
                        Source = "funding source",
                        ServiceId = 0,
                    }
                },
                CostOptions = new List<CostOption>
                {
                    new CostOption
                    {
                        AmountDescription = "Amount Description",
                        Amount = 100000000,
                        Option = "options",
                        ValidFrom = new DateTime(2023, 1, 1).ToUniversalTime(),
                        ValidTo = new DateTime(2023, 1, 1).ToUniversalTime(),
                        ServiceId = 0
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                        ServiceId = 0
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "Local",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                        ServiceId = 0
                    }
                },
                Schedules = new List<Schedule>
                {
                    new Schedule
                    {
                        Description = "Schedule",
                        OpensAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                        ClosesAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                        ByDay = "MO,TU",
                        ByMonthDay = "byMonth",
                        DtStart = "dtStart",
                        Freq = null,
                        ValidFrom = new DateTime(2023, 1, 1).ToUniversalTime(),
                        ValidTo = new DateTime(2023, 1, 1).ToUniversalTime(),
                        ServiceId = 0
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.FamilyHub,
                            Name = "South Family Hub",
                            Description = "Winton Children’s Centre",
                            Latitude = 53.48801070060149D,
                            Longitude = -2.368140748303118D,
                            GeoPoint = new Point(-2.368140748303118D, 53.48801070060149D) { SRID = GeoPoint.WGS84 },
                            Address1 = "Brindley Street",
                            City = "Manchester",
                            PostCode = "M30 8AB",
                            Country = "United Kingdom",
                            StateProvince = "Salford",
                            LocationType= LocationType.Postal,
                            AccessibilityForDisabilities = new List<AccessibilityForDisabilities>
                            {
                                new AccessibilityForDisabilities
                                {
                                    Accessibility = "AccessibilityForDisabilities",
                                    LocationId = 0
                                }
                            },
                            Schedules = new List<Schedule>
                            {
                                new Schedule
                                {
                                    Description = "Schedule",
                                    OpensAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                                    ClosesAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                                    ByDay = "WE",
                                    ByMonthDay = "byMonth",
                                    DtStart = "dtStart",
                                    Freq = null,
                                    ValidFrom = new DateTime(2023, 1, 1).ToUniversalTime(),
                                    ValidTo = new DateTime(2023, 1, 1).ToUniversalTime(),
                                }
                            },
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Ms",
                                    Name = "Kate Berry",
                                    Telephone = "0161 686 5260",
                                    TextPhone = "0161 686 5260",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        },
                        Schedules = new List<Schedule>
                        {
                            new Schedule
                            {
                                Description = "Schedule",
                                OpensAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                                ClosesAt = new DateTime(2023, 1, 1).ToUniversalTime(),
                                ByDay = "TH",
                                ByMonthDay = "byMonth",
                                DtStart = "dtStart",
                                Freq = null,
                                ValidFrom = new DateTime(2023, 1, 1).ToUniversalTime(),
                                ValidTo = new DateTime(2023, 1, 1).ToUniversalTime(),
                            }
                        }
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "breastfeeding support",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 1
                    }
                },
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Title = "Ms",
                        Name = "Kate Berry",
                        Telephone = "0161 686 5260",
                        TextPhone = "0161 686 5260",
                        Url = "https://www.gov.uk",
                        Email = "help@gov.uk"
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.FamilyExperience,
                Name = "Defunct Family Hub",
                Description = "Family Hub",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Defunct,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 90,
                        MinimumAge = 0,
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "Local",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.FamilyHub,
                            Name = "North Family Hub",
                            Description = "Swinton Gateway",
                            Longitude = 53.5124278D,
                            Latitude = -2.342044D,
                            GeoPoint = new Point(-2.342044D, 53.5124278D) { SRID = GeoPoint.WGS84 },
                            Address1 = "100 Chorley Road",
                            City = "Manchester",
                            PostCode = "M27 6BP",
                            Country = "United Kingdom",
                            StateProvince = "Salford",
                            LocationType= LocationType.Postal,
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Ms",
                                    Name = "Kate Berry",
                                    Telephone = "0161 778 0495",
                                    TextPhone = "0161 778 0495",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        }
                    }
                }
            },
        };
    }

    public static IList<Service> SeedBristolServices(long organisationId)
    {
        return new List<Service>
        {
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.InformationSharing,
                Name = "Aid for Children with Tracheostomies",
                Description = @"Aid for Children with Tracheostomies is a national self help group operating as a registered charity and is run by parents of children with a tracheostomy and by people who sympathise with the needs of such families. ACT as an organisation is non profit making, it links groups and individual members throughout Great Britain and Northern Ireland.",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.Online,
                    }
                },
                Schedules = new List<Schedule>
                {
                    new Schedule
                    {
                        AttendingType = AttendingType.Online.ToString()
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 13,
                        MinimumAge = 0,
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en"
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "National",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "Hearing and sight",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 3
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Bristol-Service-1-Location",
                            Description = "",
                            Latitude = 51.48801070060149D,
                            Longitude = -1.66526,
                            GeoPoint = new Point(-1.66526, 51.48801070060149D) { SRID = GeoPoint.WGS84 },
                            Address1 = "7A Boyce's Ave, Clifton",
                            City = "Bristol",
                            PostCode = "BS8 4AA",
                            Country = "England",
                            StateProvince = "Bristol",
                            LocationType= LocationType.Postal
                        }
                    }
                },
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Title = "Mr",
                        Name = "John Smith",
                        Telephone = "01827 65778",
                        TextPhone = "01827 65778",
                        Url = "https://www.gov.uk",
                        Email = "help@gov.uk"
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.InformationSharing,
                Name = "Test Service - Free - 10 to 15 yrs",
                Description = @"This is a test service.",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 15,
                        MinimumAge = 10,
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en"
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "National",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "Childrens Activities",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 4
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Bristol-Service-2-Location",
                            Description = "",
                            Latitude = 51.6312,
                            Longitude = -1.66526,
                            GeoPoint = new Point(-1.66526, 51.6312) { SRID = GeoPoint.WGS84 },
                            Address1 = "7A Boyce's Ave, Clifton",
                            City = "Bristol",
                            PostCode = "BS8 4AA",
                            Country = "England",
                            StateProvince = "Bristol",
                            LocationType= LocationType.Postal
                        }
                    }
                },
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Title = "Mr",
                        Name = "John Smith",
                        Telephone = "01827 65711",
                        TextPhone = "01827 65711",
                        Url = "https://www.gov.uk",
                        Email = "help@gov.uk"
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.InformationSharing,
                Name = "Test Service - Paid - 0 to 13yrs",
                Description = @"This is a paid test service.",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = false,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.Telephone,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 13,
                        MinimumAge = 0,
                    }
                },
                CostOptions = new List<CostOption>
                {
                    new CostOption
                    {
                        Option = "Session",
                        Amount = 45.0m,
                        AmountDescription = "AmountDescription"
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en"
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "National",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Bristol-Service-3-Location",
                            Description = "",
                            Longitude = 51.63123,
                            Latitude = -1.66519,
                            GeoPoint = new Point(-1.66519, 51.63123) { SRID = GeoPoint.WGS84 },
                            Address1 = "7A Boyce's Ave, Clifton",
                            City = "Bristol",
                            PostCode = "BS8 4AA",
                            Country = "England",
                            StateProvince = "Bristol",
                            LocationType= LocationType.Postal
                        }
                    }
                },
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Title = "Mr",
                        Name = "John Smith",
                        Telephone = "01827 64328",
                        TextPhone = "01827 64328",
                        Url = "https://www.gov.uk",
                        Email = "help@gov.uk"
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "parent child activities",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 4
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.InformationSharing,
                Name = "Test Service - Paid - 15 to 20yrs - Afrikaans",
                Description = @"This is an Afrikaans test service.",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Active,
                CanFamilyChooseDeliveryLocation = true,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 20,
                        MinimumAge = 15,
                    }
                },
                CostOptions = new List<CostOption>
                {
                    new CostOption
                    {
                        Option = "Hour",
                        Amount = 25.0m,
                        AmountDescription = "AmountDescription"
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "Afrikaans",
                        Code = "af"
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "National",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Bristol-Service-4-Location",
                            Description = "",
                            Longitude = 51.63123,
                            Latitude = -1.66519,
                            GeoPoint = new Point(-1.66519, 51.63123) { SRID = GeoPoint.WGS84 },
                            Address1 = "7A Boyce's Ave, Clifton",
                            City = "Bristol",
                            PostCode = "BS8 4AA",
                            Country = "England",
                            StateProvince = "",
                            LocationType= LocationType.Postal,
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Mr",
                                    Name = "John Smith",
                                    Telephone = "01827 64328",
                                    TextPhone = "01827 64328",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        }
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "swimming",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 4
                    }
                }
            },
            new Service
            {
                OrganisationId = organisationId,
                ServiceType = ServiceType.InformationSharing,
                Name = "Test Service - Defunct",
                Description = "This is a Defunct test service.",
                DeliverableType = DeliverableType.NotSet,
                Status = ServiceStatusType.Defunct,
                CanFamilyChooseDeliveryLocation = true,
                ServiceDeliveries = new List<ServiceDelivery>
                {
                    new ServiceDelivery
                    {
                        Name = AttendingType.InPerson,
                    },
                    new ServiceDelivery
                    {
                        Name = AttendingType.Online,
                    }
                },
                Eligibilities = new List<Eligibility>
                {
                    new Eligibility
                    {
                        EligibilityType = null,
                        MaximumAge = 90,
                        MinimumAge = 10,
                    }
                },
                CostOptions = new List<CostOption>
                {
                    new CostOption
                    {
                        Option = "Hour",
                        Amount = 25.0m,
                        AmountDescription = "AmountDescription"
                    }
                },
                Languages = new List<Language>
                {
                    new Language
                    {
                        Name = "English",
                        Code = "en",
                    }
                },
                ServiceAreas = new List<ServiceArea>
                {
                    new ServiceArea
                    {
                        ServiceAreaName = "National",
                        Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    }
                },
                ServiceAtLocations = new List<ServiceAtLocation>
                {
                    new ServiceAtLocation
                    {
                        Id = 0,
                        LocationId = 0,
                        ServiceId = 0,
                        Location = new Location
                        {
                            OrganisationId = organisationId,
                            LocationTypeCategory = LocationTypeCategory.NotSet,
                            Name = "Bristol-Service-5-Location",
                            Description = "",
                            Longitude = 51.63123,
                            Latitude = -1.66519,
                            GeoPoint = new Point(-1.66519, 51.63123) { SRID = GeoPoint.WGS84 },
                            Address1 = "7A Boyce's Ave, Clifton",
                            City = "Bristol",
                            PostCode = "BS8 4AA",
                            Country = "England",
                            StateProvince = "",
                            LocationType= LocationType.Postal,
                            Contacts = new List<Contact>
                            {
                                new Contact
                                {
                                    Title = "Mr",
                                    Name = "John Smith",
                                    Telephone = "01827 64328",
                                    TextPhone = "01827 64328",
                                    Url = "https://www.gov.uk",
                                    Email = "help@gov.uk"
                                }
                            }
                        }
                    }
                },
                Taxonomies = new List<Taxonomy>
                {
                    new Taxonomy
                    {
                        Name = "swimming",
                        TaxonomyType = TaxonomyType.ServiceCategory,
                        ParentId = 4
                    }
                }
            }
        };
    }

    public static string CreateBearerToken(string role, string? bearerTokenSigningKey)
    {
        Debug.Assert(bearerTokenSigningKey != null, nameof(bearerTokenSigningKey) + " != null");
        var claims = new List<Claim> { new Claim("role", role) };
        var identity = new ClaimsIdentity(claims, "Test");
        var user = new ClaimsPrincipal(identity);

        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(bearerTokenSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: user.Claims,
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}