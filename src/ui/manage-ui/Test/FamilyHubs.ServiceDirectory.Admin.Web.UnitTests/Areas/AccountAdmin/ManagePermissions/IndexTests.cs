using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class IndexTests
    {
        private readonly IIdamClient _mockIdamClient;
        private readonly ICacheService _mockCacheService;
        
        private const int OrganisationId = 1;

        public IndexTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
            _mockCacheService = Substitute.For<ICacheService>();
        }

        [Theory]
        [InlineData(null, null, null, null, null, null, null)]
        [InlineData(2, "name", "email", "organisation", true, true, "sorby")]
        public async Task OnGet_SendsCorrectQueryParameters(
            int? pageNumber, string? name, string? email, string? organisation, bool? isLa, bool? isVcs, string? sortBy)
        {
            //  Arrange
            var expectedPageNumber = pageNumber ?? 1;
            var sut = new IndexModel(_mockIdamClient, _mockCacheService)
            {
                PageContext =
                {
                    HttpContext = GetMockHttpContext(OrganisationId, RoleTypes.DfeAdmin)
                }
            };

            //  Act
            await sut.OnGet(pageNumber, name, email, organisation, isLa, isVcs, sortBy);

            //  Assert
            await _mockIdamClient.Received(1).GetAccounts(
                expectedPageNumber,
                name,
                email,
                organisation,
                isLa,
                isVcs,
                sortBy);
        }

        [Fact]
        public void OnPost_Redirects()
        {
            //  Arrange
            var sut = new IndexModel(_mockIdamClient, _mockCacheService);

            //  Act
            var response = sut.OnPost();

            //  Assert
            Assert.IsType<RedirectToPageResult>(response);
        }


        // TODO: Look into moving this to shared helper class as it's duplicated in multiple projects
        private HttpContext GetMockHttpContext(long organisationId, string userRole)
        {
            var mockUser = Substitute.For<ClaimsPrincipal>();
            var claims = new List<Claim>
            {
                new(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()),
                new(FamilyHubsClaimTypes.Role, userRole)
            };

            mockUser.Claims.Returns(claims);


            var mockHttpContext = Substitute.For<HttpContext>();
            mockHttpContext.User.Returns(mockUser);

            return mockHttpContext;
        }

    }
}
