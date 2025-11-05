using BankProjekt.Core.Accounts;
using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core.Users
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockoutEndTime { get; set; } = null;

        public List<Account> Accounts { get; set; }

        public User(string id, string name, string password = "")
        {
            Id = id;
            Name = name;
            Password = password;
            IsAdmin = false;
            Accounts = new List<Account>();
        }        

        public override string ToString()
        {
            string s = "";
            s += "-----------------------------------------------\n";
            s += "Username: " + Name;
            s += "\nUser ID: " + Id;
            s += "\nIs admin ? " + IsAdmin;
            s += "\nNumber of accounts: " + Accounts.Count;
            s += "\n-----------------------------------------------\n";

            return s;
        }
    }

}
