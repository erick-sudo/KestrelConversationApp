namespace Core.Interfaces.Entities;

public interface IEmployee
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string PhoneNumber { get; set; }

    string Email { get; set; }

    string FullName { get; }
}
