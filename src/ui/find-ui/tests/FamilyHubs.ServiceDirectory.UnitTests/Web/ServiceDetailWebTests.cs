using System.Web;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Web;

[Collection("WebTests")]
public class ServiceDetailWebTests : BaseWebTest
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();

    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_serviceDirectoryClient);
    }

    [Fact]
    public async Task ServiceDetail()
    {
        _serviceDirectoryClient.GetServiceById(Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(TestData.ExampleServices.First());

        // Act
        var returnUri = "/ServiceFilter?postcode=W1A%201AA&adminarea=E09000033&latitude=51.518562&longitude=-0.143799&frompostcodesearch=true";
        var page = await Navigate($"/ServiceDetail?serviceId=1&fromUrl={HttpUtility.UrlEncode(returnUri)}");

        // Assert
        var backLink = page.QuerySelector(".govuk-back-link");
        Assert.IsAssignableFrom<IHtmlAnchorElement>(backLink);
        var anchor = (IHtmlAnchorElement?) backLink;
        Assert.Equal($"{BaseUrl}{returnUri}", anchor?.Href);

        var heading = page.QuerySelector("h1");
        Assert.IsAssignableFrom<IHtmlHeadingElement>(heading);
        Assert.Equal(TestData.ExampleServices[0].Name, heading.GetInnerText());
    }
}
