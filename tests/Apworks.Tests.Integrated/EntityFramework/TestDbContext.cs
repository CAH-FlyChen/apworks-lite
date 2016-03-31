using Apworks.Tests.Integrated.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests.Integrated.EntityFramework
{
    public class TestDbContext : DbContext
    {
        public TestDbContext() : base("Data Source=localhost; Initial Catalog=TestDbContext; Integrated Security=True; Connect Timeout=120; MultipleActiveResultSets=True")
        { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

    }
}
