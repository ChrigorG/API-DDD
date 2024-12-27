using Application.Interface.Generic;
using Entities.Entity;

namespace Application.Interface
{
    public interface INewsApplication : IGenericApplication<NewsEntity>
    {
        Task<List<NewsEntity>> GetAllNews();

        Task AddNews(NewsEntity news);

        Task UpdateNews(NewsEntity news);
    }
}
