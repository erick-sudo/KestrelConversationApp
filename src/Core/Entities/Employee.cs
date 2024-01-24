using Core.Interfaces.Entities;
using Core.Interfaces.Entities.Base;
using Core.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Employee : IEmployee, ICompanyEntity, IAuditableEntity, IBaseEntity
{
    public Employee()
    {
        Id = SequentialGuid.NewGuid();
        IsActive = false;
        IsDeleted = false;
        CreatedAt = DateTimeOffset.UtcNow;
        TotalScore = 0;
        Questions = new HashSet<Question>();
        Answers = new HashSet<Answer>();
        Feedbacks = new HashSet<Feedback>();
        Guesses = new HashSet<Guess>();
    }

    public Employee(string firstName, string lastName, string phoneNumber, string email, string createdBy)
        : this()
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        CreatedBy = createdBy;
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public Guid CompanyId { get; set; }

    [Column(Order = 2), Required]
    public string FirstName { get; set; }

    [Column(Order = 3), Required]
    public string LastName { get; set; }

    [Column(Order = 4), Required]
    public string PhoneNumber { get; set; }

    [Column(Order = 5), Required]
    public string Email { get; set; }

    [Column(Order = 6), Required]
    public int TotalScore { get; set; }

    [Column(Order = 7), Required]
    public bool IsActive { get; set; }

    [Column(Order = 8), Required]
    public bool IsDeleted { get; set; }

    [Column(Order = 9), Required]
    public DateTimeOffset CreatedAt { get; set; }

    [Column(Order = 10), Required]
    public string CreatedBy { get; set; } 

    [Column(Order = 11)]
    public DateTimeOffset? DeletedAt { get; set; }

    [Column(Order = 12)]
    public string? DeletedBy { get; set; }

    [Column(Order = 13)]
    public DateTimeOffset? LastModifiedAt { get; set; }

    [Column(Order = 14)]
    public string? LastModifiedBy { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public Company Company { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public ICollection<Guess> Guesses { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}
