using Apworks.Querying;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests
{
    [TestFixture]
    public class QueryingTests
    {
        private readonly List<Customer> customers = new List<Customer> {
            new Customer { Id = Guid.NewGuid(), Age = 20 },
            new Customer { Id = Guid.NewGuid(), Age = 30 },
            new Customer { Id = Guid.NewGuid(), Age = 35 },
            new Customer { Id = Guid.NewGuid(), Age = 40 },
            new Customer { Id = Guid.NewGuid(), Age = 10 }
        };

        [Test]
        public void AddExpressionToSortSpecificationTest()
        {
            var sortSpecification = new SortSpecification<Guid, Customer>();
            sortSpecification.Add(x => x.Name.FirstName, SortOrder.Ascending);
            Assert.AreEqual(1, sortSpecification.Count);
            Assert.AreEqual("Name.FirstName", sortSpecification.First().Key);
        }

        [Test]
        public void AddAsExpressionAndGetExpressionFromSortSpecificationTest()
        {
            var sortSpecification = new SortSpecification<Guid, Customer>();
            sortSpecification.Add(x => x.Name.LastName, SortOrder.Descending);
            var expression = sortSpecification.Specifications.First().Item1;
            Assert.AreEqual(1, expression.Parameters.Count);
            Assert.AreEqual(typeof(Customer), expression.Parameters[0].Type);
        }

        [Test]
        public void AddPropertyNameToSortSpecificationTest()
        {
            var sortSpecification = new SortSpecification<Guid, Customer>();
            sortSpecification.Add("Id", SortOrder.Ascending);
            var expression = sortSpecification.Specifications.First().Item1;
            Assert.AreEqual(1, sortSpecification.Count);
            Assert.AreEqual(typeof(Customer), expression.Parameters[0].Type);
        }

        [Test]
        public void ListSortWithSortSpecificationTest()
        {
            var sortSpecification = new SortSpecification<Guid, Customer>();
            sortSpecification.Add("Age", SortOrder.Ascending);

            var query = customers.AsQueryable().OrderBy(sortSpecification.Specifications.First().Item1);
            Assert.AreEqual(10, query.First().Age);
        }
    }
}
