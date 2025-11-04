using System;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.Menus
{
    public class UserCreationUI
    {
        private readonly Bank _bank;
        private readonly UserManagementService _userManagementService;

        public UserCreationUI(Bank bank) 
        {
            _bank = bank;
            _userManagementService = new UserManagementService(_bank);

        }

        public void AddUserMenu()
        {
            Console.WriteLine("Enter new user ID:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            User newUser;
            newUser = _userManagementService.CreateUser(id, name, password);
            Console.WriteLine($"User [{newUser.Name}] created.");
        }

        public void AddAdminMenu()
        {
            Console.WriteLine("Enter new admin ID:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            User newUser;
            newUser = _userManagementService.AddAdmin(id, name, password);
            Console.WriteLine($"Admin [{newUser}] created.");
        }
    }
}
