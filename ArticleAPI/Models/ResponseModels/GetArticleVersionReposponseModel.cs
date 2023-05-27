namespace ArticleAPI.Models.ResponseModels;

public class GetArticleVersionReposponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string> ImageUrls { get; set; }
}