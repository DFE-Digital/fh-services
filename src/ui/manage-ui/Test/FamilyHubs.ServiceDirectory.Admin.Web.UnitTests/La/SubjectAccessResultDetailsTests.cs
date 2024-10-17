using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.La;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.La;

public class SubjectAccessResultDetailsTests
{
    private readonly IRequestDistributedCache _mockRequestDistributedCache;
    private readonly IReferralService _mockReferralService;
    private readonly SubjectAccessResultDetailsModel _subjectAccessResultDetailsModel;

    public SubjectAccessResultDetailsTests()
    {
        var settings = new Dictionary<string, string>
            {
                { "GovUkOidcConfiguration:AppHost", "https://localhost:7216" }
            };
        IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection((IEnumerable<KeyValuePair<string, string?>>)settings).Build();

        _mockRequestDistributedCache = Substitute.For<IRequestDistributedCache>();
        _mockReferralService = Substitute.For<IReferralService>();
        var httpContext = new DefaultHttpContext();
        CompiledPageActionDescriptor compiledPageActionDescriptor = new() { DisplayName = "/La/SubjectAccessResultDetails" };
        _subjectAccessResultDetailsModel = new SubjectAccessResultDetailsModel(_mockRequestDistributedCache, _mockReferralService, configuration)
            {
                PageContext =
                {
                    HttpContext = httpContext,
                    ActionDescriptor = compiledPageActionDescriptor
                }
            };
    }

    [Fact]
    public async Task ThenSubjectAccessResultDetailsOnGetIsSuccessfull()
    {
        //Arrange
        _mockRequestDistributedCache.GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>())
            .Returns(_ => Task.FromResult<SubjectAccessRequestViewModel?>(new SubjectAccessRequestViewModel
            {
                SelectionType = "email", Value1 = "TestUser@email.com"
            }));

        _mockReferralService.GetReferralsByRecipient(Arg.Any<SubjectAccessRequestViewModel>())
            .Returns(GetReferralList());
        //Act
        await _subjectAccessResultDetailsModel.OnGet("", SharedKernel.Razor.Dashboard.SortOrder.none);

        // Assert
        await _mockRequestDistributedCache.Received(1).GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>());
        _subjectAccessResultDetailsModel.ReferralDtos.Should().BeEquivalentTo(GetReferralList());
    }

    [Fact]
    public async Task ThenSubjectAccessResultDetailsOnGetJustRetruns()
    {
        //Arrange
        _mockRequestDistributedCache.GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>())
            .Returns(_ => Task.FromResult<SubjectAccessRequestViewModel?>(null));

        //Act
        await _subjectAccessResultDetailsModel.OnGet("", SharedKernel.Razor.Dashboard.SortOrder.none);

        // Assert
        await _mockRequestDistributedCache.Received(1).GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>());
        _subjectAccessResultDetailsModel.ReferralDtos.Should().BeEquivalentTo(new List<ReferralDto>());
    }

    private static List<ReferralDto> GetReferralList()
    {
        List<ReferralDto> listReferrals =
        [
            new()
            {
                ReferrerTelephone = "0121 555 7777",
                ReasonForSupport = "Reason For Support",
                EngageWithFamily = "Engage With Family",
                RecipientDto = new RecipientDto
                {
                    Name = "Test User",
                    Email = "TestUser@email.com",
                    Telephone = "078873456",
                    TextPhone = "078873456",
                    AddressLine1 = "Address Line 1",
                    AddressLine2 = "Address Line 2",
                    TownOrCity = "Birmingham",
                    County = "Country",
                    PostCode = "B30 2TV"
                },
                ReferralUserAccountDto = new UserAccountDto
                {
                    Id = 5,
                    EmailAddress = "Joe.Professional@email.com",
                    Name = "Joe Professional",
                    PhoneNumber = "011 222 3333",
                    Team = "Social Work team North"
                },
                Status = new ReferralStatusDto
                {
                    Id = 1,
                    Name = "New",
                    SortOrder = 1,
                    SecondrySortOrder = 0,
                },
                ReferralServiceDto = new ReferralServiceDto
                {
                    Id = 1,
                    Name = "Test Service",
                    Description = "Test Service Description",
                    Url = "www.TestService.com",
                    OrganisationDto = new OrganisationDto
                    {
                        Id = 1,
                        Name = "Test Organisation",
                        Description = "Test Organisation Description",
                    }
                }
            }

        ];

        return listReferrals;
    }
}
