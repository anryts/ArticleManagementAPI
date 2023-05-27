namespace UserAPI.Data.Entities;

public class UserJob
{
    public Guid JobId { get; set; }
    public Guid UserId { get; set; }
    public Job? Job { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }

}