using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVersionColumnFromDedsTaxonomyTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                schema: "deds",
                table: "TaxonomyTerms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "deds",
                table: "TaxonomyTerms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
