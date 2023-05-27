namespace ArticleAPI.Data.Entities;

public class Channel
{
    public Guid Id { get; set; }
    public bool IsPrivate { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid AuthorId { get; set; }

    public User? Author { get; set; }
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}