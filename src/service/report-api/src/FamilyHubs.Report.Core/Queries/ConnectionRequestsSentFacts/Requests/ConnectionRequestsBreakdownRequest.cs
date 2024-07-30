namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class ConnectionRequestsBreakdownRequest
{
    public DateTime? Date { get; private set; }

    public ConnectionRequestsBreakdownRequest(DateTime? date)
    {
        Date = date;
    }
}
