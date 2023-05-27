namespace UserAPI.Data.Entities;

public class Job
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<UserJob> Users { get; set; } = new List<UserJob>();
}