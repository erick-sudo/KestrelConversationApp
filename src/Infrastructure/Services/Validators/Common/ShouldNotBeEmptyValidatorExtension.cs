using FluentValidation;

namespace Common.Validators;

public static class ShouldNotBeEmptyValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldNotBeEmpty<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty();
    }

    public static IRuleBuilderOptions<T, TType> ShouldNotBeEmpty<T, TType>(this IRuleBuilder<T, TType> rule)
        where TType : class
    {
        return rule
            .NotEmpty();
    }

    public static IRuleBuilderOptions<T, string> ShouldNotBeEmpty<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .NotEmpty()
            .WithName(withName);
    }

    public static IRuleBuilderOptions<T, TType> ShouldNotBeEmpty<T, TType>(this IRuleBuilder<T, TType> rule, string withName)
        where TType : class
    {
        return rule
            .NotEmpty()
            .WithName(withName);
    }
}
