using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Referral.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToManyRelationshipBetweenOrganisationAndReferralServiceAndRemoveOneToOneRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_ReferralServices_ReferralServiceId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_ReferralServiceId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "ReferralServiceId",
                table: "Organisations");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "ReferralServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralServices_OrganizationId",
                table: "ReferralServices",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralServices_Organisations_OrganizationId",
                table: "ReferralServices",
                column: "OrganizationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralServices_Organisations_OrganizationId",
                table: "ReferralServices");

            migrationBuilder.DropIndex(
                name: "IX_ReferralServices_OrganizationId",
                table: "ReferralServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "ReferralServices");

            migrationBuilder.AddColumn<long>(
                name: "ReferralServiceId",
                table: "Organisations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_ReferralServiceId",
                table: "Organisations",
                column: "ReferralServiceId",
                unique: true,
                filter: "[ReferralServiceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_ReferralServices_ReferralServiceId",
                table: "Organisations",
                column: "ReferralServiceId",
                principalTable: "ReferralServices",
                principalColumn: "Id");
        }
    }
}
