using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectory.Core.Commands.Taxonomies.CreateTaxonomy;
using FamilyHubs.ServiceDirectory.Core.Commands.Taxonomies.UpdateTaxonomy;
using FamilyHubs.ServiceDirectory.Core.Queries.Taxonomies.GetTaxonomies;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Taxonomies;

public class WhenUsingTaxonomyCommands : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenCreateTaxonomy()
    {
        //Arrange
        var logger = Substitute.For<ILogger<CreateTaxonomyCommandHandler>>();
        var testTaxonomy = GetTestTaxonomyDto();
        var command = new CreateTaxonomyCommand(testTaxonomy);
        var handler = new CreateTaxonomyCommandHandler(TestDbContext, Mapper, logger);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
    }

    [Fact]
    public async Task ThenUpdateTaxonomy()
    {
        var dbTaxonomy = new Taxonomy
        {
            Name = "Test 1 Taxonomy",
            TaxonomyType = TaxonomyType.ServiceCategory,
            ParentId = null
        };
        TestDbContext.Taxonomies.Add(dbTaxonomy);
        await TestDbContext.SaveChangesAsync();
        var testTaxonomy = GetTestTaxonomyDto();
        var logger = Substitute.For<ILogger<UpdateTaxonomyCommandHandler>>();

        var command = new UpdateTaxonomyCommand(dbTaxonomy.Id, testTaxonomy);
        var handler = new UpdateTaxonomyCommandHandler(TestDbContext, logger);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
    }

    [Fact]
    public async Task ThenHandle_ThrowsException_WhenTaxonomyNotFound()
    {
        // Arrange
        var dbTaxonomy = new Taxonomy
        {
            Name = "Test 1 Taxonomy",
            TaxonomyType = TaxonomyType.ServiceCategory,
            ParentId = null
        };
        TestDbContext.Taxonomies.Add(dbTaxonomy);
        await TestDbContext.SaveChangesAsync();
        var logger = Substitute.For<ILogger<UpdateTaxonomyCommandHandler>>();
        var handler = new UpdateTaxonomyCommandHandler(TestDbContext, logger);
        var command = new UpdateTaxonomyCommand(dbTaxonomy.Id, default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task ThenHandle_ThrowsNotFoundException_WhenTaxonomyIdNotFound()
    {
        // Arrange
        var logger = Substitute.For<ILogger<UpdateTaxonomyCommandHandler>>();
        var handler = new UpdateTaxonomyCommandHandler(TestDbContext, logger);
        var command = new UpdateTaxonomyCommand(Random.Shared.Next(), default!);

        // Act
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task ThenGetTaxonomies()
    {
        var dbTaxonomy = new Taxonomy
        {
            Name = "Test 1 Taxonomy",
            TaxonomyType = TaxonomyType.ServiceCategory,
            ParentId = null
        };
        TestDbContext.Taxonomies.Add(dbTaxonomy);
        await TestDbContext.SaveChangesAsync();


        var command = new GetTaxonomiesCommand(TaxonomyType.ServiceCategory, 1, 1000, null);
        var handler = new GetTaxonomiesCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Last().Name.Should().Be("Test 1 Taxonomy");
        result.Items.Last().TaxonomyType.Should().Be(TaxonomyType.ServiceCategory);
    }

    [Fact]
    public async Task ThenGetTaxonomiesWithNullRequest()
    {
        var dbTaxonomy = new Taxonomy
        {
            Name = "Test 1 Taxonomy",
            TaxonomyType = TaxonomyType.ServiceCategory,
            ParentId = null
        };
        TestDbContext.Taxonomies.Add(dbTaxonomy);
        await TestDbContext.SaveChangesAsync();
        var handler = new GetTaxonomiesCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(new GetTaxonomiesCommand(TaxonomyType.NotSet, null, null, null), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Last().Name.Should().Be("Test 1 Taxonomy");
        result.Items.Last().TaxonomyType.Should().Be(TaxonomyType.ServiceCategory);
    }

    private static TaxonomyDto GetTestTaxonomyDto()
    {
        return new TaxonomyDto
        {
            Name = "Test Taxonomy",
            TaxonomyType = TaxonomyType.ServiceCategory,
            ParentId = null
        };
    }
}
