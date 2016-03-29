using Apworks.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Apworks.Tests
{
    public class Customer : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }

        public Name Name { get; set; }

        public int Age { get; set; }

        public string SerialNumber { get; set; }
    }

    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AgeGreaterThan30Specification : Specification<Customer>
    {
        public override Expression<Func<Customer, bool>> Expression => customer => customer.Age > 30;
    }

    public class SerialNumberEqualsSpecification : Specification<Customer>
    {
        private readonly string serialNumber;
        public SerialNumberEqualsSpecification(string serialNumber)
        {
            this.serialNumber = serialNumber;
        }

        public override Expression<Func<Customer, bool>> Expression => x => x.SerialNumber.Equals(this.serialNumber);
    }

}
