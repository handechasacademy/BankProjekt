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
            //Manage Account
            Console.WriteLine("                                                      \r\n |\\/|  _. ._   _.  _   _     /\\   _  _  _      ._ _|_ \r\n |  | (_| | | (_| (_| (/_   /--\\ (_ (_ (_) |_| | | |_ \r\n                   _|                                 ");
            Console.Write("Account number: ");
            string accountNum = Console.ReadLine();
            var account = finder.FindAccountByAccountNumber(_user, accountNum);

            while (true)
            {
                Console.Clear();
                //Manage Account
                Console.WriteLine("                                                      \r\n |\\/|  _. ._   _.  _   _     /\\   _  _  _      ._ _|_ \r\n |  | (_| | | (_| (_| (/_   /--\\ (_ (_ (_) |_| | | |_ \r\n                   _|                                 ");
                Console.WriteLine(account.AccountNumber);
                Console.WriteLine($"Balance: {account.Balance} {account.Currency}");
                Console.WriteLine();
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Transfer funds to another user");
                Console.WriteLine("4. Transfer funds to another account");
                Console.WriteLine("5. Transaction history");
                Console.WriteLine("6. Llast N transactions");
                Console.WriteLine("7. Deposits");
                Console.WriteLine("8. Withdrawals");
                Console.WriteLine("9. Transfers");
                Console.WriteLine("10. Total balance");
                Console.WriteLine("0. Return");

                Console.Write("\nChoice: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("");
                        _handler.HandleDeposit(account); 
                        break;
                    case "2":
                        _handler.HandleWithdrawal(account); 
                        break;
                    case "3": 
                        _handler.HandleTransferToUser(account); 
                        break;
                    case "4": 
                        _handler.HandleInternalTransfer(account); 
                        break;
                    case "5":
                        _handler.ViewTransactionHistory(account);
                        break;
                    case "6":
                        _handler.ViewLastNTransactions(account);
                        break;
                    case "7":
                        _handler.ViewDeposits(account);
                        break;
                    case "8":
                        _handler.ViewWithdrawals(account);
                        break;
                    case "9":
                        _handler.ViewTransfers(account);
                        break;
                    case "10":
                        _handler.ViewTotalBalance();
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
