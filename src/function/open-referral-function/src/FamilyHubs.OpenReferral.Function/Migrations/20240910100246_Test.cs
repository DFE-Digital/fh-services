using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.OpenReferral.Function.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deds");

            migrationBuilder.CreateTable(
                name: "MetaTableDescription",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Taxonomy",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Location",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Location_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationIdentifier",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentifierScheme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdentifierType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Identifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationIdentifier", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_OrganizationIdentifier_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Program",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Program_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxonomyTerm",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_TaxonomyTerm_Taxonomy_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalSchema: "deds",
                        principalTable: "Taxonomy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accessibility",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessibility", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Accessibility_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Address_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Service_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostOption",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_CostOption_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funding",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funding", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Funding_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funding_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocument",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Document = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocument", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_RequiredDocument_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceArea",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ServiceArea_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAtLocation",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAtLocation", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ServiceAtLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceAtLocation_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationOrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceOrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationOrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationOrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Contact_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Schedule_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Phone_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contact",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phone_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phone_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phone_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phone_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceOrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Language_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Language_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phone",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Language_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkEntity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccessibilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FundingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MetaTableDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequiredDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Attribute_Accessibility_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalSchema: "deds",
                        principalTable: "Accessibility",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "deds",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contact",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_CostOption_CostOptionId",
                        column: x => x.CostOptionId,
                        principalSchema: "deds",
                        principalTable: "CostOption",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Funding_FundingId",
                        column: x => x.FundingId,
                        principalSchema: "deds",
                        principalTable: "Funding",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "deds",
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_MetaTableDescription_MetaTableDescriptionId",
                        column: x => x.MetaTableDescriptionId,
                        principalSchema: "deds",
                        principalTable: "MetaTableDescription",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_OrganizationIdentifier_OrganizationIdentifierId",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "deds",
                        principalTable: "OrganizationIdentifier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phone",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Program",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_RequiredDocument_RequiredDocumentId",
                        column: x => x.RequiredDocumentId,
                        principalSchema: "deds",
                        principalTable: "RequiredDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "deds",
                        principalTable: "Schedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_ServiceArea_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalSchema: "deds",
                        principalTable: "ServiceArea",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attribute_TaxonomyTerm_TaxonomyTermId",
                        column: x => x.TaxonomyTermId,
                        principalSchema: "deds",
                        principalTable: "TaxonomyTerm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastActionDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastActionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreviousValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacementValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccessibilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FundingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MetaTableDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequiredDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxonomyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Metadata_Accessibility_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalSchema: "deds",
                        principalTable: "Accessibility",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "deds",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contact",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_CostOption_CostOptionId",
                        column: x => x.CostOptionId,
                        principalSchema: "deds",
                        principalTable: "CostOption",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Funding_FundingId",
                        column: x => x.FundingId,
                        principalSchema: "deds",
                        principalTable: "Funding",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "deds",
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_MetaTableDescription_MetaTableDescriptionId",
                        column: x => x.MetaTableDescriptionId,
                        principalSchema: "deds",
                        principalTable: "MetaTableDescription",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_OrganizationIdentifier_OrganizationIdentifierId",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "deds",
                        principalTable: "OrganizationIdentifier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phone",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Program",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_RequiredDocument_RequiredDocumentId",
                        column: x => x.RequiredDocumentId,
                        principalSchema: "deds",
                        principalTable: "RequiredDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "deds",
                        principalTable: "Schedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_ServiceArea_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalSchema: "deds",
                        principalTable: "ServiceArea",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_TaxonomyTerm_TaxonomyTermId",
                        column: x => x.TaxonomyTermId,
                        principalSchema: "deds",
                        principalTable: "TaxonomyTerm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Metadata_Taxonomy_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalSchema: "deds",
                        principalTable: "Taxonomy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessibility_LocationId",
                schema: "deds",
                table: "Accessibility",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LocationId",
                schema: "deds",
                table: "Address",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AccessibilityId",
                schema: "deds",
                table: "Attribute",
                column: "AccessibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_AddressId",
                schema: "deds",
                table: "Attribute",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ContactId",
                schema: "deds",
                table: "Attribute",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CostOptionId",
                schema: "deds",
                table: "Attribute",
                column: "CostOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_FundingId",
                schema: "deds",
                table: "Attribute",
                column: "FundingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_LanguageId",
                schema: "deds",
                table: "Attribute",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_LocationId",
                schema: "deds",
                table: "Attribute",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_MetaTableDescriptionId",
                schema: "deds",
                table: "Attribute",
                column: "MetaTableDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_OrganizationId",
                schema: "deds",
                table: "Attribute",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_OrganizationIdentifierId",
                schema: "deds",
                table: "Attribute",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_PhoneId",
                schema: "deds",
                table: "Attribute",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ProgramId",
                schema: "deds",
                table: "Attribute",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_RequiredDocumentId",
                schema: "deds",
                table: "Attribute",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ScheduleId",
                schema: "deds",
                table: "Attribute",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ServiceAreaId",
                schema: "deds",
                table: "Attribute",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ServiceAtLocationId",
                schema: "deds",
                table: "Attribute",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ServiceId",
                schema: "deds",
                table: "Attribute",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                column: "TaxonomyTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LocationId",
                schema: "deds",
                table: "Contact",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_OrganizationId",
                schema: "deds",
                table: "Contact",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ServiceAtLocationId",
                schema: "deds",
                table: "Contact",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ServiceId",
                schema: "deds",
                table: "Contact",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOption_ServiceId",
                schema: "deds",
                table: "CostOption",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Funding_OrganizationId",
                schema: "deds",
                table: "Funding",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Funding_ServiceId",
                schema: "deds",
                table: "Funding",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_LocationId",
                schema: "deds",
                table: "Language",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_PhoneId",
                schema: "deds",
                table: "Language",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_ServiceId",
                schema: "deds",
                table: "Language",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OrganizationId",
                schema: "deds",
                table: "Location",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_AccessibilityId",
                schema: "deds",
                table: "Metadata",
                column: "AccessibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_AddressId",
                schema: "deds",
                table: "Metadata",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_AttributeId",
                schema: "deds",
                table: "Metadata",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_ContactId",
                schema: "deds",
                table: "Metadata",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_CostOptionId",
                schema: "deds",
                table: "Metadata",
                column: "CostOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_FundingId",
                schema: "deds",
                table: "Metadata",
                column: "FundingId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_LanguageId",
                schema: "deds",
                table: "Metadata",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_LocationId",
                schema: "deds",
                table: "Metadata",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_MetaTableDescriptionId",
                schema: "deds",
                table: "Metadata",
                column: "MetaTableDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_OrganizationId",
                schema: "deds",
                table: "Metadata",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_OrganizationIdentifierId",
                schema: "deds",
                table: "Metadata",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_PhoneId",
                schema: "deds",
                table: "Metadata",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_ProgramId",
                schema: "deds",
                table: "Metadata",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_RequiredDocumentId",
                schema: "deds",
                table: "Metadata",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_ScheduleId",
                schema: "deds",
                table: "Metadata",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_ServiceAreaId",
                schema: "deds",
                table: "Metadata",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_ServiceAtLocationId",
                schema: "deds",
                table: "Metadata",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_ServiceId",
                schema: "deds",
                table: "Metadata",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_TaxonomyId",
                schema: "deds",
                table: "Metadata",
                column: "TaxonomyId");

            migrationBuilder.CreateIndex(
                name: "IX_Metadata_TaxonomyTermId",
                schema: "deds",
                table: "Metadata",
                column: "TaxonomyTermId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationIdentifier_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifier",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ContactId",
                schema: "deds",
                table: "Phone",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_LocationId",
                schema: "deds",
                table: "Phone",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_OrganizationId",
                schema: "deds",
                table: "Phone",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ServiceAtLocationId",
                schema: "deds",
                table: "Phone",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ServiceId",
                schema: "deds",
                table: "Phone",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_OrganizationId",
                schema: "deds",
                table: "Program",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocument_ServiceId",
                schema: "deds",
                table: "RequiredDocument",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_LocationId",
                schema: "deds",
                table: "Schedule",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ServiceAtLocationId",
                schema: "deds",
                table: "Schedule",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ServiceId",
                schema: "deds",
                table: "Schedule",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_OrganizationId",
                schema: "deds",
                table: "Service",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ProgramId",
                schema: "deds",
                table: "Service",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceArea_ServiceId",
                schema: "deds",
                table: "ServiceArea",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAtLocation_LocationId",
                schema: "deds",
                table: "ServiceAtLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAtLocation_ServiceId",
                schema: "deds",
                table: "ServiceAtLocation",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyTerm_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerm",
                column: "TaxonomyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metadata",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Accessibility",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Address",
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
                name: "MetaTableDescription",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "OrganizationIdentifier",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "RequiredDocument",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Schedule",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ServiceArea",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "TaxonomyTerm",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Phone",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Taxonomy",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ServiceAtLocation",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Program",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "deds");
        }
    }
}
