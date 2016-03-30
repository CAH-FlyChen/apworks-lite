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
        public DbSet<Customer> Customers { get; set; }

        
    }
}
