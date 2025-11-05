using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankProjekt.Core
{
    public class Transaction
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public int BufferMinutes { get; set; }

        public bool IsPending()
        {
            TimeSpan timePassed = DateTime.Now - Timestamp;
            return timePassed.TotalMinutes < BufferMinutes;
        }


        public Transaction(string id, string accountNumber, decimal amount, DateTime timestamp, string type, int bufferMinutes)
        {
            Id = id;
            AccountNumber = accountNumber;
            Amount = amount;
            Timestamp = timestamp;
            Type = type;
            BufferMinutes = bufferMinutes;
        }

        public override string ToString()
        {
            string pendingNote = IsPending() ? " [PENDING]" : "";
            string s = "";

            s += "-----------------------------------------------\n";
            s += "Amount: " + Amount + pendingNote;
            s += "Date: " + Timestamp;
            s += "Type: " + Type;
            s += "-----------------------------------------------\n";

            return s;
        }
    }

}