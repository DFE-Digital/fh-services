using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConnectionRequestRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SysEndTime",
                schema: "idam",
                table: "OrganisationDim");

            migrationBuilder.DropColumn(
                name: "SysStartTime",
                schema: "idam",
                table: "OrganisationDim");

            RemoveIdentityOnId(migrationBuilder, "dim", "ServiceSearchFacts");

            // this is what EF generates when you ask it to remove the identity on a column
            // but it doesn't actually work, so we do it manually in RemoveIdentityOnId above instead

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    schema: "dim",
            //    table: "ServiceSearchFacts",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "datetime2(7)",
                precision: 7,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldPrecision: 7);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            RemoveIdentityOnId(migrationBuilder, "dim", "ConnectionRequestsSentFacts");

            // (as above), original EF generated removal of identity

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    schema: "dim",
            //    table: "ConnectionRequestsSentFacts",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // original (non-working) EF generated addition of identity on the ID column.
            // if we ever need to re-add the identity, we'll need to write custom code to re-add it

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    schema: "dim",
            //    table: "ServiceSearchFacts",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "SysEndTime",
                schema: "idam",
                table: "OrganisationDim",
                type: "datetime2(7)",
                precision: 7,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SysStartTime",
                schema: "idam",
                table: "OrganisationDim",
                type: "datetime2(7)",
                precision: 7,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "datetime2(7)",
                precision: 7,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldPrecision: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "dim",
                table: "ConnectionRequestsSentFacts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            // original (non-working) EF generated addition of identity on the ID column

            //migrationBuilder.AlterColumn<long>(
            //    name: "Id",
            //    schema: "dim",
            //    table: "ConnectionRequestsSentFacts",
            //    type: "bigint",
            //    nullable: false,
            //    oldClrType: typeof(long),
            //    oldType: "bigint")
            //    .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <summary>
        /// Entity Framework Core does not (seem to) support removing the identity on a column.
        /// So we jump through some hoops to remove the identity on the Id column.
        /// </summary>
        private void RemoveIdentityOnId(MigrationBuilder migrationBuilder, string schema, string tableName)
        {
            string primaryKeyConstraintName = $"PK_{tableName}";

            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: primaryKeyConstraintName,
                schema: schema,
                table: tableName);

            // Add a temporary column
            migrationBuilder.AddColumn<long>(
                name: "TempId",
                schema: schema,
                table: tableName,
                type: "bigint",
                nullable: false,
                defaultValue: -1L);

            // Copy data from Id to TempId
            migrationBuilder.Sql($"UPDATE {schema}.{tableName} SET TempId = Id;");

            // Drop the original Id column
            migrationBuilder.DropColumn(
                name: "Id",
                schema: schema,
                table: tableName);

            // Rename TempId to Id
            migrationBuilder.RenameColumn(
                name: "TempId",
                schema: schema,
                table: tableName,
                newName: "Id");

            // Add the primary key back
            migrationBuilder.AddPrimaryKey(
                name: primaryKeyConstraintName,
                schema: schema,
                table: tableName,
                column: "Id");
        }
    }
}
