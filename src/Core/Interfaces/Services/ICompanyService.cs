using Core.DTOs.Company;
using Core.Entities;
using Core.Identity;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Core.Interfaces.Services;

public interface ICompanyService 
{ 
    Task<IDataResult<Company>> GetCompanyByIdAsync(Guid companyId);
    Task<IDataResult<IEnumerable<Company>>> GetAllCompaniesAsync(Expression<Func<Company, bool>> predicate);
    Task<IResult> CreateCompanyAsync(CreateCompanyDto createCompanyDto);
    Task<IResult> UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto);
    Task<IResult> DeleteCompanyAsync(DeleteCompanyDto deleteCompanyDto);
    Task<IResult> UpdateCompanyActivationStatusAsync(Guid companyId, bool status);
    Task<IDataResult<ApplicationUser>> ActivateCompanyAsync(Guid companyId);
    Task<IDataResult<IEnumerable<ListCompanyDto>>> GetInactiveCompaniesAsync();
    Task<IResult> CheckIfCompanyActiveAsync(Guid companyId);
}
