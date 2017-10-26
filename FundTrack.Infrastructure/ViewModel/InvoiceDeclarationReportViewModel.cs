using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class InvoiceDeclarationReportViewModel
    {       
            public string BankAccount{ get; set; }
            public decimal? BeginIncomeMonthSum { get; set; }
            public decimal TotalIncomeSum { get; set; }
            public decimal TransferIncome { get; set; }
            public decimal FlowOutcome { get; set; }
            public decimal TransferOutcome { get; set; }
            public string BankAccountTooltip { get; set; }
    }

}

