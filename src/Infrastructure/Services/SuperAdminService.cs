using Core.DTOs.Company;
using Core.DTOs.Employee;
using Core.Identity;
using Core.Interfaces.Services;
using Core.Utilities.Results;

namespace Infrastructure.Services;

public class SuperAdminService(
    ICompanyService _companyService,
    IEmployeeService _employeeService) : ISuperAdminService
{
    public async Task<IResult> UpdateCompanyActivationStatusAsync(Guid companyId, bool status)
        => await _companyService.UpdateCompanyActivationStatusAsync(companyId, status);

    public async Task<IDataResult<IEnumerable<ListCompanyDto>>> GetInactiveCompaniesAsync()
        => await _companyService.GetInactiveCompaniesAsync();

    public async Task<IDataResult<IEnumerable<ListEmployeeDto>>> GetInactiveEmployeesAsync()
        => await _employeeService.GetInactiveEmployeesAsync();

    public async Task<IDataResult<ApplicationUser>> ActivateCompanyAsync(Guid companyId)
        => await _companyService.ActivateCompanyAsync(companyId);
}
