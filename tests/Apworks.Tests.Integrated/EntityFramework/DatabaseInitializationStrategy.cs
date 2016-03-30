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
            var customer = new Customer { Name = "Sunny Chen", Email = "sunny.chen@pki.com", DateCreated = DateTime.Now }; 
            base.Seed(context);
        }
    }
}
