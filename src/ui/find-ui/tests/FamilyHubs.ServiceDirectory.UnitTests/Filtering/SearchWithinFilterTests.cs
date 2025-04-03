using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Web.Filtering.Filters;

namespace FamilyHubs.ServiceDirectory.UnitTests.Filtering;

public class SearchWithinFilterTests
{
    private readonly ServicesParams _servicesParams = new("", 0, 0);

    [Fact]
    private void WhenSearchWithinFilterHasNoCriteria_Properties_AreNull()
    {
        var filter = new SearchWithinFilter();
        filter.AddFilterCriteria([], _servicesParams);

        Assert.Null(_servicesParams.MaximumProximityMeters);
    }

    [Fact]
    private void WhenSearchWithinFilterHasOneMileCriteria_Properties_AreCorrect()
    {
        var filter = new SearchWithinFilter();
        filter.AddFilterCriteria([filter.Aspects.First()], _servicesParams);

        Assert.Equal(1609, _servicesParams.MaximumProximityMeters);
    }
}
