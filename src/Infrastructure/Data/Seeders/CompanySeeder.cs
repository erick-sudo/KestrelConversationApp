using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class CompanySeeder
{
    public static ModelBuilder SeedCompanies(this ModelBuilder modelBuilder)
    {
        var companies = GetCompanies();

        foreach (var company in companies)
        {
            modelBuilder.Entity<Company>()
                .HasData(company);
        }

        return modelBuilder;
    }

    public static List<Company> GetCompanies()
    {
        var companies = new List<Company>()
        {
            new("TEA", "R-000000", "Seeder")
            {
                Id = Guid.Parse("6c809ef3-4d53-47b8-9014-e3d45c7a4707"),
                CreatedAt = new DateTimeOffset(2023, 01, 01, 00, 00, 00, 00, TimeSpan.Zero),
                IsActive = true
            },
            new("Test Company1", "R-111111", "Seeder")
            {
                Id = Guid.Parse("36a735d5-2dd9-4c95-9870-08db2b0faddb"),
                CreatedAt = new DateTimeOffset(2023, 01, 01, 00, 00, 00, 00, TimeSpan.Zero)
            },
            new("Test Company2", "R-222222", "Seeder")
            {
                Id = Guid.Parse("09d94c77-db51-4ecc-9871-08db2b0faddb"),
                CreatedAt = new DateTimeOffset(2023, 01, 02, 00, 00, 00, 00, TimeSpan.Zero)
            }
        };

        return companies;
    }
}
