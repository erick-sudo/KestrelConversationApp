using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Employee;

public class UpdateEmployeeDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public Guid CompanyId { get; set; }
}
