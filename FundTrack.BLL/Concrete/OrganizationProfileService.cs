using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using System.Collections.Generic;
using System;
using FundTrack.DAL.Concrete;
using FundTrack.Infrastructure;
using System.Threading.Tasks;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Service to work with organization profile
    /// </summary>
    public class OrganizationProfileService : IOrganizationProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManagementService _imgManageService;

        /// <summary>
        /// Creates new instance of OrganizationProfileService
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public OrganizationProfileService(IUnitOfWork unitOfWork, IImageManagementService imgManageService)
        {
            _unitOfWork = unitOfWork;
            _imgManageService = imgManageService;
        }

        /// <summary>
        /// Gets general information about organization by its id
        /// </summary>
        /// <param name="id"> Id of organization </param>
        /// <returns>OrganizationViewModel</returns>
        public OrganizationViewModel GetOrganizationById(int id)
        {
            var organization = _unitOfWork.OrganizationRepository.Get(id);
            var result = new OrganizationViewModel
            {
                Id = organization.Id,
                Name = organization.Name,
                Description = organization.Description,
                IsBanned = false,
                LogoUrl = organization.LogoUrl
            };
            return result;
        }

        /// <summary>
        /// Edits description of organization
        /// </summary>
        /// <param name="item">OrganizationViewModel to edit</param>
        /// <returns>Edited OrganizationViewModel</returns>
        public OrganizationViewModel EditDescription(OrganizationViewModel item)
        {
            var organizationToUpdate = new Organization { Id = item.Id, Name = item.Name, Description = item.Description };
            var update = _unitOfWork.OrganizationRepository.Update(organizationToUpdate);
            _unitOfWork.SaveChanges();
            var result = new OrganizationViewModel { Id = update.Id, Description = update.Description, Name = update.Name, IsBanned = false };
            return result;
        }

        /// <summary>
        /// Gets all addresses of organization
        /// </summary>
        /// <param name="id"> Id of needed organization </param>
        /// <returns>Returs organization id and its addresses</returns>
        public EditAddressViewModel GetOrgAddress(int id)
        {
            var addressOrg = _unitOfWork.OrganizationAddressRepository.Read().Where(a => a.OrgId == id);
            List<Address> addresses = new List<Address>();
            foreach (var orgAddress in addressOrg)
            {
                addresses.Add(_unitOfWork.AddressRepository.Read().FirstOrDefault(a => a.Id == orgAddress.AddressId));
            }
            var result = new EditAddressViewModel { OrgId = id, ErrorMessage = string.Empty, Addresses = new AddressViewModel[addresses.Count()] };
            int iterator = 0;
            foreach (var address in addresses)
            {
                result.Addresses[iterator] = new AddressViewModel
                {
                    Id = address.Id,
                    City = address.City,
                    Country = address.Country,
                    House = address.Building,
                    Street = address.Street
                };
                iterator++;
            }
            return result;
        }

        /// <summary>
        /// Edits organization address
        /// </summary>
        /// <param name="item">Address to edit</param>
        /// <returns>Edited list of addresses</returns>
        public EditAddressViewModel EditAddress(AddressViewModel item)
        {
            var addressToUpdate = new Address
            {
                Id = item.Id,
                Building = item.House,
                City = item.City,
                Country = item.Country,
                Street = item.Street
            };
            var result = _unitOfWork.AddressRepository.Update(addressToUpdate);
            _unitOfWork.SaveChanges();
            item = new AddressViewModel
            {
                Id = result.Id,
                City = result.City,
                Country = result.Country,
                House = result.Building,
                Street = result.Street
            };
            var orgAddress = _unitOfWork.OrganizationAddressRepository.Read().FirstOrDefault(o => o.AddressId == item.Id);
            return GetOrgAddress(orgAddress.OrgId);
        }

        /// <summary>
        /// Adds organization address
        ///</summary>
        /// <param name="addresses">Address to add</param>
        /// <returns>Organization Addresses</returns>
        public EditAddressViewModel AddAddresses(EditAddressViewModel address)
        {
            var addressToAdd = new Address
            {
                Building = address.Addresses.First().House,
                Street = address.Addresses.First().Street,
                City = address.Addresses.First().City,
                Country = address.Addresses.First().Country
            };
            var addedAddress = _unitOfWork.AddressRepository.Create(addressToAdd);
            var orgAddress = new OrgAddress { AddressId = addedAddress.Id, OrgId = address.OrgId };
            _unitOfWork.OrganizationAddressRepository.Create(orgAddress);
            _unitOfWork.SaveChanges();
            return GetOrgAddress(address.OrgId);
        }

        /// <summary>
        /// Deletes address by its id
        /// </summary>
        /// <param name="id">Id of address to delete</param>
        public void DeleteAddress(int id)
        {
            var addressToDelete = _unitOfWork.AddressRepository.Get(id);
            _unitOfWork.AddressRepository.Delete(addressToDelete.Id);
            _unitOfWork.SaveChanges();
        }

        private IEnumerable<OrganizationViewModel> convertOrganizationsToOrganizationViewModel(IEnumerable<Organization> organizations)
        {
            return organizations.Select(x => convertOrganizationToOrganizationViewModel(x));
        }

        private OrganizationViewModel convertOrganizationToOrganizationViewModel(Organization organization)
        {
            return new OrganizationViewModel
            {
                Description = organization.Description,
                LogoUrl = organization.LogoUrl,
                Name = organization.Name,
                Id = organization.Id
            };
        }

        public IEnumerable<OrganizationViewModel> GetAllOrganizations()
        {
            var allOrganizations = _unitOfWork.OrganizationRepository.Read();
            return convertOrganizationsToOrganizationViewModel(allOrganizations);
        }

        /// <summary>
        /// Get all detail about organization
        /// </summary>
        /// <param name="id">Id of organization</param>
        public OrganizationDetailViewModel GetOrganizationDetail(int id)
        {
            OrganizationDetailViewModel organizationDetail = new OrganizationDetailViewModel();
            try
            {
                OrganizationViewModel organization = GetOrganizationById(id);
                User orgAdmin = _unitOfWork.MembershipRepository.GetOrganizationAdmin(organization.Id);
                organizationDetail.Organization = organization;
                // if found admin of organization and he has phone
                if (orgAdmin != null && orgAdmin.Phones.Any())
                {
                    AddAdminInfoToOrgDetailModel(organizationDetail, orgAdmin);
                }
                AddAccountsInfoToOrgDetailModel(organizationDetail);
            }
            catch (NullReferenceException ex)
            {
                throw new DataAccessException(ErrorMessages.CantFindDataById, ex);
            }
            return organizationDetail;
        }

        private IEnumerable<OrgAccountDetailViewModel> ConvertListOrgAccountToListOrgAccountDetailViewModel(IEnumerable<OrgAccount> accounts)
        {
            return accounts
                .Select(x => ConvertOrgAccountToOrgAccountDetailViewModel(x))
                .ToList();
        }

        private OrgAccountDetailViewModel ConvertOrgAccountToOrgAccountDetailViewModel(OrgAccount account)
        {
            OrgAccountDetailViewModel orgAccountDetail = new OrgAccountDetailViewModel
            {
                AccountDescription = account.Description,
                Name = account.OrgAccountName
            };

            orgAccountDetail.Target = account.Target?.TargetName;

            if (account.BankAccount != null && account.BankAccount.CardNumber != null)
            {
                orgAccountDetail.CardNumber = account.BankAccount.CardNumber;
            }
            return orgAccountDetail;
        }


        /// <summary>
        /// Add to organization detail view model information about admin.
        /// </summary>
        /// <param name="organizationDetail">Instance of organization detail for fill.</param>
        /// <param name="orgAdminId">Admin identifier.</param>
        private void AddAdminInfoToOrgDetailModel(OrganizationDetailViewModel organizationDetail, User orgAdmin)
        {
            PersonViewModel adminViewModel = new PersonViewModel();
            adminViewModel.FirstName = orgAdmin.FirstName;
            adminViewModel.LastName = orgAdmin.LastName;
            if (orgAdmin.Phones.Count != 0)
            {
                adminViewModel.PhoneNumber = orgAdmin.Phones.FirstOrDefault().Number;
            }
            organizationDetail.Admin = adminViewModel;
        }

        /// <summary>
        /// Add to organization detail view model information about accounts.
        /// </summary>
        /// <param name="organizationDetail">Instance of organization detail for fill.</param>
        private void AddAccountsInfoToOrgDetailModel(OrganizationDetailViewModel organizationDetail)
        {
            int orgId = organizationDetail.Organization.Id;
            IEnumerable<OrgAccount> allOrgAccounts = _unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(orgId);
            IEnumerable<OrgAccount> filteredAccounts = allOrgAccounts
                .Where(x => x.BankAccId != null && x.BankAccount.CardNumber != null)
                .ToList();
            organizationDetail.OrgAccountsList = ConvertListOrgAccountToListOrgAccountDetailViewModel(filteredAccounts);
        }

        public EditLogoViewModel EditLogo(EditLogoViewModel item)
        {
            var organization = _unitOfWork.OrganizationRepository.Get(item.OrganizationId);
            if (organization!=null)
            {
                var task =  _imgManageService.UploadImage(Convert.FromBase64String(item.Base64Code));
                Task.WhenAll(task);
                organization.LogoUrl = task.Result;
                _unitOfWork.SaveChanges();

                item.LogoUrl = AzureStorageConfiguration.GetImageUrl(organization.LogoUrl);
            }
            else
            {
                throw new BusinessLogicException(ErrorMessages.BadRequestMessage);
            }
            return item;
        }
    }
}
