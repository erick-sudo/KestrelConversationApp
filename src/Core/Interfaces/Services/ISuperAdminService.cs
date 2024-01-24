using Core.DTOs.Company;
using Core.DTOs.Employee;
using Core.Identity;
using Core.Utilities.Results;

namespace Core.Interfaces.Services;

public interface ISuperAdminService
{
    Task<IResult> UpdateCompanyActivationStatusAsync(Guid companyId, bool status);
    Task<IDataResult<IEnumerable<ListCompanyDto>>> GetInactiveCompaniesAsync();
    Task<IDataResult<IEnumerable<ListEmployeeDto>>> GetInactiveEmployeesAsync();
    Task<IDataResult<ApplicationUser>> ActivateCompanyAsync(Guid companyId);
}
