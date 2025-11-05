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
            var account = _handler.GetValidatedAccount(accountNum);
            if (account == null) //had to add this if statement otherwise they get stuck in the loop
                return;

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
                Console.WriteLine("4. Internal transfer");
                Console.WriteLine("5. Transaction history");
                Console.WriteLine("6. Last N transactions");
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
                        Console.Clear();
                        //Deposit
                        Console.WriteLine("  _                       \r\n | \\  _  ._   _   _ o _|_ \r\n |_/ (/_ |_) (_) _> |  |_ \r\n         |                ");
                        _handler.HandleDeposit(account); 
                        break;
                    case "2":
                        Console.Clear();
                        //Withdraw
                        Console.WriteLine("                                 \r\n \\    / o _|_ |_   _| ._ _.      \r\n  \\/\\/  |  |_ | | (_| | (_| \\/\\/ \r\n                                 ");
                        _handler.HandleWithdrawal(account); 
                        break;
                    case "3":
                        Console.Clear();
                        //Transfer funds
                        Console.WriteLine(" ___               _           _               \r\n  | ._ _. ._   _ _|_ _  ._   _|_    ._   _|  _ \r\n  | | (_| | | _>  | (/_ |     | |_| | | (_| _> \r\n                                               ");
                        Console.WriteLine("To another user\n");
                        _handler.HandleTransferToUser(account); 
                        break;
                    case "4":
                        Console.Clear();
                        //Transfer funds
                        Console.WriteLine(" ___               _           _               \r\n  | ._ _. ._   _ _|_ _  ._   _|_    ._   _|  _ \r\n  | | (_| | | _>  | (/_ |     | |_| | | (_| _> \r\n                                               ");
                        Console.WriteLine("Internal\n");
                        _handler.HandleInternalTransfer(account); 
                        break;
                    case "5":
                        Console.Clear();
                        //Transaction History
                        Console.WriteLine(" ___                                                          \r\n  | ._ _. ._   _  _.  _ _|_ o  _  ._    |_| o  _ _|_  _  ._   \r\n  | | (_| | | _> (_| (_  |_ | (_) | |   | | | _>  |_ (_) | \\/ \r\n                                                           /  ");
                        _handler.ViewTransactionHistory(account);
                        break;
                    case "6":
                        Console.Clear();
                        //Transaction History
                        Console.WriteLine(" ___                                                          \r\n  | ._ _. ._   _  _.  _ _|_ o  _  ._    |_| o  _ _|_  _  ._   \r\n  | | (_| | | _> (_| (_  |_ | (_) | |   | | | _>  |_ (_) | \\/ \r\n                                                           /  ");
                        Console.WriteLine("Choose number of transactions to show");
                        _handler.ViewLastNTransactions(account);
                        break;
                    case "7":
                        Console.Clear();
                        //Deposits
                        Console.WriteLine("  _                          \r\n | \\  _  ._   _   _ o _|_  _ \r\n |_/ (/_ |_) (_) _> |  |_ _> \r\n         |                   ");
                        Console.WriteLine("View deposits\n");
                        _handler.ViewDeposits(account);
                        break;
                    case "8":
                        Console.Clear();
                        //Withdrawals
                        Console.WriteLine("                                          \r\n \\    / o _|_ |_   _| ._ _.       _. |  _ \r\n  \\/\\/  |  |_ | | (_| | (_| \\/\\/ (_| | _> \r\n                                          ");
                        Console.WriteLine("View withdrawals\n");
                        _handler.ViewWithdrawals(account);
                        break;
                    case "9":
                        Console.Clear();
                        //Transfers
                        Console.WriteLine(" ___               _         \r\n  | ._ _. ._   _ _|_ _  ._ _ \r\n  | | (_| | | _>  | (/_ | _> \r\n                             ");
                        Console.WriteLine("View transfers\n");
                        _handler.ViewTransfers(account);
                        break;
                    case "10":
                        Console.Clear();
                        //Total Balance
                        Console.WriteLine(" ___                _                       \r\n  |  _ _|_  _. |   |_)  _. |  _. ._   _  _  \r\n  | (_) |_ (_| |   |_) (_| | (_| | | (_ (/_ \r\n                                            ");
                        Console.WriteLine("Total balance across all accounts\n");
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
