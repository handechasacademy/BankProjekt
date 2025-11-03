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

        public HashSet<User> GetAllUsers()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            return new HashSet<User>(_bank.Users);
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

        public string SearchAccount(string searchInput)
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
                        return $"User: {user.Name} (ID: {user.Id})\nAccount Number: {account.AccountNumber}";
                    }
                }
            }
            throw new NotFoundException($"No account or user found matching '{searchInput}'.");
        }

    }
}
