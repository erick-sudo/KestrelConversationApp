using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators;

public static class ShouldContainsAtLeastOneLowerCaseCharacterValidatorExtension
{
    private static readonly Regex LowerCaseRegex = new("[a-z]+");

    public static IRuleBuilderOptions<T, string> ShouldContainsAtLeastOneLowerCaseCharacter<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .Matches(LowerCaseRegex)
            .WithMessage("'{PropertyName}' should contains at least 1 lower case character.");
    }

    public static IRuleBuilderOptions<T, string> ShouldContainsAtLeastOneLowerCaseCharacter<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .Matches(LowerCaseRegex)
            .WithName(withName)
            .WithMessage("'{PropertyName}' should contains at least 1 lower case character.");
    }
}
