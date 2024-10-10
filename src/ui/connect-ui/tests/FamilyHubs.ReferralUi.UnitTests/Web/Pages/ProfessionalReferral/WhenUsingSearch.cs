using FamilyHubs.Referral.Web.Pages.ProfessionalReferral;
using FamilyHubs.SharedKernel.Services.Postcode.Interfaces;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Web.Pages.ProfessionalReferral;

public class WhenUsingSearch
{
    private readonly IPostcodeLookup _postcodeLookup;
    public WhenUsingSearch()
    {
        _postcodeLookup = Substitute.For<IPostcodeLookup>();
    }
    [Fact]
    public async Task OnPost_WhenPostcodeIsValid_ThenValidationShouldBeTrue()
    {
        //Arrange
        _postcodeLookup
            .Get(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((PostcodeError.None, null));
        var searchModel = new SearchModel(_postcodeLookup);

        //Act
        _ = await searchModel.OnPostAsync() as PageResult;

        //Assert
        Assert.True(searchModel.PostcodeValid);
    }

    [Fact]
    public async Task OnPost_WhenPostcodeIsNotValid_ThenValidationShouldBeFalse()
    {
        //Arrange
        const string postcode = "aaa";
        _postcodeLookup
            .Get(postcode, Arg.Any<CancellationToken>())
            .Returns((PostcodeError.InvalidPostcode, null));
        var searchModel = new SearchModel(_postcodeLookup)
        {
            Postcode = postcode
        };

        _ = await searchModel.OnPostAsync() as PageResult;

        //Assert
        Assert.False(searchModel.PostcodeValid);
    }
}
