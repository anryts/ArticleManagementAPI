namespace ArticleAPI.Models.ResponseModels;

public class GetArticleResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortIntro { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CountOfLikes { get; set; }
    public int CountOfDislikes { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public List<string> ArticleTags { get; set; } = new();
}