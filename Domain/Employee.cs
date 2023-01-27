using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.Domain
{
    public sealed class Employee
    {
        public string Name { get; set; }
        public long Document { get; set; }
        public User User { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime LastAccess { get; set; } = default;

        public Employee(string name, long document)
        {
            Name = name;
            Document = document;
        }
        public Employee(string name, long document, User user)
        {
            Name = name;
            Document = document;
            User = user;
        }
    }
}
