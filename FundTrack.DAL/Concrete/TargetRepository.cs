using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class TargetRepository : ITargetRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TargetRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all targets.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Target> Read()
        {
            return _context.Targets;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the target by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Target GetTargetByName(string name)
        {
            return _context.Targets
                           .FirstOrDefault(t => t.TargetName == name);
        }

        /// <summary>
        /// Get all parent targets by organization id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Target> GetTargetsByOrganizationId(int id)
        {
            return _context.Targets.Where(t => t.OrganizationId == id);
        }

        public Target GetTargetById(int id)
        {
            return _context.Targets
                .FirstOrDefault(t => t.Id == id);
        }

        public Target Create(Target item)
        {
            var added = _context.Targets.Add(item);
            return added.Entity;
        }

        public Target Update(Target item)
        {
            var itemToUpdate = _context.Targets.FirstOrDefault(i => i.Id == item.Id);

            if (itemToUpdate == null)
                throw new DataAccessException(ErrorMessages.UpdateDataError);

            itemToUpdate.TargetName = item.TargetName;
            itemToUpdate.OrganizationId = item.OrganizationId;

            _context.Update(itemToUpdate);
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var target = _context.Targets.FirstOrDefault(c => c.Id == id);

            if (target == null)
                throw new DataAccessException(ErrorMessages.DeleteDataError);

            //does target is bonded to Organization Accounts, Finance Operations, Donations
            int dependenciesOrgAccCount = _context.OrgAccounts.Where(oa => oa.TargetId == id).Count();
            int dependenciesFinOpCount = _context.FinOps.Where(fo => fo.TargetId == id).Count();
            int dependenciesDonatesCount = _context.Donations.Where(d => d.TargetId == id).Count();
           
            if (dependenciesOrgAccCount > 0 || dependenciesFinOpCount > 0 || dependenciesDonatesCount > 0)
                throw new DataAccessException(ErrorMessages.DeleteDependentTarget);

            _context.Targets.Remove(target);
        }
    }
}
