namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class OrgConnectionRequestsRequest : ConnectionRequestsRequest
{
    public long? OrgId { get; private set; }

    public OrgConnectionRequestsRequest(long? orgId, DateTime? date, int? amountOfDays) : base(date, amountOfDays)
    {
        OrgId = orgId;
    }
}
