using Common.Enums;

namespace Common.Filters;

public class GetArticleQueryDo
{
    /// <summary>
    /// Can be title or tag
    /// </summary>
    public string? SearchKey { get; set; }
    public SortBy SortBy { get; set; }
    public bool Ascending { get; set; } = true;
    public int AmountOfArticles { get; set; } = 10;
    public int Offset { get; set; } = 0;
}