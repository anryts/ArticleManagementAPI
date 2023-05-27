using ArticleAPI.Models.RequestModels;
using FluentValidation;

namespace ArticleAPI.Validations;

public class CreateNewArticleValidation : AbstractValidator<CreateNewArticleModel>
{
    public CreateNewArticleValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Title is required");
        
        RuleFor(x => x.Content)
            .NotEmpty()
            .MinimumLength(50)
            .WithMessage("Content is required");
    }    
}