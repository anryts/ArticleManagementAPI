namespace ArticleAPI.Models.RequestModels;

public class UpdateChannelRequestModel
{
    public Guid ChannelId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
