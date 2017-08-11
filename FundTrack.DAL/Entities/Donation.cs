using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Donation Entity
    /// </summary>
    public class Donation
    {
        /// <summary>
        /// Gets or Sets Id of Donation entity
        /// </summary>
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int? UserId { get; set; }
        public int CurrencyId { get; set; }
        public int TargetId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime DonationDate { get; set; }
        public string DonatorEmail { get; set; }
        public virtual Currency Currency {get;set;}
        public virtual User User { get; set; }
        public virtual Target Target { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
