using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserAccountFromConReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFacts_UserAccountDim_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropIndex(
                name: "IX_ConnectionRequestsSentFacts_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropColumn(
                name: "UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestsSentFacts_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "UserAccountKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFacts_UserAccountDim_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "UserAccountKey",
                principalSchema: "idam",
                principalTable: "UserAccountDim",
                principalColumn: "UserAccountKey");
        }
    }
}
