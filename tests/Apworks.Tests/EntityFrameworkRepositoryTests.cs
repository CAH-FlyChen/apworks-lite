using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
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
        internal class Customer : IAggregateRoot<Guid>
        {
            public Guid Id { get; set; }
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
    }
}
