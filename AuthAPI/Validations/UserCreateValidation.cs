using APIGateway.Validations.CustomValidations;
using Common.Models.RequestModels;
using FluentValidation;

namespace APIGateway.Validations;

public class UserCreateValidation : AbstractValidator<UserCreateModel>
{
    public UserCreateValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.Password).PasswordValidation();
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Gender can be: 0, 1 or 2");
    }
}