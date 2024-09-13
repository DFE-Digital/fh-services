using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDedsSchemaWithMappingsRelationshipsNavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dedsmeta");

            migrationBuilder.RenameColumn(
                name: "Dtstart",
                schema: "deds",
                table: "Schedule",
                newName: "DtStart");

            migrationBuilder.RenameColumn(
                name: "Byyearday",
                schema: "deds",
                table: "Schedule",
                newName: "ByYearDay");

            migrationBuilder.RenameColumn(
                name: "Byweekno",
                schema: "deds",
                table: "Schedule",
                newName: "ByWeekNo");

            migrationBuilder.RenameColumn(
                name: "Bymonthday",
                schema: "deds",
                table: "Schedule",
                newName: "ByMonthDay");

            migrationBuilder.RenameColumn(
                name: "Byday",
                schema: "deds",
                table: "Schedule",
                newName: "ByDay");

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "TaxonomyTerm",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Taxonomy",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "ServiceAtLocation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "ServiceArea",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                schema: "deds",
                table: "Service",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Schedule",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                schema: "deds",
                table: "RequiredDocument",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "RequiredDocument",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Program",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Phone",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "OrganizationIdentifier",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Organization",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "MetaTableDescription",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResourceId",
                schema: "deds",
                table: "Metadata",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Metadata",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Location",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Language",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Funding",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "CostOption",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkId",
                schema: "deds",
                table: "Attribute",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Attribute",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Address",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "OrId",
                schema: "deds",
                table: "Accessibility",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

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

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyTerm_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerm",
                column: "TaxonomyId",
                unique: true,
                filter: "[TaxonomyId] IS NOT NULL");

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
                name: "IX_ServiceArea_ServiceId",
                schema: "deds",
                table: "ServiceArea",
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
                name: "IX_RequiredDocument_ServiceId",
                schema: "deds",
                table: "RequiredDocument",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_OrganizationId",
                schema: "deds",
                table: "Program",
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
                name: "IX_OrganizationIdentifier_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifier",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OrganizationId",
                schema: "deds",
                table: "Location",
                column: "OrganizationId");

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
                name: "IX_CostOption_ServiceId",
                schema: "deds",
                table: "CostOption",
                column: "ServiceId");

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
                name: "IX_Attribute_TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                column: "TaxonomyTermId",
                unique: true,
                filter: "[TaxonomyTermId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LocationId",
                schema: "deds",
                table: "Address",
                column: "LocationId");

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
                name: "IX_ContactMetadata_MetadataId",
                schema: "dedsmeta",
                table: "ContactMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOptionMetadata_MetadataId",
                schema: "dedsmeta",
                table: "CostOptionMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_FundingMetadata_MetadataId",
                schema: "dedsmeta",
                table: "FundingMetadata",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageMetadata_MetadataId",
                schema: "dedsmeta",
                table: "LanguageMetadata",
                column: "MetadataId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Accessibility_Location_LocationId",
                schema: "deds",
                table: "Accessibility",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Location_LocationId",
                schema: "deds",
                table: "Address",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_TaxonomyTerm_TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                column: "TaxonomyTermId",
                principalSchema: "deds",
                principalTable: "TaxonomyTerm",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Location_LocationId",
                schema: "deds",
                table: "Contact",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Organization_OrganizationId",
                schema: "deds",
                table: "Contact",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ServiceAtLocation_ServiceAtLocationId",
                schema: "deds",
                table: "Contact",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Service_ServiceId",
                schema: "deds",
                table: "Contact",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostOption_Service_ServiceId",
                schema: "deds",
                table: "CostOption",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funding_Organization_OrganizationId",
                schema: "deds",
                table: "Funding",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Funding_Service_ServiceId",
                schema: "deds",
                table: "Funding",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_Location_LocationId",
                schema: "deds",
                table: "Language",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_Phone_PhoneId",
                schema: "deds",
                table: "Language",
                column: "PhoneId",
                principalSchema: "deds",
                principalTable: "Phone",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_Service_ServiceId",
                schema: "deds",
                table: "Language",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                schema: "deds",
                table: "Location",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationIdentifier_Organization_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifier",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Contact_ContactId",
                schema: "deds",
                table: "Phone",
                column: "ContactId",
                principalSchema: "deds",
                principalTable: "Contact",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Location_LocationId",
                schema: "deds",
                table: "Phone",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Organization_OrganizationId",
                schema: "deds",
                table: "Phone",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_ServiceAtLocation_ServiceAtLocationId",
                schema: "deds",
                table: "Phone",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Service_ServiceId",
                schema: "deds",
                table: "Phone",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Organization_OrganizationId",
                schema: "deds",
                table: "Program",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredDocument_Service_ServiceId",
                schema: "deds",
                table: "RequiredDocument",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Location_LocationId",
                schema: "deds",
                table: "Schedule",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_ServiceAtLocation_ServiceAtLocationId",
                schema: "deds",
                table: "Schedule",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Service_ServiceId",
                schema: "deds",
                table: "Schedule",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Organization_OrganizationId",
                schema: "deds",
                table: "Service",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Program_ProgramId",
                schema: "deds",
                table: "Service",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceArea_Service_ServiceId",
                schema: "deds",
                table: "ServiceArea",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocation_Location_LocationId",
                schema: "deds",
                table: "ServiceAtLocation",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocation_Service_ServiceId",
                schema: "deds",
                table: "ServiceAtLocation",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxonomyTerm_Taxonomy_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerm",
                column: "TaxonomyId",
                principalSchema: "deds",
                principalTable: "Taxonomy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessibility_Location_LocationId",
                schema: "deds",
                table: "Accessibility");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Location_LocationId",
                schema: "deds",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_TaxonomyTerm_TaxonomyTermId",
                schema: "deds",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Location_LocationId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Organization_OrganizationId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ServiceAtLocation_ServiceAtLocationId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Service_ServiceId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOption_Service_ServiceId",
                schema: "deds",
                table: "CostOption");

            migrationBuilder.DropForeignKey(
                name: "FK_Funding_Organization_OrganizationId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropForeignKey(
                name: "FK_Funding_Service_ServiceId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_Location_LocationId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_Phone_PhoneId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_Service_ServiceId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                schema: "deds",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationIdentifier_Organization_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Contact_ContactId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Location_LocationId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Organization_OrganizationId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_ServiceAtLocation_ServiceAtLocationId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Service_ServiceId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_Organization_OrganizationId",
                schema: "deds",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredDocument_Service_ServiceId",
                schema: "deds",
                table: "RequiredDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Location_LocationId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_ServiceAtLocation_ServiceAtLocationId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Service_ServiceId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Organization_OrganizationId",
                schema: "deds",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Program_ProgramId",
                schema: "deds",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceArea_Service_ServiceId",
                schema: "deds",
                table: "ServiceArea");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocation_Location_LocationId",
                schema: "deds",
                table: "ServiceAtLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocation_Service_ServiceId",
                schema: "deds",
                table: "ServiceAtLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxonomyTerm_Taxonomy_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerm");

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

            migrationBuilder.DropIndex(
                name: "IX_TaxonomyTerm_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerm");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAtLocation_LocationId",
                schema: "deds",
                table: "ServiceAtLocation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAtLocation_ServiceId",
                schema: "deds",
                table: "ServiceAtLocation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceArea_ServiceId",
                schema: "deds",
                table: "ServiceArea");

            migrationBuilder.DropIndex(
                name: "IX_Service_OrganizationId",
                schema: "deds",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_ProgramId",
                schema: "deds",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_LocationId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_ServiceAtLocationId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_ServiceId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_RequiredDocument_ServiceId",
                schema: "deds",
                table: "RequiredDocument");

            migrationBuilder.DropIndex(
                name: "IX_Program_OrganizationId",
                schema: "deds",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Phone_ContactId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Phone_LocationId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Phone_OrganizationId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Phone_ServiceAtLocationId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Phone_ServiceId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationIdentifier_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifier");

            migrationBuilder.DropIndex(
                name: "IX_Location_OrganizationId",
                schema: "deds",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Language_LocationId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Language_PhoneId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Language_ServiceId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Funding_OrganizationId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropIndex(
                name: "IX_Funding_ServiceId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropIndex(
                name: "IX_CostOption_ServiceId",
                schema: "deds",
                table: "CostOption");

            migrationBuilder.DropIndex(
                name: "IX_Contact_LocationId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_OrganizationId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ServiceAtLocationId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ServiceId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_TaxonomyTermId",
                schema: "deds",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Address_LocationId",
                schema: "deds",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Accessibility_LocationId",
                schema: "deds",
                table: "Accessibility");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "TaxonomyTerm");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Taxonomy");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "ServiceAtLocation");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "ServiceArea");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "RequiredDocument");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "OrganizationIdentifier");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "MetaTableDescription");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Metadata");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "CostOption");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OrId",
                schema: "deds",
                table: "Accessibility");

            migrationBuilder.RenameColumn(
                name: "DtStart",
                schema: "deds",
                table: "Schedule",
                newName: "Dtstart");

            migrationBuilder.RenameColumn(
                name: "ByYearDay",
                schema: "deds",
                table: "Schedule",
                newName: "Byyearday");

            migrationBuilder.RenameColumn(
                name: "ByWeekNo",
                schema: "deds",
                table: "Schedule",
                newName: "Byweekno");

            migrationBuilder.RenameColumn(
                name: "ByMonthDay",
                schema: "deds",
                table: "Schedule",
                newName: "Bymonthday");

            migrationBuilder.RenameColumn(
                name: "ByDay",
                schema: "deds",
                table: "Schedule",
                newName: "Byday");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                schema: "deds",
                table: "Service",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                schema: "deds",
                table: "RequiredDocument",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResourceId",
                schema: "deds",
                table: "Metadata",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkId",
                schema: "deds",
                table: "Attribute",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
