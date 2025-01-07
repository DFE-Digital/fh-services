using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class UserClaimsModelTests
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly UserClaimsModel _model;

    public UserClaimsModelTests()
    {
        _model = new UserClaimsModel(_idamService);
    }

    [Fact]
    public async Task FromAccountId_OnGet_SetsUserName()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountClaimsById(account1.Id).Returns(account1.Claims.ToList());

        await _model.OnGet(account1.Id, account1.Name);
        
        _model.UserName.Should().Be(account1.Name);
    }
    
    [Fact]
    public async Task FromAccountId_OnGet_SetsUserClaims()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountClaimsById(account1.Id).Returns(account1.Claims.ToList());

        await _model.OnGet(account1.Id, account1.Name);
        
        _model.UserClaims.Should().NotBeEmpty();
    }
}