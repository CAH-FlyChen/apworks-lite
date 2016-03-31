using Apworks.Repositories.EntityFramework;
using Apworks.Specifications;
using Apworks.Tests.Integrated.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests.Integrated.EntityFramework
{
    [TestFixture]
    public class EntityFrameworkRepositoryTests
    {
        [SetUp]
        public static void Setup()
        {
            Database.SetInitializer<TestDbContext>(new DatabaseInitializationStrategy());
            new TestDbContext().Database.Initialize(true);
        }

        [Test, Category("Integrated_EntityFrameworkRepository")]
        public void GetRepositoryTest()
        {
            using (var context = new TestDbContext())
            {
                var entityFrameworkContext = new EntityFrameworkRepositoryContext(context);
                var repository = entityFrameworkContext.GetRepository<int, Customer>();
                Assert.IsNotNull(repository);
            }
        }

        [Test, Category("Integrated_EntityFrameworkRepository")]
        public void GetAllCustomersTest()
        {
            using (var context = new TestDbContext())
            {
                var entityFrameworkContext = new EntityFrameworkRepositoryContext(context);
                var repository = entityFrameworkContext.GetRepository<int, Customer>();
                var query = repository.FindAll();
                Assert.AreEqual(5, query.Count());
            }
        }

        [Test, Category("Integrated_EntityFrameworkRepository")]
        public async Task GetByKeyTest()
        {
            using (var context = new TestDbContext())
            {
                var entityFrameworkContext = new EntityFrameworkRepositoryContext(context);
                var repository = entityFrameworkContext.GetRepository<int, Customer>();
                var query = await repository.GetByKeyAsync(2);
                Assert.IsNotNull(query);
            }
        }

        [Test, Category("Integrated_EntityFrameworkRepository")]
        public void GetBySimpleSpecificationTest()
        {
            using (var context = new TestDbContext())
            {
                var entityFrameworkContext = new EntityFrameworkRepositoryContext(context);
                var repository = entityFrameworkContext.GetRepository<int, Customer>();
                var specification = new ExpressionSpecification<Customer>(x => x.Age == 20);
                var query = repository.FindAll(specification);
                Assert.AreEqual(2, query.Count());
            }
        }

        [Test, Category("Integrated_EntityFrameworkRepository")]
        public void GetByComposedSpecificationTest()
        {
            using (var context = new TestDbContext())
            {
                var entityFrameworkContext = new EntityFrameworkRepositoryContext(context);
                var repository = entityFrameworkContext.GetRepository<int, Customer>();
                var specification = new ExpressionSpecification<Customer>(x => x.Age == 20).Or(new ExpressionSpecification<Customer>(x => x.Name == "Hartman Mu"));
                var query = repository.FindAll(specification);
                Assert.AreEqual(3, query.Count());
            }
        }
    }
}
