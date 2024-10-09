using FamilyHubs.ServiceDirectory.Core.Commands.Organisations.UpdateOrganisation;
using FamilyHubs.ServiceDirectory.Core.Exceptions;
using FamilyHubs.SharedKernel.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Organisations;

public class WhenUsingUpdateOrganisationCommand : DataIntegrationTestBase
{
    private readonly IHttpContextAccessor _mockHttpContextAccessor = GetMockHttpContextAccessor(-1, RoleTypes.DfeAdmin);
    private readonly ILogger<UpdateOrganisationCommandHandler> _updateLogger = GetLogger<UpdateOrganisationCommandHandler>();

    [Fact]
    public async Task ThenUpdateOrganisationOnly()
    {
        //Arrange
        await CreateOrganisationDetails();
        var service = TestOrganisation.Services.ElementAt(0);
        TestOrganisation.Name = "Unit Test Update TestOrganisation Name";
        TestOrganisation.Description = "Unit Test Update TestOrganisation Name";

        var updateCommand = new UpdateOrganisationCommand(TestOrganisation.Id, TestOrganisation);
        var updateHandler = new UpdateOrganisationCommandHandler(_mockHttpContextAccessor, TestDbContext, Mapper, _updateLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(TestOrganisation.Id);

        var actualOrganisation = TestDbContext.Organisations.SingleOrDefault(s => s.Name == TestOrganisation.Name);
        actualOrganisation.Should().NotBeNull();
        actualOrganisation!.Description.Should().Be(TestOrganisation.Description);

        var actualService = TestDbContext.Services.SingleOrDefault(s => s.Name == service.Name);
        actualService.Should().NotBeNull();
        actualService!.Description.Should().Be(service.Description);
    }

    [Fact]
    public async Task ThenUpdateOrganisation_ThrowsForbiddenException()
    {
        //Arrange
        await CreateOrganisationDetails();
        var mockHttpContextAccessor = GetMockHttpContextAccessor(50, RoleTypes.LaManager);

        var updateCommand = new UpdateOrganisationCommand(TestOrganisation.Id, TestOrganisation);
        var updateHandler = new UpdateOrganisationCommandHandler(mockHttpContextAccessor, TestDbContext, Mapper, _updateLogger);

        //Act / Assert
        await updateHandler
            .Invoking(x => x.Handle(updateCommand, CancellationToken.None))
            .Should()
            .ThrowAsync<ForbiddenException>();
    }
}