using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Role repository
    /// </summary>
    public class RoleRepository : IRepository<Role>
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Creates new instance of role repository
        /// </summary>
        /// <param name="context">Db context to create new instance of role repository</param>
        public RoleRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates role.
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns></returns>
        public Role Create(Role item)
        {
            _context.Roles.Add(item);
            return item;
        }

        /// <summary>
        /// Deletes role from data base
        /// </summary>
        /// <param name="id">Recives id of role</param>
        public void Delete(int id)
        {
            Role _role = _context.Roles.FirstOrDefault(c => c.Id == id);
            if (_role != null)
                _context.Roles.Remove(_role);
        }

        /// <summary>
        /// Gets role by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Role Get(int id)
        {
            return _context.Roles.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all roles in database
        /// </summary>
        /// <returns>
        /// Collection all roles
        /// </returns>
        public IEnumerable<Role> Read()
        {
            return _context.Roles;
        }

        /// <summary>
        /// Updates the specified role.
        /// </summary>
        /// <param name="item">Role to update.</param>
        public Role Update(Role item)
        {
            _context.Update(item);
            return item;
        }
    }
}
