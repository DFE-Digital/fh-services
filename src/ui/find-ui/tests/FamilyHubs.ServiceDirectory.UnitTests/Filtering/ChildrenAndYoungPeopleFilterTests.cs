using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Web.Filtering.Filters;

namespace FamilyHubs.ServiceDirectory.UnitTests.Filtering;

public class ChildrenAndYoungPeopleFilterTests
{
    private readonly ServicesParams _servicesParams = new("", 0, 0);

    [Fact]
    private void WhenCaypFilterHasNoCriteria_Properties_AreNull()
    {
        var filter = new ChildrenAndYoungPeopleFilter();
        filter.AddFilterCriteria([], _servicesParams);

        Assert.Null(_servicesParams.AllChildrenYoungPeople);
        Assert.Null(_servicesParams.GivenAge);
    }

    [Fact]
    private void WhenCaypFilterHasAllAgesCriteria_Properties_AreCorrect()
    {
        var filter = new ChildrenAndYoungPeopleFilter();
        filter.AddFilterCriteria([filter.Aspects.First()], _servicesParams);

        Assert.Equal(true, _servicesParams.AllChildrenYoungPeople);
        Assert.Null(_servicesParams.GivenAge);
    }
    
    [Fact]
    private void WhenCaypFilterHasSpecificAgeCriteria_Properties_AreCorrect()
    {
        var filter = new ChildrenAndYoungPeopleFilter();
        foreach (var filterAspect in filter.Aspects.Skip(1))
        {
            filter.AddFilterCriteria([filterAspect], _servicesParams);

            Assert.NotEqual(true, _servicesParams.AllChildrenYoungPeople);
            Assert.Equal(int.Parse(filterAspect.Id), _servicesParams.GivenAge);
        }
    }
}
