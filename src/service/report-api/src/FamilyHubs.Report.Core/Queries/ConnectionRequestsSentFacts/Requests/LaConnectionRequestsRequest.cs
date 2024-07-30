namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class LaConnectionRequestsRequest : ConnectionRequestsRequest
{
    public long? LaOrgId { get; private set; }

    public LaConnectionRequestsRequest(long? laOrgId, DateTime? date, int? amountOfDays) : base(date, amountOfDays)
    {
        LaOrgId = laOrgId;
    }
}
