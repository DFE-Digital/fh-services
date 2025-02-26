namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class ThirdPartyService
{
    public int Id { get; init; }
    public required string ServiceId { get; set; }
    public required int ThirdPartyId { get; set; } // FK, Indexed
    
    public required byte StandardVersionId { get; set; } // FK, Indexed
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public required string Document { get; set; } // JSON string document
    public required long Checksum { get; set; } // Checksum of the document
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}