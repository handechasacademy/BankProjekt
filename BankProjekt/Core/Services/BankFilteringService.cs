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
    public class BankFilteringService
    {
        private readonly Bank _bank;

        public BankFilteringService(Bank bank) { _bank = bank; }

        public Account GetAccountWithMostTransactions()
        {
            var account = _bank.Accounts.Values
                .Where(a => a.Transactions != null)
                .OrderByDescending(a => a.Transactions.Count)
                .FirstOrDefault();

            if (account == null)
                throw new NotFoundException("No accounts with transactions found.");

            return account;
        }

        public List<string> SearchTransactionsWithTimestamp(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty.", nameof(searchTerm));

            var summaries = new List<string>();
            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null || user.Accounts.Count == 0)
                    continue;
                foreach (var account in user.Accounts)
                {
                    if (account.Transactions == null)
                        continue;
                    if (account.AccountNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        user.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var t in account.Transactions)
                        {
                            summaries.Add($"{t.Timestamp:G} | User: {user.Name} | Account: {account.AccountNumber} | Amount: {t.Amount}");
                        }
                    }
                }
            }
            if (summaries.Count == 0)
                throw new NotFoundException($"No transactions found matching '{searchTerm}'.");
            return summaries;
        }

        public List<string> GetLargestTransactions()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            var summary = new List<string>();
            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null)
                    continue;
                var allTransactions = user.Accounts.SelectMany(a => a.Transactions)
                                                   .ToList();
                var largestDeposit = allTransactions.Where(t => t.Amount > 0)
                                                    .OrderByDescending(t => t.Amount)
                                                    .FirstOrDefault();
                var largestWithdrawal = allTransactions.Where(t => t.Amount < 0)
                                                       .OrderBy(t => t.Amount)
                                                       .FirstOrDefault();

                summary.Add($"{user.Name} - Largest Deposit: {largestDeposit?.Amount ?? 0}, Largest Withdrawal: {largestWithdrawal?.Amount ?? 0}");
            }
            return summary;
        }
    }
    }
}
