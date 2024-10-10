using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Constants;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class EditTests
    {
        private readonly IIdamClient _mockIdamClient;
        private readonly IServiceDirectoryClient _mockServiceDirectoryClient;
        private readonly ICacheService _mockCacheService;
        
        private const long OrganisationId = 1;
        private const long ExpectedAccountId = 1234;
        private const string ExpectedEmail = "anyEmail";
        private const string ExpectedName = "anyName";
        private const string ExpectedOrganisation = "anyOrganisation";
        private const string ExpectedBackPath = "/AccountAdmin/ManagePermissions";

        public EditTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
            _mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
            _mockCacheService = Substitute.For<ICacheService>();
        }

        [Theory]
        [InlineData(RoleTypes.LaManager, RoleDescription.LaManager)]
        [InlineData(RoleTypes.LaProfessional, RoleDescription.LaProfessional)]
        [InlineData(RoleTypes.LaDualRole, $"{RoleDescription.LaManager}, {RoleDescription.LaProfessional}")]
        [InlineData(RoleTypes.VcsManager, RoleDescription.VcsManager)]
        [InlineData(RoleTypes.VcsProfessional, RoleDescription.VcsProfessional)]
        [InlineData(RoleTypes.VcsDualRole, $"{RoleDescription.VcsManager}, {RoleDescription.VcsProfessional}")]
        public async Task OnGet_SetsValues(string accountRole, string expectedRole)
        {
            //  Arrange
            var account = GetAccountDto(accountRole);
            _mockIdamClient.GetAccountById(ExpectedAccountId).Returns(account);

            var organisationDto = GetOrganisationDto();
            _mockServiceDirectoryClient.GetOrganisationById(OrganisationId, Arg.Any<CancellationToken>()).Returns(organisationDto);

            _mockCacheService.RetrieveLastPageName().Returns(Task.FromResult(ExpectedBackPath));

            var sut = new EditModel(_mockIdamClient, _mockServiceDirectoryClient, _mockCacheService)
            {
                AccountId = ExpectedAccountId.ToString()
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(ExpectedEmail, sut.Email);
            Assert.Equal(ExpectedName, sut.Name);
            Assert.Equal(ExpectedOrganisation, sut.Organisation);
            Assert.Equal(expectedRole, sut.Role);
            Assert.Equal(ExpectedBackPath, sut.BackPath);
        }

        [Fact]
        public async Task OnGet_ThrowsIfUserIsDfeAdmin()
        {
            //  Arrange
            var account = GetAccountDto(RoleTypes.DfeAdmin);
            _mockIdamClient.GetAccountById(ExpectedAccountId).Returns(account);

            var organisationDto = GetOrganisationDto();
            _mockServiceDirectoryClient.GetOrganisationById(OrganisationId, Arg.Any<CancellationToken>()).Returns(organisationDto);

            _mockCacheService.RetrieveLastPageName().Returns(Task.FromResult(ExpectedBackPath));

            var sut = new EditModel(_mockIdamClient, _mockServiceDirectoryClient, _mockCacheService)
                {
                    AccountId = ExpectedAccountId.ToString()
                };

            // Act/Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.OnGet());
            Assert.Equal("Role type not Valid", exception.Message);
        }

        private static Task<AccountDto?> GetAccountDto(string role)
        {
            var account = new AccountDto
            {
                Id = ExpectedAccountId,
                Email = ExpectedEmail,
                Name = ExpectedName
            };

            var claims = new List<AccountClaimDto>
            {
                new() { Name = FamilyHubsClaimTypes.OrganisationId, Value = OrganisationId.ToString() },
                new() { Name = FamilyHubsClaimTypes.Role, Value = role }
            };

            account.Claims = claims;

            return Task.FromResult<AccountDto?>(account);
        }

        private static Task<OrganisationDetailsDto> GetOrganisationDto()
        {
            return Task.FromResult(new OrganisationDetailsDto 
            { 
                AdminAreaCode = "Any",
                Description = "Any",
                Name = ExpectedOrganisation,
                OrganisationType = Shared.Enums.OrganisationType.NotSet,
                Id = OrganisationId
            });
        }
    }
}