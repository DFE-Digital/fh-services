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
                    { 3, "getPaginatedListOfServices", null, null, "", null, 200 },
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
