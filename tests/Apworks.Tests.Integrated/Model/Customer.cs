using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Tests.Integrated.Model
{
    public class Customer : IAggregateRoot<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastLogin { get; set; }
    }
}
