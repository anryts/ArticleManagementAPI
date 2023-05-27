using ArticleAPI.Data.Entities;

namespace ArticleAPI.Data.Repositories.Interfaces;

public interface IChannelRepository : IBaseRepository<Channel>
{
    Task<bool> IsTitleExistAsync(string title);
}