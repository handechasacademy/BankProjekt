using BankProjekt.Core;
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
        }        
        
        public User FindUserById(string id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }                   
    }
}
