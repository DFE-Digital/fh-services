using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.OpenReferral.Function.Services;
using FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.OpenReferral.IntegrationTests.Services.DedsService;

public class DedsPrototypeServiceTests
{
    private readonly FunctionDbContext _context;
    private readonly DedsPrototypeService _dedsService;

    public DedsPrototypeServiceTests()
    {
        // get in memory database
        var options = new DbContextOptionsBuilder<FunctionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
        _context = new FunctionDbContext(options);

        var logger = Substitute.For<ILogger<DedsPrototypeService>>();
        _dedsService = new DedsPrototypeService(logger, _context);

        SetupThirdParty();
        SetupStandardVersion();
    }

    [Fact]
    public async Task ShouldInsertNewService_WhenOneDoesNotExist()
    {
        // Arrange
        var document = TestDocumentFromFile("Service_Clean.json");


        // Act
        var id = await _dedsService.UpsertService(1, "1", document,
            StandardDocumentVersions.OpenReferralInternationalV3);
        var services = _context.ThirdPartyServices.ToList();

        // Assert
        Assert.NotEqual(0, id);
        Assert.Single(services);
    }

    [Fact]
    public async Task ShouldUpdateService_IfItAlreadyExists_AndHasChanges()
    {
        // Arrange
        var document = TestJsonDocument();
        var documentUpdated = TestJsonDocument("1", "Test Service 2");

        // Act
        await _dedsService.UpsertService(1, "1", document, StandardDocumentVersions.OpenReferralInternationalV3);
        await _dedsService.UpsertService(1, "1", documentUpdated, StandardDocumentVersions.OpenReferralInternationalV3);

        // Assert
        var services = _context.ThirdPartyServices.ToList();
        Assert.Single(services);
        Assert.NotEqual(document, services.First().Document);
        Assert.Equal(DateTimeOffset.Now.Year, services.First().UpdatedAt!.Value.Year);
        Assert.Equal(DateTimeOffset.Now.Month, services.First().UpdatedAt!.Value.Month);
        Assert.Equal(DateTimeOffset.Now.Day, services.First().UpdatedAt!.Value.Day);
    }

    // [Fact]
    // public async Task ShouldUpdateCorrectService_WhenThereAreTwoServicesWithSameServiceId()
    // {
    //     // TODO: 
    //     throw new NotImplementedException();
    // }

    [Fact]
    public async Task ShouldNotUpdate_IfChecksumIsTheSame()
    {
        // Arrange
        var document = TestJsonDocument();

        // Act
        await _dedsService.UpsertService(1, "1", document, StandardDocumentVersions.OpenReferralInternationalV3);
        await _dedsService.UpsertService(1, "1", document, StandardDocumentVersions.OpenReferralInternationalV3);

        // Assert
        var services = _context.ThirdPartyServices.ToList();
        Assert.Single(services);
        Assert.Null(services.First().UpdatedAt);
    }

    [Fact]
    public async Task ShouldAddCreateAtTime_WhenServiceAdded()
    {
        // Arrange
        var document = TestJsonDocument();
        
        // Act
        await _dedsService.UpsertService(1, "1", document, StandardDocumentVersions.OpenReferralInternationalV3);
        var services = _context.ThirdPartyServices.ToList();

        // Assert
        var serviceCreatedAt = services.First().CreatedAt;
        Assert.Equal(DateTime.Now.Year, serviceCreatedAt.Year);
        Assert.Equal(DateTime.Now.Month, serviceCreatedAt.Month);
        Assert.Equal(DateTime.Now.Day, serviceCreatedAt.Day);
    }

    [Fact]
    public async Task ShouldMinifyJson_WhenDocumentIsAdded()
    {
        // Arrange
        var document = TestJsonDocument();

        // Act
        await _dedsService.UpsertService(1, "1", document,
            StandardDocumentVersions.OpenReferralInternationalV3);
        var services = _context.ThirdPartyServices.ToList();

        // Assert
        Assert.NotEqual(document, services.First().Document);

        // use regex to check for indentation, newlines and excess whitespace to prove it's minified 
        Assert.Matches(@"\S+", services.First().Document);
        Assert.DoesNotMatch(@"\n|\s{2,}", services.First().Document);
    }

    [Fact]
    public async Task ShouldNotInsertService_IfDataContainsProfanity()
    {
        // Arrange
        var document = TestJsonDocument("1", "Weirdo profanity service");

        // Act
        var id = await _dedsService.UpsertService(1, "1", document,
            StandardDocumentVersions.OpenReferralInternationalV3);
        var services = _context.ThirdPartyServices.ToList();

        // Assert
        Assert.True(id == 0, "Id should be 0 as nothing was added");
        Assert.Empty(services);
    }
    
    [Fact]
    public async Task ShouldRemoveHtmlAndJavascript_WhenDocumentIsAdded()
    {
        // Arrange
        var document = TestDocumentFromFile("Service_with_html_and_javascript.json");

        // Act
        var id = await _dedsService.UpsertService(1, "1", document,
            StandardDocumentVersions.OpenReferralInternationalV3);
        var services = _context.ThirdPartyServices.ToList();

        // Assert
        Assert.NotEqual(0, id);
        Assert.Single(services);
        Assert.DoesNotContain("<p>", services.First().Document);
        Assert.DoesNotContain("</p>", services.First().Document);
        Assert.DoesNotContain("<b>", services.First().Document);
        Assert.DoesNotContain("</b>", services.First().Document);
        Assert.DoesNotContain("<a href='javascript:alert(\"Test\")'>", services.First().Document);
    }

    private string TestJsonDocument(string? id = null, string? name = null, string? description = null)
    {
        var document = new
        {
            id = id ?? "1",
            name = name ?? "Test Service",
            description = description ?? "Test Description",
            arrayOfThings = new[]
            {
                new
                {
                    id = "1",
                    name = "Test Thing"
                }
            }
        };

        return JsonSerializer.Serialize(document, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    private string TestDocumentFromFile(string fileName)
    {
        return File.ReadAllText($"TestData/{fileName}");
    }


    private void SetupThirdParty()
    {
        var thirdParty = new ThirdParty
        {
            Id = 1,
            Name = "Test Third Party",
            OpenReferralUrl = "https://test.com",
        };

        _context.ThirdParties.Add(thirdParty);
        _context.SaveChanges();
    }

    private void SetupStandardVersion()
    {
        var standardVersion = new StandardVersion
        {
            Id = (byte)StandardDocumentVersions.OpenReferralInternationalV3,
            Name = "Open Referral International V3"
        };

        _context.StandardVersions.Add(standardVersion);
        _context.SaveChanges();
    }
}