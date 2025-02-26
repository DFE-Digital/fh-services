using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldDedsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessibilityAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AccessibilityMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AddressAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AddressMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ContactAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ContactMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "CostOptionAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "CostOptionMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "FundingAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "FundingMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "LanguageAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "LanguageMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "LocationAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "LocationMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetaTableDescriptionAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetaTableDescriptionMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "OrganizationAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "OrganizationIdentifierAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "OrganizationIdentifierMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "OrganizationMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "PhoneAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "PhoneMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ProgramAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ProgramMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "RequiredDocumentAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "RequiredDocumentMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ScheduleAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ScheduleMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ServiceAreaAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ServiceAreaMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ServiceAtLocationAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ServiceAtLocationMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ServiceAttributes",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ServiceMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "TaxonomyMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "TaxonomyTermMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "Accessibilities",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "CostOptions",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Fundings",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "MetaTableDescriptions",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "OrganizationIdentifiers",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "RequiredDocuments",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ServiceAreas",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Attributes",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Metadata",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Phones",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "TaxonomyTerms",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Taxonomies",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ServiceAtLocations",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Programs",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "deds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dedsmeta");

            migrationBuilder.CreateTable(
                name: "Metadata",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastActionDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastActionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacementValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "MetaTableDescriptions",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterSet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaTableDescriptions", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LegalStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    YearIncorporated = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomies",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomies", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "MetaTableDescriptionMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetaTableDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaTableDescriptionMetadata", x => new { x.MetaTableDescriptionId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_MetaTableDescriptionMetadata_MetaTableDescriptions_MetaTableDescriptionId",
                        column: x => x.MetaTableDescriptionId,
                        principalSchema: "deds",
                        principalTable: "MetaTableDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetaTableDescriptionMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalIdentifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExternalIdentifierType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    LocationType = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Transportation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Locations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationIdentifiers",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdentifierScheme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdentifierType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationIdentifiers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_OrganizationIdentifiers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationMetadata", x => new { x.MetadataId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationMetadata_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Programs_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxonomyMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxonomyMetadata", x => new { x.MetadataId, x.TaxonomyId });
                    table.ForeignKey(
                        name: "FK_TaxonomyMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxonomyMetadata_Taxonomies_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalSchema: "deds",
                        principalTable: "Taxonomies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxonomyTerms",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TermUri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxonomyTerms", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_TaxonomyTerms_Taxonomies_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalSchema: "deds",
                        principalTable: "Taxonomies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accessibilities",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessibilities", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Accessibilities_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Attention = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Addresses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocationMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationMetadata", x => new { x.LocationId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_LocationMetadata_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationIdentifierMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationIdentifierMetadata", x => new { x.MetadataId, x.OrganizationIdentifierId });
                    table.ForeignKey(
                        name: "FK_OrganizationIdentifierMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationIdentifierMetadata_OrganizationIdentifiers_OrganizationIdentifierId",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "deds",
                        principalTable: "OrganizationIdentifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramMetadata", x => new { x.MetadataId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_ProgramMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramMetadata_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Accreditations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alert = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ApplicationProcess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssuredDate = table.Column<DateTime>(type: "date", nullable: true),
                    AssurerEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Checksum = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FeesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterpretationServices = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: true),
                    MaximumAge = table.Column<byte>(type: "tinyint", nullable: true),
                    MinimumAge = table.Column<byte>(type: "tinyint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Services_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Programs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LinkEntity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LinkType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Attributes_TaxonomyTerms_TaxonomyTermId",
                        column: x => x.TaxonomyTermId,
                        principalSchema: "deds",
                        principalTable: "TaxonomyTerms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxonomyTermMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxonomyTermMetadata", x => new { x.MetadataId, x.TaxonomyTermId });
                    table.ForeignKey(
                        name: "FK_TaxonomyTermMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxonomyTermMetadata_TaxonomyTerms_TaxonomyTermId",
                        column: x => x.TaxonomyTermId,
                        principalSchema: "deds",
                        principalTable: "TaxonomyTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessibilityMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    AccessibilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilityMetadata", x => new { x.AccessibilityId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_AccessibilityMetadata_Accessibilities_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalSchema: "deds",
                        principalTable: "Accessibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessibilityMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressMetadata", x => new { x.AddressId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_AddressMetadata_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "deds",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostOptions",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AmountDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nchar(3)", nullable: true),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOptions", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_CostOptions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fundings",
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
                    table.PrimaryKey("PK_Fundings", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Fundings_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fundings_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocuments",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocuments", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_RequiredDocuments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAreas",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExtentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAreas", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ServiceAreas_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAtLocations",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAtLocations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ServiceAtLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceAtLocations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceMetadata", x => new { x.MetadataId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceMetadata_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessibilityAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AccessibilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilityAttributes", x => new { x.AccessibilityId, x.AttributesId });
                    table.ForeignKey(
                        name: "FK_AccessibilityAttributes_Accessibilities_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalSchema: "deds",
                        principalTable: "Accessibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessibilityAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressAttributes", x => new { x.AddressId, x.AttributesId });
                    table.ForeignKey(
                        name: "FK_AddressAttributes_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "deds",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeMetadata", x => new { x.AttributeId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_AttributeMetadata_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAttributes", x => new { x.AttributesId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_LocationAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationAttributes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetaTableDescriptionAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTableDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaTableDescriptionAttributes", x => new { x.AttributesId, x.MetaTableDescriptionId });
                    table.ForeignKey(
                        name: "FK_MetaTableDescriptionAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetaTableDescriptionAttributes_MetaTableDescriptions_MetaTableDescriptionId",
                        column: x => x.MetaTableDescriptionId,
                        principalSchema: "deds",
                        principalTable: "MetaTableDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAttributes", x => new { x.AttributesId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_OrganizationAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationAttributes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationIdentifierAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationIdentifierAttributes", x => new { x.AttributesId, x.OrganizationIdentifierId });
                    table.ForeignKey(
                        name: "FK_OrganizationIdentifierAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationIdentifierAttributes_OrganizationIdentifiers_OrganizationIdentifierId",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "deds",
                        principalTable: "OrganizationIdentifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramAttributes", x => new { x.AttributesId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_ProgramAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramAttributes_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAttributes", x => new { x.AttributesId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAttributes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostOptionAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOptionAttributes", x => new { x.AttributesId, x.CostOptionId });
                    table.ForeignKey(
                        name: "FK_CostOptionAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostOptionAttributes_CostOptions_CostOptionId",
                        column: x => x.CostOptionId,
                        principalSchema: "deds",
                        principalTable: "CostOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostOptionMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    CostOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOptionMetadata", x => new { x.CostOptionId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_CostOptionMetadata_CostOptions_CostOptionId",
                        column: x => x.CostOptionId,
                        principalSchema: "deds",
                        principalTable: "CostOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostOptionMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FundingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingAttributes", x => new { x.AttributesId, x.FundingId });
                    table.ForeignKey(
                        name: "FK_FundingAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingAttributes_Fundings_FundingId",
                        column: x => x.FundingId,
                        principalSchema: "deds",
                        principalTable: "Fundings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    FundingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingMetadata", x => new { x.FundingId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_FundingMetadata_Fundings_FundingId",
                        column: x => x.FundingId,
                        principalSchema: "deds",
                        principalTable: "Fundings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocumentAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocumentAttributes", x => new { x.AttributesId, x.RequiredDocumentId });
                    table.ForeignKey(
                        name: "FK_RequiredDocumentAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredDocumentAttributes_RequiredDocuments_RequiredDocumentId",
                        column: x => x.RequiredDocumentId,
                        principalSchema: "deds",
                        principalTable: "RequiredDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocumentMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocumentMetadata", x => new { x.MetadataId, x.RequiredDocumentId });
                    table.ForeignKey(
                        name: "FK_RequiredDocumentMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredDocumentMetadata_RequiredDocuments_RequiredDocumentId",
                        column: x => x.RequiredDocumentId,
                        principalSchema: "deds",
                        principalTable: "RequiredDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAreaAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAreaAttributes", x => new { x.AttributesId, x.ServiceAreaId });
                    table.ForeignKey(
                        name: "FK_ServiceAreaAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAreaAttributes_ServiceAreas_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalSchema: "deds",
                        principalTable: "ServiceAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAreaMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAreaMetadata", x => new { x.MetadataId, x.ServiceAreaId });
                    table.ForeignKey(
                        name: "FK_ServiceAreaMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAreaMetadata_ServiceAreas_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalSchema: "deds",
                        principalTable: "ServiceAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Contacts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_ServiceAtLocations_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contacts_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ByDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ByMonthDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ByWeekNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ByYearDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ClosesAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    Count = table.Column<short>(type: "smallint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtStart = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    Freq = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Interval = table.Column<short>(type: "smallint", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpensAt = table.Column<TimeSpan>(type: "time", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleLink = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Timezone = table.Column<byte>(type: "tinyint", nullable: true),
                    Until = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: true),
                    Wkst = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Schedules_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_ServiceAtLocations_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceAtLocationAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAtLocationAttributes", x => new { x.AttributesId, x.ServiceAtLocationId });
                    table.ForeignKey(
                        name: "FK_ServiceAtLocationAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAtLocationAttributes_ServiceAtLocations_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAtLocationMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAtLocationMetadata", x => new { x.MetadataId, x.ServiceAtLocationId });
                    table.ForeignKey(
                        name: "FK_ServiceAtLocationMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAtLocationMetadata_ServiceAtLocations_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAttributes", x => new { x.AttributesId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_ContactAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactAttributes_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMetadata", x => new { x.ContactId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_ContactMetadata_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<short>(type: "smallint", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Phones_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phones_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phones_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phones_ServiceAtLocations_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phones_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleAttributes", x => new { x.AttributesId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_ScheduleAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleAttributes_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "deds",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMetadata", x => new { x.MetadataId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_ScheduleMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleMetadata_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "deds",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Languages_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Languages_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Languages_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhoneAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneAttributes", x => new { x.AttributesId, x.PhoneId });
                    table.ForeignKey(
                        name: "FK_PhoneAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneAttributes_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneMetadata", x => new { x.MetadataId, x.PhoneId });
                    table.ForeignKey(
                        name: "FK_PhoneMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneMetadata_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageAttributes",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageAttributes", x => new { x.AttributesId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_LanguageAttributes_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageAttributes_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "deds",
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageMetadata",
                schema: "dedsmeta",
                columns: table => new
                {
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageMetadata", x => new { x.LanguageId, x.MetadataId });
                    table.ForeignKey(
                        name: "FK_LanguageMetadata_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "deds",
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageMetadata_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessibilities_LocationId",
                schema: "deds",
                table: "Accessibilities",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityAttributes_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttributes",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityMetadata_MetadataId",
                schema: "dedsmeta",
                table: "AccessibilityMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressAttributes_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttributes",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_LocationId",
                schema: "deds",
                table: "Addresses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMetadata_MetadataId",
                schema: "dedsmeta",
                table: "AddressMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeMetadata_MetadataId",
                schema: "dedsmeta",
                table: "AttributeMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_TaxonomyTermId",
                schema: "deds",
                table: "Attributes",
                column: "TaxonomyTermId",
                unique: true,
                filter: "[TaxonomyTermId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAttributes_ContactId",
                schema: "dedsmeta",
                table: "ContactAttributes",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMetadata_MetadataId",
                schema: "dedsmeta",
                table: "ContactMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_LocationId",
                schema: "deds",
                table: "Contacts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OrganizationId",
                schema: "deds",
                table: "Contacts",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ServiceAtLocationId",
                schema: "deds",
                table: "Contacts",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ServiceId",
                schema: "deds",
                table: "Contacts",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOptionAttributes_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionAttributes",
                column: "CostOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOptionMetadata_MetadataId",
                schema: "dedsmeta",
                table: "CostOptionMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOptions_ServiceId",
                schema: "deds",
                table: "CostOptions",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FundingAttributes_FundingId",
                schema: "dedsmeta",
                table: "FundingAttributes",
                column: "FundingId");

            migrationBuilder.CreateIndex(
                name: "IX_FundingMetadata_MetadataId",
                schema: "dedsmeta",
                table: "FundingMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Fundings_OrganizationId",
                schema: "deds",
                table: "Fundings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fundings_ServiceId",
                schema: "deds",
                table: "Fundings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageAttributes_LanguageId",
                schema: "dedsmeta",
                table: "LanguageAttributes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageMetadata_MetadataId",
                schema: "dedsmeta",
                table: "LanguageMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LocationId",
                schema: "deds",
                table: "Languages",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_PhoneId",
                schema: "deds",
                table: "Languages",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ServiceId",
                schema: "deds",
                table: "Languages",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAttributes_LocationId",
                schema: "dedsmeta",
                table: "LocationAttributes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationMetadata_MetadataId",
                schema: "dedsmeta",
                table: "LocationMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_OrganizationId",
                schema: "deds",
                table: "Locations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaTableDescriptionAttributes_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes",
                column: "MetaTableDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaTableDescriptionMetadata_MetadataId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAttributes_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationAttributes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationIdentifierAttributes_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationIdentifierMetadata_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationIdentifiers_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifiers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationMetadata_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationMetadata",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneAttributes_PhoneId",
                schema: "dedsmeta",
                table: "PhoneAttributes",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneMetadata_PhoneId",
                schema: "dedsmeta",
                table: "PhoneMetadata",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ContactId",
                schema: "deds",
                table: "Phones",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_LocationId",
                schema: "deds",
                table: "Phones",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_OrganizationId",
                schema: "deds",
                table: "Phones",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ServiceAtLocationId",
                schema: "deds",
                table: "Phones",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ServiceId",
                schema: "deds",
                table: "Phones",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramAttributes_ProgramId",
                schema: "dedsmeta",
                table: "ProgramAttributes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramMetadata_ProgramId",
                schema: "dedsmeta",
                table: "ProgramMetadata",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_OrganizationId",
                schema: "deds",
                table: "Programs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocumentAttributes_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocumentMetadata_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocuments_ServiceId",
                schema: "deds",
                table: "RequiredDocuments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleAttributes_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleAttributes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMetadata_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleMetadata",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LocationId",
                schema: "deds",
                table: "Schedules",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ServiceAtLocationId",
                schema: "deds",
                table: "Schedules",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ServiceId",
                schema: "deds",
                table: "Schedules",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAreaAttributes_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAreaMetadata_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAreas_ServiceId",
                schema: "deds",
                table: "ServiceAreas",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAtLocationAttributes_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAtLocationMetadata_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAtLocations_LocationId",
                schema: "deds",
                table: "ServiceAtLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAtLocations_ServiceId",
                schema: "deds",
                table: "ServiceAtLocations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAttributes_ServiceId",
                schema: "dedsmeta",
                table: "ServiceAttributes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceMetadata_ServiceId",
                schema: "dedsmeta",
                table: "ServiceMetadata",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganizationId",
                schema: "deds",
                table: "Services",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProgramId",
                schema: "deds",
                table: "Services",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyMetadata_TaxonomyId",
                schema: "dedsmeta",
                table: "TaxonomyMetadata",
                column: "TaxonomyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyTermMetadata_TaxonomyTermId",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata",
                column: "TaxonomyTermId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyTerms_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerms",
                column: "TaxonomyId",
                unique: true,
                filter: "[TaxonomyId] IS NOT NULL");
        }
    }
}
