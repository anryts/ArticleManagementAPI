using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Data.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> IsExistByNameAsync(string tagName)
    {
        return await _dbSet
            .AnyAsync(tag => tag.Name == tagName);
    }

    public async Task CreateByNamesAsync(List<string> tagNames)
    {
        var tagNamesToCompare = tagNames
            .Select(x => x.ToLower())
            .ToList();

        var existsTags = await _dbSet
            .Where(tag => tagNamesToCompare.Contains(tag.Name.ToLower()))
            .Select(tag => tag.Name.ToLower())
            .ToListAsync();

        var tagsNotInDb = tagNames
            .Except(existsTags, StringComparer.OrdinalIgnoreCase)
            .ToList();

        await _dbSet
            .AddRangeAsync(tagsNotInDb
                .Select(tag => new Tag { Name = tag }));
    }
}