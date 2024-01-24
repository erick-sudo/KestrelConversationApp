using Core.Interfaces.Entities.Base;
using Core.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Answer : IBaseEntity, IAuditableEntity
{
    public Answer()
    {
        Id = SequentialGuid.NewGuid();
        IsActive = true;
        IsDeleted = false;
        CreatedAt = DateTimeOffset.UtcNow;
        Guesses = new HashSet<Guess>();
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public Guid EmployeeId { get; set; }

    [Column(Order = 2), Required]
    public Guid QuestionId { get; set; }

    [Column(Order = 3), Required]
    public string Content { get; set; }

    [Column(Order = 4), Required]
    public bool IsActive { get; set; }

    [Column(Order = 5), Required]
    public bool IsDeleted { get; set; }

    [Column(Order = 6), Required]
    public DateTimeOffset CreatedAt { get; set; }

    [Column(Order = 7), Required]
    public string CreatedBy { get; set; }

    [Column(Order = 8)]
    public DateTimeOffset? DeletedAt { get; set; }

    [Column(Order = 9)]
    public string? DeletedBy { get; set; }

    [Column(Order = 10)]
    public DateTimeOffset? LastModifiedAt { get; set; }

    [Column(Order = 11)]
    public string? LastModifiedBy { get; set; }

    public Employee AnsweredBy{ get; set; }
    public Question Question { get; set; }
    public ICollection<Guess> Guesses { get; set; }
}
