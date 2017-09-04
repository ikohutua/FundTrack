using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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
                throw new DataAccessException("Can't update item");

             itemToUpdate.TargetName = item.TargetName;
             itemToUpdate.OrganizationId = item.OrganizationId;

            _context.Update(itemToUpdate);
            return itemToUpdate;
        }

        public void Delete(int id)
        {
            var target = _context.Targets.FirstOrDefault(c => c.Id == id);

            if (target == null)
                throw new DataAccessException("Can't delete item. Invalid id.");

            _context.Targets.Remove(target);
        }
    }
}
