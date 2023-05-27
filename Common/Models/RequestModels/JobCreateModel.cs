using Common.Enums;

namespace Common.Models.RequestModels;

public class JobCreateModel
{
    public string JobName { get; set; } = null!;
    public Gender JobGender { get; set; }
}