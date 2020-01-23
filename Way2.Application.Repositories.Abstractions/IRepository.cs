using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Application.Repositories.Abstractions
{

    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        ValueTask<TEntity> AddAsync(TEntity entity);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> Query();

    }

}
