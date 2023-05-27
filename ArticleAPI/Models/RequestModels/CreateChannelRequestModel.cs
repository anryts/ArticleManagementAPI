namespace ArticleAPI.Models.RequestModels;

public class CreateChannelRequestModel
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPrivate { get; set; }
}
