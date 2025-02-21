using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class dedsNewTablesAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StandardVersions",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThirdParties",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OpenReferralUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartyServices",
                schema: "deds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ThirdPartyId = table.Column<int>(type: "int", nullable: false),
                    StandardVersionId = table.Column<byte>(type: "tinyint", nullable: false),
                    Document = table.Column<string>(type: "NVARCHAR(max)", nullable: false),
                    Checksum = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyServices", x => x.Id);
                    table.UniqueConstraint("AK_ThirdPartyServices_ServiceId_ThirdPartyId", x => new { x.ServiceId, x.ThirdPartyId });
                    table.ForeignKey(
                        name: "FK_ThirdPartyServices_StandardVersions_StandardVersionId",
                        column: x => x.StandardVersionId,
                        principalSchema: "deds",
                        principalTable: "StandardVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThirdPartyServices_ThirdParties_ThirdPartyId",
                        column: x => x.ThirdPartyId,
                        principalSchema: "deds",
                        principalTable: "ThirdParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "deds",
                table: "StandardVersions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Open Referral UK V2" },
                    { (byte)2, "Open Referral International V3.0" }
                });

            migrationBuilder.InsertData(
                schema: "deds",
                table: "ThirdParties",
                columns: new[] { "Id", "Name", "OpenReferralUrl" },
                values: new object[] { 1, "MockHsds", "https://dfe-mock-hsds.com" });

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyServices_ServiceId",
                schema: "deds",
                table: "ThirdPartyServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyServices_StandardVersionId",
                schema: "deds",
                table: "ThirdPartyServices",
                column: "StandardVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyServices_ThirdPartyId",
                schema: "deds",
                table: "ThirdPartyServices",
                column: "ThirdPartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThirdPartyServices",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "StandardVersions",
                schema: "deds");

            migrationBuilder.DropTable(
                name: "ThirdParties",
                schema: "deds");
        }
    }
}
