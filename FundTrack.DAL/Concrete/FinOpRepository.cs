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

        public FinOp Update(FinOp finOp)
        {
           this._context.FinOps.Update(finOp);
            return finOp;
        }

        public FinOp GetById(int id)
        {
           return this._context.FinOps
                .Include(fo=>fo.OrgAccountTo)
                .FirstOrDefault(fo => fo.Id == id);
        }
    }
}
