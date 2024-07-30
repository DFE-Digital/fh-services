namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class LaConnectionRequestsTotalRequest
{
    public long? LaOrgId { get; private set; }

    public LaConnectionRequestsTotalRequest(long? laOrgId)
    {
        LaOrgId = laOrgId;
    }
}
