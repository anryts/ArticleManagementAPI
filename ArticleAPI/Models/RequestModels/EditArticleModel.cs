namespace ArticleAPI.Models.RequestModels;

/// <summary>
///  Request model for editing existing articles
/// </summary>
public class EditArticleModel
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