namespace Core.Interfaces.Entities.Base;

public interface IAuditableEntity
{
    bool IsActive { get; set; }
    bool IsDeleted { get; set; }

    DateTimeOffset CreatedAt { get; set; }
    string CreatedBy { get; set; }

    DateTimeOffset? DeletedAt { get; set; }
    string? DeletedBy { get; set; }

    DateTimeOffset? LastModifiedAt { get; set; }
    string? LastModifiedBy { get; set; }
}
