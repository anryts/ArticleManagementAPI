using Common.Models.RequestModels;
using FluentValidation;

namespace APIGateway.Validations;

public class JobCreateValidation : AbstractValidator<JobCreateModel>
{
    public JobCreateValidation()
    {
        RuleFor(x => x.JobName).NotEmpty().WithMessage("Job name is required");
        RuleFor(x => x.JobGender).IsInEnum().WithMessage("Gender can be: 0, 1 or 2");
    }
}