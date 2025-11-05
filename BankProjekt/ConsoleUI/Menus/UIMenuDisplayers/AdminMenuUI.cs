using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using BankProjekt.ConsoleUI.UIMenuHandlers;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.UIMenuDisplayers
{
    public class AdminMenuUI
    {
        private readonly Bank _bank;
        private readonly User _admin;
        private readonly AdminMenuHandler _handler;

        public AdminMenuUI(Bank bank, User admin)
        {
            _bank = bank;
            _admin = admin;
            _handler = new AdminMenuHandler(bank);
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"---- Admin Menu ({_admin.Name}) ----");
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
                    case "1": _handler.ShowAllUsers(); break;
                    case "2": _handler.HandleCreateUser(); break;
                    case "3": _handler.HandleCreateAdmin(); break;
                    case "4": _handler.ShowLargestTransactions(); break;
                    case "5": _handler.ShowBalanceSummary(); break;
                    case "6": _handler.ShowAccountWithMostTransactions(); break;
                    case "7": _handler.HandleSearchAccount(); break;
                    case "8": _handler.ShowTransactionsWithTimestamp(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

