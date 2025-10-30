using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.ConsoleUI.MenuUI
{
    internal class UserMenuUI
    {
        private readonly Bank _bank;
        private readonly User _user;

        public UserMenuUI(Bank bank, User user)
        {
            _bank = bank;
            _user = user;
        }

        public void Run()
        {
            AccountManagerMenuUI accountManagerMenu = new AccountManagerMenuUI(_user, _bank);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"---- USER MENU ({_user.Name}) ----");
                Console.WriteLine("1. Open new account");
                Console.WriteLine("2. Manage existing account");
                Console.WriteLine("0. Logout");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        break;
                    case "2":
                        accountManagerMenu.Run();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}

        