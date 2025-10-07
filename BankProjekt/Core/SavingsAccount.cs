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
            Console.WriteLine($"Transferring {amount * 1.01m} ({amount * 0.01m} fee)");
            base.Withdraw(amount*1.01m);
        }
    }
}
