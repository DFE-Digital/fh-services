using System.ComponentModel;
using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.OpenReferral.Function.Models;

[Description("This DTO and relational Dtos are based wholy from the International V3.0 specification of the Open Referral API.")]
public class ServiceDto : BaseHsdsDto
{
    [JsonPropertyName("organization")]
    public OrganizationDto? Organization { get; init; }
    
    [JsonPropertyName("program")]
    public ProgramDto? Program { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("interpretation_services")]
    public string? InterpretationServices { get; init; }

    [JsonPropertyName("application_process")]
    public string? ApplicationProcess { get; init; }

    [JsonPropertyName("fees_description")]
    public string? FeesDescription { get; init; }

    [JsonPropertyName("accreditations")]
    public string? Accreditations { get; init; }

    [JsonPropertyName("eligibility_description")]
    public string? EligibilityDescription { get; init; }

    [JsonPropertyName("minimum_age")]
    public byte? MinimumAge { get; init; }

    [JsonPropertyName("maximum_age")]
    public byte? MaximumAge { get; init; }

    [JsonPropertyName("assured_date")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public DateTime? AssuredDate { get; init; }

    [JsonPropertyName("assurer_email")]
    public string? AssurerEmail { get; init; }

    [JsonPropertyName("alert")]
    public string? Alert { get; init; }

    [JsonPropertyName("last_modified")]
    public DateTime? LastModified { get; init; }

    [JsonPropertyName("phones")]
    public List<PhoneDto> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public List<ScheduleDto> Schedules { get; init; } = new();

    [JsonPropertyName("service_areas")]
    public List<ServiceAreaDto> ServiceAreas { get; init; } = new();

    [JsonPropertyName("service_at_locations")]
    public List<ServiceAtLocationDto> ServiceAtLocations { get; init; } = new();

    [JsonPropertyName("languages")]
    public List<LanguageDto> Languages { get; init; } = new();

    [JsonPropertyName("funding")]
    public List<FundingDto> Funding { get; init; } = new();

    [JsonPropertyName("cost_options")]
    public List<CostOptionDto> CostOptions { get; init; } = new();

    [JsonPropertyName("required_documents")]
    public List<RequiredDocumentDto> RequiredDocuments { get; init; } = new();

    [JsonPropertyName("contacts")]
    public List<ContactDto> Contacts { get; init; } = new();
}