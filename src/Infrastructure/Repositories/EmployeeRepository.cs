using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        
    }

    public async Task<Employee?> GetEmployeeByEmailAsync(string email)
    {
        var employee = await _dataContext.Employees
            .Where(e => e.Email == email)
            .FirstOrDefaultAsync();

        return employee;
    }
}
