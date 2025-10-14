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

        public void OpenAccount(User user, string accountNumber)
        {
            if (FindUserById(user.Id) == null)
            {
                Users.Add(user);
            }

            if (!AccountNumbers.Add(accountNumber))
            {
                Console.WriteLine("Account number already exists.");
                return;
            }

            var newAccount = new Account(accountNumber, 0m, user);

            Accounts[accountNumber] = newAccount;

            if (user.Accounts == null)
            {
                user.Accounts = new List<Account>();
            }
            user.Accounts.Add(newAccount);
            
            Console.WriteLine($"Account created {accountNumber} for user {user.Name}.");
        }

        
        public User FindUserById(string id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }                   
    }
}
