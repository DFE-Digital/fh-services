using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectory.Core.Queries.Organisations.GetOrganisationAdminAreaById;
using FamilyHubs.ServiceDirectory.Core.Queries.Organisations.GetOrganisationById;
using FluentAssertions;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Organisations;

public class WhenUsingGetOrganisationCommands : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenGetOrganisationById()
    {
        //Arrange
        await CreateOrganisationDetails();

        var getCommand = new GetOrganisationByIdCommand { Id = TestOrganisation.Id };
        var getHandler = new GetOrganisationByIdHandler(TestDbContext, Mapper);

        //Act
        var result = await getHandler.Handle(getCommand, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(TestOrganisation, opts => opts.Excluding(si => si.AdminAreaCode));
    }

    [Fact]
    public async Task ThenGetOrganisationById_ShouldThrowExceptionWhenIdDoesNotExist()
    {
        //Arrange
        var getCommand = new GetOrganisationByIdCommand { Id = Random.Shared.Next() };
        var getHandler = new GetOrganisationByIdHandler(TestDbContext, Mapper);

        // Act 
        // Assert
        await getHandler
            .Invoking(x => x.Handle(getCommand, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ThenGetAdminByOrganisationId()
    {
        //Arrange
        await CreateOrganisationDetails();

        var getCommand = new GetOrganisationAdminAreaByIdCommand { OrganisationId = TestOrganisation.Id };
        var getHandler = new GetOrganisationAdminAreaByIdCommandHandler(TestDbContext);

        //Act
        var result = await getHandler.Handle(getCommand, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().Be("XTEST");
    }
}