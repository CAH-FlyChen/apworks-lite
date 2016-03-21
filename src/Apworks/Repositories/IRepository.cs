using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public interface IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        IRepositoryContext UnitOfWork { get; }

        Task<IQueryable<TAggregateRoot>> FindAllAsync();
    }
}
