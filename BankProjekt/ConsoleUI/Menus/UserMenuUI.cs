using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.MenuUI
{
    internal class UserMenuUI
    {
        private readonly Bank _bank;
        private readonly User _user;
        private readonly UserService _userService;

        public UserMenuUI(Bank bank, User user)
        {
            _bank = bank;
            _user = user;
            _userService = new UserService(_user);
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
                        Console.WriteLine("Enter an account number to create.");
                        string accountNumber = Console.ReadLine();
                        try
                        {
                            _userService.OpenAccount(_bank, _user, accountNumber);
                        }
                        catch (InvalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine($"Account created with account number {accountNumber}");
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

        