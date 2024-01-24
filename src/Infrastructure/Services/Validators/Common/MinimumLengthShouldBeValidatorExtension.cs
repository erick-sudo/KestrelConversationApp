using FluentValidation;

namespace Common.Validators;

public static class MinimumLengthShouldBeValidatorExtension
{
    public static IRuleBuilderOptions<T, string> MinimumLengthShouldBe<T>(this IRuleBuilder<T, string> rule, int length)
    {
        return rule
            .ShouldNotBeNullOrEmpty()
            .MinimumLength(length)
            .WithMessage($"'{{PropertyName}}' should contains at least {length} characters.");
    }

    public static IRuleBuilderOptions<T, string> MinimumLengthShouldBe<T>(this IRuleBuilder<T, string> rule, int length, string withName)
    {
        return rule
            .ShouldNotBeNullOrEmpty(withName)
            .MinimumLength(length)
            .WithMessage($"'{{PropertyName}}' should contains at least {length} characters.");
    }
}
