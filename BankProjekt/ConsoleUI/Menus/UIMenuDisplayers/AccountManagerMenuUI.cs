using System;
using BankProjekt.ConsoleUI.UIMenuHandlers;
using BankProjekt.Core;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;

namespace BankProjekt.ConsoleUI.UIMenuDisplayers
{
    public class AccountManagerMenuUI
    {
        private readonly User _user;
        private readonly Bank _bank;
        private readonly AccountManagerMenuHandler _handler;

        public AccountManagerMenuUI(User user, Bank bank)
        {
            _user = user;
            _bank = bank;
            _handler = new AccountManagerMenuHandler(user, bank);
        }

        public void Run()
        {
            var finder = new FinderService(_bank);

            Console.Clear();
            Console.Write("Account number? ");
            string accountNum = Console.ReadLine();
            var account = finder.FindAccountByAccountNumber(_user, accountNum);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"---- Account Menu: {account.AccountNumber} ----");
                Console.WriteLine($"Current Balance: {account.Balance} {account.Currency}");
                Console.WriteLine("1. Deposit funds");
                Console.WriteLine("2. Withdraw funds");
                Console.WriteLine("3. Transfer funds to another user");
                Console.WriteLine("4. Transfer funds to another account");
                Console.WriteLine("5. View transaction history");
                Console.WriteLine("6. View last N transactions");
                Console.WriteLine("7. View all deposits");
                Console.WriteLine("8. View all withdrawals");
                Console.WriteLine("9. View all transfers");
                Console.WriteLine("10. Get total balance across all accounts");
                Console.WriteLine("0. Return to main menu");

                Console.Write("Enter your choice: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1": _handler.HandleDeposit(account); break;
                    case "2": _handler.HandleWithdrawal(account); break;
                    case "3": _handler.HandleTransferToUser(account); break;
                    case "4": _handler.HandleInternalTransfer(account); break;
                    case "5": _handler.ViewTransactionHistory(account); break;
                    case "6": _handler.ViewLastNTransactions(account); break;
                    case "7": _handler.ViewDeposits(account); break;
                    case "8": _handler.ViewWithdrawals(account); break;
                    case "9": _handler.ViewTransfers(account); break;
                    case "10": _handler.ViewTotalBalance(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
