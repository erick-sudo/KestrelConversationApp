using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Company;

public class UpdateCompanyDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string BusinessRegistrationNumber { get; set; }
}
