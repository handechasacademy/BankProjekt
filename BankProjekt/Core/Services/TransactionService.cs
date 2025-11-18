using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    internal class TransactionService
    {
        private readonly Bank _bank;

        public TransactionService(Bank bank)
        {
            _bank = bank;
        }

        public List<Transaction> UserGetAllTransactions(User user) //user might have 2 accounts
        {
            if (user.Accounts == null)
                throw new NotFoundException("User has no accounts.");
            return user.Accounts
                .SelectMany(acc => acc.GetTransactions())
                .ToList();
        }
        public List<Transaction> GetTransactionHistory(Account account) //for one account only
        {
            if (account == null)
                throw new NotFoundException("Account not found.");
            return account.GetTransactions();
        }
    }
}
