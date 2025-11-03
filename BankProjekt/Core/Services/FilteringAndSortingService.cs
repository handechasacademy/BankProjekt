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
    public class FilteringAndSortingService
    {
        private readonly Bank _bank;

        public FilteringAndSortingService(Bank bank) { _bank = bank; }

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

        //  USER NEEDS TO DECIDE IF THEY WANNA SORT BY AMOUNT OR DATE WHICH HAS TO BE IN UI
        public List<(DateTime timestamp, string userName, string accountNumber, decimal amount)> SearchTransactionsWithTimestamp(string searchTerm)
        {
            var matches = new List<(DateTime, string, string, decimal)>();

            foreach (var user in _bank.Users)
            {
                if (user.Accounts.Count == 0)
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
                            matches.Add((t.Timestamp, user.Name, account.AccountNumber, t.Amount));
                        }
                    }
                }
            }

            if (matches.Count == 0)
                throw new NotFoundException($"No transactions found matching '{searchTerm}'.");

            return matches;
        }

        public List<(User user, Transaction largestDeposit, Transaction largestWithdrawal)> GetLargestTransactions()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            var results = new List<(User user, Transaction largestDeposit, Transaction largestWithdrawal)>();

            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null)
                    throw new NotFoundException($"No accounts found for user '{user.Name}' (ID: {user.Id}).");

                var allTransactions = user.Accounts
                                          .SelectMany(a => a.Transactions)
                                          .ToList();

                if (allTransactions.Count == 0)
                    throw new NotFoundException($"No transactions found for user '{user.Name}' (ID: {user.Id}).");

                var largestDeposit = allTransactions.Where(t => t.Amount > 0)
                                                    .OrderByDescending(t => t.Amount)
                                                    .FirstOrDefault();

                var largestWithdrawal = allTransactions.Where(t => t.Amount < 0)
                                                       .OrderBy(t => t.Amount)
                                                       .FirstOrDefault();

                results.Add((user, largestDeposit, largestWithdrawal));
            }
            return results;
        }
    }
}
