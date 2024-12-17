using Domain.Interface;
using Domain.Interface.InterfaceService;
using Entities.Entity;

namespace Domain.Service
{
    public class ServiceNews : IServiceNews
    {
        private readonly INews _iNews;

        public ServiceNews(INews iNews)
        {
            _iNews = iNews;
        }

        public async Task AddNews(News news)
        {
            var validateTitle = news.ValidatePropertiesString(news.Title, nameof(news.Title));
            var validateInformation = news.ValidatePropertiesString(news.Information, nameof(news.Information));

            if (validateTitle && validateInformation)
            {
                news.DateRegister = DateTime.Now;
                news.Status = true;

                await _iNews.Add(news);
            }
        }

        public async Task UpdateNews(News news)
        {
            var validateTitle = news.ValidatePropertiesString(news.Title, nameof(news.Title));
            var validateInformation = news.ValidatePropertiesString(news.Information, nameof(news.Information));

            if (validateTitle && validateInformation)
            {
                news.DateUpdating = DateTime.Now;
                news.Status = true;

                await _iNews.Update(news);
            }
        }

        public async Task<List<News>> GetAllNews()
        {
            return await _iNews.AllNews(n => n.Status);
        }
    }
}
