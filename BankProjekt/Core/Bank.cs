using BankProjekt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace BankProjekt.Core
{
    public class Bank
    {
        public List<User> Users { get; set; }
        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Users = new List<User>();
            Accounts = new List<Account>();
        }

        public void OpenAccount(User user, string accountNumber)
        {
            if (!Users.Any(u => u.Id == user.Id))
            {
                Users.Add(user);
            }

            if (Accounts.Any(a => a.AccountNumber == accountNumber))
            {
                Console.WriteLine("Account number already exists.");
                return;
            }

            var newAccount = new Account(accountNumber, 0m, user);


            Accounts.Add(newAccount);


            if (user.Accounts == null)
            {
                user.Accounts = new List<Account>();
            }
            user.Accounts.Add(newAccount);

            Console.WriteLine($"Account created {accountNumber} for user {user.Name}.");
        }

        public Account FindAccount(string accountNumber)
        {
            return Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }
    }
}

