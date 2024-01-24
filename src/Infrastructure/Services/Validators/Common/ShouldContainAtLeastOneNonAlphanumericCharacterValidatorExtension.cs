using FluentValidation;

namespace Infrastructure.Services.Validators.Common;

public static class ShouldContainAtLeastOneNonAlphanumericCharacterValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldContainAtLeastOneNonAlphanumericCharacter<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .Must(password => !string.IsNullOrEmpty(password) && password.Any(c => !char.IsLetterOrDigit(c)))
            .WithMessage("Password must contain at least one non-alphanumeric character.");
    }

    public static IRuleBuilderOptions<T, string> ShouldContainAtLeastOneNonAlphanumericCharacter<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .Must(password => !string.IsNullOrEmpty(password) && password.Any(c => !char.IsLetterOrDigit(c)))
            .WithName(withName)
            .WithMessage($"{withName} must contain at least one non-alphanumeric character.");
    }
}
