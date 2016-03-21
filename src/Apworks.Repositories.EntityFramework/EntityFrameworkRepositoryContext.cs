using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories.EntityFramework
{
    internal sealed class EntityFrameworkRepositoryContext : RepositoryContext
    {
        private readonly DbContext dbContext;

        public EntityFrameworkRepositoryContext(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal DbContext DbContext => this.dbContext;

        public override async Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public override IRepository<TKey, TAggregateRoot> GetRepository<TKey, TAggregateRoot>()
        {
            return new EntityFrameworkRepository<TKey, TAggregateRoot>(this);
        }
    }
}
