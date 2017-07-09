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
        IRepository<Event> EventRepository { get; }

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
        /// Gets OrganizationAccount repository
        /// </summary>
        IRepository<OrgAccount> OrganizationAccountRepository { get; }

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
        /// Gets the repo
        /// </summary>
        IRepository<OfferedItem> OfferedItemRepository { get; }
        /// <summary>
        /// Gets the repo
        /// </summary>
        IStatusRepository StatusRepository { get; }
        /// <summary>
        /// Gets the repo
        /// </summary>
        IGoodsCategoryRepository GoodsCategoryRepository { get; }
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        void SaveChanges();
    }
}
