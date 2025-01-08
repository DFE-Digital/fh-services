using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class AddDfEAdminAccountModelTests
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly AddDfEAdminAccountModel _model;

    public AddDfEAdminAccountModelTests()
    {
        _model = new AddDfEAdminAccountModel(_idamService);
    }
    
    [Fact]
    public async Task InvalidPhoneNumber_OnPost_ReturnsPhoneNumberModelError()
    {
        _model.Name = "Jane Doe";
        _model.Email = "jd@gmail.com";
        _model.PhoneNumber = "$£123";
        
        await _model.OnPost();
        
        AssertError("PhoneNumber", "Please Enter a valid phone number", _model.ModelState);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("@temp.org")]
    [InlineData("jd@")]
    public async Task InvalidEmail_OnPost_ReturnsEmailModelError(string? email)
    {
        _model.Name = "Jane Doe";
        _model.Email = email!;
        _model.PhoneNumber = "$£123";
        
        await _model.OnPost();
        
        AssertError("Email", "Please Enter a valid email address", _model.ModelState);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task InvalidName_OnPost_ReturnsNameModelError(string? name)
    {
        _model.Name = name!;
        _model.Email = "jd@temp.org";
        _model.PhoneNumber = "$£123";
        
        await _model.OnPost();
        
        AssertError("Name", "Please Enter a valid name", _model.ModelState);
    }
    
    [Fact]
    public async Task EmailMismatch_OnPost_ReturnsEmailModelError()
    {
        var account = TestAccounts.GetAccount1();
        _model.Name = account.Name;
        _model.Email = account.Email;
        _model.PhoneNumber = account.PhoneNumber;
        _idamService.AddNewDfeAccount(_model.Name, _model.Email, _model.PhoneNumber).Returns(account.Email.ToUpper());
        
        await _model.OnPost();
        
        AssertError("Name", "Failed to create DfE Admin", _model.ModelState);
    }
    
    [Fact]
    public async Task EmailMismatch_OnPost_RedirectsToAddDfEAccountConfirmationPage()
    {
        var account = TestAccounts.GetAccount1();
        _model.Name = account.Name;
        _model.Email = account.Email;
        _model.PhoneNumber = account.PhoneNumber;
        _idamService.GetAccountIdByEmail(_model.Email).Returns(account.Id);
        _idamService.AddNewDfeAccount(_model.Name, _model.Email, _model.PhoneNumber).Returns(account.Email);
        
        var result = await _model.OnPost() as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("AddDfEAccountConfirmation");
    }
    
    private static void AssertError(string key, string error, ModelStateDictionary modelState)
    {
        var errorState = modelState.FirstOrDefault(ms => ms.Key == key);
        errorState.Should().NotBeNull();
        var errorMessage = errorState.Value?.Errors.FirstOrDefault(e => e.ErrorMessage == error);
        errorMessage.Should().NotBeNull();
    }
}