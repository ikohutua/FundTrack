using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// This class encapsulates an instance of the DbContext and exposes each repository as a property
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IOrganizationsListRepository _organizationsListRepository;
        private readonly IRepository<User> _usersListRepository;
        private FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        /// <param name="organizationsListRepositoryParam">The organizations list repository parameter.</param>
        public UnitOfWork(FundTrackContext contextParam, IOrganizationsListRepository organizationsListRepositoryParam,
            IRepository<User> userListRepositoryParam)
        {
            this._context = contextParam;
            this._organizationsListRepository = organizationsListRepositoryParam;
            this._usersListRepository = userListRepositoryParam;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IRepository<User> UsersRepository
        {
            get
            {
                return _usersListRepository;
            }
        }

        /// <summary>
        /// Gets the organizations list repository.
        /// </summary>
        /// <value>
        /// The organizations list repository.
        /// </value>
        public IOrganizationsListRepository OrganizationsListRepository
        {
            get
            {
                return _organizationsListRepository;
            }
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
