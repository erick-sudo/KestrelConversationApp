using Core.Identity;
using Core.Utilities.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class RoleSeeder
{
    public static ModelBuilder SeedRoles(this ModelBuilder modelBuilder)
    {
        var roles = GetRoles();

        foreach (var role in roles)
        {
            modelBuilder.Entity<ApplicationRole>()
                .HasData(role);
        }

        return modelBuilder;
    }

    public static List<ApplicationRole> GetRoles()
    {
        var roles = new List<ApplicationRole>()
        {
            new() {
                Id = Guid.Parse("c490685a-9a1f-42a1-8238-4a2e6b4ea8b9"),
                Name = CustomRoles.SuperAdmin,
                NormalizedName = "SUPERADMIN"
            },
            new() {
                Id = Guid.Parse("ae4c6594-a871-4563-b30d-12e27f208bca"),
                Name = CustomRoles.CompanyAdmin,
                NormalizedName = "COMPANYADMIN"
            },
            new() {
                Id = Guid.Parse("63b0a573-bf6e-46a4-96d9-bb1d0cce89d1"),
                Name = CustomRoles.Employee,
                NormalizedName = "EMPLOYEE"
            },
            new() {
                Id = Guid.Parse("8173c4ba-3e72-4fc7-9c53-ffd1dd801174"),
                Name = CustomRoles.QuestionHost,
                NormalizedName = "QUESTIONHOST"
            }
        };

        return roles;
    }
}
