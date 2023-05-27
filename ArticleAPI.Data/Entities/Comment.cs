namespace ArticleAPI.Data.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ArticleId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public Article? Article { get; set; }
    public User? Author { get; set; }
}
