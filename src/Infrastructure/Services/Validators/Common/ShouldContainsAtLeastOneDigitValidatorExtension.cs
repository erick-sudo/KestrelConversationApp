using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators;

public static class ShouldContainsAtLeastOneDigitValidatorExtension
{
    private static readonly Regex DigitRegex = new("(\\d)+");

    public static IRuleBuilderOptions<T, string> ShouldContainsAtLeastOneDigit<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .Matches(DigitRegex)
            .WithMessage("'{PropertyName}' should contains at least 1 digit.");
    }

    public static IRuleBuilderOptions<T, string> ShouldContainsAtLeastOneDigit<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .Matches(DigitRegex)
            .WithName(withName)
            .WithMessage("'{PropertyName}' should contains at least 1 digit.");
    }
}
