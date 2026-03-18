using System;

namespace ModelLayer.Exceptions
{
    /// <summary>
    /// Custom exception for quantity measurement operations
    /// </summary>
    public class QuantityMeasurementException : Exception
    {
        public QuantityMeasurementException()
        {
        }

        public QuantityMeasurementException(string message) : base(message)
        {
        }

        public QuantityMeasurementException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}