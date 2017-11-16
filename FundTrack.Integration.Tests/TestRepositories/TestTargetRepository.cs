using FundTrack.DAL.Concrete;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Target = FundTrack.DAL.Entities.Target;


namespace FundTrack.Integration.Tests.TestRepositories
{
   public class TestTargetRepository
    {
        private FundTrackContext _dbContext;

        public TestTargetRepository(FundTrackContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Target> GetTestTargets()
        {
            return new List<Target>()
            {
                new Target(){ Id = 1, OrganizationId = 1, TargetName = "Продукти"},
                new Target(){ Id = 2, OrganizationId = 1, TargetName = "Ліки" ,},
                new Target(){ Id = 3, OrganizationId = 1, TargetName = "Одяг" },
                new Target(){ Id = 4, OrganizationId = 2, TargetName = "Електроніка"},
                new Target(){ Id = 5, OrganizationId = 1, TargetName = "Бригада №1", ParentTargetId = 1 },
                new Target(){ Id = 6, OrganizationId = 1, TargetName = "Бригада №2", ParentTargetId = 2 },
                new Target(){ Id = 7, OrganizationId = 1, TargetName = "Бригада №3", ParentTargetId = 3 },
                new Target(){ Id = 8, OrganizationId = 2, TargetName = "Бригада №4", ParentTargetId = 4 },
                new Target(){ Id = 9, OrganizationId = 2, TargetName = "Госпіталь №1" },
                new Target(){ Id = 10, OrganizationId = 1, TargetName = "Госпіталь №2" },
                new Target(){ Id = 11, OrganizationId = 1, TargetName = "Госпіталь №3" },
                new Target(){ Id = 12, OrganizationId = 1, TargetName = "Госпіталь №4" },
                new Target(){ Id = 13, OrganizationId = 2, TargetName = "Госпіталь №5" }
            }.AsQueryable();
        }

        public Target GetTestTargetByField(System.Func<Target, bool> predicate)
        {
            return GetTestTargets().Where(predicate).FirstOrDefault();
        }
        public TargetViewModel ConvertToTargetViewModel(Target target)
        {
            return new TargetViewModel()
            {
                TargetId = target.Id,
                Name = target.TargetName,
                OrganizationId = target.OrganizationId,
                ParentTargetId = target.ParentTargetId
            };
        }
        public Target ConvertToTarget(TargetViewModel targetVm)
        {
            return new Target()
            {
                Id = targetVm.TargetId,
                TargetName = targetVm.Name,
                OrganizationId = targetVm.OrganizationId,
                ParentTargetId = targetVm.ParentTargetId
            };
        }


        public void AddTargetToDb(Target target)
        {
            _dbContext.Targets.Add(target);
            _dbContext.SaveChanges();
        }

        public void AddRangeOfTargetsToDb(IEnumerable<Target> target)
        {
            _dbContext.Targets.AddRange(target);
            _dbContext.SaveChanges();
        }
    }
}
