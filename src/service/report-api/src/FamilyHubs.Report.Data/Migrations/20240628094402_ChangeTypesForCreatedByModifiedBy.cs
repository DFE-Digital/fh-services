using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypesForCreatedByModifiedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "idam",
                table: "UserAccountDim");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                schema: "idam",
                table: "UserAccountDim");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "idam",
                table: "UserAccountDim",
                type: "nvarchar(320)",
                maxLength: 320,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "idam",
                table: "UserAccountDim",
                type: "nvarchar(320)",
                maxLength: 320,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "idam",
                table: "UserAccountDim");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "idam",
                table: "UserAccountDim");

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                schema: "idam",
                table: "UserAccountDim",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedById",
                schema: "idam",
                table: "UserAccountDim",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
