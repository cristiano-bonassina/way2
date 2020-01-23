using System;
using LogicArt.Framework.Core.DependencyInjection;
using Way2.Domain.Entities.Abstractions;

namespace Way2.Domain.Repositories.Abstractions.Extensions
{
    public static class InstanceProviderExtensions
    {

        public static IRepository<TEntity, TKey> GetRepositoryFor<TEntity, TKey>(this IInstanceProvider instanceProvider) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            var repositoryType = typeof(IRepository<,>).MakeGenericType(typeof(TEntity), typeof(TKey));
            return (IRepository<TEntity, TKey>)instanceProvider.GetInstance(repositoryType);
        }

        public static IRepository<TEntity, Guid> GetRepositoryFor<TEntity>(this IInstanceProvider instanceProvider) where TEntity : class, IEntity<Guid>
        {
            return GetRepositoryFor<TEntity, Guid>(instanceProvider);
        }

    }
}
