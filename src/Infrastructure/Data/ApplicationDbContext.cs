using Core.Entities;
using Core.Identity;
using Infrastructure.Data.Seeders;
using Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor)
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Guess> Guesses { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<LeadershipBoard> LeadershipBoards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Questions)
            .WithOne(q => q.PostedBy)
            .HasForeignKey(q => q.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Answers)
            .WithOne(a => a.AnsweredBy)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade); 

        modelBuilder.Seed();
    }

    public override int SaveChanges()
    {
        ChangeTracker.SetAuditProperties(contextAccessor);
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetAuditProperties(contextAccessor);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
