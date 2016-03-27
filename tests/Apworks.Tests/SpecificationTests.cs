using Apworks.Specifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests
{
    [TestFixture]
    public class SpecificationTests
    {
        private readonly List<Customer> customers = new List<Customer> {
            new Customer { Id = Guid.NewGuid(), Age = 20 },
            new Customer { Id = Guid.NewGuid(), Age = 30 },
            new Customer { Id = Guid.NewGuid(), Age = 35 },
            new Customer { Id = Guid.NewGuid(), Age = 40 },
            new Customer { Id = Guid.NewGuid(), Age = 10 }
        };

        [Test]
        public void ConvertFromSpecificationToLambdaTest()
        {
            var specification = new AgeGreaterThan30Specification();
            Expression<Func<Customer, bool>> expression = specification;
            var query = customers.Where(specification.IsSatisfiedBy);
            var query2 = customers.Where(expression.Compile());
            Assert.IsNotNull(query);
            Assert.IsNotNull(query2);
            Assert.AreEqual(query.Count(), query2.Count());
        }
    }
    
}
