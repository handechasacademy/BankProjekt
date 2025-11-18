using System;
using BankProjekt.Config;
using BankProjekt.Core.Users;

namespace BankProjekt.Core.Accounts
{
    internal class CheckingAccount : Account
    {
        public decimal AllowedDebt => -Math.Abs(Balance) * 5m; //Took some AI help here i could not think of this by myself -Hande
        public decimal InterestRate;

        public CheckingAccount(string accountNumber, decimal balance, User owner, decimal feePercentage = 0.0m, string currency = "SEK")
        : base(accountNumber, balance, owner, "Checking", feePercentage, currency)
        {
            InterestRate = BankConfig.InterestRate;
        }

        public override decimal Withdraw(decimal withdrawAmount)
        {
            decimal newBalance = Balance - withdrawAmount;

            if (newBalance < AllowedDebt)
            {
                Console.WriteLine($"Your debt would exceed {AllowedDebt:C}. Withdrawal failed.");
            }
            else
            {
                base.Withdraw(withdrawAmount);

                if (Balance < 0)
                {
                    decimal interest = Math.Abs(Balance) * InterestRate;
                    Console.WriteLine($"\nYou now have a debt of {Balance:C}. Interest will be {interest:C} per period.");
                }
            }
            return newBalance;
        }
    }
}
