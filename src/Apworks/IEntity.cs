using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks
{
    /// <summary>
    /// Represents that the implemented classes are the entities in a domain.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity key, which should be able to compare with another key
    /// with the same type.</typeparam>
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
