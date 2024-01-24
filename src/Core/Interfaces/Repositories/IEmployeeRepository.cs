using Core.Entities;
using Core.Interfaces.Repositories.Base;

namespace Core.Interfaces.Repositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<Employee?> GetEmployeeByEmailAsync(string email);
}
