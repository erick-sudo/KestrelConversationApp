using Common.Validators;
using Core.DTOs.Identity;
using FluentValidation;

namespace Infrastructure.Services.Validators.LoginValidators;

public class ChangeEmailValidator : AbstractValidator<ChangeEmailDto>
{
    public ChangeEmailValidator()
    {
        RuleFor(x => x.NewEmail)
            .ShouldBeValidEmailAddress(); 
    }
}
