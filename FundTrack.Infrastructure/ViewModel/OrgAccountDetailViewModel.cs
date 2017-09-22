using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Class which describes some details about organization's account
    /// </summary>
    public class OrgAccountDetailViewModel
    {

        /// <summary>
        /// Name of orgAccount
        /// </summary>    
        public string Name { get; set; }

        /// <summary>
        /// Description of account
        /// </summary>    
        public string AccountDescription { get; set; }

        /// <summary>
        /// Account's target
        /// </summary>    
        public string Target { get; set; }

        /// <summary>
        /// Card number if account type - bank
        /// </summary>    
        public string CardNumber { get; set; }

    }
}
