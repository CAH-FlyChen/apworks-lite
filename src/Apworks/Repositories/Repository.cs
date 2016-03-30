using Apworks.Querying;
using Apworks.Specifications;
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

        public abstract Task<TAggregateRoot> GetByKeyAsync(TKey key);

        public abstract IQueryable<TAggregateRoot> FindAll();

        public abstract IQueryable<TAggregateRoot> FindAll(Specification<TAggregateRoot> specification, SortSpecification<TKey, TAggregateRoot> sortSpecification);

        public abstract void Add(TAggregateRoot aggregateRoot);

        public abstract void Update(TAggregateRoot aggregateRoot);

        public abstract void Remove(TAggregateRoot aggregateRoot);

        public abstract bool Exists(Specification<TAggregateRoot> specification);
    }
}
