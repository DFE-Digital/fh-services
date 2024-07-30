namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;

public class ConnectionRequestsRequest
{
    public DateTime? Date { get; private set; }
    public int? AmountOfDays { get; private set; }

    public ConnectionRequestsRequest(DateTime? date, int? amountOfDays)
    {
        Date = date;
        AmountOfDays = amountOfDays;
    }
}
