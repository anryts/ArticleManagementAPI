using ArticleAPI.Models.RequestModels;
using FluentValidation;

namespace ArticleAPI.Validations;

public class EditChannelValidation : AbstractValidator<EditChannelRequestModel>
{
    public EditChannelValidation()
    {
        RuleFor(x => x.ChannelId).NotNull();
    }
}