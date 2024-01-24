using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class EmployeeSeeder
{
    public static ModelBuilder SeedEmployees(this ModelBuilder modelBuilder)
    {
        var employees = GetEmployees();

        foreach (var employee in employees)
        {
            modelBuilder.Entity<Employee>()
                .HasData(employee);
        }

        return modelBuilder;
    }

    public static List<Employee> GetEmployees()
    {
        var companies = CompanySeeder.GetCompanies();

        var employees = new List<Employee>()
        {
            new("Bruno", "Danelon", "333333333", "bruno@techexpertacademy.com", "Seeder")
            {
                Id = Guid.Parse("f7ca5d8a-c88e-4dbb-8015-fc974994a6d9"),
                CompanyId = Guid.Parse("6c809ef3-4d53-47b8-9014-e3d45c7a4707"),
                CreatedAt = new DateTimeOffset(2023, 01, 01, 00, 00, 00, 00, TimeSpan.Zero),
                IsActive = true
            },
            new("Test", "User1", "111111111", "testuser1@test.com", "Seeder")
            {
                Id = Guid.Parse("5f753883-cc79-4fb4-9d16-08db2b1280be"),
                CompanyId = companies.First(item => item.Name == "Test Company1").Id,
                CreatedAt = new DateTimeOffset(2023, 01, 01, 00, 00, 00, 00, TimeSpan.Zero)
            },
            new("Test", "User2", "222222222", "testuser2@test.com", "Seeder")
            {
                Id = Guid.Parse("50ec5ac0-2ba7-4288-9d17-08db2b1280be"),
                CompanyId = companies.First(item => item.Name == "Test Company2").Id,
                CreatedAt = new DateTimeOffset(2023, 01, 01, 00, 00, 00, 00, TimeSpan.Zero)
            },
        };

        return employees;
    }
}
