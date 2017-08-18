using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for one unit of work whis repositories
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the event image repository.
        /// </summary>
        /// <value>
        /// The event image repository.
        /// </value>
        IRepository<EventImage> EventImageRepository { get; }

        /// <summary>
        /// Gets the organizations list repository.
        /// </summary>
        /// <value>
        /// The organizations list repository.
        /// </value>
        IOrganizationsForFilteringRepository OrganizationsForFilteringRepository { get; }

        /// <summary>
        /// Gets the users repository.
        /// </summary>
        /// <value>
        /// The users repository.
        /// </value>
        IUserResporitory UsersRepository { get; }

        /// <summary>
        /// Gets the event repository.
        /// </summary>
        /// <value>
        /// The event repository.
        /// </value>
        IEventManagementRepository EventRepository { get; }

        /// <summary>
        /// Gets the organization repository
        /// </summary>
        IOrganizationRepository OrganizationRepository { get; }

        /// <summary>
        /// Gets the membership repository.
        /// </summary>
        /// <value>
        /// The membership repository.
        /// </value>
        IMembershipRepository MembershipRepository { get; }

        /// <summary>
        /// Gets address repository
        /// </summary>
        IRepository<Address> AddressRepository { get; }

        /// <summary>
        /// Gets bank account repository
        /// </summary>
        IRepository<BankAccount> BankAccountRepository { get; }

        /// <summary>
        /// Gets organization address repository
        /// </summary>
        IRepository<OrgAddress> OrganizationAddressRepository { get; }

        /// <summary>
        /// Gets role repository
        /// </summary>
        IRepository<Role> RoleRepository { get; }

        /// <summary>
        /// Gets requested item repository
        /// </summary>
        IRequestedItemRepository RequestedItemRepository { get; }

        /// <summary>
        /// Gets the user response repository.
        /// </summary>
        /// <value>
        /// The user response repository.
        /// </value>
        IUserResponseRepository UserResponseRepository { get; }

        /// <summary>
        /// Gets the repo
        /// </summary>
        IRepository<OfferedItem> OfferedItemRepository { get; }
        
        /// <summary>
        /// Gets the repo
        /// </summary>
        IStatusRepository StatusRepository { get; }

        /// <summary>
        /// Gets the bank import detail repository.
        /// </summary>
        /// <value>
        /// The bank import detail repository.
        /// </value>
        IBankImportDetailRepository BankImportDetailRepository { get; }

        /// <summary>
        /// Gets the repo
        /// </summary>
        IGoodsCategoryRepository GoodsCategoryRepository { get; }
        
        /// <summary>
        /// Gets requested item image repository
        /// </summary>
        IRequestedItemImageRepository RequestedItemImageRepository { get; }



        /// <summary>
        /// Gets the repo
        /// </summary>
        IGoodsTypeRepository GoodsTypeRepository { get; }
        /// <summary>
        /// Gets the repo
        /// </summary>
        IOfferImagesRepository OfferImagesRepository { get; }
        IOrganizationAccountRepository OrganizationAccountRepository { get; }
        IRepository<Currency> CurrencyRepositry { get; }
        ITargetRepository TargetRepository { get; }
        IDonationRepository DonationRepository { get; }

        /// <summary>
        /// Gets the fin op repository.
        /// </summary>
        /// <value>
        /// The fin op repository.
        /// </value>
        IFinOpRepository FinOpRepository { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// 
        void SaveChanges();
    }
}
