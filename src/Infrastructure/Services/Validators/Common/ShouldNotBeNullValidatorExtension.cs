using FluentValidation;

namespace Common.Validators;

public static class ShouldNotBeNullValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldNotBeNull<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotNull();
    }

    public static IRuleBuilderOptions<T, TType> ShouldNotBeNull<T, TType>(this IRuleBuilder<T, TType> rule)
        where TType : class
    {
        return rule
            .NotNull();
    }

    public static IRuleBuilderOptions<T, string> ShouldNotBeNull<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .NotNull()
            .WithName(withName);
    }

    public static IRuleBuilderOptions<T, TType> ShouldNotBeNull<T, TType>(this IRuleBuilder<T, TType> rule, string withName)
        where TType : class
    {
        return rule.NotNull()
            .WithName(withName);
    }
}
