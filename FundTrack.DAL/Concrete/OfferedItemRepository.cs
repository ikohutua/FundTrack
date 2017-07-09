using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Concrete
{
    public class OfferedItemRepository : IRepository<OfferedItem>
    {
        private readonly FundTrackContext _context;
        public OfferedItemRepository(FundTrackContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates new OfferedItem entity
        /// </summary>
        /// <param name="item">OfferedItem</param>
        /// <returns>OfferedItem</returns>
        public OfferedItem Create(OfferedItem item)
        {
            _context.OfferedItems.Add(item);
            return item;
        }
        /// <summary>
        /// Deletes OfferedItem by its id
        /// </summary>
        /// <param name="id">OfferedItem id</param>
        public void Delete(int id)
        {
            OfferedItem offeredItem = _context.OfferedItems.FirstOrDefault(x => x.Id == id);
            if (offeredItem != null)
                _context.OfferedItems.Remove(offeredItem);
        }
        /// <summary>
        /// Gets OfferedItem by its id
        /// </summary>
        /// <param name="id">OfferedItem id</param>
        /// <returns>OfferedItem</returns>
        public OfferedItem Get(int id)
        {
            return _context.OfferedItems.FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Reads all OfferedItems
        /// </summary>
        /// <returns>collection of OfferedItems</returns>
        public IEnumerable<OfferedItem> Read()
        {
            return this._context.OfferedItems.
                Include(a => a.OfferedItemImages).
                Include(b => b.GoodsCategory).
                Include(c => c.Status).
                Include(d=>d.GoodsCategory.GoodsType).
                ToList();
        }
        /// <summary>
        /// Updates OfferedItem
        /// </summary>
        /// <param name="item">OfferedItem</param>
        /// <returns>OfferedItem</returns>
        public OfferedItem Update(OfferedItem item)
        {
            _context.Update(item);
            return item;
        }
    }
}
