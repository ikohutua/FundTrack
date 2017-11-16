using FundTrack.DAL.Concrete;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using FinOp = FundTrack.DAL.Entities.FinOp;
using OrgAccount = FundTrack.DAL.Entities.OrgAccount;
using Balance = FundTrack.DAL.Entities.Balance;
using FinOpImage = FundTrack.DAL.Entities.FinOpImage;


namespace FundTrack.Integration.Tests.TestRepositories
{
    public class TestFinOpRepository
    {
        private FundTrackContext _dbContext;

        public TestFinOpRepository(FundTrackContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<FinOp> GetTestFinOps()
        {
            return new List<FinOp>()
            {
                new FinOp() {Id = 1, AccFromId = 1, AccToId = null, Amount = 100, Description = "Description1", FinOpDate = new DateTime(2017,11,1), DonationId = null, TargetId = 1, UserId = 1, FinOpType = 0},
                new FinOp() {Id = 2, AccFromId = 2, AccToId = null, Amount = 200, Description = "Description2", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = 2, UserId = 2, FinOpType = 0},
                new FinOp() {Id = 3, AccFromId = null, AccToId = 1, Amount = 300, Description = "Description3", FinOpDate = new DateTime(2017,11,3), DonationId = 1, TargetId = 1, UserId = 3, FinOpType = 1},
                new FinOp() {Id = 4, AccFromId = null, AccToId = 2, Amount = 250, Description = "Description4", FinOpDate = new DateTime(2017,11,1), DonationId = 2, TargetId = 2, UserId = 2, FinOpType = 1},
                new FinOp() {Id = 5, AccFromId = 1, AccToId = 2, Amount = 100, Description = "Description5", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = null, UserId = 3, FinOpType = 2},
                new FinOp() {Id = 6, AccFromId = 2, AccToId = 1, Amount = 400, Description = "Description6", FinOpDate = new DateTime(2017,11,4), DonationId = null, TargetId = null, UserId = 2, FinOpType = 2},
                new FinOp() {Id = 7, AccFromId = null, AccToId = 3, Amount = 220, Description = "Description7", FinOpDate = new DateTime(2017,11,1), DonationId = 3, TargetId = 2, UserId = 2, FinOpType = 1},
                new FinOp() {Id = 8, AccFromId = 3, AccToId = null, Amount = 200, Description = "Description8", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = 2, UserId = 2, FinOpType = 0},
                new FinOp() {Id = 9, AccFromId = null, AccToId = 1, Amount = 100, Description = "Description9", FinOpDate = new DateTime(2017,11,4), DonationId = null, TargetId = 1, UserId = 3, FinOpType = 1},
                new FinOp() {Id = 10, AccFromId = null, AccToId = 1, Amount = 300, Description = "Description10", FinOpDate = new DateTime(2017,11,1), DonationId = null, TargetId = 1, UserId = 3, FinOpType = 1},
            }.AsQueryable();
        }

        public IQueryable<OrgAccount> GetTestOrgAccounts()
        {
            return new List<OrgAccount>()
            {
                new OrgAccount() {Id = 1, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description1", OrgAccountName = "Рахунок1", OrgId = 1, TargetId = 1, CreationDate = new DateTime(2017,10,28), UserId = 1 },
                new OrgAccount() {Id = 2, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description2", OrgAccountName = "Рахунок2", OrgId = 1, TargetId = 2, CreationDate = new DateTime(2017,10,29), UserId = 2 },
                new OrgAccount() {Id = 3, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description3", OrgAccountName = "Рахунок3", OrgId = 1, TargetId = 1, CreationDate = new DateTime(2017,10,28), UserId = 2 },
                new OrgAccount() {Id = 4, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description4", OrgAccountName = "Рахунок4", OrgId = 2, TargetId = 2, CreationDate = new DateTime(2017,10,30), UserId = 4 }

            }.AsQueryable();
        }

        public IQueryable<Balance> GetTestBalances()
        {
            return new List<Balance>()
            {
                new Balance() {Id = 1, Amount = 1000, BalanceDate = new DateTime(2017,11,2), OrgAccountId = 1},
                new Balance() {Id = 2, Amount = 1000, BalanceDate = new DateTime(2017,11,1), OrgAccountId = 2},
                new Balance() {Id = 3, Amount = 1000, BalanceDate = new DateTime(2017,11,3), OrgAccountId = 3},
                new Balance() {Id = 4, Amount = 1000, BalanceDate = new DateTime(2017,11,1), OrgAccountId = 4}

            }.AsQueryable();
        }

        private IQueryable<FinOpImage> GetTestFinOpImages()
        {
            return new List<FinOpImage>()
            {
                new FinOpImage() {Id = 1, FinOpId = 1, ImageUrl = "bf0dc5b8-6c7a-404f-8812-cf10bf7a34e3.jpeg"},
                new FinOpImage() {Id = 2, FinOpId = 1, ImageUrl = "21c733ba-5684-4d3f-8c68-6a673bbfe2e4.jpeg"},
                new FinOpImage() {Id = 3, FinOpId = 1, ImageUrl = "dc094539-18bc-4ad5-9ea1-d4e7772947a9.jpeg"},
                new FinOpImage() {Id = 4, FinOpId = 8, ImageUrl = "dc094539-18bc-4ad5-9ea1-d4e7772947a9.jpeg"},

            }.AsQueryable();
        }

        public FinOp GetTestFinOpsById(int id)
        {
            return GetTestFinOps().Where(f => f.Id == id).Single();
        }

        public OrgAccount GetTestOrgAccountById(int id)
        {
            return GetTestOrgAccounts().Where(a => a.Id == id).Single();
        }

        public void AddFinOpToDb(FinOp finOp)
        {
            _dbContext.FinOps.Add(finOp);
            _dbContext.SaveChanges();
        }

        public void AddRangeOfFinOpsToDb(IEnumerable<FinOp> finOps)
        {
            _dbContext.FinOps.AddRange(finOps);
            _dbContext.SaveChanges();
        }

        public void AddRangeOfOrgAccountsToDb(IEnumerable<OrgAccount> orgAccounts)
        {
            _dbContext.OrgAccounts.AddRange(orgAccounts);
            _dbContext.SaveChanges();
        }

        public void AddRangeOfBalancesToDb(IEnumerable<Balance> balances)
        {
            _dbContext.Balances.AddRange(balances);
            _dbContext.SaveChanges();
        }

        public void AddRangeOfFinOpImagesToDb(IEnumerable<FinOpImage> finOpImages)
        {
            _dbContext.FinOpImages.AddRange(finOpImages);
            _dbContext.SaveChanges();
        }
    }
}
