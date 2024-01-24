using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Company;

public class DeleteCompanyDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string DeletedBy { get; set; }
}
