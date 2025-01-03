using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class ModifyUserPermissionsModelTests
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly ModifyUserPermissionsModel _model;

    public ModifyUserPermissionsModelTests()
    {
        _model = new ModifyUserPermissionsModel(_idamService, _serviceDirectoryClient)
        {
            SelectedRole = SharedKernel.Identity.RoleTypes.DfeAdmin,
            LaOrganisationName = string.Empty,
            LocalAuthorities = []
        };
    }
    
    [Fact]
    public async Task KnownAccount_OnGet_SetsName()
    {
        var account1 = TestAccounts.GetAccount1();
        _serviceDirectoryClient.GetOrganisations().Returns([TestOrganisations.Organisation1]);
        _idamService.GetAccountById(account1.Id).Returns(account1);

        await _model.OnGet(account1.Id);

        _model.Name.Should().Be(account1.Name);
    }
    
    [Fact]
    public async Task KnownAccount_OnGet_SetsLaOrganisationName()
    {
        var account1 = TestAccounts.GetAccount1();
        var organisation = TestOrganisations.Organisation1;
        _serviceDirectoryClient.GetOrganisations().Returns([organisation]);
        _idamService.GetAccountById(account1.Id).Returns(account1);

        await _model.OnGet(account1.Id);

        _model.LaOrganisationName.Should().Be(organisation.Name);
    }
    
    [Fact]
    public async Task KnownAccount_OnGet_SetsSelectedRole()
    {
        var account1 = TestAccounts.GetAccount1();
        var organisation = TestOrganisations.Organisation1;
        _serviceDirectoryClient.GetOrganisations().Returns([organisation]);
        _idamService.GetAccountById(account1.Id).Returns(account1);

        await _model.OnGet(account1.Id);

        _model.SelectedRole.Should().Be(SharedKernel.Identity.RoleTypes.DfeAdmin);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task NoSelectedRole_OnPost_SetsValidationValidFalse(string? selectedRole)
    {
        _model.SelectedRole = selectedRole!;
        
        await _model.OnPost();

        _model.ValidationValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task NoOrganisationName_OnPost_SetsValidationValidFalse(string? organisationName)
    {
        _model.SelectedRole = SharedKernel.Identity.RoleTypes.LaDualRole;
        _model.LaOrganisationName = organisationName!;
        
        await _model.OnPost();

        _model.ValidationValid.Should().BeFalse();
    }
    
    [Fact]
    public async Task SelectedRoleAndOrganisation_OnPost_RedirectsToModifiedUserClaimsConfirmation()
    {
        var organisation1 = TestOrganisations.Organisation1;
        _model.SelectedRole = SharedKernel.Identity.RoleTypes.DfeAdmin;
        _model.LaOrganisationName = organisation1.Name;
        _serviceDirectoryClient.GetOrganisations().Returns([organisation1]);
        _idamService.UpdateRoleAndOrganisation(_model.AccountId, organisation1.Id, _model.SelectedRole).Returns(true);
            
        var result = await _model.OnPost() as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("ModifiedUserClaimsConfirmation");
    }
}