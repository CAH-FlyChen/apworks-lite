using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public interface IRepositoryContext : IUnitOfWork
    {
        IRepository<TKey, TAggregateRoot> GetRepository<TKey, TAggregateRoot>()
            where TAggregateRoot : IAggregateRoot<TKey>
            where TKey : IEquatable<TKey>;
    }
}
