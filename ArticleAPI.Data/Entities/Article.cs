using NpgsqlTypes;

namespace ArticleAPI.Data.Entities;

public class Article
{
    public Guid Id { get; set; }
    public Guid? ChannelId { get; set; }
    public string Title { get; set; } = null!;

    /// <summary>
    /// File path to the article which is html file
    /// </summary>
    public string ContentPath { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
    public Guid AuthorId { get; set; }

    /// <summary>
    /// The first (500 - min and 1000 is max) characters of this Article 
    /// and must ended on completed sentence.
    /// </summary>
    public string? ShortIntro { get; set; }

    /// <summary>
    /// Delete after 30 days 
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// When delete 
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public NpgsqlTsVector SearchVector { get; set; }
    
    public User? Author { get; set; }
    public Channel? Channel { get; set; }
    public ICollection<ArticleImage>? Images { get; set; } = new List<ArticleImage>();
    public ICollection<ArticleVersion>? ArticleVersions { get; set; } = new List<ArticleVersion>();
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<ArticleLike> Likes { get; set; } = new List<ArticleLike>();
}