using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Data.Repositories;

public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
{
    public ChannelRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> IsTitleExistAsync(string title)
    {
        return await _dbSet
            .AnyAsync(x => x.Title == title);
    }
}