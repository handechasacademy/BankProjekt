using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Text.Json;

namespace BankProjekt.Core
{
    public class  Bank
    {

        public List<User> Users { get; set; }
        public Dictionary<string, Account> Accounts { get; set; }
        public HashSet<string> AccountNumbers { get; set; }

        public Bank ()
        {            
            Users = new List<User> ();
            Accounts = new Dictionary<string, Account>();
            AccountNumbers = new HashSet<string>();

            var admin1 = new User("Vivienne", "Vivienne123", "1234");
            admin1.IsAdmin = true;
            Users.Add(admin1);

            var admin2 = new User("Hande", "Hande123", "1234");
            admin2.IsAdmin = true;
            Users.Add(admin2);

            var admin3 = new User("Sepideh", "Sepideh123", "1234");
            admin3.IsAdmin = true;
            Users.Add(admin3);

            var user1 = new User("id1", "name1", "1234");
            Users.Add(user1);

            var user2 = new User("id2", "name2", "1234");
            Users.Add(user2);

        }
    }
}
