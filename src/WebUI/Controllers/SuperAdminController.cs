using Core.DTOs.Company;
using Core.DTOs.Identity;
using Core.Identity;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace AdminPortal.Controllers;

[ApiController]
[Route("superadmin")]
public class SuperAdminController(
    ISuperAdminService _superAdminService,
    IEmailService _emailService,
    UserManager<ApplicationUser> _userManager,
    IValidator<Guid> _guidValidator,
    IValidator<ChangeCompanyStatusDto> _changeCompanyStatusDtoValidator,
    IConfiguration _configuration) : BaseController(_userManager)
{

    [Authorize(Roles = CustomRoles.SuperAdmin)]
    [HttpGet("inactiveCompanies")]
    public async Task<IActionResult> GetInactiveCompanies()
    {
        var inactiveCompaniesResult = await _superAdminService.GetInactiveCompaniesAsync();

        return !inactiveCompaniesResult.Success
            ? BadRequest(new ErrorResult(Messages.NoInactiveCompany))
            : Ok(inactiveCompaniesResult);
    }

    [Authorize(Roles = CustomRoles.SuperAdmin)]
    [HttpGet("inactiveEmployees")]
    public async Task<IActionResult> GetInactiveEmployees()
    {
        var inactiveEmployeesResult = await _superAdminService.GetInactiveEmployeesAsync();

        return !inactiveEmployeesResult.Success
           ? BadRequest(new ErrorResult(Messages.NoInactiveEmployee))
           : Ok(inactiveEmployeesResult);
    }

    [Authorize(Roles = CustomRoles.SuperAdmin)]
    [HttpPost("changeCompanyStatus")]
    public async Task<IActionResult> ChangeCompanyStatus(ChangeCompanyStatusDto request)
    {
        var changeCompanyStatusValidationResponse = await _changeCompanyStatusDtoValidator.ValidateAsync(request);
        if (!changeCompanyStatusValidationResponse.IsValid)
        {
            var errorMessages = string.Join('&', changeCompanyStatusValidationResponse.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        var updateCompanyStatusResult = await _superAdminService
            .UpdateCompanyActivationStatusAsync(request.CompanyId, request.Status);

        return !updateCompanyStatusResult.Success
            ? BadRequest(updateCompanyStatusResult)
            : Ok(updateCompanyStatusResult);
    }

    [Authorize(Roles = CustomRoles.SuperAdmin)]
    [HttpPost("activateCompany/{companyId:guid}")]
    public async Task<IActionResult> ActivateCompany(Guid companyId)
    {
        var guidValidatorResponse = await _guidValidator.ValidateAsync(companyId);
        if (!guidValidatorResponse.IsValid)
        {
            var errorMessages = string.Join('&', guidValidatorResponse.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        using TransactionScope transactionScope = new (TransactionScopeAsyncFlowOption.Enabled);

        var companyResult = await _superAdminService.ActivateCompanyAsync(companyId);
        if (!companyResult.Success)
        {
            return BadRequest(companyResult);
        }

        var sendEmailResult = await SendCreatePasswordEmailAsync(user: companyResult.Data);
        if (sendEmailResult is not OkResult)
        {
            return sendEmailResult;
        }

        transactionScope.Complete();

        return Ok(new SuccessResult(companyResult.Message));
    }

    #region Helper Methods
    private async Task<SendEmailServiceRequest> GetSendEmailServiceRequestAsync(ApplicationUser user)
    {
        string passwordToken = await UserManager.GeneratePasswordResetTokenAsync(user);
        string websiteUrl = _configuration["AppConstants:WebsiteUrl"]!;

        string? link = Url.Action("CreateNewPassword", "Account",
           new { email = user.Email, token = passwordToken }, "https", websiteUrl);

        return new SendEmailServiceRequest
        {
            UserName = user.UserName!,
            ToEmail = user.Email!,
            Link = link!
        };
    }

    private async Task<IActionResult> SendCreatePasswordEmailAsync(ApplicationUser user)
    {
        var sendEmailRequest = await GetSendEmailServiceRequestAsync(user);
        var sendEmailResponse = await _emailService.SendCreatePasswordEmailAsync(sendEmailRequest, default);
        if (!sendEmailResponse.Successful)
        {
            var errorMessages = string.Join('&', sendEmailResponse.ErrorMessages.Select(e => e));
            return BadRequest(new ErrorResult(errorMessages));
        }

        return Ok();
    }
    #endregion
}
