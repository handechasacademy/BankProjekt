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

        public void HandleShowAccounts()
        {
            if (_user.Accounts.Count == 0)
            {
                Console.WriteLine("User has no accounts.");
            }
            else
            {
                foreach (var account in _user.Accounts)
                {
                    Console.WriteLine(account);
                }
            }
        }

        public void HandleCreateAccount()
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Currency (SEK, USD, EUR) [default: SEK]: ");
            string currency = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(currency)) currency = "SEK";

            Console.Write("Account type (Checking/Savings): ");
            string accountType = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("\nInvalid account number.");
                return;
            }

            if (currency != "SEK" && currency != "USD" && currency != "EUR")
            {
                Console.WriteLine("\nInvalid currency. Use SEK, USD, or EUR.");
                return;
            }

            if (accountType != "Checking" && accountType != "Savings")
            {
                Console.WriteLine("\nInvalid account type. Use 'Checking' or 'Savings'.");
                return;
            }

            try
            {
                var newAccount = _userService.OpenAccount(_bank, _user, accountNumber, currency, accountType);
                Console.WriteLine($"\nAccount created successfully!");
                Console.WriteLine("\n" + newAccount);
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
