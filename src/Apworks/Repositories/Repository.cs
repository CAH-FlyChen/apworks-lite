using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public abstract class Repository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IRepositoryContext context;

        protected Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        public IRepositoryContext Context => this.context;

        public abstract IQueryable<TAggregateRoot> FindAll();
    }
}
