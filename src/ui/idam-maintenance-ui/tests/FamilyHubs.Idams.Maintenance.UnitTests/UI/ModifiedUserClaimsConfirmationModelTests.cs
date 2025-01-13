using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class ModifiedUserClaimsConfirmationModelTests
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly ModifiedUserClaimsConfirmationModel _model;

    public ModifiedUserClaimsConfirmationModelTests()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountById(account1.Id).Returns(account1);
        _model = new ModifiedUserClaimsConfirmationModel(_idamService);
    }
    
    [Fact]
    public async Task KnownAccountId_OnGet_SetsName()
    {
        var account1 = TestAccounts.GetAccount1();
        
        await _model.OnGet(account1.Id);
        
        _model.Name.Should().Be(account1.Name);
    }
}