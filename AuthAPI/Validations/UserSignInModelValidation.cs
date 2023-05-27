using APIGateway.Validations.CustomValidations;
using Common.Models.RequestModels;
using FluentValidation;

namespace APIGateway.Validations;

public class UserSignInModelValidation : AbstractValidator<UserSignInModel>
{
    public UserSignInModelValidation()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is required");
        
        RuleFor(x => x.Password)
            .PasswordValidation();
        
        RuleFor(x => x.OtpSendServiceType)
            .IsInEnum()
            .WithMessage("OtpSendServiceType is required");
    }
}