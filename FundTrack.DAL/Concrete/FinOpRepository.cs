using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Concrete
{
    public class FinOpRepository : IFinOpRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initialize new instance if StatusRepository class
        /// </summary>
        /// <param name="context"></param>
        public FinOpRepository(FundTrackContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Creates the specified fin op.
        /// </summary>
        /// <param name="finOp">The fin op.</param>
        /// <returns></returns>
        public FinOp Create(FinOp finOp)
        {
            var createdFinOp = this._context.FinOps.Add(finOp);
            return createdFinOp.Entity;
        }

        /// <summary>
        /// Updates the specified fin op.
        /// </summary>
        /// <param name="finOp">The fin op.</param>
        /// <returns></returns>
        public FinOp Update(FinOp finOp)
        {
            //_context.Entry(finOp).State = EntityState.Modified;
            this._context.FinOps.Update(finOp);
            return finOp;
        }

        /// <summary>
        /// Gets finOp the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public FinOp GetById(int id)
        {
            return this._context.FinOps
                 .Include(fo => fo.OrgAccountTo).Include(fo=>fo.Target)
                 .FirstOrDefault(fo => fo.Id == id);
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinOp> Read()
        {
            return this._context.FinOps.Include(f => f.OrgAccountTo)
                .ThenInclude(a => a.Organization)
                .Include(f => f.OrgAccountFrom)
                .ThenInclude(a => a.Organization)
                .Include(f => f.Donation)
                .Include(f => f.Target);
        }

        /// <summary>
        /// Gets the fin op by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        public IQueryable<FinOp> GetFinOpByOrgAccount(int orgAccountId)
        {
            var finOps = this._context.FinOps
                .Include(a => a.OrgAccountTo)
                .Include(a => a.OrgAccountFrom)
                .Where(a => a.AccToId.HasValue ? a.AccToId == orgAccountId : a.AccFromId == orgAccountId);
            return finOps;
        }
    }
}
