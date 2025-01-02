using Application.Interface;
using Domain.Interface.InterfaceService;
using Entities.Entity;

namespace Application.Application
{
    public class NewsApplication : INewsApplication
    {
        private INewsService _newsService;

        public NewsApplication(
             INewsService newsService) 
        {
            _newsService = newsService;
        }

        // Generics
        public async Task<NewsEntity?> Get(int id)
        {
            return await _newsService.GetNews(id);
        }

        public async Task<List<NewsEntity>> Get()
        {
            List<NewsEntity> list = await _newsService.GetNews();
            return list.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<NewsEntity?> Add(NewsEntity entity)
        {
            entity.DateRegister = DateTime.Now;
            return await _newsService.AddNews(entity);
        }

        public async Task<NewsEntity?> Update(NewsEntity entity)
        {
            entity.DateUpdating = DateTime.Now;
            return await _newsService.UpdateNews(entity);
        }

        public async Task<NewsEntity?> Delete(NewsEntity entity)
        {
            return await _newsService.DeleteNews(entity);
        }
    }
}
