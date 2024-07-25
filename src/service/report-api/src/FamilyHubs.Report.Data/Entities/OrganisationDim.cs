namespace FamilyHubs.Report.Data.Entities;

public class OrganisationDim
{
    public long OrganisationKey { get; init; }

    public byte OrganisationTypeId { get; init; }

    public string OrganisationTypeName { get; init; } = null!;

    public long OrganisationId { get; init; }

    public string OrganisationName { get; init; } = null!;

    public DateTime Created { get; init; }

    public string CreatedBy { get; init; } = null!;

    public DateTime Modified { get; init; }

    public string ModifiedBy { get; init; } = null!;
}
