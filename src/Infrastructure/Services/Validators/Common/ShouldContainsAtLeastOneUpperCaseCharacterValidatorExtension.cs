using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators;

public static class ShouldContainsAtLeastOneUpperCaseCharacterValidatorExtension
{
    private static readonly Regex UpperCaseRegex = new("[A-Z]+");

    public static IRuleBuilderOptions<T, string> ShouldContainsAtLeastOneUpperCaseCharacter<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .Matches(UpperCaseRegex)
            .WithMessage("'{PropertyName}' should contains at least 1 upper case character.");
    }

    public static IRuleBuilderOptions<T, string> ShouldContainsAtLeastOneUpperCaseCharacter<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .Matches(UpperCaseRegex)
            .WithName(withName)
            .WithMessage("'{PropertyName}' should contains at least 1 upper case character.");
    }
}
