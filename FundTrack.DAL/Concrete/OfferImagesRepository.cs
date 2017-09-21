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
    public class OfferImagesRepository : IOfferImagesRepository
    {
        private readonly FundTrackContext _context;
        public OfferImagesRepository(FundTrackContext context)
        {
            _context = context;
        }

        public OfferedItemImage Create(OfferedItemImage item)
        {
            this._context.Add(item);
            return item;
        }

        public IEnumerable<OfferedItemImage> CreateMany(IEnumerable<OfferedItemImage> collection)
        {
            this._context.AddRange(collection);
            return collection;
        }

        public void Delete(int id)
        {
            OfferedItemImage offeredItemImage = _context.OfferedItemImages.FirstOrDefault(a => a.Id == id);
            if (offeredItemImage != null)
                _context.OfferedItemImages.Remove(offeredItemImage);
        }

        public void DeleteOfferedItemImagesByOfferedItemId(int offeredItemId)
        {
            _context.OfferedItemImages.RemoveRange(_context.OfferedItemImages.Where(x => x.OfferedItemId == offeredItemId));
            _context.SaveChanges();
        }

        public OfferedItemImage Get(int id)
        {
            return this._context.OfferedItemImages.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<OfferedItemImage> GetOfferedItemImageByOfferItemId(int offerItemId)
        {
            return this._context.OfferedItemImages.Where(a => a.OfferedItemId == offerItemId);
                
        }

        public IEnumerable<OfferedItemImage> Read()
        {
            return _context.OfferedItemImages;
        }

        public OfferedItemImage Update(OfferedItemImage item)
        {
            throw new NotImplementedException();
        }
    }
}
