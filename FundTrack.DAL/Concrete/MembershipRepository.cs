using System.Collections.Generic;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Repository from entity Membership
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IMembershipRepository" />
    public sealed class MembershipRepository : IMembershipRepository
    {
        private readonly FundTrackContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MembershipRepository(FundTrackContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>New membership</returns>
        public Membership Create(Membership item)
        {
            this.context.Membershipes.Add(item);
            return item;
        }

        /// <summary>
        /// Deletes the item by his identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.context.Membershipes.Remove(this.Get(id));
        }

        /// <summary>
        /// Gets the item for id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Membership Get(int id)
        {
            return this.context.Membershipes.Find(id);
        }

        /// <summary>
        /// Return all memberships from db.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Membership> Read()
        {
            return this.context.Membershipes;
        }

        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Exist user</returns>
        public Membership Update(Membership item)
        {
            this.context.Membershipes.Update(item);
            return item;
        }

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User role in db</returns>

        public string GetRole(int userId)
        {
            return this.context.Membershipes
                               .Include(u => u.Role)
                               .FirstOrDefault(m => m.UserId == userId)
                               .Role
                               .Name;
        }

        /// <summary>
        /// Determines whether  user has role in membership table.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// <c>true</c> if  user has role in membership table; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserHasRole(int userId)
        {
            return this.context.Membershipes.Any(m => m.UserId == userId);
        }

        public int GetOrganizationId(int userId)
        {
            return this.context.Membershipes
                              .FirstOrDefault(m => m.UserId == userId)
                              .OrgId;
        }
    }
}
