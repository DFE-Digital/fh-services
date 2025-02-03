using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.OpenReferral.UnitTests.Services.DedsService;


public class DedsServiceTests
{
    private readonly Function.Services.DedsService _dedsService;
    private readonly FunctionDbContext _context;
    
    public DedsServiceTests()
    {
        // get in memory database
        var options = new DbContextOptionsBuilder<FunctionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
        _context = new FunctionDbContext(options);
        
        var logger = Substitute.For<ILogger<Function.Services.DedsService>>();
        _dedsService = new Function.Services.DedsService(logger, _context);
    }
    
    [Fact]
    public async Task ShouldGetListOfServices_WhenServicesExist()
    {
        // Arrange
        var service = await GetTestService();
        await _context.ServicesDbSet.AddAsync(service!);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _dedsService.GetServices();
        
        // Assert
        Assert.Single(result);
        Assert.Equal(service!.OrId, result.First().OrId);
    }
    
    [Fact]
    public async Task ShouldGetSingleService_WhenServiceExists()
    {
        // Arrange
        var service = await GetTestService();
        var entity = await _context.ServicesDbSet.AddAsync(service!);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _dedsService.GetServicesById(service!.Id.ToString());
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Entity.Id, result.Id);
    }
    
    [Fact]
    public async Task ShouldAddService_WhenServicePassesValidation()
    {
        var service = await GetTestService();
        
        // Arrange
        
        // Act
        var result = await _dedsService.AddService(service!);
        
        // Assert
        Assert.NotEqual(Guid.Empty, result);
        
        // Check if the service was added to the database
        var services = await _context.ServicesDbSet.ToListAsync();
        Assert.Single(services);
        Assert.Equal(services.First().OrId, services.First().OrId);
    }
    
    [Fact]
    public async Task ShouldNotAddService_WhenServiceContainsProfanity()
    {
        // Arrange
        var service = new Service
        {
            Name = "Service with profanity",
            Status = "active",
            OrId = Guid.NewGuid(),
            Description = "weirdo profanity"
        };
        
        // Act
        var result = await _dedsService.AddService(service!);
        
        // Assert
        Assert.Equal(Guid.Empty, result);
        
        var services = await _context.ServicesDbSet.ToListAsync();
        Assert.Empty(services);
    }
    
    [Fact]
    public async Task ShouldDeleteService_WhenServiceExists()
    {
        // Read from Json file and deserialize to Service object
        var service = await GetTestService();
        
        // Arrange
        await _context.ServicesDbSet.AddAsync(service!);
        await _context.SaveChangesAsync();
        var exists = await _context.ServicesDbSet.FirstOrDefaultAsync(x => x.Id == service!.Id);
        Assert.NotNull(exists);
        
        // Act
        await _dedsService.DeleteService(service!);
        
        // Assert
        var services = await _context.ServicesDbSet.ToListAsync();
        Assert.Empty(services);
    }
    
    private static async Task<Service?> GetTestService()
    {
        // Read from Json file and deserialize to Service object
        return JsonSerializer.Deserialize<Service>(await File.ReadAllTextAsync("TestData/Service_Clean.json"));
    }
}