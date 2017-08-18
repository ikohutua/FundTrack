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
        private readonly IEventManagementRepository _eventRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IUserResponseRepository _userResponseRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<OrgAddress> _orgAddressRepository;
        private readonly IRepository<BankAccount> _bankAccountRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRequestedItemRepository _requestedItemRepository;
        private readonly IRepository<EventImage> _eventImageRepository;
        private readonly IRepository<OfferedItem> _offeredItemRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IGoodsCategoryRepository _goodsCategoryRepository;
        private readonly IRequestedItemImageRepository _requestedItemImageRepository;
        private readonly IGoodsTypeRepository _goodsTypeRepository;
        private readonly IOfferImagesRepository _offeredItemImageRepository;
        private readonly IBankImportDetailRepository _bankImportDetailRepository;
        private readonly IOrganizationAccountRepository _organizationAccountRepository;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IFinOpRepository _finOpRepository;

        private FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        /// <param name="organizationsListRepositoryParam">The organizations list repository parameter.</param>
        public UnitOfWork(FundTrackContext contextParam,
              IOrganizationsForFilteringRepository organizationsListRepositoryParam,
              IUserResporitory userListRepositoryParam,
              IEventManagementRepository eventRepository,
              IOrganizationRepository organizationRepository,
              IMembershipRepository membershipRepositoryParam,
              IRepository<Address> addressRepository,
              IRepository<OrgAddress> orgAddressRepository,
              IRepository<BankAccount> bankAccountRepository,
              IRequestedItemRepository requestedItemRepository,
              IRepository<Role> roleRepository,
              IRepository<EventImage> eventImageRepositoryParam,
              IRepository<OfferedItem> offeredItemRepository,
              IStatusRepository statusRepository,
              IUserResponseRepository userResponseRepository,
              IGoodsCategoryRepository goodsCategoryRepository,
              IRequestedItemImageRepository requestedItemImageRepository,
              IGoodsTypeRepository goodsTypeRepository,
              IOfferImagesRepository offeredItemImageRepository,
              IBankImportDetailRepository bankImportDetailRepository,
              IOrganizationAccountRepository organizationAccountRepository,
              IRepository<Currency> currencyRepository, 
              ITargetRepository targetRepository, 
              IDonationRepository donationRepository,
              IFinOpRepository finOpRepository
              )
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
            _roleRepository = roleRepository;
            _eventImageRepository = eventImageRepositoryParam;
            _offeredItemRepository = offeredItemRepository;
            _goodsCategoryRepository = goodsCategoryRepository;
            _userResponseRepository = userResponseRepository;
            _requestedItemRepository = requestedItemRepository;
            _statusRepository = statusRepository;
            _requestedItemImageRepository = requestedItemImageRepository;
            _goodsTypeRepository = goodsTypeRepository;
            _offeredItemImageRepository = offeredItemImageRepository;
            _bankImportDetailRepository = bankImportDetailRepository;
            _organizationAccountRepository = organizationAccountRepository;
            _currencyRepository = currencyRepository;
            _targetRepository = targetRepository;
            _donationRepository = donationRepository;
            _finOpRepository = finOpRepository;
        }

        /// <summary>
        /// Gets the event image repository.
        /// </summary>
        /// <value>
        /// The event image repository.
        /// </value>
        public IRepository<EventImage> EventImageRepository
        {
            get
            {
                return _eventImageRepository;
            }
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
        public IEventManagementRepository EventRepository
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
        /// Gets repo
        /// </summary>
        public IRepository<OfferedItem> OfferedItemRepository
        {
            get { return this._offeredItemRepository; }
        }

        /// <summary>
        /// Gets repo
        /// </summary>
        public IStatusRepository StatusRepository
        {
            get { return this._statusRepository; }
        }

        /// <summary>
        /// Gets repo
        /// </summary>
        public IGoodsCategoryRepository GoodsCategoryRepository
        {
            get { return this._goodsCategoryRepository; }
        }

        /// <summary>
        /// Gets the user response repository.
        /// </summary>
        /// <value>
        /// The user response repository.
        /// </value>
        public IUserResponseRepository UserResponseRepository
        {
            get
            {
                return this._userResponseRepository;
            }
        }
        /// <summary>
        /// Gets the repo
        /// </summary>
        public IGoodsTypeRepository GoodsTypeRepository
        {
            get { return this._goodsTypeRepository; }
        }


        /// <summary>
        /// Gets requested item image repository
        /// </summary>
        public IRequestedItemImageRepository RequestedItemImageRepository
        {
            get
            {
                return this._requestedItemImageRepository;
            }
        }
        /// <summary>
        /// Gets offered item image repository
        /// </summary>
        public IRepository<OfferedItemImage> OfferedItemImageRepository
        {
            get
            {
                return this._offeredItemImageRepository;
            }
        }

        /// <summary>
        /// Gets the repo
        /// </summary>
        public IOfferImagesRepository OfferImagesRepository
        {
            get
            {
                return this._offeredItemImageRepository;
            }
        }

        public IOrganizationAccountRepository OrganizationAccountRepository
        {
            get
            {
                return this._organizationAccountRepository;
            }
        }

        public IRepository<Currency> CurrencyRepositry
        {
            get
            {
                return this._currencyRepository;
            }
        }

        /// <summary>
        /// Gets the bank import detail repository.
        /// </summary>
        /// <value>
        /// The bank import detail repository.
        /// </value>
        public IBankImportDetailRepository BankImportDetailRepository
        {
            get
            {
                return this._bankImportDetailRepository;
            }
        }

        /// <summary>
        /// Returs Target Repository
        /// </summary>
        public ITargetRepository TargetRepository
        {
            get
            {
                return this._targetRepository;
            }
        }

        /// <summary>
        /// Returns Donation Repository
        /// </summary>
        public IDonationRepository DonationRepository
        {
            get
            {
                return _donationRepository;
            }
        }

        /// <summary>
        /// Gets the fin op repository.
        /// </summary>
        /// <value>
        /// The fin op repository.
        /// </value>
        public IFinOpRepository FinOpRepository
        {
            get
            {
                return _finOpRepository;
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
