using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Moq;
using NUnit.Framework;
using System;
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
    }
}
