using System.Text.Json;
using FamilyHubs.Report.Api.AcceptanceTests.Builders.Http;
using FamilyHubs.Report.Api.AcceptanceTests.Configuration;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using FluentAssertions;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;

public class ConnectionRequestsOverallSteps
{
    private readonly string _baseUrl;
    public HttpResponseMessage LastResponse { get; private set; }

    public ConnectionRequestsOverallSteps()
    {
        LastResponse = new HttpResponseMessage();
        var config = ConfigAccessor.GetApplicationConfiguration();
        _baseUrl = config.BaseUrl;
    }
    
    #region When

    public async Task SendAValidRequestForTheTotalConnectionRequests(string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            "report/connection-requests-total",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForConnectionRequestsInThePast7days(string date, string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/connection-requests-past-7-days?date={date}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForConnectionRequestsInThePast7daysWithoutDateParameter(string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            "report/connection-requests-past-7-days",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForConnectionRequestsFourWeekBreakdown(string date, string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            $"report/connection-requests-4-week-breakdown?date={date}",
            bearerToken,
            null,
            null);
    }

    public async Task SendAValidRequestForConnectionRequestsFourWeekBreakdownWithAMissingDateParameter(
        string bearerToken)
    {
        LastResponse = await HttpRequestFactory.Get(_baseUrl,
            "report/connection-requests-4-week-breakdown",
            bearerToken,
            null,
            null);
    }
    
    #endregion When
    
    # region Then
    public async Task VerifySevenDaysConnectionRequestResponseBody(HttpResponseMessage lastResponse)
    {
        string jsonString = await lastResponse.Content.ReadAsStringAsync();

        ConnectionRequests? sevenDaysResponse =
            JsonSerializer.Deserialize<ConnectionRequests>(jsonString);

        // Implicitly verifying the keys exist and explicitly verify they are set to a number
        sevenDaysResponse?.Made.Should()
            .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests made.");

        // Below assertions to be commented in when tickets for RAPI Accepted and Decline are completed

        sevenDaysResponse?.Accepted.Should()
            .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests accepted.");
        /*sevenDaysResponse.Declined.Should()
            .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests declined.");*/
    }

    public async Task VerifyTotalsSectionOfTheFourWeekBreakDownResponseBody(HttpResponseMessage response)
    {
        string jsonString = await response.Content.ReadAsStringAsync();

        ConnectionRequestsBreakdown? fourWeekResponse =
            JsonSerializer.Deserialize<ConnectionRequestsBreakdown>(jsonString,SharedSteps.JsonOptions);

        // Verify the 'totals' section is not null
        fourWeekResponse?.Totals.Should().NotBeNull("because the response should contain a 'totals' section");

        // Implicitly verifying the keys exist and explicitly verify they are set to a number
        fourWeekResponse?.Totals.Made.Should()
            .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests made.");

        // Below assertions to be commented in when tickets for RAPI Accepted and Decline are completed

        fourWeekResponse?.Totals.Accepted.Should()
            .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests accepted.");
        /*fourWeekResponse.Totals.Declined.Should()
            .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests declined.");*/
    }

    public async Task VerifyWeeklyReportsSectionOfTheFourWeekBreakDownResponseBody(HttpResponseMessage lastResponse)
    {
        var jsonString = await lastResponse.Content.ReadAsStringAsync();

        ConnectionRequestsBreakdown? fourWeekResponse =
            JsonSerializer.Deserialize<ConnectionRequestsBreakdown>(jsonString,SharedSteps.JsonOptions);

        // Verify "weeklyReports" section is present
        fourWeekResponse?.WeeklyReports.Should()
            .NotBeNull("because the response should contain a 'weekly reports' section");

        // Verify each report in "weeklyReports" contains "date" ,"made", "accepted" and "declined" keys

        foreach (var report in fourWeekResponse!.WeeklyReports)
        {
            report.Date.Should()
                .NotBeNull("because there should be a valid date set for the week.");

            report.Made.Should()
                .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests made.");

            // Below assertions to be commented in when tickets for RAPI Accepted and Decline are completed

            report.Accepted.Should()
                .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests accepted.");
            /*report.Declined.Should()
                .BeGreaterOrEqualTo(0, "because there should be a value set for connection requests declined.");*/
        }
    }
    #endregion Then
}