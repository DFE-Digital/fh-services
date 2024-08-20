using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameVcsOrganisationIdToKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VcsOrganisationId",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.AddColumn<long>(
                name: "VcsOrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestsSentFacts_VcsOrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "VcsOrganisationKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFacts_OrganisationDim_VcsOrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "VcsOrganisationKey",
                principalSchema: "idam",
                principalTable: "OrganisationDim",
                principalColumn: "OrganisationKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFacts_OrganisationDim_VcsOrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropIndex(
                name: "IX_ConnectionRequestsSentFacts_VcsOrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropColumn(
                name: "VcsOrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.AddColumn<long>(
                name: "VcsOrganisationId",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
