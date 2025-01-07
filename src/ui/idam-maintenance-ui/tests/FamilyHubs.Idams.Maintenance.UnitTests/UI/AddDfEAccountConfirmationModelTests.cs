using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class AddDfEAccountConfirmationModelTests
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly AddDfEAccountConfirmationModel _model;

    public AddDfEAccountConfirmationModelTests()
    {
        var account = TestAccounts.GetAccount1();
        _idamService.GetAccountById(account.Id).Returns(account);
        _model = new AddDfEAccountConfirmationModel(_idamService);
    }
    
    [Fact]
    public async Task KnownAccountId_OnGet_SetsName()
    {
        var account = TestAccounts.GetAccount1();
        
        await _model.OnGet(account.Id);
        
        _model.Name.Should().Be(account.Name);
    }
}