using System;

namespace PrivatService.ViewModels
{
    public class BankImportDetailsViewModel
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public int AppCode { get; set; }
        public string Card { get; set; }
        public string CardAmount { get; set; }
        public string Description { get; set; }
        public string Rest { get; set; }
        public string Terminal { get; set; }
        public DateTime Trandate { get; set; }
        public bool IsLooked { get; set; }
        //public Nullable<int> FinOpId { get; set; }
    }
}
