using ArticleAPI.Models.ResponseModels;
using MediatR;

namespace ArticleAPI.Channels.Queries;

public class GetChannelsForUserQuery : IRequest<IEnumerable<GetChannelsForUserResponseModel>>
{
    public bool ShowOnlyMyChannels { get; set; } = false;
    public int Offset { get; set; } = 0;
    public int Take { get; set; } = 10;
}