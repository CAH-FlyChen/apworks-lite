using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks
{
    public interface IUnitOfWork
    {
        Guid Id { get; }

        Task CommitAsync();
    }
}
