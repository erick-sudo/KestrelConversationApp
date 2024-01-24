using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Employee;

public class DeleteEmployeeDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string DeletedBy { get; set; }
}
