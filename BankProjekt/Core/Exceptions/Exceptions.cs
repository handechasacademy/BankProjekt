using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core.Exceptions
{
    public class Exceptions
    {
        public class InvalidInputException : Exception
        {
            public InvalidInputException(string message) : base(message) { }
        }
        public class DuplicateException : Exception
        {
            public DuplicateException(string message) : base(message) { }
        }
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }
        public class FundIssueException: Exception // when they are broke
        {
            public FundIssueException(string message): base(message) { }
        }
    }
}