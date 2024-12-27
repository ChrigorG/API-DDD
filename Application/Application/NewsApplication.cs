using Application.Interface;
using Domain.Interface;
using Domain.Interface.InterfaceService;
using Entities.Entity;

namespace Application.Application
{
    public class NewsApplication : INewsApplication
    {
        private INews _iNews;
        private INewsService _newsService;

        public NewsApplication(INews iNews,
             INewsService newsService) 
        {
            _iNews = iNews;
            _newsService = newsService;
        }

        // Custom 
        public async Task<List<NewsEntity>> GetAllNews()
        {
            return await _newsService.GetAllNews();
        }

        public async Task<NewsEntity?> AddNews(NewsEntity news)
        {
            return await _newsService.AddNews(news);
        }

        public async Task<NewsEntity?> UpdateNews(NewsEntity news)
        {
            return await _newsService.UpdateNews(news);
        }


        // Generics
        public async Task<NewsEntity?> Get(int id)
        {
            return await _iNews.Get(id);
        }

        public async Task<List<NewsEntity>> Get()
        {
            return await _iNews.Get();
        }

        public async Task<NewsEntity?> Add(NewsEntity entity)
        {
            return await _iNews.Add(entity);
        }

        public async Task<NewsEntity?> Update(NewsEntity entity)
        {
            return await _iNews.Update(entity);
        }

        public async Task<NewsEntity?> Delete(NewsEntity entity)
        {
            return await _iNews.Delete(entity);
        }
    }
}
