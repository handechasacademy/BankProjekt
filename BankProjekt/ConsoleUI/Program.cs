
using BankProjekt.ConsoleUI.Menus;
using BankProjekt.ConsoleUI.MenuUI;
using BankProjekt.ConsoleUI;
using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using BankProjekt.Core.Services;
using System;
using System.Security.Principal;

namespace BankProjekt.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            var loginService = new LoginService(bank.Users);
            var loginUI = new LoginUI(loginService);

            while (true)
            {
                Console.WriteLine("----BANK----");
                User loggedInUser = loginUI.Run();


                if (loggedInUser == null)
                {
                    return;
                }
                if (loggedInUser.IsAdmin)
                {
                    var adminMenu = new AdminMenuUI(bank, loggedInUser);
                    adminMenu.Run();
                }
                else if (loggedInUser.IsAdmin == false)
                {
                    var userMenu = new UserMenuUI(bank, loggedInUser);
                    userMenu.Run();
                }
            }
        }
    }
}