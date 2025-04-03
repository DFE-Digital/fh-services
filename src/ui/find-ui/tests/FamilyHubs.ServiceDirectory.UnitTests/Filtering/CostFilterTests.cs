using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Web.Filtering.Filters;

namespace FamilyHubs.ServiceDirectory.UnitTests.Filtering;

public class CostFilterTests
{
    private readonly ServicesParams _servicesParams = new("", 0, 0);

    [Fact]
    private void WhenCostFilterHasNoCriteria_IsPaidFor_AreNull()
    {
        var filter = new CostFilter();
        filter.AddFilterCriteria([], _servicesParams);

        Assert.Null(_servicesParams.IsPaidFor);
    }

    [Fact]
    private void WhenCostFilterHasFreeCriteria_IsPaidFor_IsFalse()
    {
        var filter = new CostFilter();
        filter.AddFilterCriteria([filter.Aspects.First()], _servicesParams);

        Assert.False(_servicesParams.IsPaidFor);
    }
}
