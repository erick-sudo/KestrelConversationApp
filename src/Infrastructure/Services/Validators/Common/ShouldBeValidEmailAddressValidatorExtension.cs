using FluentValidation;

namespace Common.Validators;

public static class ShouldBeValidEmailAddressValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldBeValidEmailAddress<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .ShouldNotBeNullOrEmpty("Email")
            .EmailAddress()
            .WithMessage("Please provide a valid email address.");
    }
}
