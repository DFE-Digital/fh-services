using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Web.Filtering.Filters;

namespace FamilyHubs.ServiceDirectory.UnitTests.Filtering;

public class CategoryFilterTests
{
    private readonly ServicesParams _servicesParams = new("", 0, 0);

    [Fact]
    private void WhenCategoryFilterHasNoCriteria_Properties_AreNull()
    {
        var filter = new CategoryFilter(TestData.TaxonomyDtos);
        filter.AddFilterCriteria([], _servicesParams);

        Assert.Equal([], _servicesParams.TaxonomyIds);
    }

    [Fact]
    private void WhenCategoryFilterHasAllCriteria_Properties_AreCorrect()
    {
        var filter = new CategoryFilter(TestData.TaxonomyDtos);
        filter.AddFilterCriteria(filter.SubFilters.Select(x => x.Aspects.First()), _servicesParams);

        Assert.Equal(["7", "8", "9", "10", "11", "12"], _servicesParams.TaxonomyIds);
    }
}
