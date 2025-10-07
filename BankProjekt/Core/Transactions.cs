using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core
{
    public class Transactions
    {
        public enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }

        public class Transaction
        {
            public string Id { get; set; }
            public string AccountNumber { get; set; }
            public decimal Amount { get; set; }
            public DateTime Timestamp { get; set; }
            public TransactionType Type { get; set; }

            public Transaction(string accountNumber, decimal amount, TransactionType type)
            {
                Id = Guid.NewGuid().ToString();
                AccountNumber = accountNumber;
                Amount = amount;
                Timestamp = DateTime.Now;
                Type = type;
            }
        }

    }
}

    

