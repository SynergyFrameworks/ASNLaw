using System;

namespace Infrastructure.Common.Persistence
{
    public class SqlPagingException : Exception
    {
        public SqlPagingException() : base() { }
        public SqlPagingException(string message) : base(message)
        {
        }
    }
}
