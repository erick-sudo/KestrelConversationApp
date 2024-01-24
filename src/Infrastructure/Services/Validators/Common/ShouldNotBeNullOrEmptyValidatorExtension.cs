using FluentValidation;

namespace Common.Validators;

public static class ShouldNotBeNullOrEmptyValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .ShouldNotBeNull()
            .ShouldNotBeEmpty();
    }

    public static IRuleBuilderOptions<T, TType> ShouldNotBeNullOrEmpty<T, TType>(this IRuleBuilder<T, TType> rule)
        where TType : class
    {
        return rule
            .ShouldNotBeNull()
            .ShouldNotBeEmpty();
    }

    public static IRuleBuilderOptions<T, string> ShouldNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .ShouldNotBeNull(withName)
            .ShouldNotBeEmpty(withName);
    }

    public static IRuleBuilderOptions<T, TType> ShouldNotBeNullOrEmpty<T, TType>(this IRuleBuilder<T, TType> rule, string withName)
        where TType : class
    {
        return rule
            .ShouldNotBeNull(withName)
            .ShouldNotBeEmpty(withName);
    }
}
