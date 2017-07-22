using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Address repository
    /// </summary>
    public class AddressRepository : IRepository<Address>
    {
        private readonly FundTrackContext _context; 

        /// <summary>
        /// Creates new instance of address repository
        /// </summary>
        /// <param name="context">Db context to create new instance of address repository</param>
        public AddressRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates address.
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns></returns>
        public Address Create(Address item)
        {
            var added = _context.Addresses.Add(item);
            return added.Entity;
        }

        /// <summary>
        /// Deletes address from data base
        /// </summary>
        /// <param name="id">Recives id of address</param>
        public void Delete(int id)
        {
            Address _address = _context.Addresses.FirstOrDefault(c => c.Id == id);
            if (_address != null)
                _context.Addresses.Remove(_address);
        }

        /// <summary>
        /// Gets address by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Address Get(int id)
        {
            return _context.Addresses.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all addresses in database
        /// </summary>
        /// <returns>
        /// Collection all addresses
        /// </returns>
        public IEnumerable<Address> Read()
        {
            return _context.Addresses;
        }

        /// <summary>
        /// Updates the specified adrress.
        /// </summary>
        /// <param name="item">Address to update.</param>
        public Address Update(Address item)
        {
            var itemToUpdate = _context.Addresses.FirstOrDefault(i => i.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.City = item.City;
                itemToUpdate.Building = item.Building;
                itemToUpdate.Country = item.Country;
                itemToUpdate.Street = item.Street;
                itemToUpdate.Id = item.Id;

                return itemToUpdate;
            }
            return item;
        }
    }
}
