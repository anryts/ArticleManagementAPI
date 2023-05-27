namespace ArticleAPI.Models.RequestModels;

/// <summary>
/// Request model for creating new article
/// </summary>
public class CreateNewArticleModel
{
    /// <summary>
    /// Title of the article
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    ///  Content of the article in html format
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// List of tags for the article
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
}