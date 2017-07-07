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
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<OrgAddress> _orgAddressRepository;
        private readonly IRepository<BankAccount> _bankAccountRepository;
        private readonly IRepository<OrgAccount> _orgAccountRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRequestedItemRepository _requestedItemRepository;


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
              IMembershipRepository membershipRepositoryParam,
              IRepository<Address> addressRepository,
              IRepository<OrgAddress> orgAddressRepository,
              IRepository<BankAccount> bankAccountRepository,
              IRepository<OrgAccount> orgAccountRepository, 
              IRepository<Role> roleRepository,
              IRequestedItemRepository requestedItemRepository)
        {
            this._context = contextParam;
            this._organizationsListRepository = organizationsListRepositoryParam;
            this._usersListRepository = userListRepositoryParam;
            this._membershipRepository = membershipRepositoryParam;
            _eventRepository = eventRepository;
            _organizationRepository = organizationRepository;
            _addressRepository = addressRepository;
            _orgAddressRepository = orgAddressRepository;
            _bankAccountRepository = bankAccountRepository;
            _orgAccountRepository = orgAccountRepository;
            _roleRepository = roleRepository;
            this._requestedItemRepository = requestedItemRepository;
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
        /// Gets addresses repository.
        /// </summary>
        /// <value>
        /// Address repository.
        /// </value>
        public IRepository<Address> AddressRepository
        {
            get
            {
                return _addressRepository;
            }
        }

        /// <summary>
        /// Gets organization addresses repository.
        /// </summary>
        /// <value>
        /// Organization address repository.
        /// </value>
        public IRepository<OrgAddress> OrganizationAddressRepository
        {
            get
            {
                return _orgAddressRepository;
            }
        }

        /// <summary>
        /// Gets bank account repository.
        /// </summary>
        /// <value>
        /// Bank account repository.
        /// </value>
        public IRepository<BankAccount> BankAccountRepository
        {
            get
            {
                return _bankAccountRepository;
            }
        }

        /// <summary>
        /// Gets organization account repository.
        /// </summary>
        /// <value>
        /// Organization account repository.
        /// </value>
        public IRepository<OrgAccount> OrganizationAccountRepository
        {
            get
            {
                return _orgAccountRepository;
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
        /// Gets role repository.
        /// </summary>
        /// <value>
        /// Role repository.
        /// </value>
        public IRepository<Role> RoleRepository
        {
            get
            {
                return _roleRepository;
            }
        }

        /// <summary>
        /// Gets requestedItem repository
        /// </summary>
        public IRequestedItemRepository RequestedItemRepository
        {
            get
            {
                return this._requestedItemRepository;
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
