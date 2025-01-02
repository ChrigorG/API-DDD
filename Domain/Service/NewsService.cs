using Domain.Interface;
using Domain.Interface.InterfaceService;
using Entities.Entity;

namespace Domain.Service
{
    public class NewsService : INewsService
    {
        private readonly INews _iNews;

        public NewsService(INews iNews)
        {
            _iNews = iNews;
        }

        public async Task<List<NewsEntity>> GetNews()
        {
            return await _iNews.Get();
        }

        public async Task<NewsEntity?> GetNews(int id)
        {
            return await _iNews.Get(id);
        }

        public async Task<NewsEntity?> AddNews(NewsEntity news)
        {
            var validateTitle = news.ValidatePropertiesString(news.Title, nameof(news.Title));
            var validateInformation = news.ValidatePropertiesString(news.Information, nameof(news.Information));

            if (validateTitle && validateInformation)
            {
                news.DateRegister = DateTime.Now;
                news.Status = true;

                return await _iNews.Add(news);
            }

            return null;
        }

        public async Task<NewsEntity?> UpdateNews(NewsEntity news)
        {
            var validateTitle = news.ValidatePropertiesString(news.Title, nameof(news.Title));
            var validateInformation = news.ValidatePropertiesString(news.Information, nameof(news.Information));

            if (validateTitle && validateInformation)
            {
                news.DateUpdating = DateTime.Now;
                return await _iNews.Update(news);
            }

            return null;
        }

        public async Task<NewsEntity?> DeleteNews(NewsEntity news)
        {
            return await _iNews.Delete(news);
        }
    }
}
