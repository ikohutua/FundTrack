using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    public interface IBankImportRepository
    {
       BankImport Create(BankImport bankImport);
    }
}
