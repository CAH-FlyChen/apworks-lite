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
        [Test]
        public void AddExpressionToSortSpecificationTest()
        {
            var sortSpecification = new SortSpecification<Guid, Customer>();
            sortSpecification.Add(x => x.Name.FirstName, SortOrder.Ascending);
            Assert.AreEqual(1, sortSpecification.Count);
            Assert.AreEqual("Name.FirstName", sortSpecification.First().Key);
        }
    }
}
