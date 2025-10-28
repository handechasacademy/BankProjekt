
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

        static void HandleDeposit(Account account)
        {
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                account.Deposit(amount);
                Console.WriteLine($"Successfully deposited {amount:C}. New balance is {account.Balance:C}.");
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid amount.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void HandleWithdraw(Account account)
        {
            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0 && amount <= account.Balance)
            {
                account.Withdraw(amount);
                Console.WriteLine($"Successfully withdrew {amount:C}. New balance is {account.Balance:C}.");
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid amount.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}