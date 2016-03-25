using Apworks.Repositories.EntityFramework.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
