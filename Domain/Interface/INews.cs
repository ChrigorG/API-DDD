using Domain.Interface.Generic;
using Entities.Entity;
using System.Linq.Expressions;

namespace Domain.Interface
{
    public interface INews : IGeneric<NewsEntity>
    {
        Task<List<NewsEntity>> AllNews(Expression<Func<NewsEntity, bool>> exNews);
    }
}
