namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class OrgConnectionRequestsBreakdownRequest : ConnectionRequestsBreakdownRequest
{
    public long? OrgId { get; private set; }

    public OrgConnectionRequestsBreakdownRequest(DateTime? date, long? orgId) : base(date)
    {
        OrgId = orgId;
    }
}
