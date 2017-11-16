namespace PrivatService.ViewModels
{
    public class BankAccountsViewModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int OrgId { get; set; }
        public int? ExtractMerchantId { get; set; }
        public string ExtractMerchantPassword { get; set; }
        public bool? IsExtractEnabled { get; set; }
    }
}