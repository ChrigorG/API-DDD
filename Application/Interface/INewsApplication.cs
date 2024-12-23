using Application.Interface.Generic;
using Entities.Entity;

namespace Application.Interface
{
    public interface INewsApplication : IGenericApplication<News>
    {
        Task<List<News>> GetAllNews();

        Task AddNews(News news);

        Task UpdateNews(News news);
    }
}
