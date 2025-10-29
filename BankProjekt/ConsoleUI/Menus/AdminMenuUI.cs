using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.ConsoleUI.Menus
{
    internal class AdminMenuUI
    {
        private readonly Bank _bank;
        private readonly Admin _admin;

        public AdminMenuUI(Bank bank, Admin admin)
        {
            _bank = bank;
            _admin = admin;
        }

        public void Run()
        {
            AddAdmin addAdmin = new AddAdmin(_bank);
            AddUser addUser = new AddUser(_bank);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"---- ADMIN MENU ({_admin.Name}) ----");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Create account for user");
                Console.WriteLine("3. Create account for admin");
                Console.WriteLine("3. Show the largest deposit/withdrawal per user");
                Console.WriteLine("4. Summarize total balance per user in descending order");
                Console.WriteLine("5. Find the user with the most transactions");
                Console.WriteLine("6. Search account by account number or username");
                Console.WriteLine("7. Show transactions with timestamp and username");
                Console.WriteLine("0. Logout");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _bank.Users.ForEach(u => Console.WriteLine($"{u.Id} - {u.Name}"));
                        break;
                    case "2":
                        addUser.Run();
                        break;
                    case "3":
                        addAdmin.Run();
                        break;
                    case "4":
                        Console.WriteLine("Feature not implemented yet.");
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

