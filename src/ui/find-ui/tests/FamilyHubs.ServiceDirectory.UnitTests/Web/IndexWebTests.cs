using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Models;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Web;

[Collection("WebTests")]
public class IndexWebTests : BaseWebTest
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();

    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_serviceDirectoryClient);
    }

    [Fact]
    public async Task IndexHasStartNow()
    {
        // Act
        var page = await Navigate("/");

        // Assert
        var startButton = page.QuerySelector("[data-testid=\"start-button\"]");
        Assert.NotNull(startButton);
    }

    [Fact]
    public async Task ServiceFilter_NoResults()
    {
        _serviceDirectoryClient.GetTaxonomies(Arg.Any<CancellationToken>()).Returns(new PaginatedList<TaxonomyDto>());
        _serviceDirectoryClient.GetServices(Arg.Any<ServicesParams>()).Returns(
            (new PaginatedList<ServiceDto>([], 0, 1, 10), null)
        );

        // Act
        var page = await Navigate("/ServiceFilter?postcode=W1A 1AA&adminarea=E09000033&latitude=51.518562&longitude=-0.143799&frompostcodesearch=true");

        // Assert
        var noResultsHeading = page.QuerySelector("[data-testid=\"no-results-heading\"]");
        Assert.NotNull(noResultsHeading);
    }

    [Fact]
    public async Task ServiceFilter_NoResultsFiltered()
    {
        _serviceDirectoryClient.GetTaxonomies(Arg.Any<CancellationToken>()).Returns(new PaginatedList<TaxonomyDto>());
        _serviceDirectoryClient.GetServices(Arg.Any<ServicesParams>()).Returns(
            (new PaginatedList<ServiceDto>([], 0, 1, 10), null)
        );

        // Act
        var page = await Navigate("/ServiceFilter?postcode=W1A 1AA&adminarea=E09000033&latitude=51.518562&longitude=-0.143799");

        // Assert
        var noResultsHeading = page.QuerySelector("[data-testid=\"empty-filters-heading\"]");
        Assert.NotNull(noResultsHeading);
    }

    [Fact]
    public async Task ServiceFilter_Results()
    {
        _serviceDirectoryClient.GetTaxonomies(Arg.Any<CancellationToken>())
            .Returns(new PaginatedList<TaxonomyDto>(TestData.TaxonomyDtos, 1, 1, 10));
        _serviceDirectoryClient.GetServices(Arg.Any<ServicesParams>()).Returns(
            (new PaginatedList<ServiceDto>(TestData.ExampleServices, 25, 2, 10), null)
        );
        
        // Act
        var page = await Navigate("/ServiceFilter?postcode=W1A 1AA&adminarea=E09000033&latitude=51.518562&longitude=-0.143799&search_within=20&activities=7");

        // Assert
        var serviceEntries = page.QuerySelectorAll("[data-testid=\"service-entry\"]");
        Assert.Equal(2, serviceEntries.Length);
    }
}
