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
        public bool IsAdmin { get; set; } 

        public List<Account> Accounts { get; set; }

        public User(string id, string name, string password = "")
        {
            Id = id;
            Name = name;
            Password = password;
            IsAdmin = false;
            Accounts = new List<Account>();
        }
    }

}
