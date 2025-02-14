using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.OpenReferral.Function.Models;

public class OrganizationDto  : BaseHsdsDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("website")]
    public string? Website { get; init; }

    [JsonPropertyName("year_incorporated")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public short? YearIncorporated { get; init; }

    [JsonPropertyName("legal_status")]
    public string? LegalStatus { get; init; }

    [JsonPropertyName("logo")]
    public string? Logo { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    [JsonPropertyName("parent_organization_id")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public Guid? ParentOrganizationId { get; init; }

    [JsonPropertyName("funding")]
    public List<FundingDto> Funding { get; init; } = new();

    [JsonPropertyName("contacts")]
    public List<ContactDto> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    public List<PhoneDto> Phones { get; init; } = new();

    [JsonPropertyName("locations")]
    public List<LocationDto> Locations { get; set; } = new();

    [JsonPropertyName("programs")]
    public List<ProgramDto> Programs { get; init; } = new();

    [JsonPropertyName("organization_identifiers")]
    public List<OrganizationIdentifierDto> OrganizationIdentifiers { get; init; } = new();
}