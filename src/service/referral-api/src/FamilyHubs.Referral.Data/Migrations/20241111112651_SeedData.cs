using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Referral.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF NOT EXISTS (SELECT * FROM Statuses)
                BEGIN
                    INSERT INTO Statuses (Id, Name, SortOrder, SecondrySortOrder, Created, CreatedBy) VALUES
                        (1, 'New', 0, 1, getutcdate(), 'SYSTEM'),
                        (2, 'Opened', 1, 1, getutcdate(), 'SYSTEM'),
                        (3, 'Accepted', 2, 2, getutcdate(), 'SYSTEM'),
                        (4, 'Declined', 3, 0, getutcdate(), 'SYSTEM');
                END;
            """);
            
            migrationBuilder.Sql("""
                IF NOT EXISTS (SELECT * FROM Roles)
                BEGIN
                    INSERT INTO Roles (Id, Name, Description, Created, CreatedBy) VALUES
                        (1, 'DfeAdmin', 'DfE Administrator', getutcdate(), 'SYSTEM'),
                        (2, 'LaManager', 'Local Authority Manager', getutcdate(), 'SYSTEM'),
                        (3, 'VcsManager', 'VCS Manager', getutcdate(), 'SYSTEM'),
                        (4, 'LaProfessional', 'Local Authority Professional', getutcdate(), 'SYSTEM'),
                        (5, 'VcsProfessional', 'VCS Professional', getutcdate(), 'SYSTEM'),
                        (6, 'LaDualRole', 'Local Authority Dual Role', getutcdate(), 'SYSTEM'),
                        (7, 'VcsDualRole', 'VCS Dual Role', getutcdate(), 'SYSTEM');
                END;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Not supported
        }
    }
}
