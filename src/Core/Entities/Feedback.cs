using Core.Interfaces.Entities.Base;
using Core.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Feedback : IBaseEntity
{
    public Feedback()
    {
        Id = SequentialGuid.NewGuid();
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public Guid EmployeeId { get; set; }

    [Column(Order = 2), Required]
    public string Content { get; set; }

    public Employee SentBy { get; set; }
}