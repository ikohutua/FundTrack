using FundTrack.DAL.Concrete;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.Integration.Tests.TestRepositories
{
    public class TestIncomeReportRepository
    {
        private FundTrackContext _dbContext;

        public TestIncomeReportRepository(FundTrackContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<ReportIncomeViewModel> GetTestReports()
        {
            return new List<ReportIncomeViewModel>()
            {
                new ReportIncomeViewModel(){ TargetTo = "Цивільні", From = "", Description = "Test1", MoneyAmount=220,Date= DateTime.Parse("20-Oct-2017 20:20") },
                new ReportIncomeViewModel(){ TargetTo = "Військові", From = "", Description = "Test2", MoneyAmount=190,Date= DateTime.Parse("20-Oct-2017 20:20") },
                new ReportIncomeViewModel(){ TargetTo = "Медикаменти", From = "", Description = "Test1", MoneyAmount=220,Date= DateTime.Parse("20-Oct-2017 20:20") },
                new ReportIncomeViewModel(){ TargetTo = "Цивільні", From = "", Description = "Test1", MoneyAmount=220,Date= DateTime.Parse("20-Oct-2017 20:20") },
                new ReportIncomeViewModel(){ TargetTo = "Військові", From = "", Description = "LiqPay. Перевод с карты на карту от 02-08-2017 ID платежа 470594171", MoneyAmount=10,Date= DateTime.Parse("19-Oct-2017 10:07") },
                new ReportIncomeViewModel(){ TargetTo = "", From = "", Description = "Test", MoneyAmount=100,Date= DateTime.Parse("18-Oct-2017 00:00") },
                new ReportIncomeViewModel(){ TargetTo = "", From = "", Description = "", MoneyAmount=234.43M,Date= DateTime.Parse("08-Oct-2017 00:00") },
                new ReportIncomeViewModel(){ TargetTo = "", From = "", Description = "Перевод с карты ПриватБанка через приложение Приват24. Отправитель: Серивко Мар'яна Ярославівна", MoneyAmount=400,Date= DateTime.Parse("06-Oct-2017 12:29") }

            }.AsQueryable();
        }
        public IQueryable<Target> GetTestTargets()
        {
            return new List<Target>()
            {
                new Target(){Id=5, TargetName="Військові",OrganizationId=   1 },
                new Target(){Id=6, TargetName="Цивільні",OrganizationId=      1},
                new Target(){Id=7, TargetName="Медикаменти",OrganizationId=   1},
                new Target(){Id=93,TargetName="Загальні",OrganizationId=      1},
            }.AsQueryable();
        }
        public IQueryable<OrgAccount> GetTestOrgAccounts()
        {
            return new List<OrgAccount>()
            {
                new OrgAccount(){Id=76, AccountType="Банк", BankAccId=14, CurrencyId=3, CurrentBalance=1922, Description="приват", OrgAccountName="Приват Мерчант", OrgId=1, TargetId=93, CreationDate=DateTime.Parse("2017-07-29 12:00:00.0000000"), UserId=7 }

            }.AsQueryable();
        }
        public IQueryable<FinOp> GetTestFinOps()
        {
            return new List<FinOp>()
            {
                new FinOp(){ Id=1, AccFromId=null, AccToId=76, Amount=9.99M, Description="Зачисление суммы вклада с «Копилки»", DonationId=null, FinOpDate=DateTime.Parse("2017-09-08 12:52:27.090"),TargetId=5, UserId=6, FinOpType=1  },
                new FinOp(){ Id=2   , AccFromId=null    , AccToId=76    , Amount=10.00M , Description="123", DonationId=47  , FinOpDate=DateTime.Parse("2017-09-11 20:09:03.357"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=11  , AccFromId=null    , AccToId=76    , Amount=5.00M  , Description="на військові потреби", DonationId=   39  , FinOpDate=DateTime.Parse("2017-09-12 15:44:03.300"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=17  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="Трансфер коштів", DonationId= null   , FinOpDate=DateTime.Parse("2017-09-14 10:03:18.580"),TargetId= 5, UserId=  null, FinOpType=    2 },
                new FinOp(){ Id=18  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="Трансфер коштів", DonationId= null   , FinOpDate=DateTime.Parse("2017-09-14 10:03:31.350"),TargetId= 5, UserId=  null, FinOpType=    2 },
                new FinOp(){ Id=20  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="на військові потреби", DonationId= null  , FinOpDate=DateTime.Parse("2017-09-17 21:09:34.733"),TargetId= null, UserId=   6, FinOpType=   1 },
                new FinOp(){ Id=21  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="на військові потреби", DonationId=   46  , FinOpDate=DateTime.Parse("2017-09-17 21:27:55.140"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=22  , AccFromId=null    , AccToId=76    , Amount=200.00M    , Description="на військові потреби2", DonationId= null, FinOpDate=DateTime.Parse("2017-09-19 14:33:38.107"),TargetId=  null, UserId=   7, FinOpType=   1 },
                new FinOp(){ Id=23  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="на військові потреби_", DonationId= null, FinOpDate=DateTime.Parse("2017-09-19 18:18:28.143"),TargetId=  5   , UserId=7, FinOpType=  2 },
                new FinOp(){ Id=28  , AccFromId=null    , AccToId=76    , Amount=22.80M , Description="Testing", DonationId= null   , FinOpDate=DateTime.Parse("2017-09-21 14:27:54.727"),TargetId= 7, UserId=  null, FinOpType=    1 },
                new FinOp(){ Id=31  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="123", DonationId=    null    , FinOpDate=DateTime.Parse("2017-09-21 20:06:12.880"),TargetId= 7, UserId=  null, FinOpType=    1 },
                new FinOp(){ Id=32  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="Test", DonationId= null  , FinOpDate=DateTime.Parse("2017-09-24 19:27:25.643"),TargetId= 5, UserId=  null, FinOpType=    1 },
                new FinOp(){ Id=37  , AccFromId=null    , AccToId=76    , Amount=200.00M    , Description="Lack of money", DonationId= null, FinOpDate=DateTime.Parse("2017-09-25 00:00:00.000"),TargetId=  93, UserId=null, FinOpType= 1 },
                new FinOp(){ Id=38  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="Test FinOp", DonationId=  null   , FinOpDate=DateTime.Parse("2017-09-22 00:00:00.000"),TargetId= 6, UserId=null, FinOpType=  1 },
                new FinOp(){ Id=40  , AccFromId=null    , AccToId=76    , Amount=500.00M    , Description="Test Income another one", DonationId= null   , FinOpDate=DateTime.Parse("2017-09-26 00:00:00.000"),TargetId= 93, UserId= 6, FinOpType=   1 },
                new FinOp(){ Id=43  , AccFromId=null    , AccToId=76    , Amount=100.00M    , Description="Анонім", DonationId= null    , FinOpDate=DateTime.Parse("2017-09-22 00:00:00.000"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=50  , AccFromId=null    , AccToId=76    , Amount=123.00M    , Description="null", DonationId= null  , FinOpDate=DateTime.Parse("2017-09-30 00:00:00.000"),TargetId= 6, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=105 , AccFromId=null    , AccToId=76    , Amount=34.00M , Description="null", DonationId= null  , FinOpDate=DateTime.Parse("2017-10-02 00:00:00.000"),TargetId= 5, UserId=  7, FinOpType=   1 },
                new FinOp(){ Id=114 , AccFromId=null    , AccToId=76    , Amount=10.00M , Description="LiqPay.Перевод с карты на карту от 02-08-2017 ID платежа 470594171", DonationId= null    , FinOpDate=DateTime.Parse("2017-10-19 10:07:37.677"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=127 , AccFromId=null    , AccToId=76    , Amount=220.00M    , Description="Test1", DonationId= null , FinOpDate=DateTime.Parse("2017-10-20 20:20:00.000"),TargetId= null, UserId= null, FinOpType=  1 },
                new FinOp(){ Id=128 , AccFromId=null    , AccToId=76    , Amount=190.00M    , Description="Test2", DonationId= null , FinOpDate=DateTime.Parse("2017-10-20 20:20:00.000"),TargetId= null, UserId= null, FinOpType=  1 },
                new FinOp(){ Id=129 , AccFromId=null    , AccToId=76    , Amount=220.00M    , Description="Test1", DonationId= null , FinOpDate=DateTime.Parse("2017-10-20 20:00:00.000"),TargetId= 7, UserId=  7, FinOpType=   1 },
                new FinOp(){ Id=130 , AccFromId= null   , AccToId=76    , Amount=220.00M    , Description="Test1", DonationId= null , FinOpDate=DateTime.Parse("2017-10-20 20:40:00.000"),TargetId= 7, UserId=  7, FinOpType=   1 },
                new FinOp(){ Id=131 , AccFromId=null    , AccToId=76    , Amount=230.00M    , Description="Test1", DonationId= null , FinOpDate=DateTime.Parse("2017-10-20 20:22:00.000"),TargetId= 5, UserId=  7, FinOpType=   1 },
                new FinOp(){ Id=132 , AccFromId=null    , AccToId=76    , Amount=10 , Description="LiqPay.Перевод с карты на карту от 02-08-2017 ID платежа 470594171", DonationId= null    , FinOpDate=DateTime.Parse("2017-08-02 10:10:00.00"),TargetId=5 ,UserId=  null, FinOpType=   1 },
                new FinOp(){ Id=133 , AccFromId=null    , AccToId=76    , Amount=9.99M, Description="Зачисление суммы вклада с «Копилки»", DonationId=    49  , FinOpDate=DateTime.Parse("2017-08-03 13:46:00.000"),TargetId= null, UserId=   6, FinOpType=   1 },
                new FinOp(){ Id=134 , AccFromId=null    , AccToId=76    , Amount=220.00M    , Description="Test1", DonationId= null , FinOpDate=DateTime.Parse("2017-10-20 20:20:00.000"),TargetId= 6, UserId=  7, FinOpType=   1 },
                new FinOp(){ Id=135 , AccFromId=null    , AccToId=76    , Amount=220.00M    , Description="Test1", DonationId=  53  , FinOpDate=DateTime.Parse("2017-10-20 20:00:00.000"),TargetId= 6, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=136 , AccFromId=null    , AccToId=76    , Amount=190.00M    , Description="Test2", DonationId=  52  , FinOpDate=DateTime.Parse("2017-10-20 20:20:00.000"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=137 , AccFromId=null    , AccToId=76    , Amount=220.00M    , Description="Test1", DonationId=  51  , FinOpDate=DateTime.Parse("2017-10-20 20:40:00.000"),TargetId= 5, UserId=  6, FinOpType=   1 },
                new FinOp(){ Id=138 , AccFromId=null    , AccToId=76    , Amount=230.00M , Description="Test1", DonationId=  50  , FinOpDate=DateTime.Parse("2017-10-20 20:22:00.000"),TargetId= 7, UserId=  6, FinOpType=   1 },
             }.AsQueryable();
        }

        public void AddTargetsToDb(IEnumerable<Target> target)
        {
            _dbContext.Targets.AddRange(target);
            _dbContext.SaveChanges();
        }
        public void AddFinOpsToDb(IEnumerable<FinOp> finops)
        {
            _dbContext.FinOps.AddRange(finops);
            _dbContext.SaveChanges();
        }
        public void AddOrgAccountsToDb(IEnumerable<OrgAccount> orgAccounts)
        {
            _dbContext.OrgAccounts.AddRange(orgAccounts);
            _dbContext.SaveChanges();
        }
    }
}
