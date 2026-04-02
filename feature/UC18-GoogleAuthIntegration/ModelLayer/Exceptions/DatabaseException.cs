using System;

namespace ModelLayer.Exceptions
{
    /// <summary>
    /// Custom exception for database-related errors
    /// </summary>
    public class DatabaseException : Exception
    {
        public DatabaseException()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}