using AutoMapper;
using Core.DTOs.Employee;
using Core.Entities;
using Core.Identity;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Infrastructure.Services;

public class EmployeeService(
    IEmployeeRepository _employeeRepository,
    IMapper _mapper,
    UserManager<ApplicationUser> _userManager,
    ICompanyService _companyService) : IEmployeeService
{
    public async Task<IDataResult<Employee>> GetEmployeeByIdAsync(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee == null)
        {
            return new ErrorDataResult<Employee>(Messages.EmployeeNotFound);
        }

        var companyActiveResult = await _companyService.CheckIfCompanyActiveAsync(employee.CompanyId);

        return !companyActiveResult.Success
            ? new ErrorDataResult<Employee>(companyActiveResult.Message)
            : new SuccessDataResult<Employee>(employee);
    }

    public async Task<IDataResult<Employee>> GetEmployeeByEmailAsync(string email)
    {
        var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
        if (employee == null)
        {
            return new ErrorDataResult<Employee>(Messages.EmployeeNotFound);
        }

        var companyActiveResult = await _companyService.CheckIfCompanyActiveAsync(employee.CompanyId);

        return !companyActiveResult.Success
            ? new ErrorDataResult<Employee>(companyActiveResult.Message)
            : new SuccessDataResult<Employee>(employee);
    }

    public async Task<IDataResult<IEnumerable<Employee>>> GetAllEmployeesAsync(Expression<Func<Employee, bool>> predicate)
    {
        var employees = await _employeeRepository.GetAllAsync(predicate);

        return employees.Any()
            ? new SuccessDataResult<IEnumerable<Employee>>(employees)
            : new ErrorDataResult<IEnumerable<Employee>>(Messages.EmptyEmployeeList);
    }

    public async Task<IDataResult<ApplicationUser>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
    {
        var companyResult = await _companyService.GetCompanyByIdAsync(createEmployeeDto.CompanyId);
        if (!companyResult.Success)
        {
            return new ErrorDataResult<ApplicationUser>(Messages.CompanyNotFound);
        }

        if (!companyResult.Data.IsActive)
        {
            return new ErrorDataResult<ApplicationUser>(Messages.CompanyNotActiveError);
        }

        var employeeList = await _employeeRepository.GetAllAsync(e => e.Email == createEmployeeDto.Email);
        if (employeeList.Any())
        {
            return new ErrorDataResult<ApplicationUser>(Messages.EmployeeAlreadyExists);
        }

        return await CreateEmployeeWithApplicationUser(createEmployeeDto);
    }

    public async Task<IResult> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
    {
        var companyResult = await _companyService.GetCompanyByIdAsync(updateEmployeeDto.CompanyId);
        if (!companyResult.Success)
        {
            return new ErrorResult(Messages.CompanyNotFound);
        }

        var employeeResult = await GetEmployeeByIdAsync(updateEmployeeDto.Id);
        if (!employeeResult.Success)
        {
            return employeeResult;
        }

        var updateResult = await CompleteUpdateProcessAsync(updateEmployeeDto, employeeResult.Data);

        return updateResult > 0
            ? new SuccessResult(Messages.UpdateEmployeeSuccess)
            : new ErrorResult(Messages.UpdateEmployeeError);
    }

    public async Task<IResult> DeleteEmployeeAsync(DeleteEmployeeDto DeleteEmployeeDto)
    {
        var employeeResult = await GetEmployeeByIdAsync(DeleteEmployeeDto.Id);
        if (!employeeResult.Success)
        {
            return employeeResult;
        }

        var deleteResult = await _employeeRepository.DeleteAsync(employeeResult.Data.Id);

        return deleteResult > 0
            ? new SuccessResult(Messages.DeleteEmployeeSuccess)
            : new ErrorResult(Messages.DeleteEmployeeError);
    }

    public async Task<IResult> UpdateEmployeeActivationStatusAsync(Guid employeeId, bool status)
    {
        var result = await GetEmployeeByIdAsync(employeeId);
        if (!result.Success)
        {
            return result;
        }

        result.Data.IsActive = status;
        var changeResult = await _employeeRepository.SaveChangesAsync();

        return changeResult > 0
            ? new SuccessResult(Messages.EmployeeStatusChanged)
            : new ErrorResult(Messages.EmployeeStatusChangeError);
    }

    public async Task<IDataResult<IEnumerable<ListEmployeeDto>>> GetInactiveEmployeesAsync()
    {
        var result = await GetAllEmployeesAsync(e => !e.IsActive);

        return result.Success
            ? new SuccessDataResult<IEnumerable<ListEmployeeDto>>(_mapper.Map<IEnumerable<ListEmployeeDto>>(result.Data))
            : new ErrorDataResult<IEnumerable<ListEmployeeDto>>(result.Message);
    }

    public async Task<IResult> ChangeEmailAsync(string currentEmail, string newEmail)
    {
        var employeeResult = await GetEmployeeByEmailAsync(currentEmail);
        if (!employeeResult.Success)
        {
            return employeeResult;
        }

        employeeResult.Data.Email = newEmail;
        var updateResult = await _employeeRepository.UpdateAsync(employeeResult.Data);

        return updateResult > 0
            ? new SuccessResult(Messages.UpdateEmployeeSuccess)
            : new ErrorResult(Messages.UpdateEmployeeError);
    }

    #region Helper Methods
    private async Task<int> CompleteUpdateProcessAsync(UpdateEmployeeDto updateEmployeeDto, Employee employee)
    {
        var employeeToUpdate = employee;
        employeeToUpdate.Id = updateEmployeeDto.Id;
        employeeToUpdate.FirstName = updateEmployeeDto.FirstName;
        employeeToUpdate.LastName = updateEmployeeDto.LastName;
        employeeToUpdate.PhoneNumber = updateEmployeeDto.PhoneNumber;
        employeeToUpdate.Email = updateEmployeeDto.Email;

        var updateResult = await _employeeRepository.UpdateAsync(employeeToUpdate);
        return updateResult;
    }

    private async Task<IDataResult<ApplicationUser>> CreateEmployeeWithApplicationUser(CreateEmployeeDto createEmployeeDto)
    {
        var employeeToAdd = _mapper.Map<Employee>(createEmployeeDto);
        var appUserToAdd = _mapper.Map<ApplicationUser>(employeeToAdd);

        var employeeToAddResult = await _employeeRepository.AddAsync(employeeToAdd);
        var appUserToAddResult = await _userManager.CreateAsync(appUserToAdd);
        var roleResult = await _userManager.AddToRoleAsync(appUserToAdd, CustomRoles.Employee);

        var claims = new Claim[]
        {
            new(ClaimTypes.Email, employeeToAdd.Email),
            new(CustomClaims.EmployeeId, employeeToAdd.Id.ToString()),
            new(CustomClaims.CompanyId, employeeToAdd.CompanyId.ToString())
        };
        var claimsResult = await _userManager.AddClaimsAsync(appUserToAdd, claims);

        var isCreatingEmployeeSuccessfull = employeeToAddResult > 0
            && appUserToAddResult.Succeeded
            && roleResult.Succeeded
            && claimsResult.Succeeded;

        return isCreatingEmployeeSuccessfull 
            ? new SuccessDataResult<ApplicationUser>(data: appUserToAdd, message: Messages.CreateEmployeeSuccess)
            : new ErrorDataResult<ApplicationUser>(Messages.AddEmployeeError);
    }
    #endregion
}
