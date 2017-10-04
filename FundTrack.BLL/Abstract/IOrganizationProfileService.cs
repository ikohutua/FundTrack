using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for work with organization profile
    /// </summary>
    public interface IOrganizationProfileService 
    {
        /// <summary>
        /// Gets OrganizationViewModel by its id
        /// </summary>
        /// <param name="id">Id of organization</param>
        /// <returns>OrganizationViewModel</returns>
        OrganizationViewModel GetOrganizationById(int id);

        /// <summary>
        /// Edits description of organization
        /// </summary>
        /// <param name="item"> Organization to edit</param>
        /// <returns> Edited organization</returns>
        OrganizationViewModel EditDescription(OrganizationViewModel item);

        /// <summary>
        /// Gets organization addresses by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EditAddressViewModel GetOrgAddress(int id);

        /// <summary>
        /// Edits addresses of organization
        /// </summary>
        /// <param name="item">Org address to edit</param>
        /// <returns>Edited view model</returns>
        EditAddressViewModel EditAddress(AddressViewModel item);

        /// <summary>
        /// Adds organization address
        ///</summary>
        /// <param name="addresses">Address to add</param>
        /// <returns>Organization Addresses</returns>
        EditAddressViewModel AddAddresses(EditAddressViewModel addresses);

        /// <summary>
        /// Deletes address by its id
        /// </summary>
        /// <param name="id">Id of address to delete</param>
        void DeleteAddress(int id);

        /// <summary>
        /// Get list of all organizations
        /// </summary>
        IEnumerable<OrganizationViewModel> GetAllOrganizations();

        /// <summary>
        /// Get details about organization by id
        /// </summary>
        /// <param name="id">Id of organization</param>
        OrganizationDetailViewModel GetOrganizationDetail(int id);

        /// <summary>
        /// Edite current logo
        /// </summary>
        /// <param name="item">Edit logo view mode</param>
        /// <returns>Edited logo view mode</returns>
        EditLogoViewModel EditLogo(EditLogoViewModel item);
    }
}
