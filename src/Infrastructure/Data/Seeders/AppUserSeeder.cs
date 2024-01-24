using Core.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class AppUserSeeder
{
    public static ModelBuilder SeedAppUsers(this ModelBuilder modelBuilder)
    {
        var superAdmins = GetAppUsers();

        foreach (var superAdmin in superAdmins)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasData(superAdmin);
        }

        return modelBuilder;
    }

    public static List<ApplicationUser> GetAppUsers()
    {
        var superAdmins = new List<ApplicationUser>()
        {
            new()
            {
                Id = Guid.Parse("4531aaa1-97bd-43a7-82e2-2e040469154f"),
                EmployeeId = Guid.Parse("f7ca5d8a-c88e-4dbb-8015-fc974994a6d9"),
                UserName = "Bruno Danelon",
                NormalizedUserName = "BRUNO DANELON",
                Email = "bruno@techexpertacademy.com",
                NormalizedEmail = "BRUNO@TECHEXPERTACADEMY.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEMgpzUuKp0sQRyrZJhBubeE/Q3qiAoJzsfUNTjPwNd0Fbw6YPymyrseT7jnrsqmr2g==",
                SecurityStamp = "O2QAJA2JY53X3MU476YW24YL3FRZFSXN",
                ConcurrencyStamp = "add11d33-4651-4495-ba96-8772e64c4ac0",
                PhoneNumber = "333333333",
            }
        };

        return superAdmins;
    }
}
