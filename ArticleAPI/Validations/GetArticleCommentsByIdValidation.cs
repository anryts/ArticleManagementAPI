using ArticleAPI.Models.RequestModels;
using FluentValidation;

namespace ArticleAPI.Validations;

public class GetArticleCommentsByIdValidation : AbstractValidator<GetArticleCommentsByIdRequestModel>
{
    public GetArticleCommentsByIdValidation()
    {
        RuleFor(x => x.ArticleId)
            .NotEmpty()
            .WithMessage("ArticleId required");
    }
}