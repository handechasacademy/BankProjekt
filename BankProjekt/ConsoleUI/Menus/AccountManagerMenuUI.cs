using BankProjekt.ConsoleUI.Menus;
using BankProjekt.ConsoleUI.ServiceUI;
using BankProjekt.Core;
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
            AccountSelectorUI accountSelector = new AccountSelectorUI(_user);
            InternalTransferUI transferUI = new InternalTransferUI(_bank, _user);

            Console.Clear();
            
            accountSelector.Run();

            while (true) 
            {
                Console.Clear();
                Console.WriteLine($"---- ACCOUNT MENU: {accountSelector._account} ----");
                Console.WriteLine($"Current Balance: {accountSelector._account.Balance}");
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
                        accountSelector._account.ShowTransactionHistory();
                        break;
                    case "0":
                        return;
                    case "3":
                        transferUI.Run(_bank, accountSelector._account);
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
