using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectionFactTableWithOrganisationAndUserAccountDimTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "idam");

            migrationBuilder.CreateTable(
                name: "OrganisationDim",
                schema: "idam",
                columns: table => new
                {
                    OrganisationKey = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    OrganisationTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrganisationId = table.Column<long>(type: "bigint", nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    SysStartTime = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    SysEndTime = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationDim", x => x.OrganisationKey)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountDim",
                schema: "idam",
                columns: table => new
                {
                    UserAccountKey = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    UserAccountRoleTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    UserAccountRoleTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrganisationTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    OrganisationId = table.Column<long>(type: "bigint", nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    OrganisationTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    LastModifiedById = table.Column<long>(type: "bigint", nullable: false),
                    SysStartTime = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    SysEndTime = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountDim", x => x.UserAccountKey)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionRequestsSentFact",
                schema: "dim",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateKey = table.Column<int>(type: "int", nullable: false),
                    TimeKey = table.Column<int>(type: "int", nullable: false),
                    OrganisationKey = table.Column<long>(type: "bigint", nullable: false),
                    UserAccountKey = table.Column<long>(type: "bigint", nullable: false),
                    ConnectionRequestsSentMetricsId = table.Column<long>(type: "bigint", nullable: false),
                    RequestTimestamp = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    RequestCorrelationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResponseTimestamp = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: true),
                    HttpResponseCode = table.Column<short>(type: "smallint", nullable: true),
                    ConnectionRequestId = table.Column<long>(type: "bigint", nullable: true),
                    ConnectionRequestReferenceCode = table.Column<string>(type: "nchar(6)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2(7)", precision: 7, nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionRequestsSentFact", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ConnectionRequestsSentFact_DateDim_DateKey",
                        column: x => x.DateKey,
                        principalSchema: "dim",
                        principalTable: "DateDim",
                        principalColumn: "DateKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionRequestsSentFact_OrganisationDim_OrganisationKey",
                        column: x => x.OrganisationKey,
                        principalSchema: "idam",
                        principalTable: "OrganisationDim",
                        principalColumn: "OrganisationKey");
                    table.ForeignKey(
                        name: "FK_ConnectionRequestsSentFact_TimeDim_TimeKey",
                        column: x => x.TimeKey,
                        principalSchema: "dim",
                        principalTable: "TimeDim",
                        principalColumn: "TimeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionRequestsSentFact_UserAccountDim_UserAccountKey",
                        column: x => x.UserAccountKey,
                        principalSchema: "idam",
                        principalTable: "UserAccountDim",
                        principalColumn: "UserAccountKey");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestsSentFact_DateKey",
                schema: "dim",
                table: "ConnectionRequestsSentFact",
                column: "DateKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestsSentFact_OrganisationKey",
                schema: "dim",
                table: "ConnectionRequestsSentFact",
                column: "OrganisationKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestsSentFact_TimeKey",
                schema: "dim",
                table: "ConnectionRequestsSentFact",
                column: "TimeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestsSentFact_UserAccountKey",
                schema: "dim",
                table: "ConnectionRequestsSentFact",
                column: "UserAccountKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionRequestsSentFact",
                schema: "dim");

            migrationBuilder.DropTable(
                name: "OrganisationDim",
                schema: "idam");

            migrationBuilder.DropTable(
                name: "UserAccountDim",
                schema: "idam");
        }
    }
}
