using Core.DTOs.Company;
using Core.DTOs.Identity;
using Core.DTOs;
using Core.Identity;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AdminPortal.Controllers;

[ApiController]
[Route("account")]
public class AccountController(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IIdentityService _identityService,
    IEmailService _emailService,
    IJwtService _jwtService,
    IEmployeeService _employeeService,
    IValidator<LoginDto> _loginDtoValidator,
    IValidator<CreateCompanyDto> _createCompanyDtoValidator,
    IValidator<ResetPasswordDto> _resetPasswordDtoValidator,
    IValidator<CreateNewPasswordDto> _createNewPasswordDtoValidator,
    IValidator<ChangePasswordDto> _changePasswordDtoValidator,
    IValidator<ChangeEmailDto> _changeEmailDtoValidator,
    IConfiguration configuration) : BaseController(_userManager)
{
    #region Login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginRequest)
    {
        var loginValidationResponse = await _loginDtoValidator.ValidateAsync(loginRequest);
        if (!loginValidationResponse.IsValid)
        {
            var errorMessages = string.Join('&', loginValidationResponse.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        var loginServiceResponse = await _identityService.LoginAsync(loginRequest, default);
        if (!loginServiceResponse.Success)
        {
            return HandleLoginResponse(loginServiceResponse);
        }

        var tokenResult = await SendRequestAndGetTokenFromJwtService(loginServiceResponse);
        if (!tokenResult.Success)
        {
            return BadRequest(tokenResult);
        }

        return Ok(new SuccessDataResult<string>(data: tokenResult.Data, message: Messages.LoginSuccess));
    }

    #endregion

    #region Register
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateCompanyDto registerRequest)
    {
        var registerValidationResponse = await _createCompanyDtoValidator.ValidateAsync(registerRequest);
        if (!registerValidationResponse.IsValid)
        {
            var errorMessages = string.Join('&', registerValidationResponse.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResult(errorMessages));
        }

        var registerServiceResponse = await _identityService.RegisterAsync(registerRequest, default);
        if (!registerServiceResponse.Success)
        {
            return BadRequest(registerServiceResponse);
        }

        return Ok(registerServiceResponse);
    }
    #endregion

    #region ResetPassword
    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound(new ErrorResult(Messages.UserNotFound));
        }

        var sendEmailRequest = await GetSendEmailServiceRequestAsync(user, "ResetPassword");
        var sendEmailResponse = await _emailService.SendPasswordResetEmailAsync(sendEmailRequest, default);
        if (!sendEmailResponse.Successful)
        {
            var errorMessages = string.Join('&', sendEmailResponse.ErrorMessages.Select(e => e));
            return BadRequest(new ErrorResult(errorMessages));
        }

        return Ok(new SuccessResult(Messages.EmailSendSuccess));
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordRequest)
    {
        var resetPasswordValidationResult = await _resetPasswordDtoValidator.ValidateAsync(resetPasswordRequest);
        if (!resetPasswordValidationResult.IsValid)
        {
            var errorMessages = string.Join('&', resetPasswordValidationResult.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResult(errorMessages));
        }

        var resetPasswordResult = await _identityService.ResetPasswordAsync(resetPasswordRequest, default);

        return HandleResetPasswordResponse(resetPasswordResult);
    } 
    #endregion

    #region CreateNewPassword
    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string email)
    {
        var user = await UserManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound(new ErrorResult(Messages.UserNotFound));
        }

        if (user.EmailConfirmed)
        {
            return BadRequest(new ErrorResult(Messages.UserEmailAlreadyConfirmed));
        }

        var sendEmailRequest = await GetSendEmailServiceRequestAsync(user, "CreateNewPassword");
        var sendEmailResponse = await _emailService.SendCreatePasswordEmailAsync(sendEmailRequest, default);
        if (!sendEmailResponse.Successful)
        {
            var errorMessages = string.Join('&', sendEmailResponse.ErrorMessages.Select(e => e));
            return BadRequest(new ErrorResult(errorMessages));
        }

        return Ok(new SuccessResult(Messages.EmailSendSuccess));
    }

    [HttpPost("createNewPassword")]
    public async Task<IActionResult> CreateNewPassword(CreateNewPasswordDto createNewPasswordRequest)
    {
        var createNewPasswordValidationResult = await _createNewPasswordDtoValidator.ValidateAsync(createNewPasswordRequest);
        if (!createNewPasswordValidationResult.IsValid)
        {
            var errorMessages = string.Join('&', createNewPasswordValidationResult.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        var createNewPasswordResult = await _identityService.CreateNewPasswordAsync(createNewPasswordRequest, default);

        return HandleCreateNewPasswordResponse(createNewPasswordResult);
    }
    #endregion

    #region ChangePassword & ChangeEmail
    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordRequest)
    {
        var changePasswordValidationResponse = await _changePasswordDtoValidator.ValidateAsync(changePasswordRequest);
        if (!changePasswordValidationResponse.IsValid)
        {
            var errorMessages = string.Join('&', changePasswordValidationResponse.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        var currentEmail = Email;
        var changePasswordResult = await _identityService.ChangePassswordAsync(currentEmail, changePasswordRequest, default);

        return HandleChangePasswordResponse(changePasswordResult);
    }

    [Authorize]
    [HttpPost("changeEmail")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailDto changeEmailRequest)
    {
        var changePasswordValidationResponse = await _changeEmailDtoValidator.ValidateAsync(changeEmailRequest);
        if (!changePasswordValidationResponse.IsValid)
        {
            var errorMessages = string.Join('&', changePasswordValidationResponse.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResult(errorMessages));
        }

        var currentEmail = Email;
        var changeEmailResult = await _identityService.ChangeEmailAsync(currentEmail, changeEmailRequest, default);

        return HandleChangeEmailResponse(changeEmailResult);
    }
    #endregion

    #region Logout
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new SuccessResult(Messages.LogoutSuccess));
    }
    #endregion

    #region Helper Methods
    private async Task<SendEmailServiceRequest> GetSendEmailServiceRequestAsync(ApplicationUser user, string actionName)
    {
        string passwordToken = await UserManager.GeneratePasswordResetTokenAsync(user);
        string websiteUrl = configuration["AppConstants:WebsiteUrl"]!;

        string? link = Url.Action(actionName, "Account",
            new { email = user.Email, token = passwordToken }, "https", websiteUrl);

        return new SendEmailServiceRequest
        {
            UserName = user.UserName!,
            ToEmail = user.Email!,
            Link = link!
        };
    }

    private async Task<IDataResult<string>> SendRequestAndGetTokenFromJwtService(IDataResult<ApplicationUser> loginServiceResponse)
    {
        var employeeResult = await _employeeService.GetEmployeeByEmailAsync(loginServiceResponse.Data.Email!);
        if (!employeeResult.Success)
        {
            return new ErrorDataResult<string>(message: employeeResult.Message);
        }

        var model = new GenerateTokenServiceRequest
        {
            EmployeeId = loginServiceResponse.Data.EmployeeId.ToString()!,
            Email = loginServiceResponse.Data.Email!,
            Roles = loginServiceResponse.Message,
            CompanyId = employeeResult.Data.CompanyId.ToString()
        };

        var token = _jwtService.GenerateToken(model, 60).Token;
        return new SuccessDataResult<string>(data: token);
    }

    private IActionResult HandleLoginResponse(IDataResult<ApplicationUser> loginServiceResponse)
    {
        if (loginServiceResponse.Message == Messages.UserNotFound)
            return NotFound(loginServiceResponse);

        if (loginServiceResponse.Message == Messages.EmailOrPasswordError)
            return Unauthorized(loginServiceResponse);

        return BadRequest(loginServiceResponse);
    }

    private IActionResult HandleChangePasswordResponse(Core.Utilities.Results.IResult changePasswordResult)
    {
        if (changePasswordResult.Success)
        {
            _signInManager.SignOutAsync();
            return Ok(changePasswordResult);
        }

        if (changePasswordResult.Message == Messages.UserNotFound) 
            return NotFound(changePasswordResult); 

        return BadRequest(changePasswordResult);
    }

    private IActionResult HandleChangeEmailResponse(Core.Utilities.Results.IResult changeEmailResult)
    {
        if (changeEmailResult.Success)
        {
            _signInManager.SignOutAsync();
            return Ok(changeEmailResult);
        }

        if (changeEmailResult.Message == Messages.UserNotFound)
            return NotFound(changeEmailResult);

        return BadRequest(changeEmailResult);
    }

    private IActionResult HandleCreateNewPasswordResponse(Core.Utilities.Results.IResult createNewPasswordResult)
    {
        if (createNewPasswordResult.Success) 
            return Ok(createNewPasswordResult); 
         
        if (createNewPasswordResult.Message == Messages.UserNotFound) 
            return NotFound(createNewPasswordResult); 

        return BadRequest(createNewPasswordResult);
    }

    private IActionResult HandleResetPasswordResponse(Core.Utilities.Results.IResult resetPasswordResult)
    {
        if (resetPasswordResult.Success)
            return Ok(resetPasswordResult);

        if (resetPasswordResult.Message == Messages.UserNotFound)
            return NotFound(resetPasswordResult);

        return BadRequest(resetPasswordResult);
    }
    #endregion
}
