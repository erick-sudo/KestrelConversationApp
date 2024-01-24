using Common.Validators;
using Core.DTOs.Identity;
using FluentValidation;

namespace Infrastructure.Services.Validators.AccountValidators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .ShouldBeValidPassword();

        RuleFor(x => x.NewPassword)
            .ShouldBeValidNewPassword("New Password");

        RuleFor(x => x.NewPasswordConfirm)
            .ShouldBeValidNewPassword("New Password Confirm")
            .Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match");
    }
}
