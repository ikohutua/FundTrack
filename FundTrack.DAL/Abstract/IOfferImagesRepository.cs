using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Abstract
{
    public interface IOfferImagesRepository:IRepository<OfferedItemImage>
    {
        /// <summary>
        /// Creates offered item image repository in database
        /// </summary>
        /// <param name="item">Offered item image entity</param>
        /// <returns></returns>
        new OfferedItemImage Create(OfferedItemImage item);
        /// <summary>
        /// Creates many offered item images together
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        IEnumerable<OfferedItemImage> CreateMany(IEnumerable<OfferedItemImage> collection);

        /// <summary>
        /// Deletes Offered Item Image entity by its id
        /// </summary>
        /// <param name="id">Offered Item image entity id</param>
        new void Delete(int id);

        /// <summary>
        /// Gets Offered Item Image entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new OfferedItemImage Get(int id);

        /// <summary>
        /// Reads all Offered Item Image entities from a Database
        /// </summary>
        /// <returns></returns>
        new IEnumerable<OfferedItemImage> Read();

        /// <summary>
        /// Updates Offered Item Image entity
        /// </summary>
        /// <param name="item">Offered Item Image entity</param>
        /// <returns>Updated Offered Item Image repository</returns>
        new OfferedItemImage Update(OfferedItemImage item);


        /// <summary>
        /// Deletes offered item images by offered item id property
        /// </summary>
        /// <param name="offeredItemId">offered item id property value</param>
        void DeleteOfferedItemImagesByOfferedItemId(int offeredItemId);
        /// <summary>
        /// Gets all offered item images that has specified offer item id field
        /// </summary>
        /// <param name="offerItemId">offer item id</param>
        /// <returns>collection of OfferedItemImages</returns>
        IEnumerable<OfferedItemImage> GetOfferedItemImageByOfferItemId(int offerItemId);

    }
}
