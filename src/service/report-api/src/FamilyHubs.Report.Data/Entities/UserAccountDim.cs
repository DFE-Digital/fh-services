namespace FamilyHubs.Report.Data.Entities;

public class UserAccountDim
{
    public long UserAccountKey { get; init; }

    public long UserAccountId { get; init; }

    public byte UserAccountRoleTypeId { get; init; }

    public string UserAccountRoleTypeName { get; init; } = null!;

    public byte OrganisationTypeId { get; init; }

    public long OrganisationId { get; init; }

    public string OrganisationName { get; init; } = null!;

    public string OrganisationTypeName { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Email { get; init; } = null!;

    public byte Status { get; init; }

    public DateTime Created { get; init; }

    public string CreatedBy { get; init; } = null!;

    public DateTime LastModified { get; init; }

    public string LastModifiedBy { get; init; } = null!;

    public DateTime SysStartTime { get; init; }

    public DateTime SysEndTime { get; init; }
}
