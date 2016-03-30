using Apworks.Querying;
using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Apworks.Specifications;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests
{
    [TestFixture]
    public class EntityFrameworkRepositoryTests
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

        private DbSet<Customer> CreateMockCustomerSet()
        {
            var customerDataSet = customers.AsQueryable();
            var mockCustomerSet = new Mock<DbSet<Customer>>();
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerDataSet.Provider);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerDataSet.Expression);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerDataSet.ElementType);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(() => customerDataSet.GetEnumerator());
            return mockCustomerSet.Object;
        }

        [Test]
        public void RepositoryInitializationTest()
        {
            var mockRepositoryContext = new Mock<IRepositoryContext>();
            Assert.Throws<RepositoryException>(() => new EntityFrameworkRepository<Guid, Customer>(mockRepositoryContext.Object));
        }

        [Test]
        public void GetRepositoryTest()
        {
            var dbContext = new Mock<DbContext>();
            var repositoryContext = new EntityFrameworkRepositoryContext(dbContext.Object);
            var repository = repositoryContext.GetRepository<Guid, Customer>();
            Assert.IsNotNull(repository);
        }

        [Test]
        public void CheckCachedRepositoryCountTest()
        {
            var dbContext = new Mock<DbContext>();
            var repositoryContext = new EntityFrameworkRepositoryContext(dbContext.Object);
            var repository = repositoryContext.GetRepository<Guid, Customer>();
            Assert.AreEqual(1, repositoryContext.CachedRepositories.Count());
        }

        [Test]
        public void GetRepositoryMultiThreadingTest()
        {
            var dbContext = new Mock<DbContext>();
            var repositoryContext = new EntityFrameworkRepositoryContext(dbContext.Object);
            var taskCount = 1000;
            var taskList = new List<Task>();
            for (var i = 0; i < taskCount; i++)
            {
                taskList.Add(Task.Run(() => repositoryContext.GetRepository<Guid, Customer>()));
            }
            Task.WaitAll(taskList.ToArray());
            Assert.AreEqual(1, repositoryContext.CachedRepositories.Count());
        }

        [Test]
        public void GetRepositoryMultiThreadingRepositoryInstanceTest()
        {
            var dbContext = new Mock<DbContext>();
            var repositoryContext = new EntityFrameworkRepositoryContext(dbContext.Object);
            var bag = new ConcurrentBag<IRepository<Guid, Customer>>();
            var taskCount = 1000;
            var taskList = new List<Task>();
            for (var i = 0; i < taskCount; i++)
            {
                taskList.Add(Task.Run(() => bag.Add(repositoryContext.GetRepository<Guid, Customer>())));
            }
            Task.WaitAll(taskList.ToArray());
            Assert.AreEqual(1000, bag.Count); // Ensure that every thread
            Assert.IsTrue(bag.All(x => x != null));
            Assert.IsTrue(bag.Distinct().Count() == 1);
        }

        [Test]
        public void FindAllTest()
        {
            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(x => x.Set<Customer>()).Returns(CreateMockCustomerSet());
            var repositoryContext = new EntityFrameworkRepositoryContext(mockDbContext.Object);
            var repository = repositoryContext.GetRepository<Guid, Customer>();
            var query = repository.FindAll();
            Assert.AreEqual(7, query.Count());
        }

        [Test]
        public void FindAllWithSortDescendingTest()
        {
            var sortSpecification = new SortSpecification<Guid, Customer> { { c => c.Age, SortOrder.Descending } };
            var querySpecification = new AnySpecification<Customer>();
            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(x => x.Set<Customer>()).Returns(CreateMockCustomerSet());
            var repositoryContext = new EntityFrameworkRepositoryContext(mockDbContext.Object);
            var repository = repositoryContext.GetRepository<Guid, Customer>();
            var query = repository.FindAll(querySpecification, sortSpecification);
            Assert.AreEqual(7, query.Count());
            Assert.AreEqual(55, query.First().Age);
            Assert.AreEqual(10, query.Last().Age);
        }
    }
}
