using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.UIMenuHandlers
{
    internal class UserMenuHandler
    {
        private readonly User _user;
        private readonly Bank _bank;
        private readonly UserService _userService;

        public UserMenuHandler(User user, Bank bank)
        {
            _user = user;
            _bank = bank;
            _userService = new UserService(_user);
        }

        public void HandleCreateAccount()
        {
            Console.WriteLine("\n---- Create New Account ----");

            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter currency (SEK, USD, EUR) [default: SEK]: ");
            string currency = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(currency)) currency = "SEK";

            Console.Write("Select account type (Checking/Savings): ");
            string accountType = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            if (currency != "SEK" && currency != "USD" && currency != "EUR")
            {
                Console.WriteLine("Invalid currency. Use SEK, USD, or EUR.");
                return;
            }

            if (accountType != "Checking" && accountType != "Savings")
            {
                Console.WriteLine("Invalid account type. Use 'Checking' or 'Savings'.");
                return;
            }

            try
            {
                _userService.OpenAccount(_bank, _user, accountNumber, currency, accountType);
                Console.WriteLine($"Account created successfully!");
                Console.WriteLine($"  Account Number: {accountNumber}");
                Console.WriteLine($"  Currency: {currency}");
                Console.WriteLine($"  Type: {accountType}");
                Console.WriteLine($"  Initial Balance: 0.00");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DuplicateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
