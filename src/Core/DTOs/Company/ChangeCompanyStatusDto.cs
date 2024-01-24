namespace Core.DTOs.Company;

public class ChangeCompanyStatusDto
{
    public Guid CompanyId { get; set; }

    public bool Status { get; set; }
}
