using FluentValidation;

namespace APIGateway.Validations.CustomValidations;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> PasswordValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
        return options;
    }
}