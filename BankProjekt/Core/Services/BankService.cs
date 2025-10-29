using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    public class BankService
    {
        private Bank _bank;

        public BankService(Bank bank) { _bank = bank; }

        public User FindUserById(string id)
        {
            var user = _bank.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new NotFoundException($"User with ID '{id}' was not found.");
            return user;
        }

        public void TransferFundsBetweenUsers(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(fromAccountNumber) || string.IsNullOrWhiteSpace(toAccountNumber))
                throw new InvalidInputException("Account number cannot be empty.");

            if (amount <= 0)
                throw new InvalidInputException("Transfer amount must be positive.");

            var fromAccount = _bank.Accounts.GetValueOrDefault(fromAccountNumber);
            var toAccount = _bank.Accounts.GetValueOrDefault(toAccountNumber);

            if (fromAccount == null)
                throw new NotFoundException($"Source account '{fromAccountNumber}' not found.");
            if (toAccount == null)
                throw new NotFoundException($"Destination account '{toAccountNumber}' not found.");

            if (fromAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds in the source account.");

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
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

        public List<User> GetAllUsers()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            return _bank.Users;
        }

        public List<(User user, decimal totalBalance)> GetTotalBalanceSummaries()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            var results = new List<(User user, decimal totalBalance)>();

            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null)
                    throw new NotFoundException($"No accounts found for user '{user.Name}' (ID: {user.Id}).");

                decimal total = user.Accounts.Sum(a => a.Balance);
                results.Add((user, total));
            }

            return results;
        }

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

        public (User user, Account account) SearchAccount(string searchInput)
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null)
                    continue;

                foreach (var account in user.Accounts)
                {
                    if (account.AccountNumber.Contains(searchInput, StringComparison.OrdinalIgnoreCase) ||
                        user.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase))
                    {
                        return (user, account);
                    }
                }
            }

            throw new NotFoundException($"No account or user found matching '{searchInput}'.");
        }

        //  USER NEEDS TO DECIDE IF THEY WANNA SORT BY AMOUNT OR DATE WHICH HAS TO BE IN UI
        public List<(DateTime timestamp, string userName, string accountNumber, decimal amount)> SearchTransactionsWithTimestamp(string searchInput)
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

                    if (account.AccountNumber.Contains(searchInput, StringComparison.OrdinalIgnoreCase) ||
                        user.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var t in account.Transactions)
                        {
                            matches.Add((t.Timestamp, user.Name, account.AccountNumber, t.Amount));
                        }
                    }
                }
            }

            if (matches.Count == 0)
                throw new NotFoundException($"No transactions found matching '{searchInput}'.");

            return matches;
        }

    }
}
