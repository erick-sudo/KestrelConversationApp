using Core.DTOs.Company;
using Core.DTOs.Identity;
using Core.Identity;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;
using System.Transactions;
using System.Web;

namespace Infrastructure.Services;

public class IdentityService(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    ICompanyService _companyService,
    IEmployeeService _employeeService) : IIdentityService
{
    public async Task<IDataResult<ApplicationUser>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return new ErrorDataResult<ApplicationUser>(message: Messages.UserNotFound);
        }

        await _signInManager.SignOutAsync();
        var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, true);
        if (signInResult.IsLockedOut)
        {
            return new ErrorDataResult<ApplicationUser>(message: Messages.UserLockedOutError);
        }

        if (!signInResult.Succeeded)
        {
            return new ErrorDataResult<ApplicationUser>(message: Messages.EmailOrPasswordError);
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var rolesAsString = string.Join(",", userRoles);

        return new SuccessDataResult<ApplicationUser>(data: user, message: rolesAsString);
    }

    public async Task<IResult> RegisterAsync(CreateCompanyDto createCompanyDto, CancellationToken cancellationToken)
    {
        var employeeResult = await _employeeService.GetEmployeeByEmailAsync(createCompanyDto.Email);
        if (employeeResult.Message is not Messages.EmployeeNotFound)
        {
            return new ErrorResult(message: Messages.EmployeeAlreadyExists);
        }

        var createCompanyResult = await _companyService.CreateCompanyAsync(createCompanyDto);
        var message = createCompanyResult.Message;

        return !createCompanyResult.Success
            ? new ErrorResult(message)
            : new SuccessResult(message);
    }

    public async Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
        var email = resetPasswordDto.Email;
        var password = resetPasswordDto.NewPassword;
        var token = HttpUtility.UrlDecode(resetPasswordDto.Token);

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
        if (!resetPasswordResult.Succeeded)
        {
            return CreateErrorResultWithIdentityErrorsMessage(resetPasswordResult);
        }

        var securityStampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!securityStampResult.Succeeded)
        {
            return CreateErrorResultWithIdentityErrorsMessage(securityStampResult);
        }

        user.EmailConfirmed = true;
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return CreateErrorResultWithIdentityErrorsMessage(updateResult);
        }

        transactionScope.Complete();
        return new SuccessResult(Messages.ResetPasswordSuccess);
    }

    public async Task<IResult> CreateNewPasswordAsync(CreateNewPasswordDto createNewPasswordDto, CancellationToken cancellationToken)
    {
        using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
        var email = createNewPasswordDto.Email;
        var password = createNewPasswordDto.NewPassword;
        var token = HttpUtility.UrlDecode(createNewPasswordDto.Token);

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
        if (!resetPasswordResult.Succeeded)
        {
            return CreateErrorResultWithIdentityErrorsMessage(resetPasswordResult);
        }

        var securityStampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!securityStampResult.Succeeded)
        {
            return CreateErrorResultWithIdentityErrorsMessage(securityStampResult);
        }

        user.EmailConfirmed = true;
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return CreateErrorResultWithIdentityErrorsMessage(updateResult);
        }

        transactionScope.Complete();
        return new SuccessResult(Messages.CreateNewPasswordSuccess);
    }

    public async Task<IResult> ChangePassswordAsync(string currentEmail, ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(currentEmail);
        if (user == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        var currentPassword = changePasswordDto.CurrentPassword;
        var newPassword = changePasswordDto.NewPassword;

        var passwordCheckResult = await _userManager.CheckPasswordAsync(user, currentPassword);
        if (!passwordCheckResult)
        {
            return new ErrorResult(Messages.InvalidCurrentPassword);
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!changePasswordResult.Succeeded)
        {
            var errorMessages = string.Join('&', changePasswordResult.Errors.Select(e => e.Description));
            return new ErrorResult(errorMessages);
        }

        return new SuccessResult(Messages.PasswordChangeSuccess);
    }

    public async Task<IResult> ChangeEmailAsync(string currentEmail, ChangeEmailDto changeEmailDto, CancellationToken cancellationToken)
    {
        var newEmail = changeEmailDto.NewEmail;

        var user = await _userManager.FindByEmailAsync(currentEmail);
        if (user == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        var existingUser = await _userManager.FindByEmailAsync(newEmail);
        if (existingUser != null && existingUser.Id != user.Id)
        {
            return new ErrorResult(Messages.EmailAlreadyExists);
        }

        using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);

        user.Email = newEmail;
        user.EmailConfirmed = false;
        var changeEmailResult = await _userManager.UpdateAsync(user);
        if (!changeEmailResult.Succeeded)
        {
            var errorMessages = string.Join('&', changeEmailResult.Errors.Select(e => e.Description));
            return new ErrorResult(errorMessages);
        }

        var changeEmployeeEmailResult = await _employeeService.ChangeEmailAsync(currentEmail, newEmail);
        if (!changeEmployeeEmailResult.Success)
        {
            return changeEmployeeEmailResult;
        }

        transactionScope.Complete();
        return new SuccessResult(Messages.EmailChangeSuccess);
    }

    #region Helper Methods
    private static IResult CreateErrorResultWithIdentityErrorsMessage(IdentityResult resetPasswordResult)
    {
        var errorMessages = string.Join('&', resetPasswordResult.Errors.Select(e => e.Description));

        return new ErrorResult(errorMessages);
    }
    #endregion
}
