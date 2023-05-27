namespace ArticleAPI.Models.ResponseModels;

public class GetChannelsForUserResponseModel
{
    public Guid ChannelId { get; set; }
    public int CountOfSubscribers { get; set; }
    public int CountOfArticles { get; set; }
}
