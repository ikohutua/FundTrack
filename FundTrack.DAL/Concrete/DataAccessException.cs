using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Data access exception class
    /// </summary>
    public class DataAccessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Data access class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public DataAccessException(string message)
            :base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the Data access class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public DataAccessException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
