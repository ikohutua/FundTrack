using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// OrgAccount entity
    /// </summary>
    public class OrgAccount
    {
        /// <summary>
        /// Gets or Sets Id of OrgAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets Id of BankAccount
        /// </summary>
        public int? BankAccId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Currency
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or Sets Name of OrgAccount
        /// </summary>
        public string OrgAccountName { get; set; }

        /// <summary>
        /// Gets or Sets Type of OrgAccount
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or Sets Description of OrgAccount
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets CurrentBalance on OrgAccount
        /// </summary>
        public decimal CurrentBalance { get; set; }

        public int TargetId { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Gets or Sets BankAccount navigation property
        /// </summary>
        public virtual BankAccount BankAccount { get; set; }

        /// <summary>
        /// Gets or Sets Currency navigation property
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or Sets Target navigation property
        /// </summary>
        public virtual Target Target { get; set; }

        /// <summary>
        /// Gets or Sets Balance navigation property
        /// </summary>
        public virtual ICollection<Balance> Balances { get; set; }

        /// <summary>
        /// Gets or Sets FinOpFrom navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOpsFrom { get; set; }

        /// <summary>
        /// Gets or Sets FinOpTo navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOpsTo { get; set; }
        #region Operators
        public static implicit operator OrgAccount (OrgAccountViewModel model)
        {
            return new OrgAccount
            {
                Id = model.Id,
                OrgId = model.OrgId,
                BankAccId = model.BankAccId,
                CurrencyId = model.CurrencyId,
                OrgAccountName = model.OrgAccountName,
                AccountType = model.AccountType,
                Description=model.Description,
                CurrentBalance=model.CurrentBalance,
            };
        }
        public static implicit operator OrgAccountViewModel (OrgAccount item)
        {
            return new OrgAccountViewModel
            {
                Id=item.Id,
                OrgId=item.OrgId,
                BankAccId=item.BankAccId,
                CurrencyId=item.CurrencyId,
                OrgAccountName=item.OrgAccountName,
                AccountType=item.AccountType,
                Description=item.Description,
                CurrentBalance=item.CurrentBalance,
            };
        }
        #endregion
    }
}
