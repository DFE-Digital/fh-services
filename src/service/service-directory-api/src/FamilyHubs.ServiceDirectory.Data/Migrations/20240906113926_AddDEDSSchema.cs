using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDEDSSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deds");

            migrationBuilder.CreateTable(
                name: "Accessibility",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessibility", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attention = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkEntity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Department = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "CostOption",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: true),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nchar(3)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AmountDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOption", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Funding",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funding", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationType = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transportation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExternalIdentifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExternalIdentifierType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastActionDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastActionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreviousValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacementValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "MetaTableDescription",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CharacterSet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaTableDescription", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    YearIncorporated = table.Column<short>(type: "smallint", nullable: false),
                    LegalStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ParentOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationIdentifier",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentifierScheme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdentifierType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Identifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationIdentifier", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Extension = table.Column<short>(type: "smallint", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocument",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Document = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocument", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: true),
                    Dtstart = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    Timezone = table.Column<byte>(type: "tinyint", nullable: true),
                    Until = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    Count = table.Column<short>(type: "smallint", nullable: true),
                    Wkst = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Freq = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Interval = table.Column<short>(type: "smallint", nullable: true),
                    Byday = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Byweekno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Bymonthday = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Byyearday = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpensAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    ClosesAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    ScheduleLink = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    AttendingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InterpretationServices = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ApplicationProcess = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FeesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accreditations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumAge = table.Column<byte>(type: "tinyint", nullable: false),
                    MaximumAge = table.Column<byte>(type: "tinyint", nullable: false),
                    AssuredDate = table.Column<DateTime>(type: "date", nullable: true),
                    AssurerEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Alert = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "ServiceArea",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExtentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceArea", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAtLocation",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAtLocation", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TaxonomyTerm",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxonomyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TermUri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxonomyTerm", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessibility",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "CostOption",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Funding",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Metadata",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "MetaTableDescription",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "OrganizationIdentifier",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Phone",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Program",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "RequiredDocument",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Schedule",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ServiceArea",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ServiceAtLocation",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Taxonomy",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "TaxonomyTerm",
                schema: "deds");
        }
    }
}
