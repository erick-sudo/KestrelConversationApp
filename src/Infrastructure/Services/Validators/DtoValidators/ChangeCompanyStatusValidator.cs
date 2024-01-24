using Common.Validators;
using Core.DTOs.Company;
using FluentValidation;

namespace Infrastructure.Services.Validators.DtoValidators;

public class ChangeCompanyStatusValidator : AbstractValidator<ChangeCompanyStatusDto>
{
    public ChangeCompanyStatusValidator()
    {
        RuleFor(x => x.CompanyId.ToString())
            .ShouldBeValidGuid();

        RuleFor(x => x.Status)
            .ShouldBeValidBoolean();
    }
}
