using Core.Interfaces.Entities.Base;
using Core.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Question : IBaseEntity, IAuditableEntity
{
    public Question()
    {
        Id = SequentialGuid.NewGuid();
        IsActive = true;
        IsDeleted = false;
        CreatedAt = DateTimeOffset.UtcNow;
        Answers = new HashSet<Answer>();
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public Guid EmployeeId { get; set; }

    [Column(Order = 2), Required]
    public string Content { get; set; }

    [Column(Order = 3), Required]
    public bool IsActive { get; set; }

    [Column(Order = 4), Required]
    public bool IsDeleted { get; set; }

    [Column(Order = 5), Required]
    public DateTimeOffset CreatedAt { get; set; }

    [Column(Order = 6), Required]
    public string CreatedBy { get; set; }

    [Column(Order = 7)]
    public DateTimeOffset? DeletedAt { get; set; }

    [Column(Order = 8)]
    public string? DeletedBy { get; set; }

    [Column(Order = 9)]
    public DateTimeOffset? LastModifiedAt { get; set; }

    [Column(Order = 10)]
    public string? LastModifiedBy { get; set; }

    public Employee PostedBy { get; set; }
    public ICollection<Answer> Answers { get; set; }
}
