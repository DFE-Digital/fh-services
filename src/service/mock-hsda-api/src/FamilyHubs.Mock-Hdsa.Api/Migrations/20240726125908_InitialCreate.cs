using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyHubs.Mock_Hdsa.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MockResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScenarioName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathParams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryParams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockResponses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MockResponses",
                columns: new[] { "Id", "OperationName", "PathParams", "QueryParams", "ResponseBody", "ScenarioName", "StatusCode" },
                values: new object[,]
                {
                    { 1, "getAPIMetaInformation", null, null, "{\r\n  \"version\": \"3.0\",\r\n  \"profile\": \"https://todo/put/our/profile/uri/here\",\r\n  \"openapi_url\": \"https://raw.githubusercontent.com/openreferral/specification/3.0/schema/openapi.json\"\r\n}\r\n", null, 200 },
                    { 2, "getFullyNestedServiceById", null, null, "", null, 200 },
                    { 3, "getPaginatedListOfServices", null, null, "    {\r\n      \"total_items\": 10,\r\n      \"total_pages\": 10,\r\n      \"page_number\": 1,\r\n      \"size\": 1,\r\n      \"first_page\": true,\r\n      \"last_page\": false,\r\n      \"empty\": false,\r\n      \"contents\": [\r\n        {\r\n          \"id\": \"ac148810-d857-441c-9679-408f346de14b\",\r\n          \"name\": \"Community Counselling\",\r\n          \"alternate_name\": \"MyCity Counselling Services\",\r\n          \"description\": \"Counselling Services provided by trained professionals. Suitable for people with mental health conditions such as anxiety, depression, or eating disorders as well as people experiencing difficult life events and circumstances. \",\r\n          \"url\": \"http://example.com/counselling\",\r\n          \"email\": \"email@example.com\",\r\n          \"status\": \"active\",\r\n          \"interpretation_services\": \"Interpretation services are available in Urdu, Polish, and Slovak\",\r\n          \"application_process\": \"If you are an NHS patient please ask your GP for a referral letter, we will then be in touch with you directly. If you are not an NHS patient you should ring our reception to arrange an appointment\",\r\n          \"fees_description\": \"Non-NHS patients are expected to pay for their counselling sessions. We charge a flat rate per hour of counselling. The current rate is \\u00a350 per hour. Please see our website for up to date prices.\",\r\n          \"wait_time\": \"wait_time\",\r\n          \"fees\": \"fees_description\",\r\n          \"accreditations\": \"All of our practitioners are accredited by the BASC, UKCP, and the Professional Standards Body\",\r\n          \"eligibility_description\": \"This service is intended for all people aged 12 and over who require counselling services in the MyCity area\",\r\n          \"minimum_age\": 12,\r\n          \"maximum_age\": 100,\r\n          \"assured_date\": \"2005-01-01\",\r\n          \"assurer_email\": \"email@example.com\",\r\n          \"licenses\": \"licences\",\r\n          \"alert\": \"Following COVID-19 we have moved most of our counselling sessions online. Please contact the reception if you require further information.\",\r\n          \"last_modified\": \"2023-03-15T10:30:45.123Z\",\r\n          \"organization\": {\r\n            \"id\": \"d9d5e0f5-d3ce-4f73-9a2f-4dd0ecc6c610\",\r\n            \"name\": \"Example Organization Inc.\",\r\n            \"alternate_name\": \"Example Org\",\r\n            \"description\": \"Example Org is a non-profit organization dedicated to providing services to qualified beneficiaries\",\r\n            \"email\": \"email@example.com\",\r\n            \"website\": \"http://example.com\",\r\n            \"tax_status\": \"tax_status\",\r\n            \"year_incorporated\": 2011,\r\n            \"legal_status\": \"Limited Company\",\r\n            \"logo\": \"https://openreferral.org/wp-content/uploads/2018/02/OpenReferral_Logo_Green-4-1.png\",\r\n            \"uri\": \"http://example.com\",\r\n            \"parent_organization_id\": \"cd09a387-91f4-4555-94ec-e799c35344cd\"\r\n          },\r\n          \"program\": {\r\n            \"id\": \"e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42\",\r\n            \"name\": \"Community Mental Health Support\",\r\n            \"alternate_name\": \"MyCity Mental Health Group\",\r\n            \"description\": \"Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services.\"\r\n          }\r\n        }\r\n      ]\r\n    }", null, 200 },
                    { 4, "getTaxonomyById", null, null, "", null, 200 },
                    { 5, "getPaginatedListOfTaxonomies", null, null, "", null, 200 },
                    { 6, "getPaginatedListOfTaxonomyTerms", null, null, "", null, 200 },
                    { 7, "getTaxonomyTermById", null, null, "", null, 200 },
                    { 8, "getOrganizationById", null, null, "", null, 200 },
                    { 9, "getPaginatedListOfOrganizations", null, null, "", null, 200 },
                    { 10, "getServiceAtLocationWithNestedDataById", null, null, "", null, 200 },
                    { 11, "getPaginatedListOfServiceAtLocation", null, null, "", null, 200 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MockResponses");
        }
    }
}
