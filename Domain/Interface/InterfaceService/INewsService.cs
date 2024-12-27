using Entities.Entity;

namespace Domain.Interface.InterfaceService
{
    // Essa Interface de Serviço não será usado herança com o Generic
    public interface INewsService
    {
        Task<List<NewsEntity>> GetAllNews();

        Task AddNews(NewsEntity news);

        Task UpdateNews(NewsEntity news);
    }
}
