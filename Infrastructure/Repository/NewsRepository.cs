using Domain.Interface;
using Entities.Entity;
using Infrastructure.Repository.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class NewsRepository : GenericRepository<NewsEntity>, INews
    {

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public NewsRepository() 
        {
            _dbContextOptions = new DbContextOptions<AppDbContext>();
        }

        public async Task<List<NewsEntity>> AllNews(Expression<Func<NewsEntity, bool>> exNews)
        {
            using (var db = new AppDbContext(_dbContextOptions))
            {
                return await db.News.Where(exNews).AsNoTracking().ToListAsync();
            }
        }
    }
}
