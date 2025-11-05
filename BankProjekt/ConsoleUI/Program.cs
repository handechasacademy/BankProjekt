
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
            BankApplication application = new BankApplication();
            application.Run();
        }
    }
}