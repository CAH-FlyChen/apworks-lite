using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public abstract class Repository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IRepositoryContext context;

        protected Repository(IRepositoryContext context)
        {
            this.context = context;
        } 

        public IRepositoryContext UnitOfWork
        {
            get
            {
                return this.context;
            }
        }

        public abstract Task<IQueryable<TAggregateRoot>> FindAllAsync();
    }
}
