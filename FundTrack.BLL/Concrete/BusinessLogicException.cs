using System;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Business logic eception class
    /// </summary>
    public class BusinessLogicException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the BusinessLogicException class.
        /// </summary>
        /// <param name="message">Message</param>
        public BusinessLogicException(string message)
            :base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the BusinessLogicException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BusinessLogicException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
