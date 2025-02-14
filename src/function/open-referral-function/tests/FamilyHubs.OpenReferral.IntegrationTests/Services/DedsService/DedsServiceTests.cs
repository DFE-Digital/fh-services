using System.Text.Json;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FamilyHubs.OpenReferral.Function.Mappers;
using FamilyHubs.OpenReferral.Function.Models;
using FamilyHubs.OpenReferral.Function.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.OpenReferral.IntegrationTests.Services.DedsService;

// Tests are currently heavily focused on using the HSDA InternationalSpec3.0 schema
// Once we have more schemas, we can refactor the tests to be more generic
public class DedsServiceTests
{
    private readonly Function.Services.DedsService _dedsService;
    private readonly FunctionDbContext _context;

    public DedsServiceTests()
    {
        var options = new DbContextOptionsBuilder<FunctionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new FunctionDbContext(options);
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddCollectionMappers();
            cfg.CreateMap<ContactDto, Contact>().EqualityComparison((dto, o) => dto.OrId == o.OrId);
            cfg.AddProfile<ServiceDtoToServiceMapper>();
        });
        var mapper = config.CreateMapper();
        
        var logger = Substitute.For<ILogger<Function.Services.DedsService>>();
        _dedsService = new Function.Services.DedsService(logger, _context, mapper);
    }
    
    [Fact]
    public async Task ShouldGetListOfServices_WhenServicesExist()
    {
        // Arrange
        var serviceOne = new Service
        {
            Name = "Service One",
            Status = "Active",
            OrId = Guid.NewGuid()
        };
        var serviceTwo = new Service
        {
            Name = "Service Two yay",
            Status = "Active",
            OrId = Guid.NewGuid()
        };
        
        await _context.ServicesDbSet.AddAsync(serviceOne);
        await _context.ServicesDbSet.AddAsync(serviceTwo);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _dedsService.GetServices();
        
        // Assert
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task ShouldAddService_FromInternation3Dto()
    {
        // Arrange
        var testFileData = await GetServiceDtoFromFile("TestData/Service_Clean.json");
        
        // Act
        var result = await _dedsService.UpsertService(testFileData, 1);
        
        // Assert
        Assert.NotEqual(Guid.Empty, result);
        
        // Check if the service was added to the database
        var services = await _context.ServicesDbSet.ToListAsync();
        Assert.Single(services);
        Assert.Equal(services.First().OrId, services.First().OrId);
    }

    [Fact]
    public async Task ShouldNotCreateService_IfItAlreadyExistsWithNoChanges()
    {
        // Arrange
        var testFileDataOne = await GetServiceDtoFromFile("TestData/Service_Clean.json");
        var testFileDataTwo = await GetServiceDtoFromFile("TestData/Service_Clean.json");
        
        // Act
        var resultOne = await _dedsService.UpsertService(testFileDataOne, 1);
        var resultTwo = await _dedsService.UpsertService(testFileDataTwo, 1);
        
        // Assert
        Assert.NotEqual(Guid.Empty, resultOne); // Enter it first
        Assert.Equal(Guid.Empty, resultTwo); // Try to enter it again
        
        var services = await _context.ServicesDbSet.ToArrayAsync();
        Assert.Single(services);
        
    }

    [Fact]
    public async Task ShouldUpdateFromInternational3Dto_WhenThereAreChanges()
    {
        // Arrange
        var testFileDataOne = await GetServiceDtoFromFile("TestData/Service_Clean.json");
        var serviceUpdateTwo = await GetServiceDtoFromFile("TestData/Service_Clean_Modified.json");
        
        // Act
        var resultOne = await _dedsService.UpsertService(testFileDataOne, 1);
        
        // Act
        var updatedServiceId = await _dedsService.UpsertService(serviceUpdateTwo, 2);
        
        // Assert
        Assert.Equal(resultOne, updatedServiceId);
        
        var services = await _context.ServicesDbSet.ToArrayAsync();
        var orgLocations = services.SelectMany(x => x.Organization!.Locations);
        
        Assert.Single(services);
        
        // Testing that fields are updating correctly
        Assert.Equal("Community Counselling changed", services.First().Name);
        Assert.Equal("Example Organization Inc. changed", services.First().Organization!.Name);
        Assert.Equal(2, services.First().Checksum);
        
        // Asserts that a new location was added from the modified file
        Assert.Contains(new Guid("d40846b7-1cb1-46eb-8d02-87ac0965bdb6") , orgLocations.Select(x => x.OrId));
    }
    
    [Fact]
    public async Task ShouldRemoveCollectionItem_WhenItNoLongerExistsInDto()
    {
        // Arrange
        var testFileDataOne = await GetServiceDtoFromFile("TestData/Service_Clean.json");
        var serviceUpdateTwo = await GetServiceDtoFromFile("TestData/Service_Clean_One_Less_Contact.json");
        
        // Act
        await _dedsService.UpsertService(testFileDataOne, 1);
        var getService = await _dedsService.GetServiceByOpenReferralId(testFileDataOne.OrId);
        var serviceLocations = getService!.ServiceAtLocations;
        var serviceContacts = serviceLocations.SelectMany(x => x.Contacts);
        Assert.Single(serviceContacts);
        
        // Act
        await _dedsService.UpsertService(serviceUpdateTwo, 2);
        var contacts = await _context.ContactDbSet.ToArrayAsync();
        
        
        // Assert
        getService = await _dedsService.GetServiceByOpenReferralId(testFileDataOne.OrId);
        serviceLocations = getService!.ServiceAtLocations;
        serviceContacts = serviceLocations.SelectMany(x => x.Contacts);
        Assert.Empty(serviceContacts);
        Assert.Single(getService.Contacts);
        
    }

    [Fact]
    public async Task ShouldNotAddService_WhenDataContainsProfanity()
    {
        // Arrange
        var servieDto = new ServiceDto
        {
            Name = "Service One",
            Description = "Weirdo profanity",
            Status = "Active",
            OrId = Guid.NewGuid()
        };
        
        // Act
        await _dedsService.UpsertService(servieDto, 1);
        
        // Assert
        var services = await _context.ServicesDbSet.ToArrayAsync();
        Assert.Empty(services);
        

    }
    
    private static async Task<ServiceDto> GetServiceDtoFromFile(string filePath)
    {
        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<ServiceDto>(json)!;
    }
}