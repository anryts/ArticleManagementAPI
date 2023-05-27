using Common.Enums;

namespace UserAPI.Models.RequestModels;

public class JobCreateModel
{
    public string JobName { get; set; } = null!;
    public Gender JobGender { get; set; }
}