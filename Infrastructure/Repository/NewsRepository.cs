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
        public NewsRepository(AppDbContext db) : base(db) { }

        public async Task<List<NewsEntity>> AllNews(Expression<Func<NewsEntity, bool>> exNews)
        {
            return await _db.News.Where(exNews).AsNoTracking().ToListAsync();
        }
    }
}
