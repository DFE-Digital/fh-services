using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;
 
public class RequiredDocumentDto  : BaseHsdsDto
{
    [JsonPropertyName("document")]
    public string? Document { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }
}