using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{  
    /// <summary>
    /// Address entity
    /// </summary>
    public class Address
    {      
        /// <summary>
        /// Id of Address
        /// </summary>
        public int Id { get; set; }
       
        /// <summary>
        /// City in Address
        /// </summary>
        public string City { get; set; }
       
        /// <summary>
        /// Country in Address
        /// </summary>
        public string Country { get; set; }
       
        /// <summary>
        /// Street in Address
        /// </summary>        
        public string Street { get; set; }

        /// <summary>
        /// UserAddresses navigation property
        /// </summary>
        public virtual ICollection<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// OrgAddresses navigation property
        /// </summary>
        public virtual ICollection<OrgAddress> OrgAddresses { get; set; }
    }
}
