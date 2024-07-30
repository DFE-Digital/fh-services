using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixNullableFieldsAndChangeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFact_DateDim_DateKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFact_OrganisationDim_OrganisationKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFact_TimeDim_TimeKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFact_UserAccountDim_UserAccountKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionRequestsSentFact",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact");

            migrationBuilder.RenameTable(
                name: "ConnectionRequestsSentFact",
                schema: "svcd_stg",
                newName: "ConnectionRequestsSentFacts",
                newSchema: "dim");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFact_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                newName: "IX_ConnectionRequestsSentFacts_UserAccountKey");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFact_TimeKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                newName: "IX_ConnectionRequestsSentFacts_TimeKey");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFact_OrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                newName: "IX_ConnectionRequestsSentFacts_OrganisationKey");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFact_DateKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                newName: "IX_ConnectionRequestsSentFacts_DateKey");

            migrationBuilder.AlterColumn<long>(
                name: "UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "OrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionRequestsSentFacts",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFacts_DateDim_DateKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "DateKey",
                principalSchema: "dim",
                principalTable: "DateDim",
                principalColumn: "DateKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFacts_OrganisationDim_OrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "OrganisationKey",
                principalSchema: "idam",
                principalTable: "OrganisationDim",
                principalColumn: "OrganisationKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFacts_TimeDim_TimeKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "TimeKey",
                principalSchema: "dim",
                principalTable: "TimeDim",
                principalColumn: "TimeKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFacts_UserAccountDim_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                column: "UserAccountKey",
                principalSchema: "idam",
                principalTable: "UserAccountDim",
                principalColumn: "UserAccountKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFacts_DateDim_DateKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFacts_OrganisationDim_OrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFacts_TimeDim_TimeKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequestsSentFacts_UserAccountDim_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionRequestsSentFacts",
                schema: "dim",
                table: "ConnectionRequestsSentFacts");

            migrationBuilder.EnsureSchema(
                name: "svcd_stg");

            migrationBuilder.RenameTable(
                name: "ConnectionRequestsSentFacts",
                schema: "dim",
                newName: "ConnectionRequestsSentFact",
                newSchema: "svcd_stg");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFacts_UserAccountKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                newName: "IX_ConnectionRequestsSentFact_UserAccountKey");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFacts_TimeKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                newName: "IX_ConnectionRequestsSentFact_TimeKey");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFacts_OrganisationKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                newName: "IX_ConnectionRequestsSentFact_OrganisationKey");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionRequestsSentFacts_DateKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                newName: "IX_ConnectionRequestsSentFact_DateKey");

            migrationBuilder.AlterColumn<long>(
                name: "UserAccountKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrganisationKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionRequestsSentFact",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFact_DateDim_DateKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                column: "DateKey",
                principalSchema: "dim",
                principalTable: "DateDim",
                principalColumn: "DateKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFact_OrganisationDim_OrganisationKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                column: "OrganisationKey",
                principalSchema: "idam",
                principalTable: "OrganisationDim",
                principalColumn: "OrganisationKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFact_TimeDim_TimeKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                column: "TimeKey",
                principalSchema: "dim",
                principalTable: "TimeDim",
                principalColumn: "TimeKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequestsSentFact_UserAccountDim_UserAccountKey",
                schema: "svcd_stg",
                table: "ConnectionRequestsSentFact",
                column: "UserAccountKey",
                principalSchema: "idam",
                principalTable: "UserAccountDim",
                principalColumn: "UserAccountKey");
        }
    }
}
