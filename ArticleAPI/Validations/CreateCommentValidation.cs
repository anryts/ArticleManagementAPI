using ArticleAPI.Models.RequestModels;
using FluentValidation;

namespace ArticleAPI.Validations;

public class CreateCommentValidation : AbstractValidator<CreateCommentRequestModel>
{
    public CreateCommentValidation()
    {
        RuleFor(x => x.ArticleId)
            .NotEmpty()
            .WithMessage("Article Id is required");

        RuleFor(x => x.CommentText)
            .NotEmpty()
            .WithMessage("Length can't be zero");

        RuleFor(x => x.CommentText)
            .MaximumLength(2000)
            .WithMessage("Length can't be more than 2000 symbols");
    }
}