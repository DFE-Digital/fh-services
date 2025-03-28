﻿using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.VcsAdmin
{
    public class ManageOrganisationsTests
    {
        private readonly IServiceDirectoryClient _mockServiceDirectoryClient;
        private readonly ICacheService _mockCacheService;
        private readonly Fixture _fixture;


        public ManageOrganisationsTests()
        {
            _mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
            _mockCacheService = Substitute.For<ICacheService>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task OnGet_DfeAdmin_SetsPaginatedList()
        {
            //  Arrange
            int? pageNumber = null;
            const string? sortBy = null;
            const string? organisationName = null;

            var mockHttpContext = GetHttpContext(RoleTypes.DfeAdmin, -1);
            var organisations = GetTestOrganisations();

            _mockServiceDirectoryClient.GetOrganisations(Arg.Any<CancellationToken>(), null, null).Returns(Task.FromResult(organisations));

            var sut = new ManageOrganisationsModel(_mockServiceDirectoryClient, _mockCacheService)
            {
                PageContext = { HttpContext = mockHttpContext }
            };

            //  Act
            await sut.OnGet(pageNumber, sortBy, organisationName);

            //  Assert
            Assert.Equal(3, sut.PaginatedOrganisations.Items.Count);
        }

        [Fact]
        public async Task OnGet_LaAdmin_SetsPaginatedList()
        {
            //  Arrange
            int? pageNumber = null;
            const string? sortBy = null;
            const string? organisationName = null;

            const long organisationId = 1;
            var mockHttpContext = GetHttpContext(RoleTypes.LaManager, organisationId);
            var organisations = GetTestOrganisations();

            _mockServiceDirectoryClient.GetOrganisationByAssociatedOrganisation(organisationId).Returns(Task.FromResult(organisations));

            var sut = new ManageOrganisationsModel(_mockServiceDirectoryClient, _mockCacheService)
            {
                PageContext = { HttpContext = mockHttpContext }
            };

            //  Act
            await sut.OnGet(pageNumber, sortBy, organisationName);

            //  Assert
            Assert.Equal(3, sut.PaginatedOrganisations.Items.Count);
        }

        [Fact]
        public async Task OnGet_SearchFilterOrganisationName_Returns_MatchingResults()
        {
            // Arrange
            int? pageNumber = null;
            const string? sortBy = null;
            const string? organisationName = "OrgName 3";

            HttpContext httpContext = GetHttpContext(RoleTypes.LaManager, 1);
            List<OrganisationDto> organisations = GetTestOrganisations();

            _mockServiceDirectoryClient.GetOrganisationByAssociatedOrganisation(1)
                .Returns(Task.FromResult(organisations));

            ManageOrganisationsModel pageModel =
                new ManageOrganisationsModel(_mockServiceDirectoryClient, _mockCacheService)
                {
                    PageContext = { HttpContext = httpContext }
                };

            // Act
            // Organisations are named "OrgName X" where X is the ID of the Org.
            await pageModel.OnGet(pageNumber, sortBy, organisationName);

            // Assert
            pageModel.PaginatedOrganisations.Items.Count.Should().Be(1);
        }

        private static HttpContext GetHttpContext(string role, long organisationId)
        {
            var claims = new List<Claim> {
                new(FamilyHubsClaimTypes.FullName, "any") ,
                new(FamilyHubsClaimTypes.AccountId, "1"),
                new(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()),
                new(FamilyHubsClaimTypes.Role, role),
            };

            return TestHelper.GetHttpContext(claims);
        }

        private List<OrganisationDto> GetTestOrganisations()
        {
            var organisations = new List<OrganisationDto>
            {
                TestHelper.CreateTestOrganisation(1, null, OrganisationType.LA, _fixture),
                TestHelper.CreateTestOrganisation(2, 1, OrganisationType.VCFS, _fixture),
                TestHelper.CreateTestOrganisation(3, 1, OrganisationType.VCFS, _fixture),
                TestHelper.CreateTestOrganisation(4, 1, OrganisationType.VCFS, _fixture)
            };

            return organisations;
        }
    }
}
