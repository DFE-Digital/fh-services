using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Web.Pages.ServiceFilter;
using FamilyHubs.SharedKernel.Services.Postcode.Interfaces;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Web;

public class WhenUsingServiceFilterModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
    private readonly IPostcodeLookup _postcodeLookup = Substitute.For<IPostcodeLookup>();
    private readonly IPageFilterFactory _pageFilterFactory = Substitute.For<IPageFilterFactory>();
    private readonly ILogger<ServiceFilterModel> _logger = Substitute.For<ILogger<ServiceFilterModel>>();

    private readonly ServiceFilterModel _serviceFilterModel;

    public WhenUsingServiceFilterModel()
    {
        _serviceFilterModel = new ServiceFilterModel(
            _serviceDirectoryClient,
            _postcodeLookup,
            _pageFilterFactory,
            _logger
        );
    }

    [Fact]
    public async Task LoadIndex()
    {
        // Act
        var result = await _serviceFilterModel.OnGet(
            "E1 1EN",
            "N00001",
            51,
            -1,
            1,
            true,
            Guid.NewGuid()
        );

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task Post_PostcodeLookup()
    {
        _postcodeLookup.Get(Arg.Any<string>()).Returns(
            (
                PostcodeError.None,
                new PostcodeInfo("W1A 1AA", 51, -1, new Codes("AD0002", "AC0003"))
            )
        );

        // Act
        var result = await _serviceFilterModel.OnPost(
            "E1 1EN",
            null,
            new FormCollection(new Dictionary<string, StringValues>())
        );

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = (RedirectToPageResult)result;
        Assert.Equal("/ServiceFilter/Index", redirectResult.PageName);
        Assert.Equal("W1A 1AA", redirectResult.RouteValues!["postcode"]);
        Assert.Equal("AC0003", redirectResult.RouteValues["adminarea"]);
        Assert.Equal(51f, redirectResult.RouteValues["latitude"]);
        Assert.Equal(-1f, redirectResult.RouteValues["longitude"]);
        Assert.Equal(true, redirectResult.RouteValues["frompostcodesearch"]);
    }
    
    [Fact]
    public async Task Post_PostcodeLookupFails()
    {
        _postcodeLookup.Get(Arg.Any<string>()).Returns((PostcodeError.InvalidPostcode, null));

        // Act
        var result = await _serviceFilterModel.OnPost(
            "E111",
            null,
            new FormCollection(new Dictionary<string, StringValues>())
        );

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = (RedirectToPageResult)result;
        Assert.Equal("/PostcodeSearch/Index", redirectResult.PageName);
    }
    
    [Fact]
    public async Task PostIndex()
    {
        // Act
        var result = await _serviceFilterModel.OnPost(
            "E1 1EN",
            "N00001",
            new FormCollection(new Dictionary<string, StringValues>
            {
                {"__RequestVerificationToken", "test"},
                {"latitude", "51"},
                {"pageNum", "2"}
            })
        );

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = (RedirectToPageResult)result;
        Assert.Equal("/ServiceFilter/Index", redirectResult.PageName);
    }

    [Fact]
    public async Task PostIndex_RemoveFilter()
    {
        // Act
        var result = await _serviceFilterModel.OnPost(
            "E1 1EN",
            "N00001",
            new FormCollection(new Dictionary<string, StringValues>
            {
                {"__RequestVerificationToken", "test"},
                {"latitude", "51"},
                {"pageNum", "2"},
                {"test-form", new StringValues(["hello", "bye"])},
                {"test-form--option-selected", "new"},
                {"remove", "test-form--hello"}
            })
        );

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = (RedirectToPageResult)result;
        Assert.Equal("/ServiceFilter/Index", redirectResult.PageName);
        Assert.Equal(2, redirectResult.RouteValues!.Count);
        Assert.Equal("bye", redirectResult.RouteValues!["test-form"]);
    }

    [Fact]
    public async Task PostIndex_RemoveAllFilters()
    {
        // Act
        var result = await _serviceFilterModel.OnPost(
            "E1 1EN",
            "N00001",
            new FormCollection(new Dictionary<string, StringValues>
            {
                {"__RequestVerificationToken", "test"},
                {"latitude", "51"},
                {"pageNum", "2"},
                {"test-form", "hello"},
                {"remove", "all"}
            })
        );

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectResult = (RedirectToPageResult)result;
        Assert.Equal("/ServiceFilter/Index", redirectResult.PageName);
        Assert.Single(redirectResult.RouteValues!);
        Assert.False(redirectResult.RouteValues!.ContainsKey("test-form"));
    }
}
