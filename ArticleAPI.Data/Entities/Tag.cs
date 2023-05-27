using NpgsqlTypes;

namespace ArticleAPI.Data.Entities;

public class Tag
{
    /// <summary>
    /// It will be used as a primary key
    /// </summary>
    public string Name { get; set; } = null!;

    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
}