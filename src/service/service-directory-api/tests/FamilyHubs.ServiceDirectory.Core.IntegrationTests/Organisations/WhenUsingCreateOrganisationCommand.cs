﻿using FamilyHubs.ServiceDirectory.Core.Commands.Organisations.CreateOrganisation;
using FamilyHubs.ServiceDirectory.Core.Exceptions;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Organisations;

public class WhenUsingCreateOrganisationCommand : DataIntegrationTestBase
{
    private readonly ILogger<CreateOrganisationCommandHandler> _createLogger = GetLogger<CreateOrganisationCommandHandler>();

    [Fact]
    public async Task ThenCreateOrganisation()
    {
        //Arrange
        var organisation = Mapper.Map<OrganisationDto>(TestOrganisation);
        var createOrganisationCommand = new CreateOrganisationCommand(organisation);
        var handler = new CreateOrganisationCommandHandler(TestDbContext, Mapper, _createLogger);

        //Act
        var result = await handler.Handle(createOrganisationCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        var actualOrganisation = TestDbContext.Organisations
            .Include(o => o.Services)
            .Include(o => o.Locations)
            .SingleOrDefault(o => o.Id == result);
        actualOrganisation.Should().NotBeNull();
        actualOrganisation!.Services.Count.Should().Be(0);
        actualOrganisation.Locations.Count.Should().Be(0);
        actualOrganisation.Should().BeEquivalentTo(organisation, options => 
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id")));
    }

    [Fact]
    public async Task ThenCreateRelatedOrganisationWithCorrectAdminAreaCode()
    {
        //Arrange
        await CreateOrganisationDetails();

        var relatedOrganisation = new OrganisationDto
        {
            OrganisationType = OrganisationType.VCFS,
            Name = "Related VCS",
            Description = "Related VCS",
            Logo = null,
            Uri = new Uri("https://www.relatedvcs.gov.uk/").ToString(),
            Url = "https://www.related.gov.uk/",
            AdminAreaCode = "XTEST",
            AssociatedOrganisationId = 1
        };

        var command = new CreateOrganisationCommand(relatedOrganisation);
        var handler = new CreateOrganisationCommandHandler(TestDbContext, Mapper, _createLogger);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);

        var actualOrganisation = TestDbContext.Organisations.SingleOrDefault(c => c.AssociatedOrganisationId == 1);
        actualOrganisation.Should().NotBeNull();
        actualOrganisation!.AdminAreaCode.Should().Be("E06000023");
    }

    [Fact]
    public async Task ThenCreateDuplicateOrganisation_ShouldThrowException()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new CreateOrganisationCommand(TestOrganisation);
        var handler = new CreateOrganisationCommandHandler(TestDbContext, Mapper, _createLogger);

        // Act 
        // Assert
        await handler
            .Invoking(x => x.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<AlreadyExistsException>();
    }
}