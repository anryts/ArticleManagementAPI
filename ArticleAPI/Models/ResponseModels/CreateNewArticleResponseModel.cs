namespace ArticleAPI.Models.ResponseModels;

public class CreateNewArticleResponseModel
{
    public Guid ArticleId { get; set; }
    public Guid ArticleVersionId { get; set; }
}