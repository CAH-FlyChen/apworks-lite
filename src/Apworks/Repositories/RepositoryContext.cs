using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        private readonly Guid id;
        private readonly ConcurrentDictionary<Type, object> cachedRepositories = new ConcurrentDictionary<Type, object>();

        public Guid Id => this.id;

        protected abstract IRepository<TKey, TAggregateRoot> CreateRepository<TKey, TAggregateRoot>()
            where TKey : IEquatable<TKey>
            where TAggregateRoot : IAggregateRoot<TKey>;

        public abstract Task CommitAsync();

        public IRepository<TKey, TAggregateRoot> GetRepository<TKey, TAggregateRoot>()
            where TKey : IEquatable<TKey>
            where TAggregateRoot : IAggregateRoot<TKey>
        {
            return (IRepository<TKey, TAggregateRoot>)cachedRepositories.GetOrAdd(typeof(TAggregateRoot), CreateRepository<TKey, TAggregateRoot>());
        }
    }
}
