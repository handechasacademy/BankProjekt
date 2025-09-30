using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Core
{
    internal class User
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Account> Accounts { get; set; }

        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}