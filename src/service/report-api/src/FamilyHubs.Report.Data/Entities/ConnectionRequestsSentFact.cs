using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyHubs.Report.Data.Entities;

public class ConnectionRequestsSentFact
{
    public long Id { get; init; }

    public int DateKey { get; init; }
    [NotMapped] public DateDim DateDim { get; init; } = null!;

    public int TimeKey { get; init; }
    [NotMapped] public TimeDim TimeDim { get; init; } = null!;

    public long? OrganisationKey { get; init; }
    [NotMapped] public OrganisationDim? OrganisationDim { get; init; }

    public long? UserAccountKey { get; init; }
    [NotMapped] public UserAccountDim? UserAccountDim { get; init; }

    public long ConnectionRequestsSentMetricsId { get; init; }

    public DateTime RequestTimestamp { get; init; }

    public string RequestCorrelationId { get; init; } = null!;

    public DateTime? ResponseTimestamp { get; init; }

    public short? HttpResponseCode { get; init; }

    public long? ConnectionRequestId { get; init; }

    public string? ConnectionRequestReferenceCode { get; init; }

    public DateTime Created { get; init; }

    public long CreatedBy { get; init; }

    public DateTime Modified { get; init; }

    public long ModifiedBy { get; init; }
}