using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Service
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationOrId { get; init; }

    [JsonPropertyName("program_id")]
    [JsonIgnore]
    public Guid ProgramOrId { get; init; }

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
    public virtual List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public virtual List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("service_areas")]
    public virtual List<ServiceArea> ServiceAreas { get; init; } = new();

    [JsonPropertyName("service_at_locations")]
    public virtual List<ServiceAtLocation> ServiceAtLocations { get; init; } = new();

    [JsonPropertyName("languages")]
    public virtual List<Language> Languages { get; init; } = new();

    [JsonPropertyName("organization")]
    public virtual Organization? Organization { get; init; }

    [JsonPropertyName("funding")]
    public virtual List<Funding> Funding { get; init; } = new();

    [JsonPropertyName("cost_options")]
    public virtual List<CostOption> CostOptions { get; init; } = new();

    [JsonPropertyName("program")]
    public virtual Program? Program { get; init; }

    [JsonPropertyName("required_documents")]
    public virtual List<RequiredDocument> RequiredDocuments { get; init; } = new();

    [JsonPropertyName("contacts")]
    public virtual List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}