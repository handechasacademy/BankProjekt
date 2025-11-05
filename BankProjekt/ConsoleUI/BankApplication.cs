using BankProjekt.ConsoleUI.Menus;
using BankProjekt.ConsoleUI.MenuUI;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.ConsoleUI
{
    internal class BankApplication
    {
        private readonly Bank _bank;
        private readonly LoginService _loginService;
        private readonly LoginUI _loginUI;

        public BankApplication()
        {
            _bank = new Bank();
            _loginService = new LoginService(_bank.Users);
            _loginUI = new LoginUI(_loginService);
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("----BANK----");
                User loggedInUser = _loginUI.Run();

                if (loggedInUser == null)
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                RunUserSession(loggedInUser);
            }
        }
        
        private void RunUserSession(User loggedInUser)
        {
            if (loggedInUser.IsAdmin)
            {
                var adminMenu = new AdminMenuUI(_bank, loggedInUser);
                adminMenu.Run();
            }
            else if (loggedInUser.IsAdmin == false)
            {
                var userMenu = new UserMenuUI(_bank, loggedInUser);
                userMenu.Run();
            }
        }
    }
}
