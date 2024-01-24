using FluentValidation; 

namespace Common.Validators;

public static class ShouldBeBooleanValidatorExtension
{
    public static IRuleBuilderOptions<T, bool> ShouldBeValidBoolean<T>(this IRuleBuilder<T, bool> rule)
    {
        return rule
            .Must(x => x == false || x == true)
            .WithMessage($"{{PropertyName}} must be either true or false");
    }
}
