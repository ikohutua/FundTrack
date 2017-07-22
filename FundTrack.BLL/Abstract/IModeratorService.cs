using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Service to work with moderators
    /// </summary>
    public interface IModeratorService
    {
        /// <summary>
        /// Gets moderators of organization
        /// </summary>
        /// <param name="organizationId">Id of organization</param>
        /// <returns>All moderators</returns>
        IEnumerable<ModeratorViewModel> GetOrganizationModerators(int organizationId);
        
        /// <summary>
        /// Get users to become moderator
        /// </summary>
        /// <param name="organizationId"> Id of organization</param>
        /// <returns>IEnumerable of users</returns>
        IEnumerable<CreateModeratorViewModel> GetAvailableUsers(int organizationId);

        /// <summary>
        /// Adds moderator to organization
        /// </summary>
        /// <param name="moderator">CreateModeratorViewModel</param>
        /// <returns>Created moderator</returns>
        ModeratorViewModel CreateModerator(CreateModeratorViewModel moderator);

        /// <summary>
        /// Deletes moderator
        /// </summary>
        /// <param name="moderatorLogin"> Login of moderator to delete</param>
        /// <returns>lModeratorViewModel</returns>
        ModeratorViewModel DeactivateModerator(string moderatorLogin);
    }
}
