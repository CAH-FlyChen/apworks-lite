using Apworks.Repositories.EntityFramework.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apworks.Specifications;

namespace Apworks.Repositories.EntityFramework
{
    internal sealed class EntityFrameworkRepository<TKey, TAggregateRoot> : Repository<TKey, TAggregateRoot>
        where TKey : IEquatable<TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        private readonly DbContext dbContext;

        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            this.dbContext = (context as EntityFrameworkRepositoryContext)?.DbContext;
            if (this.dbContext == null)
            {
                throw new RepositoryException(Resources.InvalidDbContextInstance);
            }
        }

        public override IQueryable<TAggregateRoot> FindAll()
        {
            return this.dbContext.Set<TAggregateRoot>();
        }

        public override void Add(TAggregateRoot aggregateRoot)
        {
            this.dbContext.Entry(aggregateRoot).State = EntityState.Added;
        }

        public override void Update(TAggregateRoot aggregateRoot)
        {
            this.dbContext.Entry(aggregateRoot).State = EntityState.Modified;
        }

        public override void Remove(TAggregateRoot aggregateRoot)
        {
            this.dbContext.Entry(aggregateRoot).State = EntityState.Deleted;
        }

        public override bool Exists(Specification<TAggregateRoot> specification)
        {
            return this.dbContext.Set<TAggregateRoot>().Any(specification);
        }

    }
}
