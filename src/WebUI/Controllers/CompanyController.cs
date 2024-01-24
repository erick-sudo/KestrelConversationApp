using AdminPortal.Controllers;
using Core.DTOs.Employee;
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

namespace WebUI.Controllers;

public class CompanyController(
    UserManager<ApplicationUser> _userManager,
    IEmployeeService _employeeService,
    IEmailService _emailService,
    IValidator<CreateEmployeeDto> _createEmployeeDtoValidator,
    IConfiguration _configuration) : BaseController(_userManager)
{
    #region Employee Actions

    [Authorize(Roles = CustomRoles.CompanyAdmin)]
    [HttpPost("employee/create")]
    public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeDto request)
    {
        var createEmployeeValidationResponse = await _createEmployeeDtoValidator.ValidateAsync(request);
        if (!createEmployeeValidationResponse.IsValid)
        {
            var errorMessages = string.Join('&', createEmployeeValidationResponse.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);

        var employeeResult = await _employeeService.CreateEmployeeAsync(request);
        if (!employeeResult.Success)
        {
            return BadRequest(employeeResult);
        }

        var sendEmailResult = await SendCreatePasswordEmailAsync(user: employeeResult.Data);
        if (sendEmailResult is not OkResult)
        {
            return sendEmailResult;
        }

        transactionScope.Complete();

        return Ok(new SuccessResult(employeeResult.Message));
    }
    #endregion

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
