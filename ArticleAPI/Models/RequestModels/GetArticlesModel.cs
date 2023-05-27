using Common.Enums;

namespace ArticleAPI.Models.RequestModels;

public class GetArticlesModel
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