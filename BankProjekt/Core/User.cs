using BankProjekt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } // Add this
        public string Role { get; set; } // Add this, e.g., "admin" or "user"

        public List<Account> Accounts { get; set; }

        public User(string id, string name, string password = "", string role = "user")
        {
            Id = id;
            Name = name;
            Password = password;
            Role = role;
            Accounts = new List<Account>();
        }
    }

}
