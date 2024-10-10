using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using FamilyHubs.SharedKernel.Reports.WeeklyBreakdown;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.IntegrationTests.PerformanceData;

public class ConnectTest : BaseTest
{
    private readonly IReportingClient _reportingClient = Substitute.For<IReportingClient>();
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();

    private const ServiceType TestServiceType = ServiceType.InformationSharing;

    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_reportingClient);
        services.AddSingleton(_serviceDirectoryClient);
    }

    private void SetupClient(
        WeeklyReportBreakdown breakdown, int searchCount, int recentSearchCount,
        ConnectionRequestsBreakdown crBreakdown, ConnectionRequests crMetric, ConnectionRequests recentCrMetric
    ) {
        _reportingClient
            .GetServicesSearches4WeekBreakdown(TestServiceType, Arg.Any<long?>(), Arg.Any<CancellationToken>())
            .Returns(breakdown);
        _reportingClient
            .GetServicesSearchesTotal(TestServiceType, Arg.Any<long?>(), Arg.Any<CancellationToken>())
            .Returns(searchCount);
        _reportingClient
            .GetServicesSearchesPast7Days(TestServiceType, Arg.Any<long?>(), Arg.Any<CancellationToken>())
            .Returns(recentSearchCount);

        _reportingClient
            .GetConnectionRequests4WeekBreakdown(TestServiceType, Arg.Any<long?>(), Arg.Any<CancellationToken>())
            .Returns(crBreakdown);
        _reportingClient
            .GetConnectionRequestsTotal(TestServiceType, Arg.Any<long?>(), Arg.Any<CancellationToken>())
            .Returns(crMetric);
        _reportingClient
            .GetConnectionRequestsPast7Days(TestServiceType, Arg.Any<long?>(), Arg.Any<CancellationToken>())
            .Returns(recentCrMetric);
    }

    private ConnectionRequests GenCrMetric() => GenCrDated("");

    private ConnectionRequestsDated GenCrDated(string date) =>
        new()
        {
            Date = date, Accepted = Random.Next(0, 1000000), Declined = Random.Next(0, 1000000), Made = Random.Next(0, 1000000)
        };

    [Fact]
    public async Task As_DfeAdmin_Then_Connect_Data_Should_Be_Correct()
    {
        var breakdown = new WeeklyReportBreakdown
        {
            WeeklyReports =
            [
                new WeeklyReport { Date = "Week 1", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 2", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 3", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 4", SearchCount = Random.Next(0, 1000000) }
            ],
            TotalSearchCount = Random.Next(0, 1000000)
        };
        var searchCount = Random.Next(0, 1000000);
        var recentSearchCount = Random.Next(0, 1000000);

        var crBreakdown = new ConnectionRequestsBreakdown
        {
            WeeklyReports = new[]
            {
                GenCrDated("Week 1"),
                GenCrDated("Week 2"),
                GenCrDated("Week 3"),
                GenCrDated("Week 4")
            },
            Totals = GenCrMetric()
        };
        var crMetric = GenCrMetric();
        var recentCrMetric = GenCrMetric();

        SetupClient(breakdown, searchCount, recentSearchCount, crBreakdown, crMetric, recentCrMetric);

        // Login
        await Login(StubUser.DfeAdmin);

        // Act
        var page = await Navigate("performance-data/Connect");

        var searches = page.QuerySelector("[data-testid=\"overall-searches\"] td")?.TextContent;
        Assert.Equal(searchCount.ToString(), searches);

        var searchesLast7Days = page.QuerySelector("[data-testid=\"recent-searches\"] td")?.TextContent;
        Assert.Equal(recentSearchCount.ToString(), searchesLast7Days);

        foreach (var (report, idx) in breakdown.WeeklyReports.Reverse().Select((report, idx) => (report, idx)))
        {
            var heading = page.QuerySelector($"[data-testid=\"breakdown-week{idx + 1}\"] th")?.TextContent;
            Assert.Equal(report.Date, heading);

            var text = page.QuerySelector($"[data-testid=\"breakdown-week{idx + 1}\"] td")?.TextContent;
            Assert.Equal(report.SearchCount.ToString(), text);
        }

        var total = page.QuerySelector($"[data-testid=\"breakdown-total\"] td")?.TextContent;
        Assert.Equal(breakdown.TotalSearchCount.ToString(), total);
    }

    [Fact]
    public async Task As_LaManager_Then_Connect_Data_Should_Be_Correct()
    {
        _serviceDirectoryClient
            .GetOrganisationById(Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(new OrganisationDetailsDto
            {
                Name = "Test Org",
                OrganisationType = OrganisationType.LA,
                Description = "Test Description",
                AdminAreaCode = "D123"
            });

        var breakdown = new WeeklyReportBreakdown
        {
            WeeklyReports =
            [
                new WeeklyReport { Date = "Week 1", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 2", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 3", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 4", SearchCount = Random.Next(0, 1000000) }
            ],
            TotalSearchCount = Random.Next(0, 1000000)
        };
        var searchCount = Random.Next(0, 1000000);
        var recentSearchCount = Random.Next(0, 1000000);

        var crBreakdown = new ConnectionRequestsBreakdown
        {
            WeeklyReports =
            [
                GenCrDated("Week 1"),
                GenCrDated("Week 2"),
                GenCrDated("Week 3"),
                GenCrDated("Week 4")
            ],
            Totals = GenCrMetric()
        };
        var crMetric = GenCrMetric();
        var recentCrMetric = GenCrMetric();

        SetupClient(breakdown, searchCount, recentSearchCount, crBreakdown, crMetric, recentCrMetric);

        // Login
        await Login(StubUser.LaAdmin);

        // Act
        var page = await Navigate("performance-data/Connect");

        var searches = page.QuerySelector("[data-testid=\"overall-searches\"] td")?.TextContent;
        Assert.Equal(searchCount.ToString(), searches);

        var searchesLast7Days = page.QuerySelector("[data-testid=\"recent-searches\"] td")?.TextContent;
        Assert.Equal(recentSearchCount.ToString(), searchesLast7Days);

        foreach (var (report, idx) in breakdown.WeeklyReports.Reverse().Select((report, idx) => (report, idx)))
        {
            var heading = page.QuerySelector($"[data-testid=\"breakdown-week{idx + 1}\"] th")?.TextContent;
            Assert.Equal(report.Date, heading);

            var text = page.QuerySelector($"[data-testid=\"breakdown-week{idx + 1}\"] td")?.TextContent;
            Assert.Equal(report.SearchCount.ToString(), text);
        }

        var total = page.QuerySelector($"[data-testid=\"breakdown-total\"] td")?.TextContent;
        Assert.Equal(breakdown.TotalSearchCount.ToString(), total);
    }
}
