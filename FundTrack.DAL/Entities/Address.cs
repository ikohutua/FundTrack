using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{  
    /// <summary>
    /// Address entity
    /// </summary>
    public class Address
    {      
        /// <summary>
        /// Gets or Sets Id of Address
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets City in Address
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Country in Address
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or Sets Street in Address
        /// </summary>        
        public string Street { get; set; }

        /// <summary>
        /// Gets or Sets Building in Address
        /// </summary>        
        public string Building { get; set; }

        /// <summary>
        /// Gets or Sets UserAddresses navigation property
        /// </summary>
        public virtual ICollection<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// Gets or Sets OrgAddresses navigation property
        /// </summary>
        public virtual ICollection<OrgAddress> OrgAddresses { get; set; }
    }
}
