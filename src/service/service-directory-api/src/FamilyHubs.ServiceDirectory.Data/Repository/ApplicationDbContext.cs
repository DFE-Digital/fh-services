using System.Reflection;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Data.Entities.ManyToMany;
using FamilyHubs.ServiceDirectory.Data.Interceptors;
using FamilyHubs.SharedKernel.OpenReferral.Repository;
using Microsoft.EntityFrameworkCore;
using Enums = FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.ServiceDirectory.Data.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Service>()
                .HasIndex(e => new { e.OrganisationId, e.Id })
                .IsUnique(false)
                .HasDatabaseName("IX_Services_OrganisationId_Id")
                .IncludeProperties(e => new { e.ServiceType, e.Status });

            modelBuilder.Entity<Contact>()
                .HasIndex(e => e.ServiceId)
                .IsUnique(false)
                .HasDatabaseName("IX_Contacts_ServiceId_Id")
                .IncludeProperties(e => new { e.Id, e.Title, e.Name, e.Telephone, e.TextPhone, e.Url, e.Email });

            modelBuilder.Entity<Service>()
                .HasIndex(e => new { e.ServiceType, e.Id, e.OrganisationId, e.Status })
                .IsUnique(false)
                .HasDatabaseName("IX_ServiceType_OrganisationId_Status");

            modelBuilder.Entity<ServiceDelivery>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.ServiceId, e.Id })
                    .HasDatabaseName("IX_ServiceDeliveryNonClustered")
                    .IsUnique(false)
                    .IsClustered(false);
            });

            modelBuilder.Entity<ServiceSearch>()
                .ToTable("ServiceSearches")
                .HasKey(e => e.Id);

            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.SearchTriggerEventId)
                .HasConversion<short>();
            
            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.ServiceSearchTypeId)
                .HasConversion<byte>();

            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.SearchPostcode)
                .HasMaxLength(10);
            
            modelBuilder.Entity<ServiceSearch>()
                .HasOne(e => e.SearchTriggerEvent)
                .WithMany(e => e.ServiceSearches)
                .HasForeignKey(e => e.SearchTriggerEventId)
                .IsRequired(false);
            
            modelBuilder.Entity<ServiceSearch>()
                .HasOne(e => e.ServiceSearchType)
                .WithMany(e => e.ServiceSearches)
                .HasForeignKey(e => e.ServiceSearchTypeId)
                .IsRequired();
            
            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.CorrelationId)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceSearchResult>()
                .ToTable("ServiceSearchResults")
                .HasKey(e => e.Id);

            modelBuilder.Entity<ServiceSearchResult>()
                .HasOne(e => e.Service)
                .WithMany(e => e.ServiceSearchResults)
                .HasForeignKey(e => e.ServiceId)
                // Do not delete metrics if service deleted
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .ToTable("Events")
                .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
                .Property(e => e.Id)
                .HasConversion<short>();
            
            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .HasMaxLength(100);
            
            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Event>()
                .HasData(
                    new()
                    {
                        Id = Enums.ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch,
                        Name = nameof(Enums.ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch),
                        Description = "Describes an initial, unfiltered search by a user."
                    },
                    new()
                    {
                        Id = Enums.ServiceDirectorySearchEventType.ServiceDirectorySearchFilter,
                        Name = nameof(Enums.ServiceDirectorySearchEventType.ServiceDirectorySearchFilter),
                        Description = "Describes a filtered search by a user."
                    }
                );

            modelBuilder.Entity<ServiceType>()
                .ToTable("ServiceTypes")
                .HasKey(e => e. Id);

            modelBuilder.Entity<ServiceType>()
                .Property(e => e.Id)
                .HasConversion<byte>();
            
            modelBuilder.Entity<ServiceType>()
                .Property(e => e.Name)
                .HasMaxLength(50);
            
            modelBuilder.Entity<ServiceType>()
                .Property(e => e.Description)
                .HasMaxLength(255);

            modelBuilder.Entity<ServiceType>()
                .HasData(
                    new ServiceType
                    {
                        Id = Enums.ServiceType.FamilyExperience,
                        Name = nameof(Enums.ServiceType.FamilyExperience),
                        Description = "Find"
                    },
                    new ServiceType 
                    {
                        Id = Enums.ServiceType.InformationSharing,
                        Name = nameof(Enums.ServiceType.InformationSharing),
                        Description = "Connect"
                    }
                );

            /*
             * This needs to be set so the Functional & Integration Tests don't create the [deds] and [dedsmeta] tables.
             *
             * This is because SQLite doesn't implement schemas, so there are conflicts with the [dbo] tables as they
             * have the same names. Since we don't test anything OR related from the Service Directory API anyway, it
             * can just be disabled.
             */
            if (!Database.IsSqlite())
            {
                OpenReferralDbContextExtension.OnModelCreating(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (System.Diagnostics.Debugger.IsAttached) optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public DbSet<AccessibilityForDisabilities> AccessibilityForDisabilities => Set<AccessibilityForDisabilities>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<CostOption> CostOptions => Set<CostOption>();
        public DbSet<Eligibility> Eligibilities => Set<Eligibility>();
        public DbSet<Funding> Fundings => Set<Funding>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Organisation> Organisations => Set<Organisation>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<ServiceAtLocation> ServiceAtLocations => Set<ServiceAtLocation>();
        public DbSet<ServiceArea> ServiceAreas => Set<ServiceArea>();
        public DbSet<ServiceDelivery> ServiceDeliveries => Set<ServiceDelivery>();
        public DbSet<Taxonomy> Taxonomies => Set<Taxonomy>();
        public DbSet<ServiceSearch> ServiceSearches => Set<ServiceSearch>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<ServiceSearchResult> ServiceSearchResult => Set<ServiceSearchResult>();
        public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    }
}
