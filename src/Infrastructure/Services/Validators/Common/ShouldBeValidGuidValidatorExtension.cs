using FluentValidation; 

namespace Common.Validators;

public static class ShouldBeValidGuidValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldBeValidGuid<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .ShouldNotBeNullOrEmpty()
            .Must(input => Guid.TryParse(input, out var guid) && guid != Guid.Empty)
            .WithMessage($"{{PropertyName}} must be a valid non-empty GUID.");
    }

    public static IRuleBuilderOptions<T, string> ShouldBeValidGuid<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .ShouldNotBeNullOrEmpty()
            .WithName(withName)
            .Must(input => Guid.TryParse(input, out var guid) && guid != Guid.Empty)
            .WithMessage($"{{PropertyName}} must be a valid non-empty GUID.");
    }
}
