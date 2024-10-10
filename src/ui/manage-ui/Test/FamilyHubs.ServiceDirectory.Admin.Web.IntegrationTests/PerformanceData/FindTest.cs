using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Reports.WeeklyBreakdown;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.IntegrationTests.PerformanceData;

public class FindTest : BaseTest
{
    private readonly IReportingClient _reportingClient = Substitute.For<IReportingClient>();
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();

    private const ServiceType TestServiceType = ServiceType.FamilyExperience;

    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_reportingClient);
        services.AddSingleton(_serviceDirectoryClient);
    }

    [Fact]
    public async Task As_DfeAdmin_Then_Find_Data_Should_Be_Correct()
    {
        var breakdown = new WeeklyReportBreakdown
        {
            WeeklyReports = new[]
            {
                new WeeklyReport { Date = "Week 1", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 2", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 3", SearchCount = Random.Next(0, 1000000) },
                new WeeklyReport { Date = "Week 4", SearchCount = Random.Next(0, 1000000) }
            },
            TotalSearchCount = Random.Next(0, 1000000)
        };

        _reportingClient
            .GetServicesSearches4WeekBreakdown(TestServiceType, null, Arg.Any<CancellationToken>())
            .Returns(breakdown);

        var searchCount = Random.Next(0, 1000000);
        _reportingClient
            .GetServicesSearchesTotal(TestServiceType, null, Arg.Any<CancellationToken>())
            .Returns(searchCount);
        var recentSearchCount = Random.Next(0, 1000000);
        _reportingClient
            .GetServicesSearchesPast7Days(TestServiceType, null, Arg.Any<CancellationToken>())
            .Returns(recentSearchCount);
        

        // Login
        await Login(StubUser.DfeAdmin);

        // Act
        var page = await Navigate("performance-data/Find");

        var searches = page.QuerySelector("[data-testid=\"searches\"] td")?.TextContent;
        Assert.Equal(searchCount.ToString(), searches);

        var searchesLast7Days = page.QuerySelector("[data-testid=\"recent-searches\"] td")?.TextContent;
        Assert.Equal(recentSearchCount.ToString(), searchesLast7Days);

        foreach (var (report, idx) in breakdown.WeeklyReports.Reverse().Select((report, idx) => (report, idx)))
        {
            var heading = page.QuerySelector($"[data-testid=\"searches-week{idx + 1}\"] th")?.TextContent;
            Assert.Equal(report.Date, heading);

            var text = page.QuerySelector($"[data-testid=\"searches-week{idx + 1}\"] td")?.TextContent;
            Assert.Equal(report.SearchCount.ToString(), text);
        }

        var total = page.QuerySelector($"[data-testid=\"searches-total\"] td")?.TextContent;
        Assert.Equal(breakdown.TotalSearchCount.ToString(), total);
    }

    [Fact]
    public async Task As_LaManager_Then_Find_Data_Should_Be_Correct()
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

        _reportingClient
            .GetServicesSearches4WeekBreakdown(TestServiceType, Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(breakdown);

        var searchCount = Random.Next(0, 1000000);
        _reportingClient
            .GetServicesSearchesTotal(TestServiceType, Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(searchCount);

        var recentSearchCount = Random.Next(0, 1000000);

        _reportingClient
            .GetServicesSearchesPast7Days(TestServiceType, Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(recentSearchCount);

        // Login
        await Login(StubUser.LaAdmin);

        // Act
        var page = await Navigate("performance-data/Find");

        var searches = page.QuerySelector("[data-testid=\"searches\"] td")?.TextContent;
        Assert.Equal(searchCount.ToString(), searches);

        var searchesLast7Days = page.QuerySelector("[data-testid=\"recent-searches\"] td")?.TextContent;
        Assert.Equal(recentSearchCount.ToString(), searchesLast7Days);

        foreach (var (report, idx) in breakdown.WeeklyReports.Reverse().Select((report, idx) => (report, idx)))
        {
            var heading = page.QuerySelector($"[data-testid=\"searches-week{idx + 1}\"] th")?.TextContent;
            Assert.Equal(report.Date, heading);

            var text = page.QuerySelector($"[data-testid=\"searches-week{idx + 1}\"] td")?.TextContent;
            Assert.Equal(report.SearchCount.ToString(), text);
        }

        var total = page.QuerySelector($"[data-testid=\"searches-total\"] td")?.TextContent;
        Assert.Equal(breakdown.TotalSearchCount.ToString(), total);
    }
}
