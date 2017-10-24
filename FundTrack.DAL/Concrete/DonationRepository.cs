﻿using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Repository for Donation entity
    /// </summary>
    public class DonationRepository: IDonationRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Creates instance of DonateRepository
        /// </summary>
        /// <param name="context">Context to create instance</param>
        public DonationRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates new record in Donation table
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Created item</returns>
        public Donation Create(Donation item)
        {
            var created = _context.Donations.Add(item);
            return created.Entity;
        }

        /// <summary>
        /// Read all Donations in database
        /// </summary>
        /// <returns> IQueryable of donations</returns>
        public IQueryable<Donation> Read()
        {
            return _context.Donations
                .Include(d=>d.Target)
                .Include(d=>d.OrgAccount)
                .ThenInclude(ba=>ba.Organization);
        }

        /// <summary>
        /// get donation by id
        /// </summary>
        /// <param name="id">id of donation</param>
        /// <returns></returns>
        public Donation Get(int id)
        {
            return _context.Donations.FirstOrDefault(d => d.Id == id);
        }

        public Donation Update(Donation item)
        {
            var updated = _context.Donations.Update(item);
            return updated.Entity;
        }
    }
}
