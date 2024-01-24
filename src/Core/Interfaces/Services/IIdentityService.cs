using Core.DTOs.Company;
using Core.DTOs.Identity;
using Core.Identity;
using Core.Utilities.Results;

namespace Core.Interfaces.Services;

public interface IIdentityService
{
    Task<IDataResult<ApplicationUser>> LoginAsync(LoginDto loginServiceRequest, CancellationToken cancellationToken);
    Task<IResult> RegisterAsync(CreateCompanyDto request, CancellationToken cancellationToken);
    Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordServiceRequest, CancellationToken cancellationToken);
    Task<IResult> CreateNewPasswordAsync(CreateNewPasswordDto createNewPasswordRequest, CancellationToken cancellationToken);
    Task<IResult> ChangePassswordAsync(string currentEmail, ChangePasswordDto changePasswordRequest, CancellationToken cancellationToken);
    Task<IResult> ChangeEmailAsync(string currentEmail, ChangeEmailDto changeEmailRequest, CancellationToken cancellationToken);
}
