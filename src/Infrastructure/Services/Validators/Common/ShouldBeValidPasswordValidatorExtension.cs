using FluentValidation;
using Infrastructure.Services.Validators.Common;

namespace Common.Validators;

public static class ShouldBeValidPasswordValidatorExtension
{
    public static IRuleBuilderOptions<T, string> ShouldBeValidPassword<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotNull()
            .NotEmpty()
            .MinimumLengthShouldBe(6)
            .ShouldContainsAtLeastOneLowerCaseCharacter()
            .ShouldContainsAtLeastOneUpperCaseCharacter() 
            .ShouldContainsAtLeastOneDigit()
            .ShouldContainAtLeastOneNonAlphanumericCharacter();
    }

    public static IRuleBuilderOptions<T, string> ShouldBeValidNewPassword<T>(this IRuleBuilder<T, string> rule, string withName)
    {
        return rule
            .NotNull()
            .NotEmpty()
            .MinimumLengthShouldBe(6)
            .ShouldContainsAtLeastOneLowerCaseCharacter()
            .ShouldContainsAtLeastOneUpperCaseCharacter()
            .ShouldContainsAtLeastOneDigit()
            .ShouldContainAtLeastOneNonAlphanumericCharacter(withName); 
    }
}
