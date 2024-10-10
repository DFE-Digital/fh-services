using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Web.Pages.ProfessionalReferral;
using FamilyHubs.ReferralUi.UnitTests.Services;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Web.Pages.ProfessionalReferral;

public class WhenUsingLocalOfferDetail
{
    private readonly IOrganisationClientService _mockIOrganisationClientService;
    private readonly IIdamsClient _mockIIdamsClient;

    public WhenUsingLocalOfferDetail()
    {
        _mockIOrganisationClientService = Substitute.For<IOrganisationClientService>();
        _mockIIdamsClient = Substitute.For<IIdamsClient>();
    }

    [Theory]
    [InlineData(default!)]
    [InlineData("url")]
    [InlineData("https://wwww.google.com")]
    [InlineData("http://google.com")]
    public async Task ThenOnGetAsync_LocalOfferDetailWithReferralNotEnabled(string? url)
    {
        //Arrange
        var serviceDto = BaseClientService.GetTestCountyCouncilServicesDto(1);
        foreach (var linkcontact in serviceDto.Contacts)
        {
            linkcontact.Url = url;
        }

        _mockIOrganisationClientService.GetLocalOfferById(Arg.Any<string>()).Returns(serviceDto);


        var localOfferDetailModel = new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost"),
                Headers =
                {
                    ["Referer"] = "Referer"
                }
            }
        };
        localOfferDetailModel.PageContext.HttpContext = httpContext;

        //Act 
        var result = await localOfferDetailModel.OnGetAsync(serviceDto.Id.ToString()) as PageResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PageResult>();
        localOfferDetailModel.Website.Should().BeEquivalentTo(url is null or "url" ? "" : url);
    }

    [Fact]
    public async Task ThenOnGetAsync_WithNullServiceAtLocation()
    {
        //Arrange
        var serviceDto = BaseClientService.GetTestCountyCouncilServicesDto(1);
        var deliveryDtoList = new List<ServiceDeliveryDto>(serviceDto.ServiceDeliveries)
        {
            new()
            {
                Id = 1, Name = AttendingType.Online,
                ServiceId = 1
            }
        };
        serviceDto.ServiceDeliveries = deliveryDtoList;
        serviceDto.Contacts = default!;
        _mockIOrganisationClientService.GetLocalOfferById(Arg.Any<string>()).Returns(serviceDto);

        var localOfferDetailModel = new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost"),
                Headers =
                {
                    ["Referer"] = "Referer"
                }
            }
        };
        localOfferDetailModel.PageContext.HttpContext = httpContext;

        //Act 
        var result = await localOfferDetailModel.OnGetAsync(serviceDto.Id.ToString()) as PageResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PageResult>();
        localOfferDetailModel.Email.Should().BeNullOrEmpty();
        localOfferDetailModel.Phone.Should().BeNullOrEmpty();
        localOfferDetailModel.Website.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ThenOnGetAsync_WithServiceAtLocationContainingInPerson()
    {
        //Arrange
        var serviceDto = BaseClientService.GetTestCountyCouncilServicesDto(1);
        _mockIOrganisationClientService.GetLocalOfferById(Arg.Any<string>()).Returns(serviceDto);

        var localOfferDetailModel =
            new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost"),
                Headers =
                {
                    ["Referer"] = "Referer"
                }
            }
        };
        localOfferDetailModel.PageContext.HttpContext = httpContext;

        //Act 
        var result = await localOfferDetailModel.OnGetAsync(serviceDto.Id.ToString()) as PageResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PageResult>();
        localOfferDetailModel.Phone.Should().Be("01827 65777");
        localOfferDetailModel.Website.Should().Be("https://www.google.com");
        localOfferDetailModel.Email.Should().Be("Contact@email.com");
    }

    [Fact]
    public async Task ThenOnGetAsync_LocalOfferDetailWithReferralEnabled()
    {
        //Arrange
        var serviceDto = BaseClientService.GetTestCountyCouncilServicesDto(1);
        _mockIOrganisationClientService.GetLocalOfferById(Arg.Any<string>()).Returns(serviceDto);

        var localOfferDetailModel =
            new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost"),
                Headers =
                {
                    ["Referer"] = "Referer"
                }
            }
        };
        localOfferDetailModel.PageContext.HttpContext = httpContext;

        //Act 
        var result = await localOfferDetailModel.OnGetAsync(serviceDto.Id.ToString()) as PageResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PageResult>();
        localOfferDetailModel.Phone.Should().Be("01827 65777");
        localOfferDetailModel.Website.Should().Be("https://www.google.com");
        localOfferDetailModel.Email.Should().Be("Contact@email.com");
    }

    [Fact]
    public void ThenGetDeliveryMethodsAsString_WithNullCollection()
    {
        //Arrange
        var localOfferDetailModel =
            new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);

        //Act
        var result = localOfferDetailModel.GetDeliveryMethodsAsString(default!);

        //Assert
        result.Should().Be(string.Empty);
    }

    [Fact]
    public void ThenGetLanguagesAsString_WithNullCollection()
    {
        //Arrange
        var localOfferDetailModel =
            new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);

        //Act
        var result = localOfferDetailModel.GetLanguagesAsString(default!);

        //Assert
        result.Should().Be(string.Empty);
    }

    [Fact]
    public void ThenGetLanguagesAsString_ShouldReturnLanguages()
    {
        //Arrange
        var localOfferDetailModel =
            new LocalOfferDetailModel(_mockIOrganisationClientService, _mockIIdamsClient);
        var languageDtos = new List<LanguageDto>
        {
            new() { Id = 1, Name = "English", Code = "en", ServiceId = 1 },
            new() { Id = 2, Name = "French", Code = "fr", ServiceId = 1 }
        };

        //Act
        var result = localOfferDetailModel.GetLanguagesAsString(languageDtos);

        //Assert
        result.Should().Be("English, French");
    }
}