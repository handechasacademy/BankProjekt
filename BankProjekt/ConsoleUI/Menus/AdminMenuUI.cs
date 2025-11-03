using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.ConsoleUI.Menus;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.Menus
{
    internal class AdminMenuUI
    {
        private readonly Bank _bank;
        private readonly User _admin;
        private readonly BankService _bankService;
        private readonly UserManagementService _userService;
        private readonly UserCreationUI _userCreationUI;
        private readonly FilteringAndSortingService _filteringAndSortingService;


        public AdminMenuUI(Bank bank, User admin)
        {
            _bank = bank;
            _admin = admin;
            _userService = new UserManagementService(bank);
            _userCreationUI = new UserCreationUI(_userService);
        }

        public void Run()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"---- ADMIN MENU ({_admin.Name}) ----");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Create account for user");
                Console.WriteLine("3. Create account for admin");
                Console.WriteLine("4. Show the largest deposit/withdrawal per user");
                Console.WriteLine("5. Summarize total balance per user in descending order");
                Console.WriteLine("6. Find the user with the most transactions");
                Console.WriteLine("7. Search account by account number or username");
                Console.WriteLine("8. Show transactions with timestamp and username");
                Console.WriteLine("0. Logout");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _bank.Users.ForEach(u => Console.WriteLine($"{u.Id} - {u.Name}"));
                        break;
                    case "2":
                        _userCreationUI.AddUserMenu();
                        break;
                    case "3":
                        _userCreationUI.AddAdminMenu();
                        break;
                    case "4":
                        try
                        {
                            _filteringAndSortingService.GetLargestTransactions()
                                .ForEach(Console.WriteLine);
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;
                    case "5":
                        try
                        {
                            var summaries = _bankService.GetTotalBalanceSummaries();
                            foreach (var summary in summaries)
                            {
                                Console.WriteLine($"{summary.user.Name} (ID: {summary.user.Id}) - Total Balance: {summary.totalBalance}");
                            }
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;
                    case "6":
                        try
                        {
                            var account = _filteringAndSortingService.GetAccountWithMostTransactions();
                            Console.WriteLine($"Account Number: {account.AccountNumber}");
                            Console.WriteLine($"Owner: {account.Owner.Name} (ID: {account.Owner.Id})");
                            Console.WriteLine($"Number of Transactions: {account.Transactions.Count}");
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;
                    case "7":
                        Console.Write("Enter account number or username: ");
                        string searchInput = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(_bankService.SearchAccount(searchInput));
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;
                    case "8":
                        Console.Write("Enter account number or username to search transactions: ");
                        string searchTerm = Console.ReadLine();
                        try
                        {
                            _filteringAndSortingService.SearchTransactionsWithTimestamp(searchTerm)
                                .ForEach(Console.WriteLine);
                        }
                        catch (NotFoundException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

