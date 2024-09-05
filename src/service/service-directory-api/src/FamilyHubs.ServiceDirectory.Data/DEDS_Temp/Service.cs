using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Service
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [Required]
    [JsonPropertyName("organization_id")]
    public required Guid OrganizationId { get; set; }

    [Required]
    [JsonPropertyName("program_id")]
    public required Guid ProgramId { get; set; }

    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [Required]
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("interpretation_services")]
    public string? InterpretationServices { get; set; }

    [JsonPropertyName("application_process")]
    public string? ApplicationProcess { get; set; }

    [JsonPropertyName("fees_description")]
    public string? FeesDescription { get; set; }

    [JsonPropertyName("accreditations")]
    public string? Accreditations { get; set; }

    [JsonPropertyName("eligibility_description")]
    public string? EligibilityDescription { get; set; }

    [JsonPropertyName("minimum_age")]
    public byte MinimumAge { get; set; }

    [JsonPropertyName("maximum_age")]
    public byte MaximumAge { get; set; }

    [JsonPropertyName("assured_date")]
    public DateTime? AssuredDate { get; set; }

    [JsonPropertyName("assurer_email")]
    public string? AssurerEmail { get; set; }

    [JsonPropertyName("alert")]
    public string? Alert { get; set; }

    [JsonPropertyName("last_modified")]
    public DateTime? LastModified { get; set; }

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; set; } = new();

    [JsonPropertyName("schedules")]
    [NotMapped]
    public List<Schedule> Schedules { get; set; } = new();

    [JsonPropertyName("service_areas")]
    [NotMapped]
    public List<ServiceArea> ServiceAreas { get; set; } = new();

    [JsonPropertyName("service_at_locations")]
    [NotMapped]
    public List<ServiceAtLocation> ServiceAtLocations { get; set; } = new();

    [JsonPropertyName("languages")]
    [NotMapped]
    public List<Language> Languages { get; set; } = new();

    [JsonPropertyName("organization")]
    [NotMapped]
    public Organization? Organization { get; set; }

    [JsonPropertyName("funding")]
    [NotMapped]
    public List<Funding> Funding { get; set; } = new();

    [JsonPropertyName("cost_options")]
    [NotMapped]
    public List<CostOption> CostOptions { get; set; } = new();

    [JsonPropertyName("program")]
    [NotMapped]
    public Program? Program { get; set; }

    [JsonPropertyName("required_documents")]
    [NotMapped]
    public List<RequiredDocument> RequiredDocuments { get; set; } = new();

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; set; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}