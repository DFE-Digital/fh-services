using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Service
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid OrganizationId { get; init; }

    [JsonPropertyName("program_id")]
    [JsonIgnore]
    public Guid ProgramId { get; init; }

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
    public byte MinimumAge { get; init; }

    [JsonPropertyName("maximum_age")]
    public byte MaximumAge { get; init; }

    [JsonPropertyName("assured_date")]
    public DateTime? AssuredDate { get; init; }

    [JsonPropertyName("assurer_email")]
    public string? AssurerEmail { get; init; }

    [JsonPropertyName("alert")]
    public string? Alert { get; init; }

    [JsonPropertyName("last_modified")]
    public DateTime? LastModified { get; init; }

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    [NotMapped]
    public List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("service_areas")]
    [NotMapped]
    public List<ServiceArea> ServiceAreas { get; init; } = new();

    [JsonPropertyName("service_at_locations")]
    [NotMapped]
    public List<ServiceAtLocation> ServiceAtLocations { get; init; } = new();

    [JsonPropertyName("languages")]
    [NotMapped]
    public List<Language> Languages { get; init; } = new();

    [JsonPropertyName("organization")]
    [NotMapped]
    public Organization? Organization { get; init; }

    [JsonPropertyName("funding")]
    [NotMapped]
    public List<Funding> Funding { get; init; } = new();

    [JsonPropertyName("cost_options")]
    [NotMapped]
    public List<CostOption> CostOptions { get; init; } = new();

    [JsonPropertyName("program")]
    [NotMapped]
    public Program? Program { get; init; }

    [JsonPropertyName("required_documents")]
    [NotMapped]
    public List<RequiredDocument> RequiredDocuments { get; init; } = new();

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}