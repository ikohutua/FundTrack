using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure;
using System.Linq;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using System.Collections.Generic;
namespace FundTrack.BLL.Concrete
{
    public class ModeratorService : IModeratorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModeratorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get moderators of organization
        /// </summary>
        /// <param name="organizationId"> Id of organization</param>
        /// <returns> Lists of moderators or null </returns>
        public IEnumerable<ModeratorViewModel> GetOrganizationModerators(int organizationId)
        {
            var role = _unitOfWork.RoleRepository.Read().FirstOrDefault(r => r.Name == UserRoles.Moderator);
            var membership = _unitOfWork.MembershipRepository.Read()
                .Where(m => (m.OrgId == organizationId) && (m.RoleId == role.Id));

            if (membership != null)
            {
                var result = new List<ModeratorViewModel>();
                foreach (var mem in membership)
                {
                    var user = _unitOfWork.UsersRepository.GetUserById(mem.UserId);
                    result.Add(new ModeratorViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Login = user.Login,
                        Email = user.Email
                    });
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get users to become moderator
        /// </summary>
        /// <param name="organizationId"> Id of organization</param>
        /// <returns>IEnumerable of users</returns>
        public IEnumerable<CreateModeratorViewModel> GetAvailableUsers(int organizationId)
        {
            var roleId = _unitOfWork.RoleRepository.Read().Where(r => r.Name == UserRoles.Partner).FirstOrDefault();
            var users = _unitOfWork.UsersRepository.GetUsersWithUnbannedStatus();
            var result = new List<CreateModeratorViewModel>();
            foreach(var user in users)
            {
                result.Add(new CreateModeratorViewModel
                {
                    Login = user.Login,
                    OrgId = organizationId
                });
            }
            return result;
        }

        /// <summary>
        /// Adds moderator to organization
        /// </summary>
        /// <param name="moderator">CreateModeratorViewModel</param>
        /// <returns>Created moderator</returns>
        public ModeratorViewModel CreateModerator(CreateModeratorViewModel moderator)
        {
            var roleId = _unitOfWork.RoleRepository.Read().Where(r => r.Name == UserRoles.Moderator).FirstOrDefault().Id;
            var user = _unitOfWork.UsersRepository.GetUser(moderator.Login);
            var membershipToUp = _unitOfWork.MembershipRepository.Read().Where(m => m.UserId == user.Id).FirstOrDefault();
            var membership = new Membership { Id = membershipToUp.Id, OrgId = moderator.OrgId, UserId = user.Id, RoleId = roleId };
            _unitOfWork.MembershipRepository.Update(membership);
            _unitOfWork.SaveChanges();
            var result = new ModeratorViewModel
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login, 
                Email = user.Email
            };
            return result;
        }

        /// <summary>
        /// Deletes moderator
        /// </summary>
        /// <param name="moderatorLogin"> Login of moderator to delete</param>
        /// <returns>lModeratorViewModel</returns>
        public ModeratorViewModel DeactivateModerator(string moderatorLogin)
        {
            var roleId = _unitOfWork.RoleRepository.Read().Where(r => r.Name == UserRoles.Partner).FirstOrDefault().Id;
            var user = _unitOfWork.UsersRepository.GetUser(moderatorLogin);
            var membershipToUpdate = _unitOfWork.MembershipRepository.Read().Where(m => m.UserId == user.Id).FirstOrDefault();
            var membership = new Membership { Id = membershipToUpdate.Id, RoleId = roleId, UserId = user.Id, OrgId = 1 };
            _unitOfWork.MembershipRepository.Update(membership);
            _unitOfWork.SaveChanges();
            var result = new ModeratorViewModel
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return result;
        }
    }
}
