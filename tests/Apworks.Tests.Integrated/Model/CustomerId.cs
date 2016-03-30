using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests.Integrated.Model
{
    public class CustomerId : IEquatable<CustomerId>
    {
        public int Key { get; set; }

        public bool Equals(CustomerId other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Key == other.Key;
        }
    }
}
