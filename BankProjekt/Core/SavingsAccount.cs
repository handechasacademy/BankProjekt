using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal balance, User owner) : base(accountNumber, balance, owner)
        {
        }

        public override void Withdraw(decimal amount)
        {
            if (amount > 0 && (amount*1.01m) <= Balance)
            {
                Balance -= (amount*1.01m);
                Transactions.Add(new Transaction(Owner.Id, AccountNumber, (amount*1.01m), DateTime.Now, "Withdraw"));
                Console.WriteLine($"Transferring {amount*1.01m} ({amount*0.01m} fee)");
            }
        }
    }
}
