using Common.Validators;
using Core.DTOs.Company;
using FluentValidation;

namespace Infrastructure.Services.Validators.AccountValidators;

public class RegisterValidator : AbstractValidator<CreateCompanyDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.BusinessRegistrationNumber)
            .ShouldNotBeNullOrEmpty(); 

        RuleFor(x => x.FirstName)
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.LastName)
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.Email)
            .ShouldNotBeNullOrEmpty()
            .ShouldBeValidEmailAddress();
    }
}
