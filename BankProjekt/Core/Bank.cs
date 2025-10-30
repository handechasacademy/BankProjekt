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

        public HashSet<User> Users { get; set; }
        public Dictionary<string, Account> Accounts { get; set; }
        public HashSet<string> AccountNumbers { get; set; }

        public Bank ()
        {            
            Users = new HashSet<User> ();
            Accounts = new Dictionary<string, Account>();
            AccountNumbers = new HashSet<string>();
        }
    }
}
