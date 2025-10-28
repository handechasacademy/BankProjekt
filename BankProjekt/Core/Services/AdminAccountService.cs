using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    internal class AdminAccountService
    {
        private User _user;
        private Bank _bank;
        public AdminAccountService(User user, Bank bank) { _user = user; _bank = bank; }

        public User FindUserById(string id)
        {
            try
            {
                var user = _bank.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    throw new NotFoundException($"User with ID '{id}' was not found.");

                return user;
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Account OpenAccount(User user, string accountNumber)
        {
            if (user == null || string.IsNullOrWhiteSpace(accountNumber))
                throw new InvalidInputException("User or account number is invalid.");

            if (FindUserById(user.Id) == null)
                _bank.Users.Add(user);

            if (!_bank.AccountNumbers.Add(accountNumber))
                throw new DuplicateException($"Account number '{accountNumber}' already exists.");

            var account = new Account(accountNumber, 0m, user);
            _bank.Accounts[accountNumber] = account;
            (user.Accounts ??= new List<Account>()).Add(account);

            return account;
        }

        public List<User> GetAllUsers()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            return _bank.Users;
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
                                          .SelectMany(a => a.Transactions ?? new List<Transaction>())
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
