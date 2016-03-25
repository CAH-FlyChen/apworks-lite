using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Repositories
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        IRepository<TKey, TAggregateRoot> GetRepository<TKey, TAggregateRoot>()
            where TAggregateRoot : class, IAggregateRoot<TKey>
            where TKey : IEquatable<TKey>;
    }
}
