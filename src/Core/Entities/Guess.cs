using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace Core.Entities;

public class Guess
{
    public Guess()
    {
        Id = SequentialGuid.NewGuid();
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public Guid EmployeeId { get; set; }

    [Column(Order = 2), Required]
    public Guid GuessedEmployeeId { get; set; }

    [Column(Order = 3), Required]
    public Guid AnswerId { get; set; }

    [Column(Order = 4)]
    public bool IsCorrect { get; set; }

    public Answer Answer { get; set; }
    public Employee Guesser { get; set; }
}