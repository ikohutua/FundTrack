using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public sealed class UserResponseRepository : IUserResponseRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserResponseRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserResponseRepository(FundTrackContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Creates the UserResponse in db.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>UserResponse</returns>
        public UserResponse Create(UserResponse item)
        {
            var userResponse = this._context.UserResponses.Add(item);
            return userResponse.Entity;
        }

        /// <summary>
        /// Gets the UserResponse from db by his id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>UserResponse</returns>
        public UserResponse Get(int id)
        {
            return this._context.UserResponses
                                .Include(u => u.Status)
                                .Include(u => u.RequestedItem)
                                .Include(u => u.User)
                                .FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Deletes the UserResponse by his id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this._context.UserResponses.Remove(Get(id));
        }

        /// <summary>
        /// Reads all UserResponse from db.
        /// </summary>
        /// <returns>IEnumerable UserResponse</returns>
        public IEnumerable<UserResponse> Read()
        {
            return this._context.UserResponses;
        }

        /// <summary>
        /// Reads as queryable for pagination.
        /// </summary>
        /// <returns>List of user response</returns>
        public IQueryable<UserResponse> ReadAsQueryable()
        {
            return this._context.UserResponses;
        }

        /// <summary>
        /// Reads as queryable for pagination.
        /// </summary>
        /// <returns>List of user response</returns>
        public IQueryable<UserResponse> ReadForPagination(int OrganizationId, int ItemsPerPage, int CurrentPage)
        {
            return this._context.UserResponses
                                .Where(u => u.RequestedItem.OrganizationId == OrganizationId)
                                .GetItemsPerPage(ItemsPerPage, CurrentPage);
        }

        /// <summary>
        /// Updates the UserResponse in db.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>UserResponse</returns>
        public UserResponse Update(UserResponse item)
        {
            var userResponse = this._context.UserResponses.Update(item);
            return userResponse.Entity;
        }
    }
}
