using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core
{
    internal class CheckingAccount : Account
    {
        public CheckingAccount(string accountNumber, decimal balance, User owner) : base(accountNumber, balance, owner)
        {
        }

        public override void Withdraw(decimal amount)
        {
            base.Withdraw(amount);
        }
    }
}
