using Common.Validators;
using FluentValidation;

namespace Infrastructure.Services.Validators.AccountValidators;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x.ToString()).ShouldBeValidGuid("Guid");
    }
}
