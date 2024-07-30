using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemaFromDimToSVCD_STG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "svcd_stg");

            migrationBuilder.RenameTable(
                name: "ConnectionRequestsSentFact",
                schema: "dim",
                newName: "ConnectionRequestsSentFact",
                newSchema: "svcd_stg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ConnectionRequestsSentFact",
                schema: "svcd_stg",
                newName: "ConnectionRequestsSentFact",
                newSchema: "dim");
        }
    }
}
