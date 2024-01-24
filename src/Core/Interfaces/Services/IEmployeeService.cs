using Core.DTOs.Employee;
using Core.Entities;
using Core.Identity;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Core.Interfaces.Services;

public interface IEmployeeService 
{
    Task<IDataResult<Employee>> GetEmployeeByIdAsync(Guid employeeId);
    Task<IDataResult<Employee>> GetEmployeeByEmailAsync(string email);
    Task<IDataResult<IEnumerable<Employee>>> GetAllEmployeesAsync(Expression<Func<Employee, bool>> predicate);
    Task<IDataResult<ApplicationUser>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
    Task<IResult> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
    Task<IResult> DeleteEmployeeAsync(DeleteEmployeeDto deleteEmployeeDto);
    Task<IResult> UpdateEmployeeActivationStatusAsync(Guid employeeId, bool status);
    Task<IDataResult<IEnumerable<ListEmployeeDto>>> GetInactiveEmployeesAsync();
    Task<IResult> ChangeEmailAsync(string currentEmail, string newEmail);
}
