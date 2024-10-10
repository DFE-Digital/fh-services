using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.La;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.La;

public class PersonalDetailsTests
{
    private readonly IRequestDistributedCache _mockRequestDistributedCache;
    private readonly PersonsDetailsModel _personalDetailsModel;

    public PersonalDetailsTests()
    {
        _mockRequestDistributedCache = Substitute.For<IRequestDistributedCache>();
        var httpContext = new DefaultHttpContext();
        CompiledPageActionDescriptor compiledPageActionDescriptor = new()
        {
            DisplayName = "/La/PersonsDetails"
        };
        _personalDetailsModel = new PersonsDetailsModel(_mockRequestDistributedCache)
        {
            PageContext =
            {
                HttpContext = httpContext,
                ActionDescriptor = compiledPageActionDescriptor
            }
        };
    }

    [Theory]
    [InlineData("email", "TestUser@email.com", "")]
    [InlineData("phone", "123456", "")]
    [InlineData("textphone", "1234567", "")]
    [InlineData("nameandpostcode", "Test User", "B60 1PY")]
    public async Task ThenPersonsDetailsOnGetIsSuccessfull(string selectionType, string value1, string value2)
    {
        //Arrange
        _mockRequestDistributedCache.GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>())
            .Returns(_ => Task.FromResult<SubjectAccessRequestViewModel?>(new SubjectAccessRequestViewModel
            {
                SelectionType = selectionType, Value1 = value1, Value2 = value2
            }));

        //Act
        await _personalDetailsModel.OnGet();

        // Assert
        await _mockRequestDistributedCache.Received(1).GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>());
        _personalDetailsModel.ContactSelection[0].Should().Be(selectionType);
    }

    [Fact]
    public async Task ThenPersonsDetailsOnGetIsSuccessfull_WithoutViewModel()
    {
        //Arrange
        _mockRequestDistributedCache.GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>())
            .Returns(_ => Task.FromResult<SubjectAccessRequestViewModel?>(null));

        //Act
        await _personalDetailsModel.OnGet();

        // Assert
        await _mockRequestDistributedCache.Received(1).GetAsync<SubjectAccessRequestViewModel>(Arg.Any<string>());
    }

    [Theory]
    [InlineData("email")]
    [InlineData("phone")]
    [InlineData("textphone")]
    [InlineData("nameandpostcode")]
    public async Task ThenPersonsDetailsOnPostIsSuccessfull(string selectionType)
    {
        //Arrange
        _personalDetailsModel.ContactSelection = [selectionType];
        _personalDetailsModel.Email = "TestUser@email.com";
        _personalDetailsModel.Telephone = "1234567890";
        _personalDetailsModel.Textphone = "1234567890";
        _personalDetailsModel.Name = "Test User";
        _personalDetailsModel.Postcode = "B60 1PY";
        _mockRequestDistributedCache.SetAsync(Arg.Any<string>(), Arg.Any<SubjectAccessRequestViewModel>())
            .Returns(_ => Task.FromResult(new SubjectAccessRequestViewModel() { SelectionType = "" }));
            
        //Act
        var result = await _personalDetailsModel.OnPost() as RedirectToPageResult;

        // Assert
        await _mockRequestDistributedCache.Received(1).SetAsync(Arg.Any<string>(), Arg.Any<SubjectAccessRequestViewModel>());
        _personalDetailsModel.ValidationValid.Should().BeTrue();
        ArgumentNullException.ThrowIfNull(result);
        result.PageName.Should().Be("/La/SubjectAccessResultDetails");
    }

    [Theory]
    [InlineData("email", "some_email")]
    [InlineData("phone", "some_email")]
    [InlineData("textphone", "some_email")]
    [InlineData("nameandpostcode", "some_email")]
    [InlineData("", default)]
    public async Task ThenPersonsDetailsOnPostFailValidation(string selectionType, string? value)
    {
        //Arrange
        _personalDetailsModel.Email = value;
        _personalDetailsModel.ContactSelection = [selectionType];
        if (selectionType == string.Empty)
        {
            _personalDetailsModel.ContactSelection.Clear();
        }
        _mockRequestDistributedCache.SetAsync(Arg.Any<string>(), Arg.Any<SubjectAccessRequestViewModel>())
            .Returns(_ => Task.CompletedTask);

        //Act
        await _personalDetailsModel.OnPost();

        // Assert
        await _mockRequestDistributedCache.DidNotReceive().SetAsync(Arg.Any<string>(), Arg.Any<SubjectAccessRequestViewModel>());
        _personalDetailsModel.ValidationValid.Should().BeFalse();
        switch(selectionType) 
        {
            case "email":
                _personalDetailsModel.EmailValid.Should().BeFalse();
                break;

            case "phone":
                _personalDetailsModel.PhoneValid.Should().BeFalse();
                break;

            case "textphone":
                _personalDetailsModel.TextValid.Should().BeFalse();
                break;

            case "nameandpostcode":
                _personalDetailsModel.NameValid.Should().BeFalse();
                _personalDetailsModel.PostcodeValid.Should().BeFalse();
                break;
        }
    }
}
