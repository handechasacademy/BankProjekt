using BankProjekt.Core.Accounts;
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

        public decimal GetTotalBalance()
        {
            decimal output = 0;
            foreach(var acc in Accounts)
            {
                output += acc.Balance;
            }
            return output;
        }
        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> output = new List<Transaction>();
            foreach(var acc in Accounts)
            {
                output.AddRange(acc.GetTransactions());
            }
            return output;
        }

        public Account FindAccountByAccountNumber(string accountNumber)
        {
            return Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public override string ToString()
        {
            return $"User. Name: {Name}. User ID: {Id}. Password: {Password}. Is Admin: {IsAdmin}. Amount of accounts: {Accounts.Count}";
        }
    }

}
