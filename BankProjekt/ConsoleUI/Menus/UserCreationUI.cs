using System;
using BankProjekt.Core.Services;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.Menus
{
    public class UserCreationUI
    {
        private readonly UserManagementService _userService;

        public UserCreationUI(UserManagementService userService)
        {
            _userService = userService;
        }

        public void AddUserMenu()
        {
            Console.WriteLine("Enter new user ID:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            try
            {
                _userService.CreateUser(id, name, password);
                Console.WriteLine("User created.");
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void AddAdminMenu()
        {
            Console.WriteLine("Enter new admin ID:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            try
            {
                _userService.AddAdmin(id, name, password);
                Console.WriteLine("Admin created.");
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
