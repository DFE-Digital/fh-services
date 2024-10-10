using FamilyHubs.Referral.Core;
using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Web.Pages.ProfessionalReferral;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Web.Pages.ProfessionalReferral;

public class WhenUsingCheckDetails : BaseProfessionalReferralPage
{
    private CheckDetailsModel CheckDetailsModel { get; }

    private readonly IReferralNotificationService _referralNotificationService;

    private const long CreatedRequestNumber = 6789;
    private const long OrganisationId = 12345;
    private const string ServiceName = "No shoes, no shirt, and I still get service";

    public WhenUsingCheckDetails()
    {
        var referralClientService = Substitute.For<IReferralClientService>();

        var referralResponse = new ReferralResponse
        {
            Id = CreatedRequestNumber,
            OrganisationId = OrganisationId,
            ServiceName = ServiceName
        };

        referralClientService
            .CreateReferral(Arg.Any<CreateReferralDto>(), Arg.Any<CancellationToken>())
            .Returns(referralResponse);

        _referralNotificationService = Substitute.For<IReferralNotificationService>();

        CheckDetailsModel = new CheckDetailsModel(
            ReferralDistributedCache,
            referralClientService,
            _referralNotificationService)
        {
            PageContext = GetPageContextWithUserClaims()
        };
    }

    [Fact]
    public async Task EmailOptionNotSelected_EmailIsRemoved()
    {
        ConnectionRequestModel.ContactMethodsSelected[(int)ConnectContactDetailsJourneyPage.Email] = false;

        await CheckDetailsModel.OnGetAsync("1");
        CheckDetailsModel.ConnectionRequestModel!.EmailAddress.Should().BeNull();
    }

    [Fact]
    public async Task TelephoneOptionNotSelected_TelephoneIsRemoved()
    {
        ConnectionRequestModel.ContactMethodsSelected[(int)ConnectContactDetailsJourneyPage.Telephone] = false;

        await CheckDetailsModel.OnGetAsync("1");
        CheckDetailsModel.ConnectionRequestModel!.TelephoneNumber.Should().BeNull();
    }

    [Fact]
    public async Task TextOptionNotSelected_TextIsRemoved()
    {
        ConnectionRequestModel.ContactMethodsSelected[(int)ConnectContactDetailsJourneyPage.Textphone] = false;

        await CheckDetailsModel.OnGetAsync("1");
        CheckDetailsModel.ConnectionRequestModel!.TextphoneNumber.Should().BeNull();
    }

    [Fact]
    public async Task LetterOptionNotSelected_AddressIsRemoved()
    {
        ConnectionRequestModel.ContactMethodsSelected[(int)ConnectContactDetailsJourneyPage.Letter] = false;

        await CheckDetailsModel.OnGetAsync("1");

        CheckDetailsModel.ConnectionRequestModel!.AddressLine1.Should().BeNull();
        CheckDetailsModel.ConnectionRequestModel!.AddressLine2.Should().BeNull();
        CheckDetailsModel.ConnectionRequestModel!.TownOrCity.Should().BeNull();
        CheckDetailsModel.ConnectionRequestModel!.County.Should().BeNull();
        CheckDetailsModel.ConnectionRequestModel!.Postcode.Should().BeNull();
    }

    [Fact]
    public async Task EmailOptionSelected_EmailIsPresent()
    {
        await CheckDetailsModel.OnGetAsync("1");
        CheckDetailsModel.ConnectionRequestModel!.EmailAddress.Should().NotBeNull();
    }

    [Fact]
    public async Task TelephoneOptionSelected_TelephoneIsPresent()
    {
        await CheckDetailsModel.OnGetAsync("1");
        CheckDetailsModel.ConnectionRequestModel!.TelephoneNumber.Should().NotBeNull();
    }

    [Fact]
    public async Task TextOptionSelected_TextIsPresent()
    {
        await CheckDetailsModel.OnGetAsync("1");
        CheckDetailsModel.ConnectionRequestModel!.TextphoneNumber.Should().NotBeNull();
    }

    [Fact]
    public async Task LetterOptionSelected_AddressIsPresent()
    {
        await CheckDetailsModel.OnGetAsync("1");

        CheckDetailsModel.ConnectionRequestModel!.AddressLine1.Should().NotBeNull();
        CheckDetailsModel.ConnectionRequestModel!.AddressLine2.Should().NotBeNull();
        CheckDetailsModel.ConnectionRequestModel!.TownOrCity.Should().NotBeNull();
        CheckDetailsModel.ConnectionRequestModel!.County.Should().NotBeNull();
        CheckDetailsModel.ConnectionRequestModel!.Postcode.Should().NotBeNull();
    }

    [Fact]
    public async Task ThenOnPostAsync_NextPageIsConfirmation()
    {
        var result = await CheckDetailsModel.OnPostAsync("1") as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("/ProfessionalReferral/Confirmation");
    }

    [Fact]
    public async Task OnPostAsync_ThenNotificationIsSent()
    {
        await CheckDetailsModel.OnPostAsync("1");

        await _referralNotificationService.Received(1).OnCreateReferral(
            ProfessionalEmail,
            OrganisationId,
            ServiceName,
            CreatedRequestNumber);
    }
}
