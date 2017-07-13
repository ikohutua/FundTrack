using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Requested item repository class
    /// </summary>
    public class RequestedItemImageRepository : IRequestedItemImageRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initialize new instance of RequestedItemRepository class
        /// </summary>
        /// <param name="context"></param>
        public RequestedItemImageRepository(FundTrackContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Save image in database
        /// </summary>
        /// <param name="item">Requested item model</param>
        /// <returns>Requested item entity</returns>
        public RequestedItemImage Create(RequestedItemImage item)
        {
            var createdImage = this._context.RequestedItemImages.Add(item);

            return createdImage.Entity;
        }

        /// <summary>
        /// Delete image from database
        /// </summary>
        /// <param name="id">Id of image</param>
        public void Delete(int id)
        {
            var image = this.Get(id);
            this._context.RequestedItemImages.Remove(image);
        }

        /// <summary>
        /// Gets image from database
        /// </summary>
        /// <param name="id">Id of image</param>
        /// <returns></returns>
        public RequestedItemImage Get(int id)
        {
            var image = this._context.RequestedItemImages.FirstOrDefault(i => i.Id == id);

            return image;
        }

        /// <summary>
        /// Gets all images from database
        /// </summary>
        /// <returns>List of images</returns>
        public IEnumerable<RequestedItemImage> Read()
        {
            var images = this._context.RequestedItemImages
                .Include(i => i.RequestedItem);

            return images;
        }

        /// <summary>
        /// Save list of of images for specific requested item
        /// </summary>
        /// <param name="images">List of requested items</param>
        public void SaveListOfImages(IEnumerable<RequestedItemImage> images)
        {
            foreach(var image in images)
            {
                this._context.RequestedItemImages.Add(image);
            }
        }

        /// <summary>
        /// Update image in database
        /// </summary>
        /// <param name="item">Requested item image</param>
        /// <returns></returns>
        public RequestedItemImage Update(RequestedItemImage item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets images from database by requested item id
        /// </summary>
        /// <param name="requestedItemId">Id of requested item</param>
        /// <returns></returns>
        public IEnumerable<RequestedItemImage> GetImagesByRequestedItemId(int requestedItemId)
        {
            var requestedItemImages = this._context.RequestedItemImages
                .Where(image => image.RequestedItemId == requestedItemId);

            return requestedItemImages;
        }

        /// <summary>
        /// Delete images from database by requested item id
        /// </summary>
        /// <param name="requestedItemId"></param>
        public void DeleteImagesByRequestedItemId(int requestedItemId)
        {
            var imagesToDelete = this.GetImagesByRequestedItemId(requestedItemId);

            foreach(var image in imagesToDelete)
            {
                this._context.RequestedItemImages.Remove(image);
            }            
        }
    }
}
