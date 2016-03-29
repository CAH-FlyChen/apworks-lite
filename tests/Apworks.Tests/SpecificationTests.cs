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
            new Customer { Id = Guid.NewGuid(), Age = 10 , SerialNumber = "C001"},
            new Customer { Id = Guid.NewGuid(), Age = 20 , SerialNumber = "A001" },
            new Customer { Id = Guid.NewGuid(), Age = 30 , SerialNumber = "A001"},
            new Customer { Id = Guid.NewGuid(), Age = 35 , SerialNumber = "B001"},
            new Customer { Id = Guid.NewGuid(), Age = 40 , SerialNumber = "C001"},
            new Customer { Id = Guid.NewGuid(), Age = 50 , SerialNumber = "C001"},
            new Customer { Id = Guid.NewGuid(), Age = 55 , SerialNumber = "C001"}
        };

        [Test]
        public void AnySpecificationTest()
        {
            var anySpecification = new AnySpecification<Customer>();
            var query = customers.Where(anySpecification.IsSatisfiedBy);
            Assert.AreEqual(7, query.Count());
        }

        [Test]
        public void NotSpecificationTest()
        {
            var notSpecification = new NotSpecification<Customer>(new AgeGreaterThan30Specification());
            var query = customers.Where(notSpecification.IsSatisfiedBy);
            Assert.AreEqual(3, query.Count());
        }

        [Test]
        public void AndSpecificationTest()
        {
            var specification = new AgeGreaterThan30Specification().And(new SerialNumberEqualsSpecification("B001"));
            var query = customers.Where(specification.IsSatisfiedBy);
            Assert.AreEqual(1, query.Count());
        }

        [Test]
        public void AndNotSpecificationTest()
        {
            var specification = new AgeGreaterThan30Specification().AndNot(new SerialNumberEqualsSpecification("B001"));
            var query = customers.Where(specification.IsSatisfiedBy);
            Assert.AreEqual(3, query.Count());
        }

        [Test]
        public void ExpressionSpecificationTest()
        {
            Expression<Func<Customer, bool>> expr = x => x.SerialNumber == "C001";
            var specification = new ExpressionSpecification<Customer>(expr);
            var query = customers.Where(specification.IsSatisfiedBy);
            Assert.AreEqual(4, query.Count());
        }

        [Test]
        public void NoneSpecificationTest()
        {
            var specification = new NoneSpecification<Customer>();
            var query = customers.Where(specification.IsSatisfiedBy);
            Assert.AreEqual(0, query.Count());
        }

        [Test]
        public void OrSpecificationTest()
        {
            var specification = new AgeGreaterThan30Specification().Or(new SerialNumberEqualsSpecification("A001"));
            var query = customers.Where(specification.IsSatisfiedBy);
            Assert.AreEqual(6, query.Count());
        }

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
