using BankProjekt.ConsoleUI.Menus;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.ConsoleUI.MenuUI
{
    internal class AccountManagerMenuUI
    {
        private readonly User _user;
        private readonly Bank _bank;
        public AccountManagerMenuUI(User user, Bank bank) 
        {
            _user = user;
            _bank = bank;
        }

        public void Run() 
        {
            var finder = new FinderService(_bank);

            Console.Clear();

            Console.Write("Account number ? ");
            string accountNum = Console.ReadLine();
            var account = finder.FindAccountByAccountNumber(_user, accountNum);

            while (true) 
            {
                Console.Clear();
                Console.WriteLine($"---- ACCOUNT MENU: {account} ----");
                Console.WriteLine($"Current Balance: {account.Balance}");
                Console.WriteLine("1. Deposit funds");
                Console.WriteLine("2. Withdraw funds");
                Console.WriteLine("3. Transfer funds");
                Console.WriteLine("4. View transaction history");
                Console.WriteLine("0. Return to main menu");

                Console.Write("Enter your choice: ");
                string option = Console.ReadLine();

                Console.WriteLine();

                switch (option) 
                {
                    case "4":
                        account.ShowTransactionHistory();
                        break;
                    case "0":
                        return;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}
