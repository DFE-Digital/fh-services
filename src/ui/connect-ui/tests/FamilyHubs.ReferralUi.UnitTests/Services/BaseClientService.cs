﻿using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.ReferralUi.UnitTests.Services;

public class BaseClientService
{
    /// <summary>
    /// Returns a test County Council Service DTO, no fields are null
    /// </summary>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public static ServiceDto GetTestCountyCouncilServicesDto(long parentId)
    {
        ServiceDto service = new()
        {
            Id = 1,
            OrganisationId = parentId,
            ServiceType = ServiceType.FamilyExperience,
            Name = "Unit Test Service",
            Description = "Unit Test Service Description",
            Accreditations = null,
            AssuredDate = DateTime.Now,
            DeliverableType = DeliverableType.NotSet,
            Status = ServiceStatusType.Active,
            Fees = null,
            CanFamilyChooseDeliveryLocation = false,
            ServiceAreas = new List<ServiceAreaDto>
            {
                new()
                {
                    Id = 2,
                    ServiceAreaName = "National",
                    Uri = "http://statistics.data.gov.uk/id/statistical-geography/K02000001",
                    ServiceId = 1,

                }
            },
            Contacts = new List<ContactDto>
            {
                new()
                {
                    Id = 3,
                    ServiceId = 1,
                    Title = "Mr",
                    Name = "Contact",
                    Email = "Contact@email.com",
                    Telephone = "01827 65777",
                    TextPhone = "01827 65777",
                    Url = "www.google.com"
                }
            },
            Eligibilities = new List<EligibilityDto>
            {
                new()
                {
                    Id = 4,
                    ServiceId = 1,
                    EligibilityType = null,
                    MinimumAge = 1,
                    MaximumAge = 13,
                }
            },
            CostOptions = new List<CostOptionDto>(),
            Languages = new List<LanguageDto>
            {
                new()
                {
                    Id = 5,
                    ServiceId = 1,
                    Name = "English",
                    Code = "en"
                }
            },
            ServiceDeliveries = new List<ServiceDeliveryDto>(),
            Schedules = new List<ScheduleDto>(),
            Locations = new List<LocationDto>
            {
                new()
                {
                    Id = 6,
                    Schedules = new List<ScheduleDto>(),
                    Name = "Shepcoat",
                    Address1 = "77 Sheepcote Lane",
                    Address2 = "Stathe",
                    City = "Tamworth",
                    PostCode = "B77 3JN",
                    StateProvince = "Staffordshire",
                    Country = "England",
                    Latitude = 52.6312,
                    Longitude = -1.66526,
                    LocationTypeCategory = LocationTypeCategory.NotSet,
                    LocationType = LocationType.Postal
                }
            },
            Fundings = new List<FundingDto>(),
            Taxonomies = new List<TaxonomyDto>
            {
                new()
                {
                    Id = 7,
                    Name = "UnitTest bccprimaryservicetype:38",
                    TaxonomyType = TaxonomyType.ServiceCategory,

                }
            }
        };

        return service;
    }
}
