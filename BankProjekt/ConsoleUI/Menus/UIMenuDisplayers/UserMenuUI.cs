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
                Console.WriteLine($"---- User Menu ({_user.Name}) ----");
                Console.WriteLine("1. Open new account");
                Console.WriteLine("2. Manage existing account");
                Console.WriteLine("0. Logout");

                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _handler.HandleCreateAccount();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        var accountManagerMenu = new AccountManagerMenuUI(_user, _bank);
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
