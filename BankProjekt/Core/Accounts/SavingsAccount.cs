using BankProjekt.Config;
using BankProjekt.Core.Users;
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
        public SavingsAccount(string accountNumber, decimal balance, User owner, decimal feePercentage = 0.0m, string currency = "SEK") : base(accountNumber, balance, owner, "Savings", feePercentage, currency) {}

        public override decimal Withdraw(decimal withdrawAmount)
        {
            if (_withdrawalCounter > 3)
            {
                decimal fee = withdrawAmount * BankConfig.GetFeePercentage;
                base.Withdraw(withdrawAmount + fee);
                Console.WriteLine($"\nFee of {fee:C} applied (withdrawal #{_withdrawalCounter + 1}).");
            }
            else
            {
                base.Withdraw(withdrawAmount);
                Console.WriteLine($"\nFree withdrawal #{_withdrawalCounter + 1} of 3.");
            }

            _withdrawalCounter++;
            return Balance;
        }
    }
}
