using FamilyHubs.ServiceDirectory.Core.Commands.Organisations.DeleteOrganisation;
using FamilyHubs.ServiceDirectory.Core.Exceptions;
using FamilyHubs.SharedKernel.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Organisations;

public class WhenUsingDeleteOrganisationCommand : DataIntegrationTestBase
{
    public readonly IHttpContextAccessor _mockHttpContextAccessor;

    public readonly ILogger<DeleteOrganisationCommandHandler> DeleteLogger =
        Substitute.For<ILogger<DeleteOrganisationCommandHandler>>();

    public WhenUsingDeleteOrganisationCommand()
    {
        _mockHttpContextAccessor = GetMockHttpContextAccessor(-1, RoleTypes.DfeAdmin);
    }

    [Fact]
    public async Task ThenDeleteOrganisation()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new DeleteOrganisationCommand(1);
        var handler = new DeleteOrganisationCommandHandler(TestDbContext, _mockHttpContextAccessor, DeleteLogger);

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().Be(true);

    }

    [Fact]
    public async Task ThenDeleteOrganisationThatDoesNotExist_ShouldThrowException()
    {
        //Arrange
        var command = new DeleteOrganisationCommand(Random.Shared.Next());
        var handler = new DeleteOrganisationCommandHandler(TestDbContext, _mockHttpContextAccessor, DeleteLogger);

        // Act 
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    private IHttpContextAccessor GetMockHttpContextAccessor(long organisationId, string userRole)
    {
        var mockUser = Substitute.For<ClaimsPrincipal>();
        var claims = new List<Claim>();
        claims.Add(new Claim(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()));
        claims.Add(new Claim(FamilyHubsClaimTypes.Role, userRole));

        mockUser.Claims.Returns(claims);

        var mockHttpContext = Substitute.For<HttpContext>();
        mockHttpContext.User.Returns(mockUser);

        var mockHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        mockHttpContextAccessor.HttpContext.Returns(mockHttpContext);

        return mockHttpContextAccessor;
    }
}
