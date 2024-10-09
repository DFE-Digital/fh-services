using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.DeleteService;
using FluentAssertions;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Services;

public class WhenUsingDeleteServiceCommand : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenDeleteService()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new DeleteServiceByIdCommand(1);
        var handler = new DeleteServiceByIdCommandHandler(TestDbContext, GetLogger<DeleteServiceByIdCommandHandler>());

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().BeTrue();
    }

    [Fact]
    public async Task ThenDeleteServiceThatDoesNotExist_ShouldThrowException()
    {
        //Arrange
        var command = new DeleteServiceByIdCommand(Random.Shared.Next());
        var handler = new DeleteServiceByIdCommandHandler(TestDbContext, GetLogger<DeleteServiceByIdCommandHandler>());

        // Act 
        // Assert
        await handler
            .Invoking(x => x.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
    }
}
