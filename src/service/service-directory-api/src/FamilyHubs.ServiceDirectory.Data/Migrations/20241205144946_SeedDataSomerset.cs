using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataSomerset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 IF NOT EXISTS (SELECT * FROM Organisations WHERE Name = 'Somerset Council')
                                 BEGIN
                                 INSERT INTO Organisations (OrganisationType, Name, Description, AdminAreaCode, Uri, Url) VALUES
                                    ('LA', 'Somerset Council', 'Somerset Council', 'E06000066', 'https://www.somerset.gov.uk/', 'https://www.somerset.gov.uk/');
                                 END;
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Method intentionally left empty.
        }
    }
}
