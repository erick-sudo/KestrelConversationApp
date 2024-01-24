using Common.Validators;
using Core.DTOs.Identity;
using FluentValidation;

namespace Infrastructure.Services.Validators.AccountValidators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .ShouldBeValidEmailAddress();

        RuleFor(x => x.NewPassword)
            .ShouldBeValidPassword();

        RuleFor(x => x.NewPasswordConfirm)
            .ShouldBeValidPassword()
            .Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match");
    }
}
