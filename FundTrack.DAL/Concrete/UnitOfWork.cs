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
        private readonly IOrganizationsForFilteringRepository _organizationsListRepository;
        private readonly IUserResporitory _usersListRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMembershipRepository _membershipRepository;

        private FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        /// <param name="organizationsListRepositoryParam">The organizations list repository parameter.</param>
        public UnitOfWork(FundTrackContext contextParam,
              IOrganizationsForFilteringRepository organizationsListRepositoryParam,
              IUserResporitory userListRepositoryParam,
              IRepository<Event> eventRepository,
              IOrganizationRepository organizationRepository,
              IMembershipRepository membershipRepositoryParam)
        {
            this._context = contextParam;
            this._organizationsListRepository = organizationsListRepositoryParam;
            this._usersListRepository = userListRepositoryParam;
            this._membershipRepository = membershipRepositoryParam;
            _eventRepository = eventRepository;
            _organizationRepository = organizationRepository;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IUserResporitory UsersRepository
        {
            get
            {
                return _usersListRepository;
            }
        }

        /// <summary>
        /// Gets the events repository.
        /// </summary>
        /// <value>
        /// The events repository.
        /// </value>
        public IRepository<Event> EventRepository
        {
            get
            {
                return _eventRepository;
            }
        }

        /// <summary>
        /// Gets organizations repository
        /// </summary>
        public IOrganizationRepository OrganizationRepository
        {
            get
            {
                return _organizationRepository;
            }
        }
        
        /// <summary>
        /// Gets the organizations list repository.
        /// </summary>
        /// <value>
        /// The organizations list repository.
        /// </value>
        public IOrganizationsForFilteringRepository OrganizationsForFilteringRepository
        {
            get
            {
                return _organizationsListRepository;
            }
        }

        /// <summary>
        /// Gets the membership repository.
        /// </summary>
        /// <value>
        /// The membership repository.
        /// </value>
        public IMembershipRepository MembershipRepository
        {
            get
            {
                return _membershipRepository;
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
