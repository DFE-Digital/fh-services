using FamilyHubs.Notification.Api.Client;
using FamilyHubs.Notification.Api.Client.Templates;
using FamilyHubs.RequestForSupport.Core.ApiClients;
using FamilyHubs.RequestForSupport.UnitTests.Helpers;
using FamilyHubs.RequestForSupport.Web.Errors;
using FamilyHubs.RequestForSupport.Web.Pages.Vcs;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace FamilyHubs.RequestForSupport.UnitTests;

public class WhenUsingTheVcsDetailsPage : BaseWhenUsingPage
{
    private const int ReferralId = 123;
    private readonly VcsRequestDetailsPageModel _pageModel;

    private INotifications Notifications { get; }
    private INotificationTemplates<NotificationType> NotificationTemplates { get; }
    private ILogger<VcsRequestDetailsPageModel> Logger { get; }

    public WhenUsingTheVcsDetailsPage()
    {
        Notifications = Substitute.For<INotifications>();
        NotificationTemplates = Substitute.For<INotificationTemplates<NotificationType>>();
        var optionsFamilyHubsUiOptions = Substitute.For<IOptions<FamilyHubsUiOptions>>();
        var familyHubsUiOptions = new FamilyHubsUiOptions();
        Logger = Substitute.For<ILogger<VcsRequestDetailsPageModel>>();

        familyHubsUiOptions.Urls.Add("ThisWeb", "http://example.com");

        optionsFamilyHubsUiOptions.Value.Returns(familyHubsUiOptions);

        _pageModel = new VcsRequestDetailsPageModel(
            MockReferralClientService,
            Notifications,
            NotificationTemplates,
            optionsFamilyHubsUiOptions,
            Logger)
        {
            PageContext = GetPageContext(),
            TempData = MockTempDataDictionary
        };
    }

    [Fact]
    public async Task OnGetShouldRetrieveReferral()
    {
        //Act
        await _pageModel.OnGet(1, []);

        //Assert
        _pageModel.Referral.Should().BeEquivalentTo(TestHelpers.GetMockReferralDto());
    }

    [Fact]
    public async Task OnGet_SetsReasonForRejection()
    {
        // Act
        await _pageModel.OnGet(ReferralId, new List<ErrorId> { ErrorId.ReasonForDecliningTooLong });

        // Assert
        _pageModel.ReasonForRejection.Should().Be(ReasonForDecliningTempDataValue);
    }

    [Theory]
    [InlineData(ReferralStatus.Accepted, AcceptDecline.Accepted, null)]
    [InlineData(ReferralStatus.Declined, AcceptDecline.Declined, "Reason for Rejection")]
    public async Task OnPostShouldSetAcceptOrDeclinedStatusCorrectly(
        ReferralStatus expectedReferralStatus, AcceptDecline acceptDecline, string? expectedReason)
    {
        // Arrange
        _pageModel.AcceptOrDecline = acceptDecline;
        
        // We are setting this explicitly because it's the pages OnGet that sets this value from TempData, this test is only testing the OnPost
        _pageModel.ReasonForRejection = "Reason for Rejection";

        MockReferralClientService.UpdateReferralStatus(ReferralId, expectedReferralStatus, expectedReason)
            .Returns("1");

        // Act
        await _pageModel.OnPost(UserAction.AcceptDecline, ReferralId);

        // Assert
        await MockReferralClientService.Received(1).UpdateReferralStatus(ReferralId, expectedReferralStatus, expectedReason);
    }
}
