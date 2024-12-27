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
                news.Status = true;

                return await _iNews.Update(news);
            }

            return null;
        }

        public async Task<List<NewsEntity>> GetAllNews()
        {
            return await _iNews.AllNews(n => n.Status);
        }
    }
}
