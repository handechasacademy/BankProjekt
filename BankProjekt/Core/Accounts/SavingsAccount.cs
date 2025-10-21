using BankProjekt.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core.Accounts
{
    public class SavingsAccount : Account
    {
        private int _withdrawalCounter = 0;
        public SavingsAccount(string accountNumber, decimal balance, User owner) : base(accountNumber, balance, owner, BankConfig.GetFeePercentage){}

        public override void Withdraw(decimal amount)
        {
            if(_withdrawalCounter > 3)
            {
                base.Withdraw(amount-amount*BankConfig.GetFeePercentage);
                _withdrawalCounter++;
            }
            else
            {
                base.Withdraw(amount);
            }
            
        }
    }
}
