using Core.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;
using Core.Interfaces.Entities.Base;

namespace Core.Entities;

public class Company : ICompany, IAuditableEntity, IBaseEntity
{
    public Company()
    {
        Id = SequentialGuid.NewGuid();
        IsActive = false;
        IsDeleted = false;
        CreatedAt = DateTimeOffset.UtcNow;
        Employees = new HashSet<Employee>();
    }

    public Company(string name, string businessRegistrationNumber, string createdBy)
        : this()
    {
        Name = name;
        BusinessRegistrationNumber = businessRegistrationNumber;
        CreatedBy = createdBy;
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public string Name { get; set; }

    [Column(Order = 2), Required]
    public string BusinessRegistrationNumber { get; set; }

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

    public ICollection<Employee> Employees { get; set; }
}
