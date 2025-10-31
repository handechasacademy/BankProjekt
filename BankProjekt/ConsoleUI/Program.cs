using BankProjekt.Core;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;

namespace BankProjekt.ConsoleUI
{
    internal class Program
    {
        private static ILoginService _loginService;

        static void Main(string[] args)
        {
            Bank bank = new Bank();

            // Create users for login
            var users = new List<User>
            {
                new User("1", "Sam", "1234"),
                new User("2", "Sara", "abcd")
            };

            _loginService = new LoginService(users);

            AdminMenuUI adminMenu;
            UserMenuUI userMenu;

            Console.WriteLine("---- BANK SYSTEM ----");

            User LoggedInUser = Login(bank);

            if (!LoggedInUser.IsAdmin)
            {
                userMenu = new UserMenuUI(bank, LoggedInUser);
                userMenu.Run();
            }
            else
            {
                adminMenu = new AdminMenuUI(bank, LoggedInUser);
                adminMenu.Run();
            }
        }

        private static User Login(Bank bank)
        {
            while (true)
            {
                Console.WriteLine("\n---- USER LOGIN ----");
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                try
                {
                    User user = _loginService.Login(username, password);
                    Console.WriteLine($"Welcome {user.Name}!");
                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

