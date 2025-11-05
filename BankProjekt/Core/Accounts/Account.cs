using BankProjekt.Core.Users;
using java.util;
using System;
using System.Collections.Generic;
using System.Transactions;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Accounts
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public User Owner { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string AccountType { get; set; }
        public decimal FeePercentage {  get; set; }
        public string Currency { get; set; }

        public Account(string accountNumber, decimal balance, User owner, string accountType, decimal feePercentage = 0.0m, string currency = "SEK")
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Owner = owner;
            AccountType = accountType;
            Transactions = new List<Transaction>();
            FeePercentage = feePercentage;
            Currency = currency;
        }

        public void Deposit(decimal depositAmount)
        {
            if (depositAmount <= 0)
                throw new InvalidInputException("Deposit amount must be positive.");

            Balance += depositAmount;
            Transactions.Add(new Transaction(Owner.Id, AccountNumber, depositAmount, DateTime.Now, "Deposit", bufferMinutes: 0));
        }

        public virtual decimal Withdraw(decimal withdrawAmount)
        {
            if (withdrawAmount <= 0)
                throw new InvalidInputException("Withdrawal amount must be positive.");
            if (Balance < withdrawAmount)
                throw new FundIssueException("Insufficient funds.");
            Balance -= withdrawAmount;
            Transactions.Add(new Transaction(Owner.Id, AccountNumber, -withdrawAmount, DateTime.Now, "Withdraw", bufferMinutes: 0));
            return Balance;
        }


        public List<Transaction> GetTransactions()
        {
            List<Transaction> output = new List<Transaction>();
            foreach(var tran in Transactions)
            {
                output.Add(tran);
            }
            return output;
        }        

        public override string ToString()
        {
            string s = "";

            s += "-----------------------------------------------\n";
            s += "Account Number:     " + AccountNumber;
            s += "\nCurrency:          " + Currency;
            s += "\nType:              " + AccountType;
            s += "\nBalance:           " + Balance;
            s += "\n-----------------------------------------------\n";

            return s;
        }
    }
}
