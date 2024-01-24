using Common.Validators;
using Core.DTOs.Identity;
using FluentValidation;

namespace Infrastructure.Services.Validators.LoginValidators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .ShouldBeValidEmailAddress();

        RuleFor(x => x.Password) 
            .ShouldBeValidPassword();
    }
}
