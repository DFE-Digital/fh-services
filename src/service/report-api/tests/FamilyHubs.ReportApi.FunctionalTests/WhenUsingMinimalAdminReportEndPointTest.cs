using System.Net;
using System.Text.Json;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using FamilyHubs.SharedKernel.Reports.WeeklyBreakdown;
using Xunit;

namespace FamilyHubs.ReportApi.FunctionalTests;

public class WhenUsingMinimalAdminReportEndPointTest : BaseEndPointTest, IAsyncLifetime
{
    public async Task InitializeAsync() => await InitialiseDatabase();

    [Theory]
    [InlineData("2024-01-07", "7")]
    [InlineData("2024-01-14", "7")]
    [InlineData("2024-02-11", "7")]
    [InlineData("2024-02-18", "7")]
    [InlineData("2024-03-31", "0")]
    public async Task Then_ServiceSearches_PastSevenDays_Admin_ShouldBeCorrect(string dateStr, string expected)
    {
        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/service-searches-past-7-days?date={dateStr}&serviceTypeId={ServiceTypeId}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task Then_ServiceSearches_FourWeekBreakdown_Admin_January_ShouldBeCorrect()
    {
        WeeklyReportBreakdown wRbDto = new()
        {
            WeeklyReports = new[]
            {
                new WeeklyReport { Date = "1 January to 7 January", SearchCount = 7 },
                new WeeklyReport { Date = "8 January to 14 January", SearchCount = 7 },
                new WeeklyReport { Date = "15 January to 21 January", SearchCount = 7 },
                new WeeklyReport { Date = "22 January to 28 January", SearchCount = 7 }
            },
            TotalSearchCount = 28
        };

        string expectedJsonStr = JsonSerializer.Serialize(wRbDto, JsonOptions);

        const string dateStr = "2024-01-31";

        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/service-searches-4-week-breakdown?date={dateStr}&serviceTypeId={ServiceTypeId}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expectedJsonStr, result);
    }

    [Fact]
    public async Task Then_ServiceSearches_FourWeekBreakdown_Admin_February_ShouldBeCorrect()
    {
        WeeklyReportBreakdown wRbDto = new()
        {
            WeeklyReports = new[]
            {
                new WeeklyReport { Date = "29 January to 4 February", SearchCount = 7 },
                new WeeklyReport { Date = "5 February to 11 February", SearchCount = 7 },
                new WeeklyReport { Date = "12 February to 18 February", SearchCount = 7 },
                new WeeklyReport { Date = "19 February to 25 February", SearchCount = 7 }
            },
            TotalSearchCount = 28
        };

        string expectedJsonStr = JsonSerializer.Serialize(wRbDto, JsonOptions);

        const string dateStr = "2024-02-29";

        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/service-searches-4-week-breakdown?date={dateStr}&serviceTypeId={ServiceTypeId}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expectedJsonStr, result);
    }

    [Fact]
    public async Task Then_ServiceSearches_Total_Admin_ShouldBeCorrect()
    {
        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/service-searches-total?serviceTypeId={ServiceTypeId}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal("60", result);
    }

    [Theory]
    [InlineData("2024-01-07", 7)]
    [InlineData("2024-01-14", 7)]
    [InlineData("2024-02-11", 7)]
    [InlineData("2024-02-18", 7)]
    [InlineData("2024-03-31", 0)]
    public async Task Then_ConnectionRequests_PastSevenDays_Admin_ShouldBeCorrect(string dateStr, int requestsMade)
    {
        ConnectionRequests cR = new()
        {
            Made = requestsMade
        };

        string expectedJsonStr = JsonSerializer.Serialize(cR, JsonOptions);

        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/connection-requests-past-7-days?date={dateStr}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expectedJsonStr, result);
    }

    [Fact]
    public async Task Then_ConnectionRequests_FourWeekBreakdown_Admin_January_ShouldBeCorrect()
    {
        ConnectionRequestsBreakdown cRb = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 28
            },
            WeeklyReports = new[]
            {
                new ConnectionRequestsDated
                {
                    Date = "1 January to 7 January",
                    Made = 7
                },
                new ConnectionRequestsDated
                {
                    Date = "8 January to 14 January",
                    Made = 7
                },
                new ConnectionRequestsDated
                {
                    Date = "15 January to 21 January",
                    Made = 7
                },
                new ConnectionRequestsDated
                {
                    Date = "22 January to 28 January",
                    Made = 7
                }
            }
        };

        string expectedJsonStr = JsonSerializer.Serialize(cRb, JsonOptions);

        const string dateStr = "2024-01-31";

        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/connection-requests-4-week-breakdown?date={dateStr}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expectedJsonStr, result);
    }

    [Fact]
    public async Task Then_ConnectionRequests_FourWeekBreakdown_Admin_February_ShouldBeCorrect()
    {
        ConnectionRequestsBreakdown cRb = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 28
            },
            WeeklyReports = new[]
            {
                new ConnectionRequestsDated
                {
                    Date = "29 January to 4 February",
                    Made = 7
                },
                new ConnectionRequestsDated
                {
                    Date = "5 February to 11 February",
                    Made = 7
                },
                new ConnectionRequestsDated
                {
                    Date = "12 February to 18 February",
                    Made = 7
                },
                new ConnectionRequestsDated
                {
                    Date = "19 February to 25 February",
                    Made = 7
                }
            }
        };

        string expectedJsonStr = JsonSerializer.Serialize(cRb, JsonOptions);

        const string dateStr = "2024-02-29";

        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/connection-requests-4-week-breakdown?date={dateStr}",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expectedJsonStr, result);
    }

    [Fact]
    public async Task Then_ConnectionRequests_Total_Admin_ShouldBeCorrect()
    {
        ConnectionRequests cR = new()
        {
            Made = 60
        };

        string expectedJsonStr = JsonSerializer.Serialize(cR, JsonOptions);

        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/connection-requests-total",
                RoleTypes.DfeAdmin));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        string result = await httpResponseMessage.Content.ReadAsStringAsync();
        Assert.Equal(expectedJsonStr, result);
    }

    [Theory]
    [InlineData(RoleTypes.LaManager)]
    [InlineData(RoleTypes.VcsManager)]
    [InlineData(RoleTypes.LaProfessional)]
    [InlineData(RoleTypes.VcsProfessional)]
    [InlineData(RoleTypes.VcsDualRole)]
    [InlineData(RoleTypes.LaDualRole)]
    [InlineData(RoleTypes.ServiceAccount)]
    public async Task Then_WrongRole_Will_BeForbidden(string roleType)
    {
        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/service-searches-total?serviceTypeId={ServiceTypeId}",
                roleType));

        Assert.Equal(HttpStatusCode.Forbidden, httpResponseMessage.StatusCode);
    }

    [Theory]
    [InlineData(RoleTypes.DfeAdmin)]
    public async Task Then_CorrectRole_Will_BeOK(string roleType)
    {
        HttpResponseMessage httpResponseMessage =
            await Client.SendAsync(CreateHttpGetRequest(
                $"report/service-searches-total?serviceTypeId={ServiceTypeId}",
                roleType));

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
