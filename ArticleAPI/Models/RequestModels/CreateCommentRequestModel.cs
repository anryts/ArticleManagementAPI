namespace ArticleAPI.Models.RequestModels;

public class CreateCommentRequestModel
{
    public Guid ArticleId { get; set; }
    public string CommentText { get; set; }
}
