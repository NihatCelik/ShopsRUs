using System;

namespace Core.Exceptions
{
    public class TransactionScopeException : Exception
    {
        public TransactionScopeException()
        {
        }
        public TransactionScopeException(string message) : base(message)
        {
        }
    }
}
