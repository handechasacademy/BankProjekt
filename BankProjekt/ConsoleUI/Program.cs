
using BankProjekt.ConsoleUI.Menus;
using BankProjekt.ConsoleUI.MenuUI;
using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;
using System.Security.Principal;

namespace BankProjekt.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            AdminMenuUI adminMenu;
            UserMenuUI userMenu;

            Console.WriteLine("----BANK----");

            User LoggedInUser = Login(bank);

            if (LoggedInUser.IsAdmin == false)
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
    }
}