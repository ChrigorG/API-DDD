using Domain.Interface.Generic;
using Entities.Entity;
using System.Linq.Expressions;

namespace Domain.Interface
{
    public interface INews : IGeneric<News>
    {
        Task<List<News>> AllNews(Expression<Func<News, bool>> exNews);
    }
}
