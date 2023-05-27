namespace ArticleAPI.Models.ResponseModels;

public class GetCommentRelatedToArticleResponseModel
{
    public Guid CommentId { get; set; }
    public string CommentText { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
}
