using System.Security.Claims;
using AutoFixture;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FamilyHubs.ServiceDirectory.Core.Helper;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Data.Interceptors;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests;

public abstract class DataIntegrationTestBase : IDisposable, IAsyncDisposable
{
    protected ApplicationDbContext TestDbContext { get; }

    protected static ILogger<T> GetLogger<T>() => Substitute.For<ILogger<T>>();

    protected IMapper Mapper { get; }
    protected IConfiguration Configuration { get; }
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Fixture _fixtureObjectGenerator;

    protected OrganisationDetailsDto TestOrganisation { get; }
    protected OrganisationDetailsDto TestOrganisationFreeService { get; }
    protected OrganisationDto TestOrganisationWithoutAnyServices { get; }

    private static readonly ILoggerFactory TestLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    private static readonly string[] ConfigAction = ["CreatedBy", "Created", "LastModified", "LastModified"];

    private static int _dbSuffix;

    protected DataIntegrationTestBase()
    {
        _fixtureObjectGenerator = new Fixture();
        TestOrganisation = TestDataProvider.GetTestCountyCouncilDto();
        TestOrganisationFreeService = TestDataProvider.GetTestCountyCouncilWithFreeServiceDto();
        TestOrganisationWithoutAnyServices = TestDataProvider.GetTestCountyCouncilWithoutAnyServices();

        var serviceProvider = CreateNewServiceProvider();

        TestDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        Mapper = serviceProvider.GetRequiredService<IMapper>();
        Configuration = serviceProvider.GetRequiredService<IConfiguration>();

        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

        InitialiseDatabase();
    }

    protected LocationDto GetTestLocation()
    {
        var locationDto = TestOrganisation.Services.ElementAt(0).Locations.ElementAt(0);
        locationDto.OrganisationId = TestDbContext.Organisations.First().Id;
        return locationDto;
    }

    protected async Task<OrganisationDetailsDto> CreateOrganisationDetails(OrganisationDetailsDto? organisationDto = null)
    {
        var organisationWithServices = Mapper.Map<Organisation>(organisationDto ?? TestOrganisation);

        organisationWithServices.Locations.Add(organisationWithServices.Services.First().Locations.First());

        TestDbContext.Organisations.Add(organisationWithServices);

        await TestDbContext.SaveChangesAsync();

        return Mapper.Map(organisationWithServices, organisationDto ?? TestOrganisation);
    }

    protected async Task<OrganisationDto> CreateOrganisation(OrganisationDto? organisationDto = null)
    {
        var organisationWithoutAnyServices = Mapper.Map<Organisation>(organisationDto ?? TestOrganisationWithoutAnyServices);

        var organisationWithServices = Mapper.Map<Organisation>(organisationDto ?? TestOrganisation);

        organisationWithoutAnyServices.Locations.Add(organisationWithServices.Services.First().Locations.First());

        TestDbContext.Organisations.Add(organisationWithoutAnyServices);

        await TestDbContext.SaveChangesAsync();

        return Mapper.Map(organisationWithoutAnyServices, organisationDto ?? TestOrganisationWithoutAnyServices);
    }

    protected long CreateLocation(LocationDto locationDto)
    {
        var existingLocation = Mapper.Map<Location>(locationDto);

        TestDbContext.Locations.Add(existingLocation);

        TestDbContext.SaveChanges();

        return existingLocation.Id;
    }

    protected async Task AddServiceAtLocationSchedule(long serviceAtLocationId, ScheduleDto scheduleDto)
    {
        var schedule = Mapper.Map<Schedule>(scheduleDto);

        var serviceAtLocation = TestDbContext.ServiceAtLocations.First(sal => sal.Id == serviceAtLocationId);

        serviceAtLocation.Schedules.Add(schedule);

        await TestDbContext.SaveChangesAsync();
    }

    protected async Task<List<Service>> CreateManyTestServicesQueryTesting()
    {
        var testOrganisations = TestDbContext.Organisations.Select(o => new { o.Id, o.Name })
            .Where(o => o.Name == "Bristol County Council" || o.Name == "Salford City Council")
            .ToDictionary(arg => arg.Name, arg => arg.Id);

        var services = new List<Service>();

        services.AddRange(TestDataProvider.SeedBristolServices(testOrganisations["Bristol County Council"]));
        services.AddRange(TestDataProvider.SeedSalfordService(testOrganisations["Salford City Council"]));


        TestDbContext.Services.AddRange(services);

        await TestDbContext.SaveChangesAsync();

        return services;
    }

    private void InitialiseDatabase()
    {
        TestDbContext.Database.EnsureDeleted();
        TestDbContext.Database.EnsureCreated();
        TestDbContext.Database.ExecuteSqlRaw($"UPDATE geometry_columns SET srid = {GeoPoint.WGS84} WHERE f_table_name = 'locations';");

        var organisationSeedData = new OrganisationSeedData(TestDbContext);

        if (!TestDbContext.Taxonomies.Any())
            organisationSeedData.SeedTaxonomies().GetAwaiter().GetResult();

        if (!TestDbContext.Organisations.Any())
            organisationSeedData.SeedOrganisations().GetAwaiter().GetResult();
    }

    private ServiceProvider CreateNewServiceProvider()
    {
        var serviceDirectoryConnection = $"Data Source=sd-{Interlocked.Increment(ref _dbSuffix)}.db;Mode=ReadWriteCreate;Cache=Shared;Foreign Keys=True;Recursive Triggers=True;Default Timeout=30;Pooling=True";

        var auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(_httpContextAccessor);

        var inMemorySettings = new Dictionary<string, string?> {
            {"UseSqlite", "true"}
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        return new ServiceCollection().AddEntityFrameworkSqlite()
            .AddDbContext<ApplicationDbContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseLoggerFactory(TestLoggerFactory);
                dbContextOptionsBuilder.UseSqlite(serviceDirectoryConnection, opt =>
                {
                    opt.UseNetTopologySuite().MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString());
                });
            })
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton(auditableEntitySaveChangesInterceptor)
            .AddAutoMapper((serviceProvider, cfg) =>
            {
                var auditProperties = ConfigAction;
                cfg.AddProfile<AutoMappingProfiles>();
                cfg.AddCollectionMappers();
                cfg.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
                cfg.ShouldMapProperty = pi => !auditProperties.Contains(pi.Name);
            }, typeof(AutoMappingProfiles))
            .BuildServiceProvider();
    }

    protected Organisation CreateChildOrganisation(Organisation parent)
    {
        var child = new Organisation 
        { 
            AdminAreaCode = parent.AdminAreaCode,
            AssociatedOrganisationId = parent.Id,
            Description = _fixtureObjectGenerator.Create<string>(),
            Name = _fixtureObjectGenerator.Create<string>(),
            OrganisationType = Shared.Enums.OrganisationType.VCFS,
            Id = _fixtureObjectGenerator.Create<long>()
        };

        return child;
    }

    protected static IHttpContextAccessor GetMockHttpContextAccessor(long organisationId, string userRole)
    {
        var mockUser = Substitute.For<ClaimsPrincipal>();
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()),
            new(FamilyHubsClaimTypes.Role, userRole)
        };

        mockUser.Claims.Returns(claims);

        var mockHttpContext = Substitute.For<HttpContext>();
        mockHttpContext.User.Returns(mockUser);

        var mockHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        mockHttpContextAccessor.HttpContext.Returns(mockHttpContext);

        return mockHttpContextAccessor;
    }

    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        await TestDbContext.Database.EnsureDeletedAsync();
    }
}
