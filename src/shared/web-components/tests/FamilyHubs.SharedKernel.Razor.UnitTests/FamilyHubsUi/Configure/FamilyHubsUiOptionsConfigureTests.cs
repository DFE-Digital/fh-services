using Microsoft.Extensions.Configuration;
using System.Text.Json;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options.Configure;
using FamilyHubs.SharedKernel.Razor.UnitTests.FamilyHubsUi.Configure.Helpers;
using FluentAssertions;
using NSubstitute;

namespace FamilyHubs.SharedKernel.Razor.UnitTests.FamilyHubsUi.Configure;

public class FamilyHubsUiOptionsConfigureTests : FamilyHubsUiOptionsTestBase
{
    private readonly IConfiguration _configuration;
    private readonly FamilyHubsUiOptionsConfigure _familyHubsUiOptionsConfigure;

    public FamilyHubsUiOptionsConfigureTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _familyHubsUiOptionsConfigure = new FamilyHubsUiOptionsConfigure(_configuration);
    }

    [Fact]
    public void Configure_NoMutationTest()
    {
        var expectedFamilyHubsUiOptions = DeepClone(FamilyHubsUiOptions);

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        FamilyHubsUiOptions.Should().BeEquivalentTo(expectedFamilyHubsUiOptions);
    }

    [Theory]
    [InlineData("/lower", "lower")]
    [InlineData("/mix", "MiX")]
    [InlineData("/upper", "UPPER")]
    [InlineData("/multi-word", "Multi word")]
    [InlineData("/-x--y-z", " X  y z")]
    public void Configure_GeneratedHeaderNavigationUrlTests(string expectedUrl, string text)
    {
        var link = FamilyHubsUiOptions.Header.NavigationLinks[0];
        link.Url = null;
        link.Text = text;

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        var actualLink = FamilyHubsUiOptions.Header.NavigationLinks.FirstOrDefault();
        Assert.NotNull(actualLink);
        Assert.Equal(expectedUrl, actualLink.Url);
    }

    [Theory]
    [InlineData("/lower", "lower")]
    [InlineData("/mix", "MiX")]
    [InlineData("/upper", "UPPER")]
    [InlineData("/multi-word", "Multi word")]
    [InlineData("/-x--y-z", " X  y z")]
    public void Configure_GeneratedHeaderActionUrlTests(string expectedUrl, string text)
    {
        var link = FamilyHubsUiOptions.Header.ActionLinks[0];
        link.Url = null;
        link.Text = text;

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        var actualLink = FamilyHubsUiOptions.Header.ActionLinks.FirstOrDefault();
        Assert.NotNull(actualLink);
        Assert.Equal(expectedUrl, actualLink.Url);
    }

    [Theory]
    [InlineData("/lower", "lower")]
    [InlineData("/mix", "MiX")]
    [InlineData("/upper", "UPPER")]
    [InlineData("/multi-word", "Multi word")]
    [InlineData("/-x--y-z", " X  y z")]
    public void Configure_GeneratedFooterUrlTests(string expectedUrl, string text)
    {
        var link = FamilyHubsUiOptions.Footer.Links[0];
        link.Url = null;
        link.Text = text;

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        var actualLink = FamilyHubsUiOptions.Footer.Links.FirstOrDefault();
        Assert.NotNull(actualLink);
        Assert.Equal(expectedUrl, actualLink.Url);
    }

    [Fact]
    public void Configure_HeaderNavigationConfigUrlTest()
    {
        const string configKey = "A:B";
        const string configValue = "configValue";
        _configuration[configKey].Returns(configValue);

        var link = FamilyHubsUiOptions.Header.NavigationLinks[0];
        link.Url = null;
        link.ConfigUrl = configKey;

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        var actualLink = FamilyHubsUiOptions.Header.NavigationLinks.FirstOrDefault();
        Assert.NotNull(actualLink);
        Assert.Equal(configValue, actualLink.Url);
    }

    [Fact]
    public void Configure_HeaderActionConfigUrlTest()
    {
        const string configKey = "A:B";
        const string configValue = "configValue";
        _configuration[configKey].Returns(configValue);

        var link = FamilyHubsUiOptions.Header.ActionLinks[0];
        link.Url = null;
        link.ConfigUrl = configKey;

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        var actualLink = FamilyHubsUiOptions.Header.ActionLinks.FirstOrDefault();
        Assert.NotNull(actualLink);
        Assert.Equal(configValue, actualLink.Url);
    }

    [Fact]
    public void Configure_FooterConfigUrlTest()
    {
        const string configKey = "A:B";
        const string configValue = "configValue";
        _configuration[configKey].Returns(configValue);

        var link = FamilyHubsUiOptions.Footer.Links[0];
        link.Url = null;
        link.ConfigUrl = configKey;

        // act
        _familyHubsUiOptionsConfigure.Configure(FamilyHubsUiOptions);

        var actualLink = FamilyHubsUiOptions.Footer.Links.FirstOrDefault();
        Assert.NotNull(actualLink);
        Assert.Equal(configValue, actualLink.Url);
    }

    private static T DeepClone<T>(T obj)
    {
        string json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json)!;
    }
}