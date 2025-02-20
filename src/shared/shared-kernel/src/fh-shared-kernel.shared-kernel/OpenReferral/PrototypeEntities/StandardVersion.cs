using System.Diagnostics.CodeAnalysis;

namespace FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;

public class StandardVersion
{
    public byte Id { get; set; }
    public required string Name { get; set; }
}

[SuppressMessage("ReSharper", "InconsistentNaming")] 
public enum StandardDocumentVersions : byte
{
    OpenReferralUkV2 = 1,
    OpenReferralInternationalV3 = 2
}