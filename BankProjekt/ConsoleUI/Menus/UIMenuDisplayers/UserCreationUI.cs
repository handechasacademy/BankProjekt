using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.UIMenuDisplayers
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
            Console.Write("User ID: ");
            string id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("User ID cannot be empty.");
                return;
            }

            Console.Write("Username: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            try
            {
                var newUser = _userManagementService.CreateUser(id, name, password);
                Console.Clear();
                Console.WriteLine($"\nUser [{newUser.Name}] created successfully.");
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddAdminMenu()
        {
            Console.Write("Admin ID: ");
            string id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Admin ID cannot be empty.");
                return;
            }

            Console.Write("Username: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            try
            {
                Console.Clear();
                var newAdmin = _userManagementService.AddAdmin(id, name, password);
                Console.WriteLine($"Admin [{newAdmin.Name}] created successfully.");
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

