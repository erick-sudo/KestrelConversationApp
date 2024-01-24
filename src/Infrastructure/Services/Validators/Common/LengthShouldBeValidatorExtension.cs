using FluentValidation;

namespace Common.Validators;

public static class LengthShouldBeValidatorExtension
{
    public static IRuleBuilderOptions<T, string> LengthShouldBe<T>(this IRuleBuilder<T, string> rule, int length)
    {
        return rule
            .ShouldNotBeNullOrEmpty()
            .Length(length)
            .WithMessage($"'{{PropertyName}}' should contains at least {length} characters.");
    }

    public static IRuleBuilderOptions<T, string> LengthShouldBe<T>(this IRuleBuilder<T, string> rule, int length, string withName)
    {
        return rule
            .ShouldNotBeNullOrEmpty(withName)
            .Length(length)
            .WithMessage($"'{{PropertyName}}' should contains at least {length} characters.");
    }
}
