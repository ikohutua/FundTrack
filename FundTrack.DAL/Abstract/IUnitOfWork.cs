namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for one unit of work whis repositories
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the organizations list repository.
        /// </summary>
        /// <value>
        /// The organizations list repository.
        /// </value>
        IOrganizationsListRepository OrganizationsListRepository { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        void SaveChanges();
    }
}
