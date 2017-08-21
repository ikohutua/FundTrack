using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Concrete
{
    public class FinOpRepository:IFinOpRepository
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
            var createdFinOp=this._context.FinOps.Add(finOp);
            return createdFinOp.Entity;
        }

        /// <summary>
        /// Updates the specified fin op.
        /// </summary>
        /// <param name="finOp">The fin op.</param>
        /// <returns></returns>
        public FinOp Update(FinOp finOp)
        {
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
                .Include(fo=>fo.OrgAccountTo)
                .FirstOrDefault(fo => fo.Id == id);
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinOp> Read()
        {
            return this._context.FinOps;
        }

        public IEnumerable<FinOp> GetFinOpByOrgAccount(int orgAccountId)
        {
            return this._context.FinOps
                .Include(a => a.OrgAccountFrom)
                .Include(a => a.OrgAccountTo)
                .Where(a => orgAccountId == (a.OrgAccountTo != null ? a.OrgAccountTo.Id : a.OrgAccountFrom.Id));
        }
    }
}
