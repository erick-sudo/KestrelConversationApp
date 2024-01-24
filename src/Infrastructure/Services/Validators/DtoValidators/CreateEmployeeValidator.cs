using Common.Validators;
using Core.DTOs.Employee;
using FluentValidation;

namespace Infrastructure.Services.Validators.DtoValidators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.LastName)
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.PhoneNumber)
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.CompanyId.ToString())
            .ShouldNotBeNullOrEmpty();

        RuleFor(x => x.Email)
            .ShouldNotBeNullOrEmpty()
            .ShouldBeValidEmailAddress();
    }
}
