using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FundTrack.BLL.Abstract;
using FundTrack.BLL.Concrete;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Moq;
using Xunit;

namespace FundTrack.BLL.Tests
{
    public sealed class OrganizationStatisticsServiceTests
    {
        private readonly List<FinOp> _testFinOps;

        private OrgAccount testAccount = new OrgAccount
        {
            Id = 76,
            OrgId = 1
        };

        public OrganizationStatisticsServiceTests()
        {
            _testFinOps = new List<FinOp>
            {
                new FinOp
                {
                    Id = 40,
                    AccToId = 76,
                    Amount = 500,
                    FinOpDate = new DateTime(2017, 09, 26),
                    TargetId = 93,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 93,
                        TargetName = "Загальні",
                        OrganizationId = 1,
                        ParentTargetId = null
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 41,
                    AccFromId = 96,
                    Amount = 100,
                    FinOpDate = new DateTime(2017, 09, 29),
                    TargetId = null,
                    FinOpType = 0,
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 42,
                    AccToId = 77,
                    Amount = 55,
                    FinOpDate = new DateTime(2017, 09, 29),
                    TargetId = null,
                    FinOpType = 1,
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 50,
                    AccToId = 76,
                    Amount = 123,
                    FinOpDate = new DateTime(2017, 09, 30),
                    TargetId = 6,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 6,
                        TargetName = "Цивільні",
                        OrganizationId = 1,
                        ParentTargetId = null
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 51,
                    AccToId = 76,
                    Amount = 123,
                    FinOpDate = new DateTime(2017, 09, 30),
                    TargetId = 5,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 5,
                        TargetName = "Військові",
                        OrganizationId = 1,
                        ParentTargetId = null
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 52,
                    AccToId = 77,
                    Amount = 200,
                    FinOpDate = new DateTime(2017, 09, 30),
                    TargetId = 108,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 108,
                        TargetName = "subВійськові1",
                        OrganizationId = 1,
                        ParentTargetId = 5,
                        ParentTarget = new Target {Id = 5, TargetName = "Військові", OrganizationId = 1}
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 105,
                    AccFromId = 76,
                    Amount = 34,
                    FinOpDate = new DateTime(2017, 10, 02),
                    TargetId = 109,
                    FinOpType = 0,
                    Target = new Target
                    {
                        Id = 109,
                        TargetName = "subВійськові2",
                        OrganizationId = 1,
                        ParentTargetId = 5,
                        ParentTarget = new Target {Id = 5, TargetName = "Військові", OrganizationId = 1}
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 106,
                    AccToId = 76,
                    Amount = 234.43m,
                    FinOpDate = new DateTime(2017, 10, 02),
                    TargetId = 109,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 109,
                        TargetName = "subВійськові2",
                        OrganizationId = 1,
                        ParentTargetId = 5,
                        ParentTarget = new Target {Id = 5, TargetName = "Військові", OrganizationId = 1}
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 110,
                    AccToId = 76,
                    Amount = 234.43m,
                    FinOpDate = new DateTime(2017, 10, 02),
                    TargetId = 96,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 96,
                        TargetName = "subЦивільні1",
                        OrganizationId = 1,
                        ParentTargetId = 6,
                        ParentTarget = new Target {Id = 6, TargetName = "Цивільні", OrganizationId = 1}
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 111,
                    AccToId = 76,
                    Amount = 214.43m,
                    FinOpDate = new DateTime(2017, 10, 05),
                    TargetId = 96,
                    FinOpType = 1,
                    Target = new Target
                    {
                        Id = 96,
                        TargetName = "subЦивільні1",
                        OrganizationId = 1,
                        ParentTargetId = 6,
                        ParentTarget = new Target {Id = 6, TargetName = "Цивільні", OrganizationId = 1}
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                },
                new FinOp
                {
                    Id = 111,
                    AccFromId = 76,
                    Amount = 125.15m,
                    FinOpDate = new DateTime(2017, 10, 06),
                    TargetId = 96,
                    FinOpType = 0,
                    Target = new Target
                    {
                        Id = 96,
                        TargetName = "subЦивільні1",
                        OrganizationId = 1,
                        ParentTargetId = 6,
                        ParentTarget = new Target {Id = 6, TargetName = "Цивільні", OrganizationId = 1}
                    },
                    OrgAccountFrom = testAccount,
                    OrgAccountTo = testAccount
                }
            };
        }

        [Fact]
        public void GetReportForIncomeFinopsByTargetsTest()
        {
            //Arrange
            var repository = new Mock<IFinOpRepository>();
            repository.Setup(r => r.Read())
                .Returns(_testFinOps.AsQueryable);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.FinOpRepository)
                .Returns(repository.Object);

            var targetService = new Mock<ITargetService>();

            var service = new OrganizationStatisticsService(unitOfWork.Object, targetService.Object);

            //Act
            var result = service.GetReportForFinopsByTargets(1, Constants.FinOpTypeIncome,
                new DateTime(2017, 09, 26), new DateTime(2017, 10, 26));


            //Assert
            Assert.Equal(result.Count(), 4);
            Assert.Equal(result.Sum(r => r.Sum), 1684.29m);
        }

        [Fact]
        public void GetSubTargetsTest()
        {
            //Arrange
            List<TargetViewModel> testTargets = new List<TargetViewModel>
            {
                new TargetViewModel {TargetId = 5, Name = "Військові"},
                new TargetViewModel {TargetId = 108, Name = "subВійськові1", ParentTargetId = 5},
                new TargetViewModel {TargetId = 109, Name = "subВійськові2", ParentTargetId = 5}
            };

            var repository = new Mock<IFinOpRepository>();
            repository.Setup(r => r.Read())
                .Returns(_testFinOps.AsQueryable);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.FinOpRepository)
                .Returns(repository.Object);

            var targetService = new Mock<ITargetService>();
            targetService.Setup(t => t.GetTargets(1, 5)).Returns(testTargets);

            var service = new OrganizationStatisticsService(unitOfWork.Object, targetService.Object);

            //Act
            var result = service.GetSubTargets(1, Constants.FinOpTypeIncome, 5, new DateTime(2017, 09, 26),
                new DateTime(2017, 10, 26));

            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(result.Sum(r => r.Sum), 557.43m);
        }

        [Fact]
        public void GetFinOpsByTargetIdTest()
        {
            //Arrange
            var repository = new Mock<IFinOpRepository>();
            repository.Setup(r => r.Read())
                .Returns(_testFinOps.AsQueryable);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.FinOpRepository)
                .Returns(repository.Object);

            var targetService = new Mock<ITargetService>();

            var service = new OrganizationStatisticsService(unitOfWork.Object, targetService.Object);

            //Act
            var result = service.GetFinOpsByTargetId(Constants.FinOpTypeIncome, 96, new DateTime(2017, 09, 26),
                new DateTime(2017, 10, 26));


            //Assert
            Assert.Equal(result.Count(), 2);
        }

    }
}
