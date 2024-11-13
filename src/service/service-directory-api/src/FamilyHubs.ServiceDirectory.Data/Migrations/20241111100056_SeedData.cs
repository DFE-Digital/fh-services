using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.ServiceDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedTaxonomies(migrationBuilder);
            SeedOrganisations(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Not supported
        }

        public void SeedTaxonomies(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF NOT EXISTS (SELECT * FROM Taxonomies)
                BEGIN
                    BEGIN TRANSACTION;
                    DECLARE @ParentTaxonomies table(id bigint, name nvarchar(255));
                    DECLARE @Taxonomy bigint;
                
                    INSERT Taxonomies (Name, TaxonomyType) OUTPUT INSERTED.Id, INSERTED.Name INTO @ParentTaxonomies VALUES
                        ('Activities, clubs and groups', 'ServiceCategory'),
                        ('Family support', 'ServiceCategory'),
                        ('Health', 'ServiceCategory'),
                        ('Pregnancy, birth and early years', 'ServiceCategory'),
                        ('Special educational needs and disabilities (SEND)', 'ServiceCategory'),
                        ('Transport', 'ServiceCategory');
                
                    SELECT @Taxonomy = id FROM @ParentTaxonomies WHERE name = 'Activities, clubs and groups';
                    INSERT INTO Taxonomies (Name, TaxonomyType, ParentId) VALUES
                        ('Activities', 'ServiceCategory', @Taxonomy),
                        ('Before and after school clubs', 'ServiceCategory', @Taxonomy),
                        ('Holiday clubs and schemes', 'ServiceCategory', @Taxonomy),
                        ('Music, arts and dance', 'ServiceCategory', @Taxonomy),
                        ('Parent, baby and toddler groups', 'ServiceCategory', @Taxonomy),
                        ('Pre-school playgroup', 'ServiceCategory', @Taxonomy),
                        ('Sports and recreation', 'ServiceCategory', @Taxonomy);
                
                    SELECT @Taxonomy = id FROM @ParentTaxonomies WHERE name = 'Family support';
                    INSERT INTO Taxonomies (Name, TaxonomyType, ParentId) VALUES
                        ('Bullying and cyber bullying', 'ServiceCategory', @Taxonomy),
                        ('Debt and welfare advice', 'ServiceCategory', @Taxonomy),
                        ('Domestic abuse', 'ServiceCategory', @Taxonomy),
                        ('Intensive targeted family support', 'ServiceCategory', @Taxonomy),
                        ('Money, benefits and housing', 'ServiceCategory', @Taxonomy),
                        ('Parenting support', 'ServiceCategory', @Taxonomy),
                        ('Reducing parental conflict', 'ServiceCategory', @Taxonomy),
                        ('Separating and separated parent support', 'ServiceCategory', @Taxonomy),
                        ('Stopping smoking', 'ServiceCategory', @Taxonomy),
                        ('Substance misuse (including alcohol and drug)', 'ServiceCategory', @Taxonomy),
                        ('Targeted youth support', 'ServiceCategory', @Taxonomy),
                        ('Youth justice services', 'ServiceCategory', @Taxonomy);
                
                    SELECT @Taxonomy = id FROM @ParentTaxonomies WHERE name = 'Health';
                    INSERT INTO Taxonomies (Name, TaxonomyType, ParentId) VALUES
                        ('Hearing and sight', 'ServiceCategory', @Taxonomy),
                        ('Mental health, social and emotional support', 'ServiceCategory', @Taxonomy),
                        ('Nutrition and weight management', 'ServiceCategory', @Taxonomy),
                        ('Oral health', 'ServiceCategory', @Taxonomy),
                        ('Public health services', 'ServiceCategory', @Taxonomy);
                
                    SELECT @Taxonomy = id FROM @ParentTaxonomies WHERE name = 'Pregnancy, birth and early years';
                    INSERT INTO Taxonomies (Name, TaxonomyType, ParentId) VALUES
                        ('Birth registration', 'ServiceCategory', @Taxonomy),
                        ('Early years language and learning', 'ServiceCategory', @Taxonomy),
                        ('Health visiting', 'ServiceCategory', @Taxonomy),
                        ('Infant feeding support (including breastfeeding)', 'ServiceCategory', @Taxonomy),
                        ('Midwife and maternity', 'ServiceCategory', @Taxonomy),
                        ('Perinatal mental health support (pregnancy to one year post birth)', 'ServiceCategory', @Taxonomy);
                
                    SELECT @Taxonomy = id FROM @ParentTaxonomies WHERE name = 'Special educational needs and disabilities (SEND)';
                    INSERT INTO Taxonomies (Name, TaxonomyType, ParentId) VALUES
                        ('Autistic Spectrum Disorder (ASD)', 'ServiceCategory', @Taxonomy),
                        ('Breaks and respite', 'ServiceCategory', @Taxonomy),
                        ('Early years support', 'ServiceCategory', @Taxonomy),
                        ('Groups for parents and carers of children with SEND', 'ServiceCategory', @Taxonomy),
                        ('Hearing impairment', 'ServiceCategory', @Taxonomy),
                        ('Learning difficulties and disabilities', 'ServiceCategory', @Taxonomy),
                        ('Multi-sensory impairment', 'ServiceCategory', @Taxonomy),
                        ('Other difficulties or disabilities', 'ServiceCategory', @Taxonomy),
                        ('Physical disabilities', 'ServiceCategory', @Taxonomy),
                        ('Social, emotional and mental health support', 'ServiceCategory', @Taxonomy),
                        ('Speech, language and communication needs', 'ServiceCategory', @Taxonomy),
                        ('Visual impairment', 'ServiceCategory', @Taxonomy);
                
                    SELECT @Taxonomy = id FROM @ParentTaxonomies WHERE name = 'Transport';
                    INSERT INTO Taxonomies (Name, TaxonomyType, ParentId) VALUES
                        ('Community transport', 'ServiceCategory', @Taxonomy);
                
                    COMMIT;
                END;
            """);
        }

        public void SeedOrganisations(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                 IF NOT EXISTS (SELECT * FROM Organisations)
                 BEGIN
                     INSERT INTO Organisations (OrganisationType, Name, Description, AdminAreaCode, Uri, Url) VALUES
                         ('LA', 'Bristol County Council', 'Bristol County Council', 'E06000023', 'https://www.bristol.gov.uk/', 'https://www.bristol.gov.uk/'),
                         ('LA', 'Lancashire County Council', 'Lancashire County Council', 'E10000017', 'https://www.lancashire.gov.uk/', 'https://www.lancashire.gov.uk/'),
                         ('LA', 'London Borough of Redbridge', 'London Borough of Redbridge', 'E09000026', 'https://www.redbridge.gov.uk/', 'https://www.redbridge.gov.uk/'),
                         ('LA', 'Salford City Council', 'Salford City Council', 'E08000006', 'https://www.salford.gov.uk/', 'https://www.salford.gov.uk/'),
                         ('LA', 'Suffolk County Council', 'Suffolk County Council', 'E10000029', 'https://www.suffolk.gov.uk/', 'https://www.suffolk.gov.uk/'),
                         ('LA', 'Tower Hamlets Council', 'Tower Hamlets Council', 'E09000030', 'https://www.towerhamlets.gov.uk/', 'https://www.towerhamlets.gov.uk/'),
                         ('LA', 'Lewisham Council', 'Lewisham Council', 'E09000023', 'https://lewisham.gov.uk/', 'https://lewisham.gov.uk/'),
                         ('LA', 'North East Lincolnshire Council', 'North East Lincolnshire Council', 'E06000012', 'https://www.nelincs.gov.uk/', 'https://www.nelincs.gov.uk/'),
                         ('LA', 'City of Wolverhampton Council', 'City of Wolverhampton Council', 'E08000031', 'https://www.wolverhampton.gov.uk/', 'https://www.wolverhampton.gov.uk/'),
                         ('LA', 'Sheffield City Council', 'Sheffield City Council', 'E08000019', 'https://www.sheffield.gov.uk/', 'https://www.sheffield.gov.uk/');
                 END;
            """);
        }
    }
}
