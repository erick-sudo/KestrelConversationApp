using AutoMapper;
using Core.DTOs.Company;
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

public class CompanyService(
    ICompanyRepository _companyRepository,
    IMapper _mapper,
    UserManager<ApplicationUser> _userManager) : ICompanyService
{
    public async Task<IDataResult<Company>> GetCompanyByIdAsync(Guid companyId)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);

        return company == null
            ? new ErrorDataResult<Company>(Messages.CompanyNotFound)
            : new SuccessDataResult<Company>(company);
    }

    public async Task<IDataResult<IEnumerable<Company>>> GetAllCompaniesAsync(Expression<Func<Company, bool>> predicate)
    {
        var companies = await _companyRepository.GetAllAsync(predicate);

        return companies.Any()
            ? new SuccessDataResult<IEnumerable<Company>>(companies)
            : new ErrorDataResult<IEnumerable<Company>>(Messages.EmptyCompanyList);
    }

    public async Task<IResult> CreateCompanyAsync(CreateCompanyDto createCompanyDto)
    {
        var companyResult = await _companyRepository
            .GetAllAsync(c => c.BusinessRegistrationNumber == createCompanyDto.BusinessRegistrationNumber);
        if (companyResult.Any())
        {
            return new ErrorResult(Messages.CompanyAlreadyExists);
        } 

        var companyToCreate = _mapper.Map<Company>(createCompanyDto);
        var employee = _mapper.Map<Employee>(createCompanyDto);
        employee.CreatedBy = employee.Email;
        companyToCreate.Employees.Add(employee);
        var addResult = await _companyRepository.AddAsync(companyToCreate);

        return addResult > 0
            ? new SuccessResult(Messages.CreateCompanySuccess)
            : new ErrorResult(Messages.AddCompanyError);
    }

    public async Task<IResult> UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto)
    {
        var companyResult = await GetCompanyByIdAsync(updateCompanyDto.Id);
        if (!companyResult.Success)
        {
            return companyResult;
        }

        var companyActiveResult = await CheckIfCompanyActiveAsync(companyResult.Data.Id);
        if (!companyActiveResult.Success)
        {
            return companyActiveResult;
        }

        companyResult.Data.Name = updateCompanyDto.Name;
        companyResult.Data.BusinessRegistrationNumber = updateCompanyDto.BusinessRegistrationNumber;
        var updateResult = await _companyRepository.UpdateAsync(companyResult.Data);

        return updateResult > 0
            ? new SuccessResult(Messages.UpdateCompanySuccess)
            : new ErrorResult(Messages.UpdateCompanyError);
    }

    public async Task<IResult> DeleteCompanyAsync(DeleteCompanyDto deleteCompanyDto)
    {
        var companyResult = await GetCompanyByIdAsync(deleteCompanyDto.Id);
        if (!companyResult.Success)
        {
            return companyResult;
        }

        var companyActiveResult = await CheckIfCompanyActiveAsync(companyResult.Data.Id);
        if (!companyActiveResult.Success)
        {
            return companyActiveResult;
        }

        var deleteResult = await _companyRepository.DeleteAsync(companyResult.Data.Id);

        return deleteResult > 0
            ? new SuccessResult(Messages.DeleteCompanySuccess)
            : new ErrorResult(Messages.DeleteCompanyError);
    }

    public async Task<IResult> UpdateCompanyActivationStatusAsync(Guid companyId, bool status)
    {
        var result = await GetCompanyByIdAsync(companyId);
        if (!result.Success)
        {
            return result;
        }

        var company = result.Data;
        company.IsActive = status;
        var changeResult = await _companyRepository.SaveChangesAsync();

        return changeResult > 0
            ? new SuccessResult(Messages.CompanyStatusChanged)
            : new ErrorResult(Messages.CompanyStatusChangeError);
    }

    public async Task<IDataResult<ApplicationUser>> ActivateCompanyAsync(Guid companyId)
    {
        var company = await _companyRepository.GetCompanyWithEmployeesAsync(companyId);
        if (company is null)
        {
            return new ErrorDataResult<ApplicationUser>(message: Messages.CompanyNotFound);
        }

        if (company.IsActive)
        {
            return new ErrorDataResult<ApplicationUser>(Messages.CompanyAlreadyActivated);
        }

        company.IsActive = true;
        var employee = company.Employees.OrderBy(x => x.CreatedAt).FirstOrDefault();
        employee!.IsActive = true;
        var updateResult = await _companyRepository.UpdateAsync(company);

        var claims = new Claim[]
        {
            new(ClaimTypes.Email, employee.Email),
            new(CustomClaims.EmployeeId, employee.Id.ToString()),
            new(CustomClaims.CompanyId, company.Id.ToString())
        };
        var appUserToAdd = _mapper.Map<ApplicationUser>(employee);
        var identityResult = await _userManager.CreateAsync(appUserToAdd);
        var roleResult = await _userManager.AddToRoleAsync(appUserToAdd, CustomRoles.CompanyAdmin);
        var claimsResult = await _userManager.AddClaimsAsync(appUserToAdd, claims);

        var isActivationSuccessfull = identityResult.Succeeded
            && updateResult > 0
            && roleResult.Succeeded
            && claimsResult.Succeeded;

        return isActivationSuccessfull
            ? new SuccessDataResult<ApplicationUser>(appUserToAdd, Messages.CompanyActivationSuccess)
            : new ErrorDataResult<ApplicationUser>(message: Messages.CompanyActivationError);
    }

    public async Task<IDataResult<IEnumerable<ListCompanyDto>>> GetInactiveCompaniesAsync()
    {
        var result = await GetAllCompaniesAsync(c => !c.IsActive);

        return result.Success
            ? new SuccessDataResult<IEnumerable<ListCompanyDto>>(_mapper.Map<IEnumerable<ListCompanyDto>>(result.Data))
            : new ErrorDataResult<IEnumerable<ListCompanyDto>>(result.Message);
    }

    public async Task<IResult> CheckIfCompanyActiveAsync(Guid companyId)
    {
        var companyActiveResult = await _companyRepository.IsCompanyActiveAsync(companyId);

        return !companyActiveResult
            ? new ErrorResult(Messages.CompanyNotActiveError)
            : new SuccessResult();
    }
}
