using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVcsOrgIdColumnToConnectionRequestsSentFact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VcsOrganisationId",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VcsOrganisationId",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");
        }
    }
}
