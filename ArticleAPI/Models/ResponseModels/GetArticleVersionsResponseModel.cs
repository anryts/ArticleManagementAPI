namespace ArticleAPI.Models.ResponseModels;

public class GetArticleVersionsResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortIntro { get; set; }
    public DateTime UpdatedAt { get; set; }
}