using FamilyHubs.ServiceDirectory.Shared.Dto.BaseDto;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.ServiceDirectory.Shared.Dto;

public record ServiceDto : DtoBase
{
    public long OrganisationId { get; set; }
    public required string Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Fees { get; set; }
    public string? Accreditations { get; set; }
    public DateTime? AssuredDate { get; set; }
    public required ServiceType ServiceType { get; set; }
    public ServiceStatusType Status { get; set; }
    public string? InterpretationServices { get; set; }
    public DeliverableType DeliverableType { get; set; }
    public double? Distance { get; set; }
    public bool CanFamilyChooseDeliveryLocation { get; set; }
    public ICollection<ServiceDeliveryDto> ServiceDeliveries { get; set; } = new List<ServiceDeliveryDto>();
    public ICollection<EligibilityDto> Eligibilities { get; set; } = new List<EligibilityDto>();
    public ICollection<FundingDto> Fundings { get; set; } = new List<FundingDto>();
    public ICollection<CostOptionDto> CostOptions { get; set; } = new List<CostOptionDto>();
    public ICollection<LanguageDto> Languages { get; set; } = new List<LanguageDto>();
    public ICollection<ServiceAreaDto> ServiceAreas { get; set; } = new List<ServiceAreaDto>();
    public ICollection<LocationDto> Locations { get; set; } = new List<LocationDto>();
    public ICollection<TaxonomyDto> Taxonomies { get; set; } = new List<TaxonomyDto>();
    public ICollection<ScheduleDto> Schedules { get; set; } = new List<ScheduleDto>();
    public ICollection<ContactDto> Contacts { get; set; } = new List<ContactDto>();
    public ICollection<ServiceAtLocationDto> ServiceAtLocations { get; set; } = new List<ServiceAtLocationDto>();
}