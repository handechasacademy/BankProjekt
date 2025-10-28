using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BankProjekt.Core.Accounts
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public User Owner { get; set; }
        public List<Transaction> Transactions { get; set; }

        public decimal FeePercentage {  get; set; }

        public Account(string accountNumber, decimal balance, User owner, decimal feePercentage = 0.0m)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Owner = owner;
            Transactions = new List<Transaction>();
            FeePercentage = feePercentage;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Transactions.Add(new Transaction(Owner.Id, AccountNumber, amount, DateTime.Now, "Deposit"));
            }
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount > 0) 
            {
                Balance -= amount;
            }
            Transactions.Add(new Transaction(Owner.Id, AccountNumber, amount, DateTime.Now, "Withdraw"));
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
        public void ShowTransactionHistory()
        {
            Console.WriteLine("---- TRANSACTION HISTORY ----");
            if (Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            foreach (Transaction transaction in Transactions)
            {
                Console.WriteLine($"ID: {transaction.Id}");
                Console.WriteLine($"Account Number: {transaction.AccountNumber}");
                Console.WriteLine($"Amount: {transaction.Amount:C}");
                Console.WriteLine($"Date and Time: {transaction.Timestamp}");
                Console.WriteLine($"Type: {transaction.Type}");
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return $"Account of {Owner}(Account number: {AccountNumber}) contains ${Balance}.";
        }
    }
}
