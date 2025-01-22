using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class programIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Programs_ProgramId",
                schema: "deds",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgramId",
                schema: "deds",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Programs_ProgramId",
                schema: "deds",
                table: "Services",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Programs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Programs_ProgramId",
                schema: "deds",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProgramId",
                schema: "deds",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Programs_ProgramId",
                schema: "deds",
                table: "Services",
                column: "ProgramId",
                principalSchema: "deds",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
