using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for with Requested item repository image
    /// </summary>
    public interface IRequestedItemImageRepository : IRepository<RequestedItemImage>
    {
        /// <summary>
        /// Save list of images in database
        /// </summary>
        /// <param name="images"></param>
        void SaveListOfImages(IEnumerable<RequestedItemImage> images);

        /// <summary>
        /// Gets all images from database
        /// </summary>
        /// <returns>List of images</returns>
        IEnumerable<RequestedItemImage> GetImagesByRequestedItemId(int requestedItemId);

        /// <summary>
        /// Delete images from database by requested item id
        /// </summary>
        /// <param name="requestedItemId"></param>
        void DeleteImagesByRequestedItemId(int requestedItemId);
    }
}
