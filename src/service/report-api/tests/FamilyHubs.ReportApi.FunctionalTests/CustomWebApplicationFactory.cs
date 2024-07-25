using System.Globalization;
using FamilyHubs.Report.Api;
using FamilyHubs.Report.Data.Entities;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyHubs.ReportApi.FunctionalTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection = new("DataSource=:memory:");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor? context =
                services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ReportDbContext));

            /*
             * Since the functional tests execute Program.cs this means they initialise the MS SQL Server Db Context
             * in declared in there, which is undesired behaviour.
             *
             * To resolve this, this block of code finds all references to any Db Context and removes them all, which
             * means that we can then add the in-memory SQLite version of the Db Context declared below to run our tests against.
             */
            if (context != null)
            {
                services.Remove(context);
                ServiceDescriptor[] options = services.Where(r =>
                    (r.ServiceType == typeof(DbContextOptions)) || (r.ServiceType.IsGenericType &&
                                                                    r.ServiceType.GetGenericTypeDefinition() ==
                                                                    typeof(DbContextOptions<>))).ToArray();

                foreach (ServiceDescriptor option in options) services.Remove(option);
            }

            services.AddDbContext<ReportDbContext>(options =>
            {
                _connection.Open();

                options.UseSqlite(_connection)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            });
        });

        builder.UseEnvironment("Development");
    }

    public async Task CreateAndSeedDatabase()
    {
        using IServiceScope serviceScope = Services.CreateScope();
        ReportDbContext reportDbContext = serviceScope.ServiceProvider.GetRequiredService<ReportDbContext>();

        await reportDbContext.Database.EnsureCreatedAsync();

        await SeedDateDimTable(reportDbContext);
        await SeedTimeDimTable(reportDbContext);
        await SeedServiceSearchesDimTable(reportDbContext);
        await SeedOrganisationDimTable(reportDbContext);
        await SeedServiceSearchFactTable(reportDbContext);
        await SeedConnectionRequestsSentFact(reportDbContext);
    }

    private static async Task SeedDateDimTable(IReportDbContext reportDbContext)
    {
        DateTime currentDate = DateTime.Parse("2024-01-01");
        DateTime endDate = DateTime.Parse("2024-03-31");

        CultureInfo cultureInfo = new CultureInfo("en-GB");

        while (currentDate <= endDate)
        {
            DateDim dateDim = new()
            {
                Date = currentDate,
                DateString = currentDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                DayNumberOfWeek = (byte)currentDate.DayOfWeek,
                DayOfWeekName = currentDate.DayOfWeek.ToString(),
                DayNumberOfMonth = (byte)currentDate.Day,
                DayNumberOfYear = (short)currentDate.DayOfYear,
                WeekNumberOfYear = (byte)ISOWeek.GetWeekOfYear(currentDate),
                MonthName = cultureInfo.DateTimeFormat.GetMonthName(currentDate.Month),
                MonthNumberOfYear = (byte)currentDate.Month,
                CalendarQuarterNumberOfYear = (byte)((currentDate.Month + 2) / 3),
                CalendarYearNumber = (short)currentDate.Year,
                IsWeekend = currentDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday,
                IsLeapYear = DateTime.IsLeapYear(currentDate.Year)
            };

            reportDbContext.AddDateDim(dateDim);

            currentDate = currentDate.AddDays(1);
        }

        await reportDbContext.SaveChangesAsync();
    }

    private static async Task SeedTimeDimTable(IReportDbContext reportDbContext)
    {
        TimeSpan currentTime = DateTime.Parse("2024-01-01").Date.TimeOfDay;

        TimeDim timeDim = new()
        {
            Time = currentTime,
            TimeString = currentTime.ToString(@"hh\:mm\:ss"),
            HourNumberOfDay = (byte)currentTime.Hours,
            MinuteNumberOfHour = (byte)currentTime.Minutes,
            SecondNumberOfMinute = (byte)currentTime.Seconds
        };

        reportDbContext.AddTimeDim(timeDim);

        await reportDbContext.SaveChangesAsync();
    }

    private static async Task SeedServiceSearchesDimTable(IReportDbContext reportDbContext)
    {
        ServiceSearchesDim serviceSearchesDimOne = new()
        {
            ServiceSearchId = 1,
            ServiceTypeId = (byte)ServiceType.InformationSharing,
            ServiceTypeName = "Find",
            EventId = 1,
            EventName = "Event",
            OrganisationId = 1,
            PostCode = "AB1 1AB",
            SearchRadiusMiles = 4,
            HttpRequestTimestamp = DateTime.UtcNow,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow
        };

        ServiceSearchesDim serviceSearchesDimTwo = new()
        {
            ServiceSearchId = 2,
            ServiceTypeId = (byte)ServiceType.InformationSharing,
            ServiceTypeName = "Find",
            EventId = 1,
            EventName = "Event",
            OrganisationId = 2,
            PostCode = "AB1 1AB",
            SearchRadiusMiles = 4,
            HttpRequestTimestamp = DateTime.UtcNow,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow
        };

        reportDbContext.AddServiceSearchesDim(serviceSearchesDimOne);
        reportDbContext.AddServiceSearchesDim(serviceSearchesDimTwo);

        await reportDbContext.SaveChangesAsync();
    }

    private static async Task SeedOrganisationDimTable(IReportDbContext reportDbContext)
    {
        OrganisationDim organisationDimOne = new()
        {
            OrganisationKey = 1,
            OrganisationTypeId = byte.MaxValue,
            OrganisationTypeName = "",
            OrganisationId = 10,
            OrganisationName = "",
            Created = DateTime.UtcNow,
            CreatedBy = "",
            Modified = DateTime.UtcNow,
            ModifiedBy = ""
        };

        OrganisationDim organisationDimTwo = new()
        {
            OrganisationKey = 2,
            OrganisationTypeId = byte.MaxValue,
            OrganisationTypeName = "",
            OrganisationId = 20,
            OrganisationName = "",
            Created = DateTime.UtcNow,
            CreatedBy = "",
            Modified = DateTime.UtcNow,
            ModifiedBy = ""
        };

        reportDbContext.AddOrganisationDim(organisationDimOne);
        reportDbContext.AddOrganisationDim(organisationDimTwo);

        await reportDbContext.SaveChangesAsync();
    }

    private static async Task SeedServiceSearchFactTable(IReportDbContext reportDbContext)
    {
        long serviceSearchFactId = 1;
        int dateKey = 1;
        long serviceSearchId = 1;

        // January
        for (int i = 1; i <= 31; i++)
        {
            reportDbContext.AddServiceSearchFact(new ServiceSearchFact
            {
                Id = serviceSearchFactId++,
                DateKey = dateKey++,
                TimeKey = 1,
                ServiceSearchesKey = 1,
                ServiceSearchId = serviceSearchId++,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            });
        }

        // February
        for (int i = 1; i <= 29; i++)
        {
            reportDbContext.AddServiceSearchFact(new ServiceSearchFact
            {
                Id = serviceSearchFactId++,
                DateKey = dateKey++,
                TimeKey = 1,
                ServiceSearchesKey = 2,
                ServiceSearchId = serviceSearchId++,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            });
        }

        await reportDbContext.SaveChangesAsync();
    }

    private static async Task SeedConnectionRequestsSentFact(IReportDbContext reportDbContext)
    {
        int connectionRequestsSentFactId = 1;
        int dateKey = 1;

        // January
        for (int i = 1; i <= 31; i++)
        {
            reportDbContext.AddConnectionRequestsSentFact(new ConnectionRequestsSentFact
            {
                Id = connectionRequestsSentFactId++,
                DateKey = dateKey++,
                TimeKey = 1,
                OrganisationKey = 1,
                ConnectionRequestsSentMetricsId = 0,
                RequestTimestamp = DateTime.UtcNow,
                RequestCorrelationId = "",
                VcsOrganisationId = 32,
                Created = DateTime.UtcNow,
                CreatedBy = "",
                Modified = DateTime.UtcNow,
                ModifiedBy = ""
            });
        }

        // February
        for (int i = 1; i <= 29; i++)
        {
            reportDbContext.AddConnectionRequestsSentFact(new ConnectionRequestsSentFact
            {
                Id = connectionRequestsSentFactId++,
                DateKey = dateKey++,
                TimeKey = 1,
                OrganisationKey = 2,
                ConnectionRequestsSentMetricsId = 0,
                RequestTimestamp = DateTime.UtcNow,
                RequestCorrelationId = "",
                VcsOrganisationId = 64,
                Created = DateTime.UtcNow,
                CreatedBy = "",
                Modified = DateTime.UtcNow,
                ModifiedBy = ""
            });
        }

        await reportDbContext.SaveChangesAsync();
    }

    public new void Dispose()
    {
        _connection.Close();
        _connection.Dispose();

        base.Dispose();
    }
}
