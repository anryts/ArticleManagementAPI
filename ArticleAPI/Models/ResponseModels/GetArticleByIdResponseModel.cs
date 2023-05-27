namespace ArticleAPI.Models.ResponseModels;

public class GetArticleByIdResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public int CountOfLikes { get; set; }
    public int CountOfDislikes { get; set; }
    public List<string> ArticleImageUrls { get; set; } = new();
    public List<string> ArticleTags { get; set; } = new();
}