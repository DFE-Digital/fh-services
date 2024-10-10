using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDedsSchemaWithPluralTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessibility_Location_LocationId",
                schema: "deds",
                table: "Accessibility");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityAttribute_Accessibility_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityAttribute_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityMetadata_Accessibility_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Location_LocationId",
                schema: "deds",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressAttribute_Address_AddressId",
                schema: "dedsmeta",
                table: "AddressAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressAttribute_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressMetadata_Address_AddressId",
                schema: "dedsmeta",
                table: "AddressMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_TaxonomyTerm_TaxonomyTermId",
                schema: "deds",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeContact_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeContact");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeContact_Contact_ContactId",
                schema: "dedsmeta",
                table: "AttributeContact");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCostOption_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeCostOption");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCostOption_CostOption_CostOptionId",
                schema: "dedsmeta",
                table: "AttributeCostOption");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeFunding_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeFunding");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeFunding_Funding_FundingId",
                schema: "dedsmeta",
                table: "AttributeFunding");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLanguage_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLanguage_Language_LanguageId",
                schema: "dedsmeta",
                table: "AttributeLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLocation_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLocation_Location_LocationId",
                schema: "dedsmeta",
                table: "AttributeLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeMetadata_Attribute_AttributeId",
                schema: "dedsmeta",
                table: "AttributeMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeMetaTableDescription_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeMetaTableDescription_MetaTableDescription_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOrganization_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOrganization_Organization_OrganizationId",
                schema: "dedsmeta",
                table: "AttributeOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOrganizationIdentifier_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOrganizationIdentifier_OrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributePhone_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributePhone");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributePhone_Phone_PhoneId",
                schema: "dedsmeta",
                table: "AttributePhone");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeProgram_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeProgram_Program_ProgramId",
                schema: "dedsmeta",
                table: "AttributeProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeRequiredDocument_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeRequiredDocument_RequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeSchedule_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeSchedule_Schedule_ScheduleId",
                schema: "dedsmeta",
                table: "AttributeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeService_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeService");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeService_Service_ServiceId",
                schema: "dedsmeta",
                table: "AttributeService");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeServiceArea_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeServiceArea");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeServiceArea_ServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "AttributeServiceArea");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeServiceAtLocation_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeServiceAtLocation_ServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation");

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
                name: "FK_ContactMetadata_Contact_ContactId",
                schema: "dedsmeta",
                table: "ContactMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOption_Service_ServiceId",
                schema: "deds",
                table: "CostOption");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOptionMetadata_CostOption_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Funding_Organization_OrganizationId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropForeignKey(
                name: "FK_Funding_Service_ServiceId",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingMetadata_Funding_FundingId",
                schema: "dedsmeta",
                table: "FundingMetadata");

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
                name: "FK_LanguageMetadata_Language_LanguageId",
                schema: "dedsmeta",
                table: "LanguageMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                schema: "deds",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationMetadata_Location_LocationId",
                schema: "dedsmeta",
                table: "LocationMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataOrganization_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataOrganization_Organization_OrganizationId",
                schema: "dedsmeta",
                table: "MetadataOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataOrganizationIdentifier_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataOrganizationIdentifier_OrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataPhone_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataPhone_Phone_PhoneId",
                schema: "dedsmeta",
                table: "MetadataPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataProgram_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataProgram_Program_ProgramId",
                schema: "dedsmeta",
                table: "MetadataProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataRequiredDocument_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataRequiredDocument_RequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataSchedule_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataSchedule_Schedule_ScheduleId",
                schema: "dedsmeta",
                table: "MetadataSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataService_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataService");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataService_Service_ServiceId",
                schema: "dedsmeta",
                table: "MetadataService");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataServiceArea_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataServiceArea");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataServiceArea_ServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "MetadataServiceArea");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataServiceAtLocation_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataServiceAtLocation_ServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataTaxonomy_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataTaxonomy");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataTaxonomy_Taxonomy_TaxonomyId",
                schema: "dedsmeta",
                table: "MetadataTaxonomy");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataTaxonomyTerm_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_MetadataTaxonomyTerm_TaxonomyTerm_TaxonomyTermId",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_MetaTableDescriptionMetadata_MetaTableDescription_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionMetadata");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxonomyTerm",
                schema: "deds",
                table: "TaxonomyTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Taxonomy",
                schema: "deds",
                table: "Taxonomy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAtLocation",
                schema: "deds",
                table: "ServiceAtLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceArea",
                schema: "deds",
                table: "ServiceArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                schema: "deds",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                schema: "deds",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredDocument",
                schema: "deds",
                table: "RequiredDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Program",
                schema: "deds",
                table: "Program");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phone",
                schema: "deds",
                table: "Phone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationIdentifier",
                schema: "deds",
                table: "OrganizationIdentifier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                schema: "deds",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaTableDescription",
                schema: "deds",
                table: "MetaTableDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataTaxonomyTerm",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataTaxonomy",
                schema: "dedsmeta",
                table: "MetadataTaxonomy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataServiceAtLocation",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataServiceArea",
                schema: "dedsmeta",
                table: "MetadataServiceArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataService",
                schema: "dedsmeta",
                table: "MetadataService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataSchedule",
                schema: "dedsmeta",
                table: "MetadataSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataRequiredDocument",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataProgram",
                schema: "dedsmeta",
                table: "MetadataProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataPhone",
                schema: "dedsmeta",
                table: "MetadataPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataOrganizationIdentifier",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetadataOrganization",
                schema: "dedsmeta",
                table: "MetadataOrganization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                schema: "deds",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                schema: "deds",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funding",
                schema: "deds",
                table: "Funding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostOption",
                schema: "deds",
                table: "CostOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                schema: "deds",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeServiceAtLocation",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeServiceArea",
                schema: "dedsmeta",
                table: "AttributeServiceArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeService",
                schema: "dedsmeta",
                table: "AttributeService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeSchedule",
                schema: "dedsmeta",
                table: "AttributeSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeRequiredDocument",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeProgram",
                schema: "dedsmeta",
                table: "AttributeProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributePhone",
                schema: "dedsmeta",
                table: "AttributePhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeOrganizationIdentifier",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeOrganization",
                schema: "dedsmeta",
                table: "AttributeOrganization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeMetaTableDescription",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeLocation",
                schema: "dedsmeta",
                table: "AttributeLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeLanguage",
                schema: "dedsmeta",
                table: "AttributeLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeFunding",
                schema: "dedsmeta",
                table: "AttributeFunding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeCostOption",
                schema: "dedsmeta",
                table: "AttributeCostOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeContact",
                schema: "dedsmeta",
                table: "AttributeContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attribute",
                schema: "deds",
                table: "Attribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressAttribute",
                schema: "dedsmeta",
                table: "AddressAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                schema: "deds",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessibilityAttribute",
                schema: "dedsmeta",
                table: "AccessibilityAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accessibility",
                schema: "deds",
                table: "Accessibility");

            migrationBuilder.RenameTable(
                name: "TaxonomyTerm",
                schema: "deds",
                newName: "TaxonomyTerms",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Taxonomy",
                schema: "deds",
                newName: "Taxonomies",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ServiceAtLocation",
                schema: "deds",
                newName: "ServiceAtLocations",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ServiceArea",
                schema: "deds",
                newName: "ServiceAreas",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Service",
                schema: "deds",
                newName: "Services",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Schedule",
                schema: "deds",
                newName: "Schedules",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "RequiredDocument",
                schema: "deds",
                newName: "RequiredDocuments",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Program",
                schema: "deds",
                newName: "Programs",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Phone",
                schema: "deds",
                newName: "Phones",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "OrganizationIdentifier",
                schema: "deds",
                newName: "OrganizationIdentifiers",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Organization",
                schema: "deds",
                newName: "Organizations",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "MetaTableDescription",
                schema: "deds",
                newName: "MetaTableDescriptions",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "MetadataTaxonomyTerm",
                schema: "dedsmeta",
                newName: "TaxonomyTermMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataTaxonomy",
                schema: "dedsmeta",
                newName: "TaxonomyMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataServiceAtLocation",
                schema: "dedsmeta",
                newName: "ServiceAtLocationMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataServiceArea",
                schema: "dedsmeta",
                newName: "ServiceAreaMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataService",
                schema: "dedsmeta",
                newName: "ServiceMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataSchedule",
                schema: "dedsmeta",
                newName: "ScheduleMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataRequiredDocument",
                schema: "dedsmeta",
                newName: "RequiredDocumentMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataProgram",
                schema: "dedsmeta",
                newName: "ProgramMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataPhone",
                schema: "dedsmeta",
                newName: "PhoneMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataOrganizationIdentifier",
                schema: "dedsmeta",
                newName: "OrganizationIdentifierMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetadataOrganization",
                schema: "dedsmeta",
                newName: "OrganizationMetadata",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Location",
                schema: "deds",
                newName: "Locations",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Language",
                schema: "deds",
                newName: "Languages",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Funding",
                schema: "deds",
                newName: "Fundings",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "CostOption",
                schema: "deds",
                newName: "CostOptions",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Contact",
                schema: "deds",
                newName: "Contacts",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "AttributeServiceAtLocation",
                schema: "dedsmeta",
                newName: "ServiceAtLocationAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeServiceArea",
                schema: "dedsmeta",
                newName: "ServiceAreaAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeService",
                schema: "dedsmeta",
                newName: "ServiceAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeSchedule",
                schema: "dedsmeta",
                newName: "ScheduleAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeRequiredDocument",
                schema: "dedsmeta",
                newName: "RequiredDocumentAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeProgram",
                schema: "dedsmeta",
                newName: "ProgramAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributePhone",
                schema: "dedsmeta",
                newName: "PhoneAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeOrganizationIdentifier",
                schema: "dedsmeta",
                newName: "OrganizationIdentifierAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeOrganization",
                schema: "dedsmeta",
                newName: "OrganizationAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeMetaTableDescription",
                schema: "dedsmeta",
                newName: "MetaTableDescriptionAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeLocation",
                schema: "dedsmeta",
                newName: "LocationAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeLanguage",
                schema: "dedsmeta",
                newName: "LanguageAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeFunding",
                schema: "dedsmeta",
                newName: "FundingAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeCostOption",
                schema: "dedsmeta",
                newName: "CostOptionAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AttributeContact",
                schema: "dedsmeta",
                newName: "ContactAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Attribute",
                schema: "deds",
                newName: "Attributes",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "AddressAttribute",
                schema: "dedsmeta",
                newName: "AddressAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "deds",
                newName: "Addresses",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "AccessibilityAttribute",
                schema: "dedsmeta",
                newName: "AccessibilityAttributes",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Accessibility",
                schema: "deds",
                newName: "Accessibilities",
                newSchema: "deds");

            migrationBuilder.RenameIndex(
                name: "IX_TaxonomyTerm_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerms",
                newName: "IX_TaxonomyTerms_TaxonomyId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAtLocation_ServiceId",
                schema: "deds",
                table: "ServiceAtLocations",
                newName: "IX_ServiceAtLocations_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAtLocation_LocationId",
                schema: "deds",
                table: "ServiceAtLocations",
                newName: "IX_ServiceAtLocations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceArea_ServiceId",
                schema: "deds",
                table: "ServiceAreas",
                newName: "IX_ServiceAreas_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_ProgramId",
                schema: "deds",
                table: "Services",
                newName: "IX_Services_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_OrganizationId",
                schema: "deds",
                table: "Services",
                newName: "IX_Services_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_ServiceId",
                schema: "deds",
                table: "Schedules",
                newName: "IX_Schedules_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_ServiceAtLocationId",
                schema: "deds",
                table: "Schedules",
                newName: "IX_Schedules_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_LocationId",
                schema: "deds",
                table: "Schedules",
                newName: "IX_Schedules_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_RequiredDocument_ServiceId",
                schema: "deds",
                table: "RequiredDocuments",
                newName: "IX_RequiredDocuments_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Program_OrganizationId",
                schema: "deds",
                table: "Programs",
                newName: "IX_Programs_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_ServiceId",
                schema: "deds",
                table: "Phones",
                newName: "IX_Phones_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_ServiceAtLocationId",
                schema: "deds",
                table: "Phones",
                newName: "IX_Phones_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_OrganizationId",
                schema: "deds",
                table: "Phones",
                newName: "IX_Phones_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_LocationId",
                schema: "deds",
                table: "Phones",
                newName: "IX_Phones_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_ContactId",
                schema: "deds",
                table: "Phones",
                newName: "IX_Phones_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationIdentifier_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifiers",
                newName: "IX_OrganizationIdentifiers_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataTaxonomyTerm_TaxonomyTermId",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata",
                newName: "IX_TaxonomyTermMetadata_TaxonomyTermId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataTaxonomy_TaxonomyId",
                schema: "dedsmeta",
                table: "TaxonomyMetadata",
                newName: "IX_TaxonomyMetadata_TaxonomyId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata",
                newName: "IX_ServiceAtLocationMetadata_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata",
                newName: "IX_ServiceAreaMetadata_ServiceAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataService_ServiceId",
                schema: "dedsmeta",
                table: "ServiceMetadata",
                newName: "IX_ServiceMetadata_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataSchedule_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleMetadata",
                newName: "IX_ScheduleMetadata_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataRequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata",
                newName: "IX_RequiredDocumentMetadata_RequiredDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataProgram_ProgramId",
                schema: "dedsmeta",
                table: "ProgramMetadata",
                newName: "IX_ProgramMetadata_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataPhone_PhoneId",
                schema: "dedsmeta",
                table: "PhoneMetadata",
                newName: "IX_PhoneMetadata_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataOrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata",
                newName: "IX_OrganizationIdentifierMetadata_OrganizationIdentifierId");

            migrationBuilder.RenameIndex(
                name: "IX_MetadataOrganization_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationMetadata",
                newName: "IX_OrganizationMetadata_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_OrganizationId",
                schema: "deds",
                table: "Locations",
                newName: "IX_Locations_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_ServiceId",
                schema: "deds",
                table: "Languages",
                newName: "IX_Languages_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_PhoneId",
                schema: "deds",
                table: "Languages",
                newName: "IX_Languages_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_LocationId",
                schema: "deds",
                table: "Languages",
                newName: "IX_Languages_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Funding_ServiceId",
                schema: "deds",
                table: "Fundings",
                newName: "IX_Fundings_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Funding_OrganizationId",
                schema: "deds",
                table: "Fundings",
                newName: "IX_Fundings_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_CostOption_ServiceId",
                schema: "deds",
                table: "CostOptions",
                newName: "IX_CostOptions_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ServiceId",
                schema: "deds",
                table: "Contacts",
                newName: "IX_Contacts_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ServiceAtLocationId",
                schema: "deds",
                table: "Contacts",
                newName: "IX_Contacts_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_OrganizationId",
                schema: "deds",
                table: "Contacts",
                newName: "IX_Contacts_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_LocationId",
                schema: "deds",
                table: "Contacts",
                newName: "IX_Contacts_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes",
                newName: "IX_ServiceAtLocationAttributes_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes",
                newName: "IX_ServiceAreaAttributes_ServiceAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeService_ServiceId",
                schema: "dedsmeta",
                table: "ServiceAttributes",
                newName: "IX_ServiceAttributes_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeSchedule_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleAttributes",
                newName: "IX_ScheduleAttributes_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeRequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes",
                newName: "IX_RequiredDocumentAttributes_RequiredDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeProgram_ProgramId",
                schema: "dedsmeta",
                table: "ProgramAttributes",
                newName: "IX_ProgramAttributes_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributePhone_PhoneId",
                schema: "dedsmeta",
                table: "PhoneAttributes",
                newName: "IX_PhoneAttributes_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeOrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes",
                newName: "IX_OrganizationIdentifierAttributes_OrganizationIdentifierId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeOrganization_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationAttributes",
                newName: "IX_OrganizationAttributes_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeMetaTableDescription_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes",
                newName: "IX_MetaTableDescriptionAttributes_MetaTableDescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeLocation_LocationId",
                schema: "dedsmeta",
                table: "LocationAttributes",
                newName: "IX_LocationAttributes_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeLanguage_LanguageId",
                schema: "dedsmeta",
                table: "LanguageAttributes",
                newName: "IX_LanguageAttributes_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeFunding_FundingId",
                schema: "dedsmeta",
                table: "FundingAttributes",
                newName: "IX_FundingAttributes_FundingId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeCostOption_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionAttributes",
                newName: "IX_CostOptionAttributes_CostOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeContact_ContactId",
                schema: "dedsmeta",
                table: "ContactAttributes",
                newName: "IX_ContactAttributes_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Attribute_TaxonomyTermId",
                schema: "deds",
                table: "Attributes",
                newName: "IX_Attributes_TaxonomyTermId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressAttribute_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttributes",
                newName: "IX_AddressAttributes_AttributesId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_LocationId",
                schema: "deds",
                table: "Addresses",
                newName: "IX_Addresses_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessibilityAttribute_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttributes",
                newName: "IX_AccessibilityAttributes_AttributesId");

            migrationBuilder.RenameIndex(
                name: "IX_Accessibility_LocationId",
                schema: "deds",
                table: "Accessibilities",
                newName: "IX_Accessibilities_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxonomyTerms",
                schema: "deds",
                table: "TaxonomyTerms",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Taxonomies",
                schema: "deds",
                table: "Taxonomies",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAtLocations",
                schema: "deds",
                table: "ServiceAtLocations",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAreas",
                schema: "deds",
                table: "ServiceAreas",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                schema: "deds",
                table: "Services",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                schema: "deds",
                table: "Schedules",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredDocuments",
                schema: "deds",
                table: "RequiredDocuments",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programs",
                schema: "deds",
                table: "Programs",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phones",
                schema: "deds",
                table: "Phones",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationIdentifiers",
                schema: "deds",
                table: "OrganizationIdentifiers",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                schema: "deds",
                table: "Organizations",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaTableDescriptions",
                schema: "deds",
                table: "MetaTableDescriptions",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxonomyTermMetadata",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata",
                columns: new[] { "MetadataId", "TaxonomyTermId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxonomyMetadata",
                schema: "dedsmeta",
                table: "TaxonomyMetadata",
                columns: new[] { "MetadataId", "TaxonomyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAtLocationMetadata",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata",
                columns: new[] { "MetadataId", "ServiceAtLocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAreaMetadata",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata",
                columns: new[] { "MetadataId", "ServiceAreaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceMetadata",
                schema: "dedsmeta",
                table: "ServiceMetadata",
                columns: new[] { "MetadataId", "ServiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleMetadata",
                schema: "dedsmeta",
                table: "ScheduleMetadata",
                columns: new[] { "MetadataId", "ScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredDocumentMetadata",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata",
                columns: new[] { "MetadataId", "RequiredDocumentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramMetadata",
                schema: "dedsmeta",
                table: "ProgramMetadata",
                columns: new[] { "MetadataId", "ProgramId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneMetadata",
                schema: "dedsmeta",
                table: "PhoneMetadata",
                columns: new[] { "MetadataId", "PhoneId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationIdentifierMetadata",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata",
                columns: new[] { "MetadataId", "OrganizationIdentifierId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationMetadata",
                schema: "dedsmeta",
                table: "OrganizationMetadata",
                columns: new[] { "MetadataId", "OrganizationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                schema: "deds",
                table: "Locations",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                schema: "deds",
                table: "Languages",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fundings",
                schema: "deds",
                table: "Fundings",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostOptions",
                schema: "deds",
                table: "CostOptions",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                schema: "deds",
                table: "Contacts",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAtLocationAttributes",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes",
                columns: new[] { "AttributesId", "ServiceAtLocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAreaAttributes",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes",
                columns: new[] { "AttributesId", "ServiceAreaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAttributes",
                schema: "dedsmeta",
                table: "ServiceAttributes",
                columns: new[] { "AttributesId", "ServiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleAttributes",
                schema: "dedsmeta",
                table: "ScheduleAttributes",
                columns: new[] { "AttributesId", "ScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredDocumentAttributes",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes",
                columns: new[] { "AttributesId", "RequiredDocumentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramAttributes",
                schema: "dedsmeta",
                table: "ProgramAttributes",
                columns: new[] { "AttributesId", "ProgramId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneAttributes",
                schema: "dedsmeta",
                table: "PhoneAttributes",
                columns: new[] { "AttributesId", "PhoneId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationIdentifierAttributes",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes",
                columns: new[] { "AttributesId", "OrganizationIdentifierId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationAttributes",
                schema: "dedsmeta",
                table: "OrganizationAttributes",
                columns: new[] { "AttributesId", "OrganizationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaTableDescriptionAttributes",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes",
                columns: new[] { "AttributesId", "MetaTableDescriptionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationAttributes",
                schema: "dedsmeta",
                table: "LocationAttributes",
                columns: new[] { "AttributesId", "LocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageAttributes",
                schema: "dedsmeta",
                table: "LanguageAttributes",
                columns: new[] { "AttributesId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundingAttributes",
                schema: "dedsmeta",
                table: "FundingAttributes",
                columns: new[] { "AttributesId", "FundingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostOptionAttributes",
                schema: "dedsmeta",
                table: "CostOptionAttributes",
                columns: new[] { "AttributesId", "CostOptionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactAttributes",
                schema: "dedsmeta",
                table: "ContactAttributes",
                columns: new[] { "AttributesId", "ContactId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                schema: "deds",
                table: "Attributes",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressAttributes",
                schema: "dedsmeta",
                table: "AddressAttributes",
                columns: new[] { "AddressId", "AttributesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "deds",
                table: "Addresses",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessibilityAttributes",
                schema: "dedsmeta",
                table: "AccessibilityAttributes",
                columns: new[] { "AccessibilityId", "AttributesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accessibilities",
                schema: "deds",
                table: "Accessibilities",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Accessibilities_Locations_LocationId",
                schema: "deds",
                table: "Accessibilities",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityAttributes_Accessibilities_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityAttributes",
                column: "AccessibilityId",
                principalSchema: "deds",
                principalTable: "Accessibilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityMetadata_Accessibilities_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityMetadata",
                column: "AccessibilityId",
                principalSchema: "deds",
                principalTable: "Accessibilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAttributes_Addresses_AddressId",
                schema: "dedsmeta",
                table: "AddressAttributes",
                column: "AddressId",
                principalSchema: "deds",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Locations_LocationId",
                schema: "deds",
                table: "Addresses",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressMetadata_Addresses_AddressId",
                schema: "dedsmeta",
                table: "AddressMetadata",
                column: "AddressId",
                principalSchema: "deds",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeMetadata_Attributes_AttributeId",
                schema: "dedsmeta",
                table: "AttributeMetadata",
                column: "AttributeId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_TaxonomyTerms_TaxonomyTermId",
                schema: "deds",
                table: "Attributes",
                column: "TaxonomyTermId",
                principalSchema: "deds",
                principalTable: "TaxonomyTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ContactAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAttributes_Contacts_ContactId",
                schema: "dedsmeta",
                table: "ContactAttributes",
                column: "ContactId",
                principalSchema: "deds",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMetadata_Contacts_ContactId",
                schema: "dedsmeta",
                table: "ContactMetadata",
                column: "ContactId",
                principalSchema: "deds",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Locations_LocationId",
                schema: "deds",
                table: "Contacts",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Organizations_OrganizationId",
                schema: "deds",
                table: "Contacts",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ServiceAtLocations_ServiceAtLocationId",
                schema: "deds",
                table: "Contacts",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Services_ServiceId",
                schema: "deds",
                table: "Contacts",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostOptionAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "CostOptionAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostOptionAttributes_CostOptions_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionAttributes",
                column: "CostOptionId",
                principalSchema: "deds",
                principalTable: "CostOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostOptionMetadata_CostOptions_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionMetadata",
                column: "CostOptionId",
                principalSchema: "deds",
                principalTable: "CostOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostOptions_Services_ServiceId",
                schema: "deds",
                table: "CostOptions",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FundingAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "FundingAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FundingAttributes_Fundings_FundingId",
                schema: "dedsmeta",
                table: "FundingAttributes",
                column: "FundingId",
                principalSchema: "deds",
                principalTable: "Fundings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FundingMetadata_Fundings_FundingId",
                schema: "dedsmeta",
                table: "FundingMetadata",
                column: "FundingId",
                principalSchema: "deds",
                principalTable: "Fundings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fundings_Organizations_OrganizationId",
                schema: "deds",
                table: "Fundings",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundings_Services_ServiceId",
                schema: "deds",
                table: "Fundings",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "LanguageAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageAttributes_Languages_LanguageId",
                schema: "dedsmeta",
                table: "LanguageAttributes",
                column: "LanguageId",
                principalSchema: "deds",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMetadata_Languages_LanguageId",
                schema: "dedsmeta",
                table: "LanguageMetadata",
                column: "LanguageId",
                principalSchema: "deds",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Locations_LocationId",
                schema: "deds",
                table: "Languages",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Phones_PhoneId",
                schema: "deds",
                table: "Languages",
                column: "PhoneId",
                principalSchema: "deds",
                principalTable: "Phones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Services_ServiceId",
                schema: "deds",
                table: "Languages",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "LocationAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAttributes_Locations_LocationId",
                schema: "dedsmeta",
                table: "LocationAttributes",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationMetadata_Locations_LocationId",
                schema: "dedsmeta",
                table: "LocationMetadata",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Organizations_OrganizationId",
                schema: "deds",
                table: "Locations",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MetaTableDescriptionAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetaTableDescriptionAttributes_MetaTableDescriptions_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes",
                column: "MetaTableDescriptionId",
                principalSchema: "deds",
                principalTable: "MetaTableDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetaTableDescriptionMetadata_MetaTableDescriptions_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionMetadata",
                column: "MetaTableDescriptionId",
                principalSchema: "deds",
                principalTable: "MetaTableDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "OrganizationAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationAttributes_Organizations_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationAttributes",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationIdentifierAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationIdentifierAttributes_OrganizationIdentifiers_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes",
                column: "OrganizationIdentifierId",
                principalSchema: "deds",
                principalTable: "OrganizationIdentifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationIdentifierMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationIdentifierMetadata_OrganizationIdentifiers_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata",
                column: "OrganizationIdentifierId",
                principalSchema: "deds",
                principalTable: "OrganizationIdentifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationIdentifiers_Organizations_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifiers",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "OrganizationMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationMetadata_Organizations_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationMetadata",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "PhoneAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneAttributes_Phones_PhoneId",
                schema: "dedsmeta",
                table: "PhoneAttributes",
                column: "PhoneId",
                principalSchema: "deds",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "PhoneMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneMetadata_Phones_PhoneId",
                schema: "dedsmeta",
                table: "PhoneMetadata",
                column: "PhoneId",
                principalSchema: "deds",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                schema: "deds",
                table: "Phones",
                column: "ContactId",
                principalSchema: "deds",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Locations_LocationId",
                schema: "deds",
                table: "Phones",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "deds",
                table: "Phones",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_ServiceAtLocations_ServiceAtLocationId",
                schema: "deds",
                table: "Phones",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Services_ServiceId",
                schema: "deds",
                table: "Phones",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ProgramAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramAttributes_Programs_ProgramId",
                schema: "dedsmeta",
                table: "ProgramAttributes",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ProgramMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramMetadata_Programs_ProgramId",
                schema: "dedsmeta",
                table: "ProgramMetadata",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Organizations_OrganizationId",
                schema: "deds",
                table: "Programs",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredDocumentAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredDocumentAttributes_RequiredDocuments_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes",
                column: "RequiredDocumentId",
                principalSchema: "deds",
                principalTable: "RequiredDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredDocumentMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredDocumentMetadata_RequiredDocuments_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata",
                column: "RequiredDocumentId",
                principalSchema: "deds",
                principalTable: "RequiredDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredDocuments_Services_ServiceId",
                schema: "deds",
                table: "RequiredDocuments",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ScheduleAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleAttributes_Schedules_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleAttributes",
                column: "ScheduleId",
                principalSchema: "deds",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ScheduleMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleMetadata_Schedules_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleMetadata",
                column: "ScheduleId",
                principalSchema: "deds",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Locations_LocationId",
                schema: "deds",
                table: "Schedules",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ServiceAtLocations_ServiceAtLocationId",
                schema: "deds",
                table: "Schedules",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Services_ServiceId",
                schema: "deds",
                table: "Schedules",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAreaAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAreaAttributes_ServiceAreas_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes",
                column: "ServiceAreaId",
                principalSchema: "deds",
                principalTable: "ServiceAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAreaMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAreaMetadata_ServiceAreas_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata",
                column: "ServiceAreaId",
                principalSchema: "deds",
                principalTable: "ServiceAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAreas_Services_ServiceId",
                schema: "deds",
                table: "ServiceAreas",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocationAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocationAttributes_ServiceAtLocations_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocationMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocationMetadata_ServiceAtLocations_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocations_Locations_LocationId",
                schema: "deds",
                table: "ServiceAtLocations",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAtLocations_Services_ServiceId",
                schema: "deds",
                table: "ServiceAtLocations",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ServiceAttributes",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAttributes_Services_ServiceId",
                schema: "dedsmeta",
                table: "ServiceAttributes",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ServiceMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceMetadata_Services_ServiceId",
                schema: "dedsmeta",
                table: "ServiceMetadata",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                schema: "deds",
                table: "Services",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Programs_ProgramId",
                schema: "deds",
                table: "Services",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxonomyMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "TaxonomyMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxonomyMetadata_Taxonomies_TaxonomyId",
                schema: "dedsmeta",
                table: "TaxonomyMetadata",
                column: "TaxonomyId",
                principalSchema: "deds",
                principalTable: "Taxonomies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxonomyTermMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxonomyTermMetadata_TaxonomyTerms_TaxonomyTermId",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata",
                column: "TaxonomyTermId",
                principalSchema: "deds",
                principalTable: "TaxonomyTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxonomyTerms_Taxonomies_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerms",
                column: "TaxonomyId",
                principalSchema: "deds",
                principalTable: "Taxonomies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessibilities_Locations_LocationId",
                schema: "deds",
                table: "Accessibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityAttributes_Accessibilities_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessibilityMetadata_Accessibilities_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressAttributes_Addresses_AddressId",
                schema: "dedsmeta",
                table: "AddressAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Locations_LocationId",
                schema: "deds",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressMetadata_Addresses_AddressId",
                schema: "dedsmeta",
                table: "AddressMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeMetadata_Attributes_AttributeId",
                schema: "dedsmeta",
                table: "AttributeMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_TaxonomyTerms_TaxonomyTermId",
                schema: "deds",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ContactAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAttributes_Contacts_ContactId",
                schema: "dedsmeta",
                table: "ContactAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMetadata_Contacts_ContactId",
                schema: "dedsmeta",
                table: "ContactMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Locations_LocationId",
                schema: "deds",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Organizations_OrganizationId",
                schema: "deds",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ServiceAtLocations_ServiceAtLocationId",
                schema: "deds",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Services_ServiceId",
                schema: "deds",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOptionAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "CostOptionAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOptionAttributes_CostOptions_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOptionMetadata_CostOptions_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_CostOptions_Services_ServiceId",
                schema: "deds",
                table: "CostOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "FundingAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingAttributes_Fundings_FundingId",
                schema: "dedsmeta",
                table: "FundingAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingMetadata_Fundings_FundingId",
                schema: "dedsmeta",
                table: "FundingMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Fundings_Organizations_OrganizationId",
                schema: "deds",
                table: "Fundings");

            migrationBuilder.DropForeignKey(
                name: "FK_Fundings_Services_ServiceId",
                schema: "deds",
                table: "Fundings");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "LanguageAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageAttributes_Languages_LanguageId",
                schema: "dedsmeta",
                table: "LanguageAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMetadata_Languages_LanguageId",
                schema: "dedsmeta",
                table: "LanguageMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Locations_LocationId",
                schema: "deds",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Phones_PhoneId",
                schema: "deds",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Services_ServiceId",
                schema: "deds",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "LocationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationAttributes_Locations_LocationId",
                schema: "dedsmeta",
                table: "LocationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationMetadata_Locations_LocationId",
                schema: "dedsmeta",
                table: "LocationMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Organizations_OrganizationId",
                schema: "deds",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_MetaTableDescriptionAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_MetaTableDescriptionAttributes_MetaTableDescriptions_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_MetaTableDescriptionMetadata_MetaTableDescriptions_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "OrganizationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationAttributes_Organizations_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationIdentifierAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationIdentifierAttributes_OrganizationIdentifiers_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationIdentifierMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationIdentifierMetadata_OrganizationIdentifiers_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationIdentifiers_Organizations_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifiers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "OrganizationMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMetadata_Organizations_OrganizationId",
                schema: "dedsmeta",
                table: "OrganizationMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "PhoneAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneAttributes_Phones_PhoneId",
                schema: "dedsmeta",
                table: "PhoneAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "PhoneMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneMetadata_Phones_PhoneId",
                schema: "dedsmeta",
                table: "PhoneMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                schema: "deds",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Locations_LocationId",
                schema: "deds",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "deds",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_ServiceAtLocations_ServiceAtLocationId",
                schema: "deds",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Services_ServiceId",
                schema: "deds",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ProgramAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramAttributes_Programs_ProgramId",
                schema: "dedsmeta",
                table: "ProgramAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ProgramMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramMetadata_Programs_ProgramId",
                schema: "dedsmeta",
                table: "ProgramMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Organizations_OrganizationId",
                schema: "deds",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredDocumentAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredDocumentAttributes_RequiredDocuments_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredDocumentMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredDocumentMetadata_RequiredDocuments_RequiredDocumentId",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredDocuments_Services_ServiceId",
                schema: "deds",
                table: "RequiredDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ScheduleAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleAttributes_Schedules_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ScheduleMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleMetadata_Schedules_ScheduleId",
                schema: "dedsmeta",
                table: "ScheduleMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Locations_LocationId",
                schema: "deds",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ServiceAtLocations_ServiceAtLocationId",
                schema: "deds",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Services_ServiceId",
                schema: "deds",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAreaAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAreaAttributes_ServiceAreas_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAreaMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAreaMetadata_ServiceAreas_ServiceAreaId",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAreas_Services_ServiceId",
                schema: "deds",
                table: "ServiceAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocationAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocationAttributes_ServiceAtLocations_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocationMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocationMetadata_ServiceAtLocations_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocations_Locations_LocationId",
                schema: "deds",
                table: "ServiceAtLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAtLocations_Services_ServiceId",
                schema: "deds",
                table: "ServiceAtLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAttributes_Attributes_AttributesId",
                schema: "dedsmeta",
                table: "ServiceAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAttributes_Services_ServiceId",
                schema: "dedsmeta",
                table: "ServiceAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "ServiceMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceMetadata_Services_ServiceId",
                schema: "dedsmeta",
                table: "ServiceMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                schema: "deds",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Programs_ProgramId",
                schema: "deds",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxonomyMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "TaxonomyMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxonomyMetadata_Taxonomies_TaxonomyId",
                schema: "dedsmeta",
                table: "TaxonomyMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxonomyTermMetadata_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxonomyTermMetadata_TaxonomyTerms_TaxonomyTermId",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxonomyTerms_Taxonomies_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxonomyTerms",
                schema: "deds",
                table: "TaxonomyTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxonomyTermMetadata",
                schema: "dedsmeta",
                table: "TaxonomyTermMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxonomyMetadata",
                schema: "dedsmeta",
                table: "TaxonomyMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Taxonomies",
                schema: "deds",
                table: "Taxonomies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                schema: "deds",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceMetadata",
                schema: "dedsmeta",
                table: "ServiceMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAttributes",
                schema: "dedsmeta",
                table: "ServiceAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAtLocations",
                schema: "deds",
                table: "ServiceAtLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAtLocationMetadata",
                schema: "dedsmeta",
                table: "ServiceAtLocationMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAtLocationAttributes",
                schema: "dedsmeta",
                table: "ServiceAtLocationAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAreas",
                schema: "deds",
                table: "ServiceAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAreaMetadata",
                schema: "dedsmeta",
                table: "ServiceAreaMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceAreaAttributes",
                schema: "dedsmeta",
                table: "ServiceAreaAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                schema: "deds",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleMetadata",
                schema: "dedsmeta",
                table: "ScheduleMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleAttributes",
                schema: "dedsmeta",
                table: "ScheduleAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredDocuments",
                schema: "deds",
                table: "RequiredDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredDocumentMetadata",
                schema: "dedsmeta",
                table: "RequiredDocumentMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredDocumentAttributes",
                schema: "dedsmeta",
                table: "RequiredDocumentAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programs",
                schema: "deds",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramMetadata",
                schema: "dedsmeta",
                table: "ProgramMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramAttributes",
                schema: "dedsmeta",
                table: "ProgramAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phones",
                schema: "deds",
                table: "Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneMetadata",
                schema: "dedsmeta",
                table: "PhoneMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneAttributes",
                schema: "dedsmeta",
                table: "PhoneAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                schema: "deds",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationMetadata",
                schema: "dedsmeta",
                table: "OrganizationMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationIdentifiers",
                schema: "deds",
                table: "OrganizationIdentifiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationIdentifierMetadata",
                schema: "dedsmeta",
                table: "OrganizationIdentifierMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationIdentifierAttributes",
                schema: "dedsmeta",
                table: "OrganizationIdentifierAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationAttributes",
                schema: "dedsmeta",
                table: "OrganizationAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaTableDescriptions",
                schema: "deds",
                table: "MetaTableDescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaTableDescriptionAttributes",
                schema: "dedsmeta",
                table: "MetaTableDescriptionAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                schema: "deds",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationAttributes",
                schema: "dedsmeta",
                table: "LocationAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                schema: "deds",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageAttributes",
                schema: "dedsmeta",
                table: "LanguageAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fundings",
                schema: "deds",
                table: "Fundings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FundingAttributes",
                schema: "dedsmeta",
                table: "FundingAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostOptions",
                schema: "deds",
                table: "CostOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostOptionAttributes",
                schema: "dedsmeta",
                table: "CostOptionAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                schema: "deds",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactAttributes",
                schema: "dedsmeta",
                table: "ContactAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                schema: "deds",
                table: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "deds",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressAttributes",
                schema: "dedsmeta",
                table: "AddressAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessibilityAttributes",
                schema: "dedsmeta",
                table: "AccessibilityAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accessibilities",
                schema: "deds",
                table: "Accessibilities");

            migrationBuilder.RenameTable(
                name: "TaxonomyTerms",
                schema: "deds",
                newName: "TaxonomyTerm",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "TaxonomyTermMetadata",
                schema: "dedsmeta",
                newName: "MetadataTaxonomyTerm",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "TaxonomyMetadata",
                schema: "dedsmeta",
                newName: "MetadataTaxonomy",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Taxonomies",
                schema: "deds",
                newName: "Taxonomy",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Services",
                schema: "deds",
                newName: "Service",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ServiceMetadata",
                schema: "dedsmeta",
                newName: "MetadataService",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ServiceAttributes",
                schema: "dedsmeta",
                newName: "AttributeService",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ServiceAtLocations",
                schema: "deds",
                newName: "ServiceAtLocation",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ServiceAtLocationMetadata",
                schema: "dedsmeta",
                newName: "MetadataServiceAtLocation",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ServiceAtLocationAttributes",
                schema: "dedsmeta",
                newName: "AttributeServiceAtLocation",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ServiceAreas",
                schema: "deds",
                newName: "ServiceArea",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ServiceAreaMetadata",
                schema: "dedsmeta",
                newName: "MetadataServiceArea",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ServiceAreaAttributes",
                schema: "dedsmeta",
                newName: "AttributeServiceArea",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Schedules",
                schema: "deds",
                newName: "Schedule",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ScheduleMetadata",
                schema: "dedsmeta",
                newName: "MetadataSchedule",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ScheduleAttributes",
                schema: "dedsmeta",
                newName: "AttributeSchedule",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "RequiredDocuments",
                schema: "deds",
                newName: "RequiredDocument",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "RequiredDocumentMetadata",
                schema: "dedsmeta",
                newName: "MetadataRequiredDocument",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "RequiredDocumentAttributes",
                schema: "dedsmeta",
                newName: "AttributeRequiredDocument",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Programs",
                schema: "deds",
                newName: "Program",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ProgramMetadata",
                schema: "dedsmeta",
                newName: "MetadataProgram",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "ProgramAttributes",
                schema: "dedsmeta",
                newName: "AttributeProgram",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Phones",
                schema: "deds",
                newName: "Phone",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "PhoneMetadata",
                schema: "dedsmeta",
                newName: "MetadataPhone",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "PhoneAttributes",
                schema: "dedsmeta",
                newName: "AttributePhone",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Organizations",
                schema: "deds",
                newName: "Organization",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "OrganizationMetadata",
                schema: "dedsmeta",
                newName: "MetadataOrganization",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "OrganizationIdentifiers",
                schema: "deds",
                newName: "OrganizationIdentifier",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "OrganizationIdentifierMetadata",
                schema: "dedsmeta",
                newName: "MetadataOrganizationIdentifier",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "OrganizationIdentifierAttributes",
                schema: "dedsmeta",
                newName: "AttributeOrganizationIdentifier",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "OrganizationAttributes",
                schema: "dedsmeta",
                newName: "AttributeOrganization",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "MetaTableDescriptions",
                schema: "deds",
                newName: "MetaTableDescription",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "MetaTableDescriptionAttributes",
                schema: "dedsmeta",
                newName: "AttributeMetaTableDescription",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Locations",
                schema: "deds",
                newName: "Location",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "LocationAttributes",
                schema: "dedsmeta",
                newName: "AttributeLocation",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Languages",
                schema: "deds",
                newName: "Language",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "LanguageAttributes",
                schema: "dedsmeta",
                newName: "AttributeLanguage",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Fundings",
                schema: "deds",
                newName: "Funding",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "FundingAttributes",
                schema: "dedsmeta",
                newName: "AttributeFunding",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "CostOptions",
                schema: "deds",
                newName: "CostOption",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "CostOptionAttributes",
                schema: "dedsmeta",
                newName: "AttributeCostOption",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Contacts",
                schema: "deds",
                newName: "Contact",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "ContactAttributes",
                schema: "dedsmeta",
                newName: "AttributeContact",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Attributes",
                schema: "deds",
                newName: "Attribute",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "deds",
                newName: "Address",
                newSchema: "deds");

            migrationBuilder.RenameTable(
                name: "AddressAttributes",
                schema: "dedsmeta",
                newName: "AddressAttribute",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "AccessibilityAttributes",
                schema: "dedsmeta",
                newName: "AccessibilityAttribute",
                newSchema: "dedsmeta");

            migrationBuilder.RenameTable(
                name: "Accessibilities",
                schema: "deds",
                newName: "Accessibility",
                newSchema: "deds");

            migrationBuilder.RenameIndex(
                name: "IX_TaxonomyTerms_TaxonomyId",
                schema: "deds",
                table: "TaxonomyTerm",
                newName: "IX_TaxonomyTerm_TaxonomyId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxonomyTermMetadata_TaxonomyTermId",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm",
                newName: "IX_MetadataTaxonomyTerm_TaxonomyTermId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxonomyMetadata_TaxonomyId",
                schema: "dedsmeta",
                table: "MetadataTaxonomy",
                newName: "IX_MetadataTaxonomy_TaxonomyId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ProgramId",
                schema: "deds",
                table: "Service",
                newName: "IX_Service_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_OrganizationId",
                schema: "deds",
                table: "Service",
                newName: "IX_Service_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceMetadata_ServiceId",
                schema: "dedsmeta",
                table: "MetadataService",
                newName: "IX_MetadataService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAttributes_ServiceId",
                schema: "dedsmeta",
                table: "AttributeService",
                newName: "IX_AttributeService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAtLocations_ServiceId",
                schema: "deds",
                table: "ServiceAtLocation",
                newName: "IX_ServiceAtLocation_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAtLocations_LocationId",
                schema: "deds",
                table: "ServiceAtLocation",
                newName: "IX_ServiceAtLocation_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAtLocationMetadata_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation",
                newName: "IX_MetadataServiceAtLocation_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAtLocationAttributes_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation",
                newName: "IX_AttributeServiceAtLocation_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAreas_ServiceId",
                schema: "deds",
                table: "ServiceArea",
                newName: "IX_ServiceArea_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAreaMetadata_ServiceAreaId",
                schema: "dedsmeta",
                table: "MetadataServiceArea",
                newName: "IX_MetadataServiceArea_ServiceAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceAreaAttributes_ServiceAreaId",
                schema: "dedsmeta",
                table: "AttributeServiceArea",
                newName: "IX_AttributeServiceArea_ServiceAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ServiceId",
                schema: "deds",
                table: "Schedule",
                newName: "IX_Schedule_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ServiceAtLocationId",
                schema: "deds",
                table: "Schedule",
                newName: "IX_Schedule_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_LocationId",
                schema: "deds",
                table: "Schedule",
                newName: "IX_Schedule_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleMetadata_ScheduleId",
                schema: "dedsmeta",
                table: "MetadataSchedule",
                newName: "IX_MetadataSchedule_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleAttributes_ScheduleId",
                schema: "dedsmeta",
                table: "AttributeSchedule",
                newName: "IX_AttributeSchedule_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_RequiredDocuments_ServiceId",
                schema: "deds",
                table: "RequiredDocument",
                newName: "IX_RequiredDocument_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RequiredDocumentMetadata_RequiredDocumentId",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument",
                newName: "IX_MetadataRequiredDocument_RequiredDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_RequiredDocumentAttributes_RequiredDocumentId",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument",
                newName: "IX_AttributeRequiredDocument_RequiredDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Programs_OrganizationId",
                schema: "deds",
                table: "Program",
                newName: "IX_Program_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramMetadata_ProgramId",
                schema: "dedsmeta",
                table: "MetadataProgram",
                newName: "IX_MetadataProgram_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramAttributes_ProgramId",
                schema: "dedsmeta",
                table: "AttributeProgram",
                newName: "IX_AttributeProgram_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_ServiceId",
                schema: "deds",
                table: "Phone",
                newName: "IX_Phone_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_ServiceAtLocationId",
                schema: "deds",
                table: "Phone",
                newName: "IX_Phone_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_OrganizationId",
                schema: "deds",
                table: "Phone",
                newName: "IX_Phone_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_LocationId",
                schema: "deds",
                table: "Phone",
                newName: "IX_Phone_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_ContactId",
                schema: "deds",
                table: "Phone",
                newName: "IX_Phone_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneMetadata_PhoneId",
                schema: "dedsmeta",
                table: "MetadataPhone",
                newName: "IX_MetadataPhone_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneAttributes_PhoneId",
                schema: "dedsmeta",
                table: "AttributePhone",
                newName: "IX_AttributePhone_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationMetadata_OrganizationId",
                schema: "dedsmeta",
                table: "MetadataOrganization",
                newName: "IX_MetadataOrganization_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationIdentifiers_OrganizationId",
                schema: "deds",
                table: "OrganizationIdentifier",
                newName: "IX_OrganizationIdentifier_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationIdentifierMetadata_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier",
                newName: "IX_MetadataOrganizationIdentifier_OrganizationIdentifierId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationIdentifierAttributes_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier",
                newName: "IX_AttributeOrganizationIdentifier_OrganizationIdentifierId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationAttributes_OrganizationId",
                schema: "dedsmeta",
                table: "AttributeOrganization",
                newName: "IX_AttributeOrganization_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_MetaTableDescriptionAttributes_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription",
                newName: "IX_AttributeMetaTableDescription_MetaTableDescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_OrganizationId",
                schema: "deds",
                table: "Location",
                newName: "IX_Location_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationAttributes_LocationId",
                schema: "dedsmeta",
                table: "AttributeLocation",
                newName: "IX_AttributeLocation_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_ServiceId",
                schema: "deds",
                table: "Language",
                newName: "IX_Language_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_PhoneId",
                schema: "deds",
                table: "Language",
                newName: "IX_Language_PhoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_LocationId",
                schema: "deds",
                table: "Language",
                newName: "IX_Language_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageAttributes_LanguageId",
                schema: "dedsmeta",
                table: "AttributeLanguage",
                newName: "IX_AttributeLanguage_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Fundings_ServiceId",
                schema: "deds",
                table: "Funding",
                newName: "IX_Funding_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Fundings_OrganizationId",
                schema: "deds",
                table: "Funding",
                newName: "IX_Funding_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_FundingAttributes_FundingId",
                schema: "dedsmeta",
                table: "AttributeFunding",
                newName: "IX_AttributeFunding_FundingId");

            migrationBuilder.RenameIndex(
                name: "IX_CostOptions_ServiceId",
                schema: "deds",
                table: "CostOption",
                newName: "IX_CostOption_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_CostOptionAttributes_CostOptionId",
                schema: "dedsmeta",
                table: "AttributeCostOption",
                newName: "IX_AttributeCostOption_CostOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_ServiceId",
                schema: "deds",
                table: "Contact",
                newName: "IX_Contact_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_ServiceAtLocationId",
                schema: "deds",
                table: "Contact",
                newName: "IX_Contact_ServiceAtLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_OrganizationId",
                schema: "deds",
                table: "Contact",
                newName: "IX_Contact_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_LocationId",
                schema: "deds",
                table: "Contact",
                newName: "IX_Contact_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAttributes_ContactId",
                schema: "dedsmeta",
                table: "AttributeContact",
                newName: "IX_AttributeContact_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Attributes_TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                newName: "IX_Attribute_TaxonomyTermId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_LocationId",
                schema: "deds",
                table: "Address",
                newName: "IX_Address_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressAttributes_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttribute",
                newName: "IX_AddressAttribute_AttributesId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessibilityAttributes_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttribute",
                newName: "IX_AccessibilityAttribute_AttributesId");

            migrationBuilder.RenameIndex(
                name: "IX_Accessibilities_LocationId",
                schema: "deds",
                table: "Accessibility",
                newName: "IX_Accessibility_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxonomyTerm",
                schema: "deds",
                table: "TaxonomyTerm",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataTaxonomyTerm",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm",
                columns: new[] { "MetadataId", "TaxonomyTermId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataTaxonomy",
                schema: "dedsmeta",
                table: "MetadataTaxonomy",
                columns: new[] { "MetadataId", "TaxonomyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Taxonomy",
                schema: "deds",
                table: "Taxonomy",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                schema: "deds",
                table: "Service",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataService",
                schema: "dedsmeta",
                table: "MetadataService",
                columns: new[] { "MetadataId", "ServiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeService",
                schema: "dedsmeta",
                table: "AttributeService",
                columns: new[] { "AttributesId", "ServiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceAtLocation",
                schema: "deds",
                table: "ServiceAtLocation",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataServiceAtLocation",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation",
                columns: new[] { "MetadataId", "ServiceAtLocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeServiceAtLocation",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation",
                columns: new[] { "AttributesId", "ServiceAtLocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceArea",
                schema: "deds",
                table: "ServiceArea",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataServiceArea",
                schema: "dedsmeta",
                table: "MetadataServiceArea",
                columns: new[] { "MetadataId", "ServiceAreaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeServiceArea",
                schema: "dedsmeta",
                table: "AttributeServiceArea",
                columns: new[] { "AttributesId", "ServiceAreaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                schema: "deds",
                table: "Schedule",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataSchedule",
                schema: "dedsmeta",
                table: "MetadataSchedule",
                columns: new[] { "MetadataId", "ScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeSchedule",
                schema: "dedsmeta",
                table: "AttributeSchedule",
                columns: new[] { "AttributesId", "ScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredDocument",
                schema: "deds",
                table: "RequiredDocument",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataRequiredDocument",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument",
                columns: new[] { "MetadataId", "RequiredDocumentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeRequiredDocument",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument",
                columns: new[] { "AttributesId", "RequiredDocumentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Program",
                schema: "deds",
                table: "Program",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataProgram",
                schema: "dedsmeta",
                table: "MetadataProgram",
                columns: new[] { "MetadataId", "ProgramId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeProgram",
                schema: "dedsmeta",
                table: "AttributeProgram",
                columns: new[] { "AttributesId", "ProgramId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phone",
                schema: "deds",
                table: "Phone",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataPhone",
                schema: "dedsmeta",
                table: "MetadataPhone",
                columns: new[] { "MetadataId", "PhoneId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributePhone",
                schema: "dedsmeta",
                table: "AttributePhone",
                columns: new[] { "AttributesId", "PhoneId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                schema: "deds",
                table: "Organization",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataOrganization",
                schema: "dedsmeta",
                table: "MetadataOrganization",
                columns: new[] { "MetadataId", "OrganizationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationIdentifier",
                schema: "deds",
                table: "OrganizationIdentifier",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetadataOrganizationIdentifier",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier",
                columns: new[] { "MetadataId", "OrganizationIdentifierId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeOrganizationIdentifier",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier",
                columns: new[] { "AttributesId", "OrganizationIdentifierId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeOrganization",
                schema: "dedsmeta",
                table: "AttributeOrganization",
                columns: new[] { "AttributesId", "OrganizationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaTableDescription",
                schema: "deds",
                table: "MetaTableDescription",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeMetaTableDescription",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription",
                columns: new[] { "AttributesId", "MetaTableDescriptionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                schema: "deds",
                table: "Location",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeLocation",
                schema: "dedsmeta",
                table: "AttributeLocation",
                columns: new[] { "AttributesId", "LocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                schema: "deds",
                table: "Language",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeLanguage",
                schema: "dedsmeta",
                table: "AttributeLanguage",
                columns: new[] { "AttributesId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funding",
                schema: "deds",
                table: "Funding",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeFunding",
                schema: "dedsmeta",
                table: "AttributeFunding",
                columns: new[] { "AttributesId", "FundingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostOption",
                schema: "deds",
                table: "CostOption",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeCostOption",
                schema: "dedsmeta",
                table: "AttributeCostOption",
                columns: new[] { "AttributesId", "CostOptionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                schema: "deds",
                table: "Contact",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeContact",
                schema: "dedsmeta",
                table: "AttributeContact",
                columns: new[] { "AttributesId", "ContactId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attribute",
                schema: "deds",
                table: "Attribute",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                schema: "deds",
                table: "Address",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressAttribute",
                schema: "dedsmeta",
                table: "AddressAttribute",
                columns: new[] { "AddressId", "AttributesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessibilityAttribute",
                schema: "dedsmeta",
                table: "AccessibilityAttribute",
                columns: new[] { "AccessibilityId", "AttributesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accessibility",
                schema: "deds",
                table: "Accessibility",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Accessibility_Location_LocationId",
                schema: "deds",
                table: "Accessibility",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityAttribute_Accessibility_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityAttribute",
                column: "AccessibilityId",
                principalSchema: "deds",
                principalTable: "Accessibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityAttribute_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AccessibilityAttribute",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibilityMetadata_Accessibility_AccessibilityId",
                schema: "dedsmeta",
                table: "AccessibilityMetadata",
                column: "AccessibilityId",
                principalSchema: "deds",
                principalTable: "Accessibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Location_LocationId",
                schema: "deds",
                table: "Address",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAttribute_Address_AddressId",
                schema: "dedsmeta",
                table: "AddressAttribute",
                column: "AddressId",
                principalSchema: "deds",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAttribute_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AddressAttribute",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressMetadata_Address_AddressId",
                schema: "dedsmeta",
                table: "AddressMetadata",
                column: "AddressId",
                principalSchema: "deds",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_TaxonomyTerm_TaxonomyTermId",
                schema: "deds",
                table: "Attribute",
                column: "TaxonomyTermId",
                principalSchema: "deds",
                principalTable: "TaxonomyTerm",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeContact_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeContact",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeContact_Contact_ContactId",
                schema: "dedsmeta",
                table: "AttributeContact",
                column: "ContactId",
                principalSchema: "deds",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCostOption_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeCostOption",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCostOption_CostOption_CostOptionId",
                schema: "dedsmeta",
                table: "AttributeCostOption",
                column: "CostOptionId",
                principalSchema: "deds",
                principalTable: "CostOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeFunding_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeFunding",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeFunding_Funding_FundingId",
                schema: "dedsmeta",
                table: "AttributeFunding",
                column: "FundingId",
                principalSchema: "deds",
                principalTable: "Funding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLanguage_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeLanguage",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLanguage_Language_LanguageId",
                schema: "dedsmeta",
                table: "AttributeLanguage",
                column: "LanguageId",
                principalSchema: "deds",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLocation_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeLocation",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLocation_Location_LocationId",
                schema: "dedsmeta",
                table: "AttributeLocation",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeMetadata_Attribute_AttributeId",
                schema: "dedsmeta",
                table: "AttributeMetadata",
                column: "AttributeId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeMetaTableDescription_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeMetaTableDescription_MetaTableDescription_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "AttributeMetaTableDescription",
                column: "MetaTableDescriptionId",
                principalSchema: "deds",
                principalTable: "MetaTableDescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOrganization_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeOrganization",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOrganization_Organization_OrganizationId",
                schema: "dedsmeta",
                table: "AttributeOrganization",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOrganizationIdentifier_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOrganizationIdentifier_OrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "AttributeOrganizationIdentifier",
                column: "OrganizationIdentifierId",
                principalSchema: "deds",
                principalTable: "OrganizationIdentifier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributePhone_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributePhone",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributePhone_Phone_PhoneId",
                schema: "dedsmeta",
                table: "AttributePhone",
                column: "PhoneId",
                principalSchema: "deds",
                principalTable: "Phone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeProgram_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeProgram",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeProgram_Program_ProgramId",
                schema: "dedsmeta",
                table: "AttributeProgram",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeRequiredDocument_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeRequiredDocument_RequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "AttributeRequiredDocument",
                column: "RequiredDocumentId",
                principalSchema: "deds",
                principalTable: "RequiredDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeSchedule_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeSchedule",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeSchedule_Schedule_ScheduleId",
                schema: "dedsmeta",
                table: "AttributeSchedule",
                column: "ScheduleId",
                principalSchema: "deds",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeService_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeService",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeService_Service_ServiceId",
                schema: "dedsmeta",
                table: "AttributeService",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeServiceArea_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeServiceArea",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeServiceArea_ServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "AttributeServiceArea",
                column: "ServiceAreaId",
                principalSchema: "deds",
                principalTable: "ServiceArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeServiceAtLocation_Attribute_AttributesId",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation",
                column: "AttributesId",
                principalSchema: "deds",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeServiceAtLocation_ServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "AttributeServiceAtLocation",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ContactMetadata_Contact_ContactId",
                schema: "dedsmeta",
                table: "ContactMetadata",
                column: "ContactId",
                principalSchema: "deds",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_CostOptionMetadata_CostOption_CostOptionId",
                schema: "dedsmeta",
                table: "CostOptionMetadata",
                column: "CostOptionId",
                principalSchema: "deds",
                principalTable: "CostOption",
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
                name: "FK_FundingMetadata_Funding_FundingId",
                schema: "dedsmeta",
                table: "FundingMetadata",
                column: "FundingId",
                principalSchema: "deds",
                principalTable: "Funding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_LanguageMetadata_Language_LanguageId",
                schema: "dedsmeta",
                table: "LanguageMetadata",
                column: "LanguageId",
                principalSchema: "deds",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                schema: "deds",
                table: "Location",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationMetadata_Location_LocationId",
                schema: "dedsmeta",
                table: "LocationMetadata",
                column: "LocationId",
                principalSchema: "deds",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataOrganization_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataOrganization",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataOrganization_Organization_OrganizationId",
                schema: "dedsmeta",
                table: "MetadataOrganization",
                column: "OrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataOrganizationIdentifier_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataOrganizationIdentifier_OrganizationIdentifier_OrganizationIdentifierId",
                schema: "dedsmeta",
                table: "MetadataOrganizationIdentifier",
                column: "OrganizationIdentifierId",
                principalSchema: "deds",
                principalTable: "OrganizationIdentifier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataPhone_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataPhone",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataPhone_Phone_PhoneId",
                schema: "dedsmeta",
                table: "MetadataPhone",
                column: "PhoneId",
                principalSchema: "deds",
                principalTable: "Phone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataProgram_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataProgram",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataProgram_Program_ProgramId",
                schema: "dedsmeta",
                table: "MetadataProgram",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataRequiredDocument_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataRequiredDocument_RequiredDocument_RequiredDocumentId",
                schema: "dedsmeta",
                table: "MetadataRequiredDocument",
                column: "RequiredDocumentId",
                principalSchema: "deds",
                principalTable: "RequiredDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataSchedule_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataSchedule",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataSchedule_Schedule_ScheduleId",
                schema: "dedsmeta",
                table: "MetadataSchedule",
                column: "ScheduleId",
                principalSchema: "deds",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataService_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataService",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataService_Service_ServiceId",
                schema: "dedsmeta",
                table: "MetadataService",
                column: "ServiceId",
                principalSchema: "deds",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataServiceArea_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataServiceArea",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataServiceArea_ServiceArea_ServiceAreaId",
                schema: "dedsmeta",
                table: "MetadataServiceArea",
                column: "ServiceAreaId",
                principalSchema: "deds",
                principalTable: "ServiceArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataServiceAtLocation_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataServiceAtLocation_ServiceAtLocation_ServiceAtLocationId",
                schema: "dedsmeta",
                table: "MetadataServiceAtLocation",
                column: "ServiceAtLocationId",
                principalSchema: "deds",
                principalTable: "ServiceAtLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataTaxonomy_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataTaxonomy",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataTaxonomy_Taxonomy_TaxonomyId",
                schema: "dedsmeta",
                table: "MetadataTaxonomy",
                column: "TaxonomyId",
                principalSchema: "deds",
                principalTable: "Taxonomy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataTaxonomyTerm_Metadata_MetadataId",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm",
                column: "MetadataId",
                principalSchema: "deds",
                principalTable: "Metadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataTaxonomyTerm_TaxonomyTerm_TaxonomyTermId",
                schema: "dedsmeta",
                table: "MetadataTaxonomyTerm",
                column: "TaxonomyTermId",
                principalSchema: "deds",
                principalTable: "TaxonomyTerm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetaTableDescriptionMetadata_MetaTableDescription_MetaTableDescriptionId",
                schema: "dedsmeta",
                table: "MetaTableDescriptionMetadata",
                column: "MetaTableDescriptionId",
                principalSchema: "deds",
                principalTable: "MetaTableDescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
