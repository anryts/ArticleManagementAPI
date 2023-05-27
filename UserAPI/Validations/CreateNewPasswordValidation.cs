using FluentValidation;
using UserAPI.Models.RequestModels;

namespace UserAPI.Validations;

public class CreateNewPasswordValidation : AbstractValidator<CreateNewPasswordModel>
{
    public CreateNewPasswordValidation()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
    }
}