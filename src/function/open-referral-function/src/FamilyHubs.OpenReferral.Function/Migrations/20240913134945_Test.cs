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

            migrationBuilder.EnsureSchema(
                name: "dedsmeta");

            migrationBuilder.CreateTable(
                name: "Metadata",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastActionDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastActionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreviousValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacementValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    CharacterSet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ParentOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy", x => x.Id)
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
                        name: "FK_MetaTableDescriptionMetadata_MetaTableDescription_MetaTableDescriptionId",
                        column: x => x.MetaTableDescriptionId,
                        principalSchema: "deds",
                        principalTable: "MetaTableDescription",
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
                    ExternalIdentifierType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "MetadataOrganization",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataOrganization", x => new { x.MetadataId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_MetadataOrganization_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataOrganization_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Identifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AlternateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "MetadataTaxonomy",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataTaxonomy", x => new { x.MetadataId, x.TaxonomyId });
                    table.ForeignKey(
                        name: "FK_MetadataTaxonomy_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataTaxonomy_Taxonomy_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalSchema: "deds",
                        principalTable: "Taxonomy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    TermUri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attention = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_LocationMetadata_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
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
                name: "MetadataOrganizationIdentifier",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataOrganizationIdentifier", x => new { x.MetadataId, x.OrganizationIdentifierId });
                    table.ForeignKey(
                        name: "FK_MetadataOrganizationIdentifier_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataOrganizationIdentifier_OrganizationIdentifier_OrganizationIdentifierId",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "deds",
                        principalTable: "OrganizationIdentifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetadataProgram",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataProgram", x => new { x.MetadataId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_MetadataProgram_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataProgram_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    LastModified = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "Attribute",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkEntity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Attribute_TaxonomyTerm_TaxonomyTermId",
                        column: x => x.TaxonomyTermId,
                        principalSchema: "deds",
                        principalTable: "TaxonomyTerm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetadataTaxonomyTerm",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxonomyTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataTaxonomyTerm", x => new { x.MetadataId, x.TaxonomyTermId });
                    table.ForeignKey(
                        name: "FK_MetadataTaxonomyTerm_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataTaxonomyTerm_TaxonomyTerm_TaxonomyTermId",
                        column: x => x.TaxonomyTermId,
                        principalSchema: "deds",
                        principalTable: "TaxonomyTerm",
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
                        name: "FK_AccessibilityMetadata_Accessibility_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalSchema: "deds",
                        principalTable: "Accessibility",
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
                        name: "FK_AddressMetadata_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "deds",
                        principalTable: "Address",
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
                    AmountDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "MetadataService",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataService", x => new { x.MetadataId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_MetadataService_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocument",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExtentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "AccessibilityAttribute",
                schema: "dedsmeta",
                columns: table => new
                {
                    AccessibilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilityAttribute", x => new { x.AccessibilityId, x.AttributesId });
                    table.ForeignKey(
                        name: "FK_AccessibilityAttribute_Accessibility_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalSchema: "deds",
                        principalTable: "Accessibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessibilityAttribute_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressAttribute",
                schema: "dedsmeta",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressAttribute", x => new { x.AddressId, x.AttributesId });
                    table.ForeignKey(
                        name: "FK_AddressAttribute_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "deds",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressAttribute_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeLocation",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeLocation", x => new { x.AttributesId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_AttributeLocation_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "deds",
                        principalTable: "Location",
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
                        name: "FK_AttributeMetadata_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
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
                name: "AttributeMetaTableDescription",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTableDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeMetaTableDescription", x => new { x.AttributesId, x.MetaTableDescriptionId });
                    table.ForeignKey(
                        name: "FK_AttributeMetaTableDescription_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeMetaTableDescription_MetaTableDescription_MetaTableDescriptionId",
                        column: x => x.MetaTableDescriptionId,
                        principalSchema: "deds",
                        principalTable: "MetaTableDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeOrganization",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOrganization", x => new { x.AttributesId, x.OrganizationId });
                    table.ForeignKey(
                        name: "FK_AttributeOrganization_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeOrganization_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "deds",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeOrganizationIdentifier",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOrganizationIdentifier", x => new { x.AttributesId, x.OrganizationIdentifierId });
                    table.ForeignKey(
                        name: "FK_AttributeOrganizationIdentifier_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeOrganizationIdentifier_OrganizationIdentifier_OrganizationIdentifierId",
                        column: x => x.OrganizationIdentifierId,
                        principalSchema: "deds",
                        principalTable: "OrganizationIdentifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeProgram",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeProgram", x => new { x.AttributesId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_AttributeProgram_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeProgram_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "deds",
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeService",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeService", x => new { x.AttributesId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_AttributeService_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "deds",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeCostOption",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeCostOption", x => new { x.AttributesId, x.CostOptionId });
                    table.ForeignKey(
                        name: "FK_AttributeCostOption_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeCostOption_CostOption_CostOptionId",
                        column: x => x.CostOptionId,
                        principalSchema: "deds",
                        principalTable: "CostOption",
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
                        name: "FK_CostOptionMetadata_CostOption_CostOptionId",
                        column: x => x.CostOptionId,
                        principalSchema: "deds",
                        principalTable: "CostOption",
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
                name: "AttributeFunding",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FundingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeFunding", x => new { x.AttributesId, x.FundingId });
                    table.ForeignKey(
                        name: "FK_AttributeFunding_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeFunding_Funding_FundingId",
                        column: x => x.FundingId,
                        principalSchema: "deds",
                        principalTable: "Funding",
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
                        name: "FK_FundingMetadata_Funding_FundingId",
                        column: x => x.FundingId,
                        principalSchema: "deds",
                        principalTable: "Funding",
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
                name: "AttributeRequiredDocument",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeRequiredDocument", x => new { x.AttributesId, x.RequiredDocumentId });
                    table.ForeignKey(
                        name: "FK_AttributeRequiredDocument_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeRequiredDocument_RequiredDocument_RequiredDocumentId",
                        column: x => x.RequiredDocumentId,
                        principalSchema: "deds",
                        principalTable: "RequiredDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetadataRequiredDocument",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataRequiredDocument", x => new { x.MetadataId, x.RequiredDocumentId });
                    table.ForeignKey(
                        name: "FK_MetadataRequiredDocument_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataRequiredDocument_RequiredDocument_RequiredDocumentId",
                        column: x => x.RequiredDocumentId,
                        principalSchema: "deds",
                        principalTable: "RequiredDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeServiceArea",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeServiceArea", x => new { x.AttributesId, x.ServiceAreaId });
                    table.ForeignKey(
                        name: "FK_AttributeServiceArea_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeServiceArea_ServiceArea_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalSchema: "deds",
                        principalTable: "ServiceArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetadataServiceArea",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataServiceArea", x => new { x.MetadataId, x.ServiceAreaId });
                    table.ForeignKey(
                        name: "FK_MetadataServiceArea_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataServiceArea_ServiceArea_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalSchema: "deds",
                        principalTable: "ServiceArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeServiceAtLocation",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeServiceAtLocation", x => new { x.AttributesId, x.ServiceAtLocationId });
                    table.ForeignKey(
                        name: "FK_AttributeServiceAtLocation_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeServiceAtLocation_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "MetadataServiceAtLocation",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAtLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataServiceAtLocation", x => new { x.MetadataId, x.ServiceAtLocationId });
                    table.ForeignKey(
                        name: "FK_MetadataServiceAtLocation_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataServiceAtLocation_ServiceAtLocation_ServiceAtLocationId",
                        column: x => x.ServiceAtLocationId,
                        principalSchema: "deds",
                        principalTable: "ServiceAtLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "AttributeContact",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeContact", x => new { x.AttributesId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_AttributeContact_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contact",
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
                        name: "FK_ContactMetadata_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "deds",
                        principalTable: "Contact",
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "AttributeSchedule",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeSchedule", x => new { x.AttributesId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_AttributeSchedule_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeSchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "deds",
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetadataSchedule",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataSchedule", x => new { x.MetadataId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_MetadataSchedule_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataSchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "deds",
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributePhone",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributePhone", x => new { x.AttributesId, x.PhoneId });
                    table.ForeignKey(
                        name: "FK_AttributePhone_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributePhone_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "MetadataPhone",
                schema: "dedsmeta",
                columns: table => new
                {
                    MetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataPhone", x => new { x.MetadataId, x.PhoneId });
                    table.ForeignKey(
                        name: "FK_MetadataPhone_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalSchema: "deds",
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetadataPhone_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalSchema: "deds",
                        principalTable: "Phone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeLanguage",
                schema: "dedsmeta",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeLanguage", x => new { x.AttributesId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_AttributeLanguage_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalSchema: "deds",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "deds",
                        principalTable: "Language",
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
                        name: "FK_LanguageMetadata_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "deds",
                        principalTable: "Language",
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
                name: "IX_Accessibility_LocationId",
                schema: "deds",
                table: "Accessibility",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityAttribute_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttribute",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibilityMetadata_MetadataId",
                schema: "dedsmeta",
                table: "AccessibilityMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LocationId",
                schema: "deds",
                table: "Address",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressAttribute_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttribute",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMetadata_MetadataId",
                schema: "dedsmeta",
                table: "AddressMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                column: "TaxonomyTermId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeContact_ContactId",
                schema: "dedsmeta",
                table: "AttributeContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeCostOption_CostOptionId",
                schema: "dedsmeta",
                table: "AttributeCostOption",
                column: "CostOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeFunding_FundingId",
                schema: "dedsmeta",
                table: "AttributeFunding",
                column: "FundingId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeLanguage_LanguageId",
                schema: "dedsmeta",
                table: "AttributeLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeLocation_LocationId",
                schema: "dedsmeta",
                table: "AttributeLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeMetadata_MetadataId",
                schema: "dedsmeta",
                table: "AttributeMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeMetaTableDescription_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription",
                column: "MetaTableDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOrganization_OrganizationId",
                schema: "dedsmeta",
                table: "AttributeOrganization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributePhone_PhoneId",
                schema: "dedsmeta",
                table: "AttributePhone",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeProgram_ProgramId",
                schema: "dedsmeta",
                table: "AttributeProgram",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeRequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeSchedule_ScheduleId",
                schema: "dedsmeta",
                table: "AttributeSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeService_ServiceId",
                schema: "dedsmeta",
                table: "AttributeService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "AttributeServiceArea",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation",
                column: "ServiceAtLocationId");

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
                name: "IX_ContactMetadata_MetadataId",
                schema: "dedsmeta",
                table: "ContactMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOption_ServiceId",
                schema: "deds",
                table: "CostOption",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOptionMetadata_MetadataId",
                schema: "dedsmeta",
                table: "CostOptionMetadata",
                column: "MetadataId");

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
                name: "IX_FundingMetadata_MetadataId",
                schema: "dedsmeta",
                table: "FundingMetadata",
                column: "MetadataId");

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
                name: "IX_LanguageMetadata_MetadataId",
                schema: "dedsmeta",
                table: "LanguageMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OrganizationId",
                schema: "deds",
                table: "Location",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationMetadata_MetadataId",
                schema: "dedsmeta",
                table: "LocationMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataOrganization_OrganizationId",
                schema: "dedsmeta",
                table: "MetadataOrganization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataOrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier",
                column: "OrganizationIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataPhone_PhoneId",
                schema: "dedsmeta",
                table: "MetadataPhone",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataProgram_ProgramId",
                schema: "dedsmeta",
                table: "MetadataProgram",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataRequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataSchedule_ScheduleId",
                schema: "dedsmeta",
                table: "MetadataSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataService_ServiceId",
                schema: "dedsmeta",
                table: "MetadataService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "MetadataServiceArea",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation",
                column: "ServiceAtLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataTaxonomy_TaxonomyId",
                schema: "dedsmeta",
                table: "MetadataTaxonomy",
                column: "TaxonomyId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataTaxonomyTerm_TaxonomyTermId",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm",
                column: "TaxonomyTermId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaTableDescriptionMetadata_MetadataId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionMetadata",
                column: "MetadataId");

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
                name: "AccessibilityAttribute",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AccessibilityMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AddressAttribute",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AddressMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeContact",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeCostOption",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeFunding",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeLanguage",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeLocation",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeMetaTableDescription",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeOrganization",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeOrganizationIdentifier",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributePhone",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeProgram",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeRequiredDocument",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeSchedule",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeService",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeServiceArea",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "AttributeServiceAtLocation",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "ContactMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "CostOptionMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "FundingMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "LanguageMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "LocationMetadata",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataOrganization",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataOrganizationIdentifier",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataPhone",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataProgram",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataRequiredDocument",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataSchedule",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataService",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataServiceArea",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataServiceAtLocation",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataTaxonomy",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetadataTaxonomyTerm",
                schema: "dedsmeta");

            migrationBuilder.DropTable(
                name: "MetaTableDescriptionMetadata",
                schema: "dedsmeta");

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
                name: "CostOption",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Funding",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Language",
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
                name: "MetaTableDescription",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "Metadata",
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
