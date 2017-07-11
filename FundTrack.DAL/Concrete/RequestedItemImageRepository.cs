using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    public class RequestedItemImageRepository : IRequestedItemImageRepository
    {
        private readonly FundTrackContext _context;

        public RequestedItemImageRepository(FundTrackContext context)
        {
            this._context = context;
        }

        public RequestedItemImage Create(RequestedItemImage item)
        {
            var createdImage = this._context.RequestedItemImages.Add(item);

            return createdImage.Entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public RequestedItemImage Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestedItemImage> Read()
        {
            var images = this._context.RequestedItemImages
                .Include(i => i.RequestedItem);

            return images;
        }

        public RequestedItemImage Update(RequestedItemImage item)
        {
            throw new NotImplementedException();
        }
    }
}
