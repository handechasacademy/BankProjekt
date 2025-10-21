

namespace BankProjekt.Core
{
    public class Admin : Bank
    {
        public Admin() : base() { }

        public void OpenAccount(User user, string accountNumber)
        {
            if (FindUserById(user.Id) == null)
            {
                Users.Add(user);
            }

            if (!AccountNumbers.Add(accountNumber))
            {
                Console.WriteLine("Account number already exists.");
                return;
            }

            var newAccount = new Account(accountNumber, 0m, user);

            Accounts[accountNumber] = newAccount;

            if (user.Accounts == null)
            {
                user.Accounts = new List<Account>();
            }
            user.Accounts.Add(newAccount);

            Console.WriteLine($"Account created {accountNumber} for user {user.Name}.");
        }

        public void ShowAllUsers()
        {
            if (Users == null)
            {
                Console.WriteLine("No users found.");
                return;
            }

            Console.WriteLine("All Users:");
            foreach (var user in Users)
            {
                Console.WriteLine($"Name: {user.Name}, ID: {user.Id}");
            }
        }              

        public void ShowLargestTransaction()
        {
            var userTransactions = Users
                .Select(user => new
                {
                    User = user,
                    LargestDeposit = user.Accounts
                        .SelectMany(a => a.Transactions)
                        .Where(t => t.Amount > 0)
                        .OrderByDescending(t => t.Amount)
                        .FirstOrDefault(),
                    LargestWithdrawal = user.Accounts
                        .SelectMany(a => a.Transactions)
                        .Where(t => t.Amount < 0)
                        .OrderBy(t => t.Amount)
                        .FirstOrDefault()
                })
                .Where(x => x.LargestDeposit != null || x.LargestWithdrawal != null);

            foreach (var transaction in userTransactions)
            {
                Console.WriteLine($"User: {transaction.User.Name} (ID: {transaction.User.Id})");

                if (transaction.LargestDeposit != null)
                {
                    Console.WriteLine($"Largest deposit: {transaction.LargestDeposit.Amount:C} - {transaction.LargestDeposit.Timestamp}");
                }

                if (transaction.LargestWithdrawal != null)
                {
                    Console.WriteLine($"Largest withdraw: {transaction.LargestWithdrawal.Amount:C} - {transaction.LargestWithdrawal.Timestamp}");
                }
            }
        }


        public void TotalBalanceSummary()
        {
            var balanceSummaries = Users.Select(user => new{User = user, Balance = user.Accounts.OrderByDescending(d => d.Balance)});

            foreach (var balance in balanceSummaries)
            {
                if (balance != null)
                {
                    Console.WriteLine($"Balance for {balance.User.Name} : {balance.Balance} ");
                }
                else
                {
                    Console.WriteLine("No account found to show balance for.");
                }
            }
        }

        
        public void AccountWithMostTransactions()
        {
            var mostActiveAccounts = Users.Where(u => u.Accounts != null && u.Accounts.Any())
                                          .Select(user => new{User = user, Account = user.Accounts
                                          .OrderByDescending(a => a.Transactions.Count)
                                          .FirstOrDefault()});
            foreach (var entry in mostActiveAccounts)
            {
                if (entry.Account != null)
                {
                    Console.WriteLine($"User: {entry.User.Name}, User ID: {entry.User.Id},Account Number: {entry.Account.AccountNumber} Transactions: {entry.Account.Transactions.Count}");
                }
                else
                {
                    Console.WriteLine($"User: {entry.User.Name} has no accounts.");
                }
            }
        }
        
        public void SearchAccount(string searchInput)
        {
            Console.WriteLine("User\t\tAccount Number\tBalance");
            Console.WriteLine("------------------------------------------");

            foreach (var user in Users)
            {
                if (user.Accounts.Count == 0)
                {
                    Console.WriteLine($"User {user.Name} has no accounts.");
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
                            Console.WriteLine($"Account {account.AccountNumber} for user {user.Name} does not match '{searchInput}'.");
                        }
                    }
                }
            }
        }


        public void SearchTransactionsWithTimestamp(string searchInput)
        {
            Console.WriteLine("Timestamp\t\tUser\tAccount Number\tAmount");
            Console.WriteLine("---------------------------------------------------------");

            foreach (var user in Users)
            {
                if (user.Accounts.Count == 0)
                {
                    Console.WriteLine("There are no accounts yet.");
                }
                else 
                { 
                    foreach (var account in user.Accounts)
                    {
                        if (account.Transactions.Count == 0)
                        {
                            Console.WriteLine("There are transactions to show yet.");
                        }
                        else 
                        { 
                            if (!account.AccountNumber.Contains(searchInput) && !user.Name.Contains(searchInput))
                            {
                                Console.WriteLine($"No transactions found for '{searchInput}' in user {user.Name} or account {account.AccountNumber}.");
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


    }
}
