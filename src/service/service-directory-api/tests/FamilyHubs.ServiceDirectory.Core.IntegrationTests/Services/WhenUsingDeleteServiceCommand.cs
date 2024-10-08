using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.DeleteService;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Services;

public class WhenUsingDeleteServiceCommand : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenDeleteService()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new DeleteServiceByIdCommand(1);
        var handler = new DeleteServiceByIdCommandHandler(TestDbContext, Substitute.For<ILogger<DeleteServiceByIdCommandHandler>>());

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().Be(true);

    }

    [Fact]
    public async Task ThenDeleteServiceThatDoesNotExist_ShouldThrowException()
    {
        //Arrange
        var command = new DeleteServiceByIdCommand(Random.Shared.Next());
        var handler = new DeleteServiceByIdCommandHandler(TestDbContext, Substitute.For<ILogger<DeleteServiceByIdCommandHandler>>());

        // Act 
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }
}
