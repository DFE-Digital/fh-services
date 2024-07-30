namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class LaConnectionRequestsBreakdownRequest : ConnectionRequestsBreakdownRequest
{
    public long? LaOrgId { get; private set; }

    public LaConnectionRequestsBreakdownRequest(DateTime? date, long? laOrgId) : base(date)
    {
        LaOrgId = laOrgId;
    }
}
