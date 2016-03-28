using Apworks.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Apworks.Tests
{
    internal class Customer : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }

        public Name Name { get; set; }

        public int Age { get; set; }
    }

    internal class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class AgeGreaterThan30Specification : Specification<Customer>
    {
        public override Expression<Func<Customer, bool>> Expression => _ => _.Age > 30;
    }
}
