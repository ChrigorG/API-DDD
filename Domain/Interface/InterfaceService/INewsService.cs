using Entities.Entity;

namespace Domain.Interface.InterfaceService
{
    // Essa Interface de Serviço não será usado herança com o Generic
    public interface INewsService
    {
        Task<List<NewsEntity>> GetNews();

        Task<NewsEntity?> GetNews(int id);

        Task<NewsEntity?> AddNews(NewsEntity news);

        Task<NewsEntity?> UpdateNews(NewsEntity news);

        Task<NewsEntity?> DeleteNews(NewsEntity news);
    }
}
