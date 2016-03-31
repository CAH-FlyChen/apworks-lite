using Apworks.Tests.Integrated.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests.Integrated.EntityFramework
{
    public class DatabaseInitializationStrategy : DropCreateDatabaseAlways<TestDbContext>
    {
        protected override void Seed(TestDbContext context)
        {
            var customerA = new Customer { Name = "Sunny Chen", Email = "sunny.chen@pki.com", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now, Age = 20 };
            var customerB = new Customer { Name = "Doris Min", Email = "doris.min@pki.com", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now, Age = 20 };
            var customerC = new Customer { Name = "David Li", Email = "david.li@pki.com", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now, Age = 30 };
            var customerD = new Customer { Name = "Hartman Mu", Email = "hartman.mu@pki.com", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now, Age = 30 };
            var customerE = new Customer { Name = "Unknown Person", Email = "unknown.person@pki.com", DateCreated = DateTime.Now, DateOfBirth = DateTime.Now, Age = 30 };
            context.Customers.Add(customerA);
            context.Customers.Add(customerB);
            context.Customers.Add(customerC);
            context.Customers.Add(customerD);
            context.Customers.Add(customerE);
            base.Seed(context);
        }
    }
}
