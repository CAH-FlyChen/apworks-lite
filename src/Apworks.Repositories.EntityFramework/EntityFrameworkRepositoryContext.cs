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
        private bool disposed;

        public EntityFrameworkRepositoryContext(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal DbContext DbContext => this.dbContext;

        protected override IRepository<TKey, TAggregateRoot> CreateRepository<TKey, TAggregateRoot>()
        {
            return new EntityFrameworkRepository<TKey, TAggregateRoot>(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed && this.dbContext != null)
                {
                    this.dbContext.Dispose();
                    this.disposed = true;
                }
            }
        }

        public override async Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
