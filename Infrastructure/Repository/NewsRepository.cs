using Domain.Interface;
using Entities.Entity;
using Infrastructure.Repository.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class NewsRepository : GenericRepository<News>, INews
    {

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public NewsRepository() 
        {
            _dbContextOptions = new DbContextOptions<AppDbContext>();
        }

        public async Task<List<News>> AllNews(Expression<Func<News, bool>> exNews)
        {
            using (var db = new AppDbContext(_dbContextOptions))
            {
                return await db.News.Where(exNews).AsNoTracking().ToListAsync();
            }
        }
    }
}
