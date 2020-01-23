using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Way2.Application.Repositories.Abstractions;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Base class for repository implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public DbContext Context { get; }
        public DbSet<TEntity> Entities { get; }

        public Repository(DbContext context)
        {
            this.Context = context;
            this.Entities = context.Set<TEntity>();
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return this.Entities.SingleOrDefaultAsync(expression);
        }

        public async ValueTask<TEntity> AddAsync(TEntity entity)
        {
            var entry = await this.Context.AddAsync(entity);
            return entry.Entity;
        }

        public IQueryable<TEntity> Query()
        {
            return this.Entities.AsQueryable();
        }

    }
}

