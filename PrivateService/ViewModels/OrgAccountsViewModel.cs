using System;

namespace PrivatService.ViewModels
{
    public class OrgAccountsViewModel
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public int? BankAccId { get; set; }
        public int CurrencyId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Description { get; set; }
        public string OrgAccountName { get; set; }
        public int OrgId { get; set; }
        public int? TargetId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? UserId { get; set; }

        public virtual BankAccountsViewModel  BankAccounts { get; set; }
    }
}
