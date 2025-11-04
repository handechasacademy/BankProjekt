using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core.Services
{
    public class AccountFilteringService
    {
        private readonly List<User> _users;

        public AccountFilteringService(List<User> users)
        {
            _users = users;
        }

        public List<Transaction> GetLastNTransactions(Account account, int n)
        {
            if (account == null || account.Transactions == null)
            {
                Console.WriteLine("No account or transactions to show.");
                return new List<Transaction>();
            }
            else
            {
                return account.Transactions.OrderByDescending(t => t.Timestamp)
                                           .Take(n)
                                           .ToList();
            }
        }

        public List<Transaction> GetDeposits(Account account)
        {
            if (account == null || account.Transactions == null)
            {
                Console.WriteLine("No account or transactions to show.");
                return new List<Transaction>();
            }
            else
            {
                return account.Transactions
                              .Where(t => t.Amount > 0)
                              .ToList();
            }                
        }

        public List<Transaction> GetWithdrawals(Account account)
        {
            if (account == null || account.Transactions == null)
            {
                Console.WriteLine("No account or transactions to show.");
                return new List<Transaction>();
            }
            else
            {
                return account.Transactions
                              .Where(t => t.Amount < 0)
                              .ToList();
            }                
        }

        public List<Transaction> GetTransfers(Account account)
        {
            if (account == null || account.Transactions == null)
            {
                Console.WriteLine("No account or transactions to show.");
                return new List<Transaction>();
            }
            else
            {
                return account.Transactions
                              .Where(t => t.Type.Equals("Transfer", StringComparison.OrdinalIgnoreCase))
                              .ToList();
            }                
        }      

    }

}
