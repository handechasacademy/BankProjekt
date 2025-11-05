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
                //Admin
                Console.WriteLine("                      \r\n  /\\   _| ._ _  o ._  \r\n /--\\ (_| | | | | | | \r\n                      ");
                Console.WriteLine(_admin.Name);
                Console.WriteLine();
                Console.WriteLine("1. Users");
                Console.WriteLine("2. Create user");
                Console.WriteLine("3. Create admin");
                Console.WriteLine("4. Largest deposit/withdrawal per user");
                Console.WriteLine("5. Summarize total balance per user in descending order");
                Console.WriteLine("6. User with most transactions");
                Console.WriteLine("7. Search account by account number");
                Console.WriteLine("8. All accounts");
                Console.WriteLine("9. Transactions with timestamp and username");
                Console.WriteLine("0. Logout");

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        //"Users"
                        Console.WriteLine("                 \r\n | |  _  _  ._ _ \r\n |_| _> (/_ | _> \r\n                 ");
                        _handler.ShowAllUsers(); 
                        break;
                    case "2":
                        Console.Clear();
                        //Create User
                        Console.WriteLine("  _                                   \r\n /  ._ _   _. _|_  _    | |  _  _  ._ \r\n \\_ | (/_ (_|  |_ (/_   |_| _> (/_ |  \r\n                                      ");
                        _handler.HandleCreateUser();
                        break;
                    case "3":
                        Console.Clear();
                        //Create Admin
                        Console.WriteLine("  _                                          \r\n /  ._ _   _. _|_  _     /\\   _| ._ _  o ._  \r\n \\_ | (/_ (_|  |_ (/_   /--\\ (_| | | | | | | \r\n                                             ");
                        _handler.HandleCreateAdmin(); 
                        break;
                    case "4": 
                        Console.Clear();
                        //Largest Transactions
                        Console.WriteLine("                           ___                                     \r\n |   _. ._ _   _   _ _|_    | ._ _. ._   _  _.  _ _|_ o  _  ._   _ \r\n |_ (_| | (_| (/_ _>  |_    | | (_| | | _> (_| (_  |_ | (_) | | _> \r\n           _|                                                      ");
                        _handler.ShowLargestTransactions();
                        break;
                    case "5":
                        Console.Clear();
                        //Balance Summary
                        Console.WriteLine("  _                          __                          \r\n |_)  _. |  _. ._   _  _    (_      ._ _  ._ _   _. ._   \r\n |_) (_| | (_| | | (_ (/_   __) |_| | | | | | | (_| | \\/ \r\n                                                      /  ");
                        _handler.ShowBalanceSummary(); 
                        break;
                    case "6": 
                        Console.Clear();
                        //Account With Most Transactions
                        Console.WriteLine("                                                                  ___                                     \r\n  /\\   _  _  _      ._ _|_   \\    / o _|_ |_    |\\/|  _   _ _|_    | ._ _. ._   _  _.  _ _|_ o  _  ._   _ \r\n /--\\ (_ (_ (_) |_| | | |_    \\/\\/  |  |_ | |   |  | (_) _>  |_    | | (_| | | _> (_| (_  |_ | (_) | | _> \r\n                                                                                                          ");
                        _handler.ShowAccountWithMostTransactions();
                        break;
                    case "7":
                        Console.Clear();
                        //Account Search
                        Console.WriteLine("                              __                  \r\n  /\\   _  _  _      ._ _|_   (_   _   _. ._ _ |_  \r\n /--\\ (_ (_ (_) |_| | | |_   __) (/_ (_| | (_ | | \r\n                                                  ");
                        _handler.HandleSearchAccount();
                        break;
                    case "8":
                        Console.Clear();
                        //All Accounts
                        Console.WriteLine("                                         \r\n  /\\  | |    /\\   _  _  _      ._ _|_  _ \r\n /--\\ | |   /--\\ (_ (_ (_) |_| | | |_ _> \r\n                                         ");
                        _handler.HandleShowAllAccounts();
                        break;
                    case "9":
                        Console.Clear();
                        //Transactions With Timestamp
                        Console.WriteLine(" ___                                                          ___                                 \r\n  | ._ _. ._   _  _.  _ _|_ o  _  ._   _   \\    / o _|_ |_     | o ._ _   _   _ _|_  _. ._ _  ._  \r\n  | | (_| | | _> (_| (_  |_ | (_) | | _>    \\/\\/  |  |_ | |    | | | | | (/_ _>  |_ (_| | | | |_) \r\n                                                                                              |   ");
                        _handler.ShowTransactionsWithTimestamp();
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

