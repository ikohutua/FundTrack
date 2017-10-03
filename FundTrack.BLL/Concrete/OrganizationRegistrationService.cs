﻿using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using FundTrack.DAL.Entities;
using System.Threading.Tasks;
using System;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Implements IOrganizationRegistrationService
    /// </summary>
    public class OrganizationRegistrationService: IOrganizationRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManagementService _imgManageService;
        /// <summary>
        /// Creates new instance of OrganizationRegistrationService
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public OrganizationRegistrationService (IUnitOfWork unitOfWork, IImageManagementService imgManageService)
        {
            _unitOfWork = unitOfWork;
            _imgManageService = imgManageService;
        }
        /// <summary>
        /// Registers new organization
        /// </summary>
        /// <param name="item"> Item to register </param>
        /// <returns>Registered item</returns>
        public OrganizationRegistrationViewModel RegisterOrganization (OrganizationRegistrationViewModel item)
        {
            var checkOrganization = _unitOfWork.OrganizationRepository.Read().Where(o => o.Name == item.Name).FirstOrDefault();
            if (checkOrganization == null)
            {
                var user = _unitOfWork.UsersRepository.GetUser(item.AdministratorLogin);
                if (user != null)
                {
                    var role = _unitOfWork.RoleRepository.Read().Where(r => r.Name == "admin").FirstOrDefault();
                    var checkMembership = _unitOfWork.MembershipRepository.Read().Where(m => (m.UserId == user.Id) && (m.RoleId == role.Id)).FirstOrDefault();
                    if (checkMembership == null)
                    {
                        var organization = new Organization { Name = item.Name, Description = item.Description };

                        if (!String.IsNullOrEmpty(item.Base64Code))
                        {
                            var task = _imgManageService.UploadImage(Convert.FromBase64String(item.Base64Code));
                            Task.WhenAll(task);
                            //TODO: uncomment 
                            //organization.LogoUrl = task.Result;
                        }

                        _unitOfWork.OrganizationRepository.Create(organization);
                        _unitOfWork.SaveChanges();

                        var membership = new Membership { UserId = user.Id, OrgId = organization.Id, RoleId = role.Id };
                        _unitOfWork.MembershipRepository.Create(membership);
                        _unitOfWork.SaveChanges();

                        var address = new Address { City = item.City, Street = item.Street, Country = item.Country, Building = item.House };
                        _unitOfWork.AddressRepository.Create(address);
                        _unitOfWork.SaveChanges();

                        var orgAddres = new OrgAddress { OrgId = organization.Id, AddressId = address.Id };
                        _unitOfWork.OrganizationAddressRepository.Create(orgAddres);
                        _unitOfWork.SaveChanges();

                        organization.OrgAddresses.Add(orgAddres);
                        _unitOfWork.SaveChanges();

                        var result = new OrganizationRegistrationViewModel
                        {
                            Name = organization.Name,
                            AdministratorLogin = item.AdministratorLogin,
                            City = address.City,
                            Country = address.Country,
                            Street = address.Street,
                            Description = organization.Description,
                            House = address.Building
                        };
                        return result;
                    }
                    else
                    {
                        item.UserError = "Цей користувач вже адмініструє організацію";
                        return item;
                    }
                }
                item.UserError = "Користувача з таким логіном не зареєстровано";
                return item;
            }
            item.NameError = "Організація з такою назвою вже існує";
            return item;
        }
    }
}
