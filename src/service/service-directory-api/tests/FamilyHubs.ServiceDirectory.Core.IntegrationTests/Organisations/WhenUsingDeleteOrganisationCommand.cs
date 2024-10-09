using FamilyHubs.ServiceDirectory.Core.Commands.Organisations.DeleteOrganisation;
using FamilyHubs.ServiceDirectory.Core.Exceptions;
using FamilyHubs.SharedKernel.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Organisations;

public class WhenUsingDeleteOrganisationCommand : DataIntegrationTestBase
{
    private readonly IHttpContextAccessor _mockHttpContextAccessor = GetMockHttpContextAccessor(-1, RoleTypes.DfeAdmin);

    private readonly ILogger<DeleteOrganisationCommandHandler> _deleteLogger = GetLogger<DeleteOrganisationCommandHandler>();

    [Fact]
    public async Task ThenDeleteOrganisation()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new DeleteOrganisationCommand(1);
        var handler = new DeleteOrganisationCommandHandler(TestDbContext, _mockHttpContextAccessor, _deleteLogger);

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
        var handler = new DeleteOrganisationCommandHandler(TestDbContext, _mockHttpContextAccessor, _deleteLogger);

        // Act 
        // Assert
        await handler
            .Invoking(x => x.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();

    }
}
