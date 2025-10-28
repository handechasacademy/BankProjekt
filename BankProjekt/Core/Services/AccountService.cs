using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    internal class AccountService
    {
        private User _user;
        private Bank _bank;
        public AccountService(User user, Bank bank) { _user = user; _bank = bank; }

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

        public void OpenAccount(User user, string accountNumber)
        {
            try
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

                Console.WriteLine($" Account '{accountNumber}' created for {user.Name}.");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowAllUser()
        {
            try
            {
                if (!_bank.Users.Any())
                {
                    throw new NotFoundException("No users found.");
                }

                Console.WriteLine("Users in the bank:");
                Console.WriteLine("-------------------");

                foreach (var user in _bank.Users)
                {
                    Console.WriteLine($"Name: {user.Name}, ID: {user.Id}, Accounts: {user.Accounts.Count}");
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void ShowLargestTransaction()
        {
            try
            {
                foreach (var user in _bank.Users)
                {
                    if (user.Accounts == null)
                        throw new NotFoundException($"No accounts found for user '{user.Name}' (ID: {user.Id}).");

                    var allTransactions = user.Accounts
                                              .SelectMany(a => a.Transactions)
                                              .ToList();

                    if (allTransactions == null)
                        throw new NotFoundException($"No transactions found for user '{user.Name}' (ID: {user.Id}).");

                    var largestDeposit = allTransactions.Where(t => t.Amount > 0)
                                                        .OrderByDescending(t => t.Amount)
                                                        .FirstOrDefault();

                    var largestWithdrawal = allTransactions.Where(t => t.Amount < 0)
                                                           .OrderBy(t => t.Amount)
                                                           .FirstOrDefault();

                    Console.WriteLine($"User: {user.Name} (ID: {user.Id})");

                    if (largestDeposit != null)
                        Console.WriteLine($"Largest deposit: {largestDeposit.Amount:C} - {largestDeposit.Timestamp}");

                    if (largestWithdrawal != null)
                        Console.WriteLine($"Largest withdraw: {largestWithdrawal.Amount:C} - {largestWithdrawal.Timestamp}");
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void TotalBalanceSummary()
        {
            try
            {
                var balanceSummaries = _bank.Users.Select(user => new { User = user, Balance = user.Accounts.OrderByDescending(d => d.Balance) });

                foreach (var balance in balanceSummaries)
                {
                    if (balance != null)
                    {
                        Console.WriteLine($"Balance for {balance.User.Name} : {balance.Balance} ");
                    }
                    else
                    {
                        throw new NotFoundException("No account found to show balance for.");
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountWithMostTransactions()
        {
            try
            {
                var mostActiveAccounts = _bank.Users.Where(u => u.Accounts != null)
                                              .Select(user => new
                                              {
                                                  User = user,
                                                  Account = user.Accounts
                                              .OrderByDescending(a => a.Transactions.Count)
                                              .FirstOrDefault()
                                              });
                foreach (var entry in mostActiveAccounts)
                {
                    if (entry.Account != null)
                    {
                        Console.WriteLine($"User: {entry.User.Name}, User ID: {entry.User.Id},Account Number: {entry.Account.AccountNumber} Transactions: {entry.Account.Transactions.Count}");
                    }
                    else
                    {
                        throw new NotFoundException($"User: {entry.User.Name} has no accounts.");
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SearchAccount(string searchInput)
        {
            Console.WriteLine("User\t\tAccount Number\tBalance");
            Console.WriteLine("------------------------------------------");

            try
            {
                foreach (var user in _bank.Users)
                {
                    if (user.Accounts.Count == 0)
                    {
                        throw new NotFoundException($"User {user.Name} has no accounts.");
                    }
                    else
                    {
                        foreach (var account in user.Accounts)
                        {
                            if (account.AccountNumber.Contains(searchInput) || user.Name.Contains(searchInput))
                            {
                                Console.WriteLine($"{user.Name}\t{account.AccountNumber}\t{account.Balance:C}");
                            }
                            else
                            {
                                throw new NotFoundException($"Account {account.AccountNumber} for user {user.Name} does not match '{searchInput}'.");
                            }
                        }
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void SearchTransactionsWithTimestamp(string searchInput)
        {
            Console.WriteLine("Timestamp\t\tUser\tAccount Number\tAmount");
            Console.WriteLine("---------------------------------------------------------");
            try
            {
                foreach (var user in _bank.Users)
                {
                    if (user.Accounts.Count == 0)
                    {
                        throw new NotFoundException("There are no accounts yet.");
                    }
                    else
                    {
                        foreach (var account in user.Accounts)
                        {
                            if (account.Transactions.Count == 0)
                            {
                                throw new NotFoundException("There are no transactions to show yet.");
                            }
                            else
                            {
                                if (!account.AccountNumber.Contains(searchInput) && !user.Name.Contains(searchInput))
                                {
                                    throw new NotFoundException($"No transactions found for '{searchInput}' in user {user.Name} or account {account.AccountNumber}.");
                                }
                                else
                                {
                                    foreach (var t in account.Transactions)
                                    {
                                        Console.WriteLine($"{t.Timestamp}\t{user.Name}\t{account.AccountNumber}\t{t.Amount:C}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
