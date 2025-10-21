using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core.Accounts
{
    internal class CheckingAccount : Account
    {
        public int AllowedDebt = -5000;
        public CheckingAccount(string accountNumber, decimal balance, User owner) : base(accountNumber, balance, owner){}

        public override void Withdraw(decimal amount)
        {
            if(Balance - amount < AllowedDebt) //can go into debt
            {
                Console.WriteLine($"Your debt is greater than {AllowedDebt}. Withdrawal failed.");
            }
            else { base.Withdraw(amount); }
        }
        
       
    }
}
