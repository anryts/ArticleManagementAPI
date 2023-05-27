namespace ArticleAPI.Models.RequestModels;

public class EditChannelRequestModel
{
    public Guid ChannelId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}