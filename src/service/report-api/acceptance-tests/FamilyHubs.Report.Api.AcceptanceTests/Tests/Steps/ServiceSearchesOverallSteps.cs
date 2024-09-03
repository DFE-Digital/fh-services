using FamilyHubs.Report.Api.AcceptanceTests.Builders.Http;
using FamilyHubs.Report.Api.AcceptanceTests.Configuration;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;

public class ServiceSearchesOverallSteps
{
    private readonly string _baseUrl;

    public ServiceSearchesOverallSteps()
    {
        LastResponse = new HttpResponseMessage();
        var config = ConfigAccessor.GetApplicationConfiguration();
        _baseUrl = config.BaseUrl;
    }

    public HttpResponseMessage LastResponse { get; private set; }

    #region When

    public async Task SendAValidRequestForPast7days(string serviceTypeId, string date, string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-past-7-days?date={date}&serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForPast7daysWithoutServiceTypeId(string date, string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-past-7-days?date={date}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForPast7daysWithoutDateParameter(string serviceTypeId, string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-past-7-days?serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearches4WeekBreakdown(string serviceTypeId, string date,
        string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-4-week-breakdown?date={date}&serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearches4WeekBreakdownWithoutServiceTypeId(string date,
        string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-4-week-breakdown?date={date}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearches4WeekBreakdownWithoutDateParameters(string serviceTypeId,
        string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-4-week-breakdown?serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearchesTotal(string serviceTypeId, string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/service-searches-total?serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearchesTotalWithoutServiceTypeIdParameters(string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            "report/service-searches-total",
            bearerToken,
            null,
            null);
    }

    #endregion When
}