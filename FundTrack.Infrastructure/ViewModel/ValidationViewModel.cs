using System.Collections.Generic;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Validation View model
    /// </summary>
    public class ValidationViewModel
    {
        /// <summary>
        /// Gets or Sets field name for validation view model
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or Sets list of error messages 
        /// </summary>
        public List<string> ErrorsMessages { get; set; }
    }
}
