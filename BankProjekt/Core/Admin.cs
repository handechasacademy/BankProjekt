using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core;

namespace BankProjekt.Core
{
    public class Admin : Bank
    {
        public List<User> Users { get; set; }
        public Dictionary<string, Account> Accounts { get; set; }
        public HashSet<string> AccountNumbers { get; set; }

        public Admin() : base() { }

        public void ShowLargestTransaction()
        {           
            var userTransactions = Users
                .Select(user => new
                {
                    User = user,
                    LargestDeposit = user.Accounts
                        .SelectMany(a => a.Transactions)
                        .Where(t => t.Amount > 0)
                        .OrderByDescending(t => t.Amount)
                        .FirstOrDefault(),
                    LargestWithdrawal = user.Accounts
                        .SelectMany(a=> a.Transactions)
                        .Where(t => t.Amount < 0)
                        .OrderBy(t => t.Amount)
                        .FirstOrDefault()
                })
                .Where(x => x.LargestDeposit != null || x.LargestWithdrawal != null);

            foreach (var transaction in userTransactions)
            {
                Console.WriteLine($"uSER: {transaction.User.Name} (ID: {transaction.User.Id})");

                if (transaction.LargestDeposit != null)
                {
                    Console.WriteLine($"Largest deposit: {transaction.LargestDeposit.Amount:C} - {transaction.LargestDeposit.Timestamp}");
                }

                if (transaction.LargestWithdrawal != null)
                {
                    Console.WriteLine($"Largest withdraw: {transaction.LargestWithdrawal.Amount:C} - {transaction.LargestWithdrawal.Timestamp}");
                }
            }
        }

        //Summera total saldo per användare och skriv ut i fallande ordning.

        public void TotalBalanceSummary()
        {
            var balanceSummaries = Users.Select(user => new
                                {
                                    User = user,
                                    Balance = user.Accounts
                                    .OrderByDescending(d => d.Balance)
                                });

            foreach (var balance in balanceSummaries)
            {
                if (balance != null)
                {
                    Console.WriteLine($"Balance for {balance.User.Name} : {balance.Balance} ");
                }
                else
                {
                    Console.WriteLine("No account found to show balance for.");
                }
            }
        }
        

    }
}
