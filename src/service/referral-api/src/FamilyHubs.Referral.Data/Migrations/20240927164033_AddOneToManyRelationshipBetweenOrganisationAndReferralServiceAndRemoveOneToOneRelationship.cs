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
            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "ReferralServices",
                type: "bigint",
                nullable: true);

            /*
             * We need to manually update the new OrganizationId column of each existing ReferralService with their
             * Organisation, where the ReferralServiceId matches the ReferralService's ID before that column is deleted
             * from the Organisations table.
             *
             * If there are any ReferralServices with a NULL OrganizationId after this step, we just delete them because
             * this means they are just dodgy data and would cause errors in the main application anyway (even before this migration),
             * as even the old code had a required relationship between ReferralServices and Organisations.
             *
             * Afterwards we can add the foreign key with intact data and then alter OrganizationId to make it NOT NULL
             */
            migrationBuilder.Sql("UPDATE [dbo].[ReferralServices] SET [OrganizationId] = [o].[Id] FROM [dbo].[Organisations] AS [o] WHERE [dbo].[ReferralServices].[Id] = [o].[ReferralServiceId]");
            migrationBuilder.Sql("DELETE FROM [dbo].[ReferralServices] WHERE [OrganizationId] IS NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralServices_Organisations_OrganizationId",
                table: "ReferralServices",
                column: "OrganizationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "ReferralServices",
                type: "bigint",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralServices_OrganizationId",
                table: "ReferralServices",
                column: "OrganizationId");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_ReferralServices_ReferralServiceId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_ReferralServiceId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "ReferralServiceId",
                table: "Organisations");
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
