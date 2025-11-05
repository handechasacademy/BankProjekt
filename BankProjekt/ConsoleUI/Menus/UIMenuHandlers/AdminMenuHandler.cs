using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.ConsoleUI.UIMenuDisplayers;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.UIMenuHandlers
{
    public class AdminMenuHandler
    {
        private readonly Bank _bank;
        private readonly BankService _bankService;
        private readonly BankFilteringService _filteringService;
        private readonly UserCreationUI _userCreationUI;

        public AdminMenuHandler(Bank bank)
        {
            _bank = bank;
            _bankService = new BankService(_bank);
            _filteringService = new BankFilteringService(_bank);
            _userCreationUI = new UserCreationUI(_bank);
        }

        public void HandleCreateUser()
        {
            try
            {
                _userCreationUI.AddUserMenu();
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void HandleShowAllAccounts()
        {
            if (_bank.Accounts.Count == 0)
            {
                Console.WriteLine("No accounts found");
            }
            else
            {
                foreach (var account in _bank.Accounts)
                {
                    Console.WriteLine(account);
                }
            }
        }
        public void HandleCreateAdmin()
        {
            try
            {
                _userCreationUI.AddAdminMenu();
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowAllUsers()
        {
            if (_bank.Users.Count == 0)
            {
                Console.WriteLine("No users found.");
            }
            else
            {
                Console.WriteLine($"{"ID",-10} {"Name",-20} {"Admin",-10} {"Accounts",-10}");
                Console.WriteLine(new string('-', 50));
                foreach (var user in _bank.Users)
                {
                    Console.WriteLine($"{user.Id,-10} {user.Name,-20} {(user.IsAdmin ? "Yes" : "No"),-10} {user.Accounts?.Count ?? 0,-10}");
                }
            }
        }

        public void HandleSearchAccount()
        {
            Console.Write("Enter account number or username: ");
            string searchInput = Console.ReadLine();

            try
            {
                var result = _bankService.SearchAccount(searchInput);
                Console.WriteLine(result);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowLargestTransactions()
        {
            try
            {
                var results = _filteringService.GetLargestTransactions();

                if (results.Count == 0)
                {
                    Console.WriteLine("No transactions found.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowBalanceSummary()
        {
            try
            {
                var summaries = _bankService.GetTotalBalanceSummaries();

                Console.WriteLine($"{"User Name",-20} {"User ID",-10} {"Total Balance",-15}");
                Console.WriteLine(new string('-', 45));

                if (summaries.Count == 0)
                {
                    Console.WriteLine("No data available.");
                }
                else
                {
                    foreach (var summary in summaries)
                    {
                        Console.WriteLine($"{summary.user.Name,-20} {summary.user.Id,-10} {summary.totalBalance,-15:C}");
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowAccountWithMostTransactions()
        {
            try
            {
                var account = _filteringService.GetAccountWithMostTransactions();

                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Owner: {account.Owner.Name} (ID: {account.Owner.Id})");
                Console.WriteLine($"Type: {account.AccountType}");
                Console.WriteLine($"Balance: {account.Balance:C} {account.Currency}");
                Console.WriteLine($"Transaction Count: {account.Transactions.Count}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowTransactionsWithTimestamp()
        {
            Console.Write("Enter account number or username to search transactions: ");
            string searchTerm = Console.ReadLine();

            try
            {
                var results = _filteringService.SearchTransactionsWithTimestamp(searchTerm);

                if (results.Count == 0)
                {
                    Console.WriteLine("\nNo transactions found.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
