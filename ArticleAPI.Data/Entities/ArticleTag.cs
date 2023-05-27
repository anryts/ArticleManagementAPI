using NpgsqlTypes;

namespace ArticleAPI.Data.Entities;

public class ArticleTag
{
    public string TagName { get; set; } = null!;
    public Guid ArticleId { get; set; }
    public Article? Article { get; set; }
    public NpgsqlTsVector SearchVector { get; set; }
    public Tag? Tag { get; set; }
}