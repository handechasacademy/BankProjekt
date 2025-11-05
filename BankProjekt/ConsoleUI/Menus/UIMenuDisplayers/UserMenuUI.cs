using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.ConsoleUI.UIMenuHandlers;
using BankProjekt.Core;
using BankProjekt.Core.Users;

namespace BankProjekt.ConsoleUI.UIMenuDisplayers
{
    internal class UserMenuUI
    {
        private readonly Bank _bank;
        private readonly User _user;
        private readonly UserMenuHandler _handler;

        public UserMenuUI(Bank bank, User user)
        {
            _bank = bank;
            _user = user;
            _handler = new UserMenuHandler(user, bank);
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                //Menu
                Console.WriteLine("                  \r\n |\\/|  _  ._      \r\n |  | (/_ | | |_| \r\n                  ");
                Console.WriteLine(_user.Name);
                Console.WriteLine();
                Console.WriteLine("1. Open new account");
                Console.WriteLine("2. Manage existing account");
                Console.WriteLine("0. Logout");
                
                Console.Write("\nEnter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _handler.HandleCreateAccount();
                        break;
                    case "2":
                        var accountManagerMenu = new AccountManagerMenuUI(_user, _bank);
                        accountManagerMenu.Run();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
