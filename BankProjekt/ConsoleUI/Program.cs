
using System;
using System.Security.Principal;
using BankProjekt.ConsoleUI;
using BankProjekt.ConsoleUI.UIMenuDisplayers;
using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;

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
                Console.WriteLine("---- BANK ----");
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
                else if(loggedInUser.IsAdmin == false)
                {
                    var userMenu = new UserMenuUI(bank, loggedInUser);
                    userMenu.Run();
                }
            }
        }
    }
}