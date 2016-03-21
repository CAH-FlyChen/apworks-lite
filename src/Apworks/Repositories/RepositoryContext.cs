using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public abstract class RepositoryContext : IRepositoryContext
    {
        private readonly Guid id;

        public Guid Id => this.id;

        public abstract Task CommitAsync();

        public abstract IRepository<TKey, TAggregateRoot> GetRepository<TKey, TAggregateRoot>()
            where TKey : IEquatable<TKey>
            where TAggregateRoot : IAggregateRoot<TKey>;
    }
}
