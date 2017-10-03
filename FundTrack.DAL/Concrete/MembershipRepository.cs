using System.Collections.Generic;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FundTrack.Infrastructure;

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
            var itemToUpdate = context.Membershipes.FirstOrDefault(i => i.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.OrgId = item.OrgId;
                itemToUpdate.RoleId = item.RoleId;
                itemToUpdate.UserId = item.UserId;

                return itemToUpdate;
            }
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
            return (int)this.context.Membershipes
                              .FirstOrDefault(m => m.UserId == userId)
                              .OrgId;
        }

        /// <summary>
        /// Gets admin of organization id. 
        /// </summary>
        /// <param name="organizationdId">The organization identifier.</param>
        /// <returns>
        /// Instance of user
        /// </returns>
        public User GetOrganizationAdmin(int organizationdId)
        {
            // Role id for admin = 2
            Membership orgAdminMembership = context.Membershipes
                                            .Include(x=>x.User)
                                            .Include(x=>x.User.Phones)
                                            .Where(x => x.OrgId == organizationdId && x.RoleId == Constants.AdminRoleId)
                                            .FirstOrDefault();
            return orgAdminMembership?.User;
        }
    }
}
