using Core.Interfaces.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace Infrastructure.ExtensionMethods;
public static class ChangeTrackerExtensions
{
    public static void SetAuditProperties(this ChangeTracker changeTracker, IHttpContextAccessor accessor)
    {
        IEnumerable<EntityEntry> entities = changeTracker
                            .Entries()
                            .Where(x => x.Entity is IAuditableEntity &&
                            (x.State == EntityState.Deleted ||
                             x.State == EntityState.Modified ||
                             x.State == EntityState.Added));
        if (entities.Any())
        {
            foreach (var entry in entities)
            {
                var timeStamp = DateTimeOffset.UtcNow;
                var userName = accessor.HttpContext?.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        HandleDelete(entry, timeStamp, userName);
                        break;
                    case EntityState.Modified:
                        HandleUpdate(entry, timeStamp, userName);
                        break;
                    case EntityState.Added:
                        HandleAdd(entry, timeStamp, userName);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private static void HandleAdd(EntityEntry entry, DateTimeOffset timeStamp, string? createdBy)
    {
        IAuditableEntity entity = (IAuditableEntity)entry.Entity;
        entity.CreatedAt = timeStamp;
        if (entity.CreatedBy is null && createdBy is not null)
        {
            entity.CreatedBy = createdBy;
        }
    }

    private static void HandleUpdate(EntityEntry entry, DateTimeOffset timeStamp, string? userName)
    {
        IAuditableEntity entity = (IAuditableEntity)entry.Entity;
        entity.LastModifiedAt = timeStamp;
        if (userName is not null)
        {
            entity.LastModifiedBy = userName;
        }
    }

    private static void HandleDelete(EntityEntry entry, DateTimeOffset timeStamp, string? userName)
    {
        IAuditableEntity entity = (IAuditableEntity)entry.Entity;
        entity.IsDeleted = true;
        entity.DeletedAt = timeStamp;
        entity.IsActive = false;
        entry.State = EntityState.Modified;
        entity.DeletedBy = userName;
    }
}
