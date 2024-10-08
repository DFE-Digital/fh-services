namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Models;

public class ServiceSearchRequest
{
    public string SearchPostcode { get; set; } = string.Empty;

    public int SearchRadiusMiles { get; set;}

    public int UserId { get; set;}

    public int HttpResponseCode { get; set;}

    public DateTime RequestTimestamp { get; set;}

    public DateTime ResponseTimestamp { get; set;}

    public string? CorrelationId { get; set;}

    public int SearchTriggerEventId { get; set;}

    public int ServiceSearchTypeId { get; set;}

    public List<ServiceSearchResults> ServiceSearchResults { get; set;} = [];

}