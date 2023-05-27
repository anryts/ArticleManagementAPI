namespace ArticleAPI.Data.Entities;

public class ArticleVersion
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public string Title { get; set; } = null!;
    public string ContentPath { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }

    public ICollection<ArticleVersionImage>? ArticleVersionImages { get; set; } = new List<ArticleVersionImage>();
    public Article? Article { get; set; }
}