using Application.Interface.Generic;
using Entities.Entity;

namespace Application.Interface
{
    public interface INewsApplication : IGenericApplication<NewsEntity>
    {
        Task<List<NewsEntity>> GetAllNews();

        Task<NewsEntity?> AddNews(NewsEntity news);

        Task<NewsEntity?> UpdateNews(NewsEntity news);
    }
}
