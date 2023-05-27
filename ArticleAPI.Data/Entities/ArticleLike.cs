namespace ArticleAPI.Data.Entities;

public class ArticleLike
{
    public Guid ArticleId { get; set; }
    public Guid AuthorId { get; set; }
    public bool? IsLike { get; set; }

    public Article? Article { get; set; }
    public User? Author { get; set; }
}
