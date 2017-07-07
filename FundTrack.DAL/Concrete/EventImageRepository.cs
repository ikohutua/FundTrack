using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Repositoruy for work with EventImage
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRepository{FundTrack.DAL.Entities.EventImage}" />
    public sealed class EventImageRepository : IRepository<EventImage>
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventImageRepository"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        public EventImageRepository(FundTrackContext contextParam)
        {
            this._context = contextParam;
        }

        /// <summary>
        /// Creates new entry in data base
        /// </summary>
        /// <param name="item">EventImage item</param>
        /// <returns>EventImage added item</returns>
        public EventImage Create(EventImage item)
        {
            var newEventImage = this._context.EventImages.Add(item);
            return newEventImage.Entity;
        }

        /// <summary>
        /// Deletes entry from data base
        /// </summary>
        /// <param name="id">Recives id of entry</param>
        public void Delete(int id)
        {
            this._context.EventImages.Remove(this.Get(id));
        }

        /// <summary>
        /// Gets one image
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one image</returns>
        public EventImage Get(int id)
        {
            return this._context.EventImages.Find(id);
        }

        /// <summary>
        /// Gets all entries in database
        /// </summary>
        /// <returns>
        /// Collection all entries
        /// </returns>
        public IEnumerable<EventImage> Read()
        {
            return this._context.EventImages;
        }

        /// <summary>
        /// Updates item in data base
        /// </summary>
        /// <param name="item">EventImage item</param>
        /// <returns>Updated item</returns>
        public EventImage Update(EventImage item)
        {
            var updateditem = this._context.EventImages.Update(item);
            return updateditem.Entity;
        }
    }
}
