namespace ArticleAPI.Models.RequestModels;

public class GetArticleCommentsByIdRequestModel
{
    public Guid ArticleId { get; set; }
    public int? Offset { get; set; }
    public int? Take { get; set; }
}
