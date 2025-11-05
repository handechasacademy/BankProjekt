
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
            BankApplication application = new BankApplication();
            application.Run();
        }
    }
}