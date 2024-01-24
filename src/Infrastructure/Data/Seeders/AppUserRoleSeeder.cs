using Core.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class AppUserRoleSeeder
{
    public static ModelBuilder SeedAppUserRoles(this ModelBuilder modelBuilder)
    {
        var appUserRoles = GetAppUserRoles();
        foreach (var userRole in appUserRoles)
        {
            modelBuilder.Entity<ApplicationUserRole>().HasData(userRole);
        }
        return modelBuilder;
    }

    public static List<ApplicationUserRole> GetAppUserRoles()
    {
        var appUserRoles = new List<ApplicationUserRole>()
        {
            new()
            {
               UserId = Guid.Parse("4531aaa1-97bd-43a7-82e2-2e040469154f"),
               RoleId = Guid.Parse("c490685a-9a1f-42a1-8238-4a2e6b4ea8b9"),
            }
        };

        return appUserRoles;
    }
}
