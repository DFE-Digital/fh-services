using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.OpenReferral.Function.Migrations
{
    /// <inheritdoc />
    public partial class TestTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_Organization_ParentOrganizationId",
                schema: "deds",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_ParentOrganizationId",
                schema: "deds",
                table: "Organization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentOrganizationId",
                schema: "deds",
                table: "Organization",
                column: "ParentOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_Organization_ParentOrganizationId",
                schema: "deds",
                table: "Organization",
                column: "ParentOrganizationId",
                principalSchema: "deds",
                principalTable: "Organization",
                principalColumn: "Id");
        }
    }
}
