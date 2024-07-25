namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class OrgConnectionRequestsTotalRequest
{
    public long? OrgId { get; private set; }

    public OrgConnectionRequestsTotalRequest(long? orgId)
    {
        OrgId = orgId;
    }
}
