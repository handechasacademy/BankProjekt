using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core
{
    public class SavingsAccount : Account
    {
        int WithdrawalCounter = 0;
        decimal WithdrawalInterest = 0.01m;
        decimal SavingsInterest = 1.03m;
        public SavingsAccount(string accountNumber, decimal balance, User owner) : base(accountNumber, balance, owner)
        {
        }

        public override void Withdraw(decimal amount)
        {
            //Gratis 3 gånger därefter avgift
            if (WithdrawalCounter < 3)
            {
                base.Withdraw(amount * WithdrawalInterest);
                WithdrawalCounter++;
            }
            else
            {
                //base.Withdraw(amount);
                Console.WriteLine($"Transferring {amount * WithdrawalInterest} ({WithdrawalInterest} interest)"); 
            }
        }
        public void InterestCalculator(int months)
        {
            Console.WriteLine($"Your balance will be {Balance*SavingsInterest} in {months} months with {(SavingsInterest-1)*100}% interest. ");
        }
    }
}
