using Core.Interfaces.Entities.Base;
using Core.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class LeadershipBoard : IBaseEntity
{
    public LeadershipBoard()
    {
        Id = SequentialGuid.NewGuid();
    }

    [Key, Column(Order = 0), Required]
    public Guid Id { get; set; }

    [Column(Order = 1), Required]
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}