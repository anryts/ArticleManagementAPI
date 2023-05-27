using ArticleAPI.Data.Entities;

namespace ArticleAPI.Data.Repositories.Interfaces;

public interface ITagRepository : IBaseRepository<Tag>
{
    Task<bool> IsExistByNameAsync(string tagName);
    Task CreateByNamesAsync(List<string> tagNames);
}