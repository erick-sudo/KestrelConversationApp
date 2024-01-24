namespace Core.DTOs;

public class GenerateTokenServiceRequest
{
    public string CompanyId { get; set; }
    public string EmployeeId { get; set; }
    public string Email { get; set; }
    public string Roles { get; set; }
}
