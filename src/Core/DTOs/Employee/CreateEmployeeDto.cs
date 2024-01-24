namespace Core.DTOs.Employee;

public class CreateEmployeeDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public Guid CompanyId { get; set; }
}
