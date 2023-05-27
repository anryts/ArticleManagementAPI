using ArticleAPI.Models.RequestModels;
using FluentValidation;

namespace ArticleAPI.Validations;

public class CreateChannelValidation : AbstractValidator<CreateChannelRequestModel>
{
    public CreateChannelValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MinimumLength(6)
            .WithMessage("Title must be not less than 6 symbols")
            .MaximumLength(100)
            .WithMessage("Title must be not longer than 100 symbols");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MinimumLength(10)
            .WithMessage("Description must be not less than 10 symbols")
            .MaximumLength(500)
            .WithMessage("Description must be not longer than 500 symbols");
    }
}