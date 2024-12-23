using Entities.Entity;

namespace Domain.Interface.InterfaceService
{
    // Essa Interface de Serviço não será usado herança com o Generic
    public interface INewsService
    {
        Task<List<News>> GetAllNews();

        Task AddNews(News news);

        Task UpdateNews(News news);
    }
}
