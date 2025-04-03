using FamilyHubs.ServiceDirectory.Web.Mappers;
using FamilyHubs.ServiceDirectory.Web.Models;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services;

public class ServiceMapperTests
{
    [Fact]
    private void ToViewModel()
    {
        var expected = new List<Service>
        {
            new(
                1,
                "ExampleService",
                6.2137273664980679d,
                ["Free"],
                ["ExampleAddress", "ExampleCity", "ExampleStateProvince", "ExamplePostCode"],
                ["A", "B", "C"],
                ["Telephone"],
                "18 to 65 years old"
            ),
            new(
                2,
                "ExampleService2",
                null,
                ["Yes, it costs money to use. Information."],
                ["Available at 2 locations"],
                [],
                ["In Person"],
                null
            )
        };

        var result = ServiceMapper.ToViewModel(TestData.ExampleServices);
        Assert.Equal(expected, result);
    }
}
