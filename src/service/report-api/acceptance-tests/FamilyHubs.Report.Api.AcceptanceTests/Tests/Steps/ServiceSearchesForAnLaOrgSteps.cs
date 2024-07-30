using FamilyHubs.Report.Api.AcceptanceTests.Builders.Http;
using FamilyHubs.Report.Api.AcceptanceTests.Configuration;
using FamilyHubs.Report.Api.AcceptanceTests.Fixtures;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;

public class ServiceSearchesForAnLaOrgSteps
{
    private readonly string _baseUrl;
    private readonly BearerTokenGenerator _bearerTokenGenerator;
    private string? _bearerToken;

    public ServiceSearchesForAnLaOrgSteps()
    {
        lastResponse = new HttpResponseMessage();
        _bearerTokenGenerator = new BearerTokenGenerator();
        var config = ConfigAccessor.GetApplicationConfiguration();
        _baseUrl = config.BaseUrl;
    }

    public HttpResponseMessage lastResponse { get; private set; }

    #region When

    public async Task SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(int laOrgId, string serviceTypeId,
        string date, string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-past-7-days/organisation/{laOrgId}?date={date}&serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrgWithoutServiceTypeIdParameter(int laOrgId,
        string date, string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-past-7-days/organisation/{laOrgId}?date={date}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrgWithoutDateParameter(int laOrgId,
        string serviceTypeId, string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-past-7-days/organisation/{laOrgId}?serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForFourWeekBreakdownForAnLaOrg(int laOrgId, string serviceTypeId, string date,
        string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-4-week-breakdown/organisation/{laOrgId}?date={date}&serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForFourWeekBreakdownForAnLaOrgWithServiceTypeIdParameterMissing(int laOrgId,
        string date, string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-4-week-breakdown/organisation/{laOrgId}?date={date}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForFourWeekBreakdownForAnLaOrgWithDateParameterMissing(int laOrgId,
        string serviceTypeId, string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-4-week-breakdown/organisation/{laOrgId}?serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearchTotalsForAnLaOrg(int laOrgId, string serviceTypeId,
        string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-total/organisation/{laOrgId}?serviceTypeId={serviceTypeId}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForServiceSearchTotalsForAnLaOrgWithSystemTypeIdParameterMissing(int laOrgId,
        string bearerToken)
    {
        lastResponse = await HttpRequestFactory.Get(
            _baseUrl,
            $"report/service-searches-total/organisation/{laOrgId}",
            bearerToken,
            null,
            null);
    }

    #endregion When
}