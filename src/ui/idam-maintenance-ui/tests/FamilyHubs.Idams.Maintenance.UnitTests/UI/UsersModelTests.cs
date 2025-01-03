using System.Web;
using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.ReferralService.Shared.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class UsersModelTests
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
    private readonly UsersModel _model;
    private const int PageNum = 1;
    private const string Name = "Jane Doe";
    private const string Email = "jd@temp.org";
    private const string Organisation = "Tower Hamlets";
    private const bool IsLa = true;
    private const bool IsVcs = true;
    private const string SortBy = "";
    
    public UsersModelTests()
    {
        _serviceDirectoryClient.GetOrganisations().Returns([TestOrganisations.Organisation1]);
        _idamService.GetAccounts(
            HttpUtility.UrlEncode(Name), HttpUtility.UrlEncode(Email), TestOrganisations.Organisation1.Id, IsLa, IsVcs, SortBy)
            .Returns(new PaginatedList<Account>([TestAccounts.GetAccount1(), TestAccounts.GetAccount2()], 2, 1, 10));

        _model = new UsersModel(_idamService, _serviceDirectoryClient)
        {
            LaOrganisationName = string.Empty,
            LocalAuthorities = []
        };
    }
    
    [Fact]
    public async Task LoadingModelForPage_OnGet_PopulatesAccounts()
    {
        await _model.OnGet(PageNum, Name, Email, Organisation, IsLa, IsVcs, SortBy);

        _model.Accounts.Items.Exists(a => a.Id == TestAccounts.GetAccount1().Id).Should().BeTrue();
        _model.Accounts.Items.Exists(a => a.Id == TestAccounts.GetAccount2().Id).Should().BeTrue();
    }
    
    [Fact]
    public async Task LoadingModelForPage_OnGet_PopulatesLocalAuthorities()
    {
        await _model.OnGet(PageNum, Name, Email, Organisation, IsLa, IsVcs, SortBy);
        
        _model.LocalAuthorities.Exists(a => a == TestOrganisations.Organisation1.Name).Should().BeTrue();
    }
}