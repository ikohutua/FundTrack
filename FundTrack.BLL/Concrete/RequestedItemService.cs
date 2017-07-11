using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using System.Linq;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Requested item service
    /// </summary>
    public sealed class RequestedItemService : IRequestedItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedItemService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public RequestedItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates requested item in database
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel CreateRequestedItem(RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                RequestedItem requestedItem = requestedItemViewModel;
                RequestedItemViewModel item = this._unitOfWork.RequestedItemRepository.Create(requestedItem);
                this._unitOfWork.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Delete requested item from database
        /// </summary>
        /// <param name="id">Id of requested item</param>
        public void DeleteRequestedItem(int id)
        {
            try
            {
                this._unitOfWork.RequestedItemRepository.Delete(id);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all requested items from database
        /// </summary>
        /// <returns>List of requested item view model</returns>
        public List<RequestedItemViewModel> GetAllItems()
        {
            try
            {
                var allRequestedItems = this._unitOfWork.RequestedItemRepository.Read();
                return allRequestedItems.Select(r => (RequestedItemViewModel)r).ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets requested item from database
        /// </summary>
        /// <param name="id">Id of requested item</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel GetItemById(int id)
        {
            try
            {
                return this._unitOfWork.RequestedItemRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Updates requested item in database
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel UpdateRequestedItem(RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                RequestedItem requestedItem = requestedItemViewModel;
                RequestedItemViewModel item = this._unitOfWork.RequestedItemRepository.Update(requestedItem);
                this._unitOfWork.SaveChanges();

                return item;

            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the requested item detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Requested Detail View Model</returns>
        public RequestedItemDetailViewModel GetRequestedItemDetail(int id)
        {
            try
            {
                return ((DbSet<RequestedItem>)this._unitOfWork.RequestedItemRepository
                                                              .Read())
                                                              .Include(i => i.Organization)
                                                              .Include(i => i.Status)
                                                              .Include(i => i.RequestedItemImages)
                                                              .Include(i => i.GoodsCategory)
                                                              .Select(item => new RequestedItemDetailViewModel
                                                              {
                                                                  Id = item.Id,
                                                                  Name = item.Name,
                                                                  Description = item.Description,
                                                                  GoodsCategoryName = item.GoodsCategory.Name,
                                                                  GoodsTypeName = item.GoodsCategory.GoodsType.Name,
                                                                  StatusName = item.Status.StatusName,
                                                                  OrganizationName = item.Organization.Name,
                                                                  ImagesUrl = item.RequestedItemImages.Select(i => i.ImageUrl).ToList(),
                                                                  MainImageUrl = item.RequestedItemImages.FirstOrDefault(i => i.IsMain == true).ImageUrl
                                                              })
                                                              .FirstOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Creates the user response and changes status RequestedItem in which this UserResponse be created.
        /// </summary>
        /// <param name="userResponseModel">The user response model.</param>
        /// <returns>Requested Detail View Model</returns>
        public UserResponseViewModel CreateUserResponse(UserResponseViewModel userResponseModel)
        {
            try
            {
                var userResponse = new UserResponse
                {
                    Description = userResponseModel.Description,
                    RequestedItemId = userResponseModel.RequestedItemId
                };

                if (userResponseModel.Id != 0)
                {
                    userResponse.UserId = userResponseModel.UserId;
                }

                var requestedItem = this._unitOfWork.RequestedItemRepository.Get(userResponse.RequestedItemId);
                var newStatusId = this._unitOfWork.StatusRepository.GetStatusByName(StuffStatus.New).Id;

                if (requestedItem.StatusId == newStatusId)
                {
                    requestedItem.StatusId = this._unitOfWork.StatusRepository.GetStatusByName(StuffStatus.InProgress).Id;
                    this._unitOfWork.RequestedItemRepository.Update(requestedItem);
                }

                var addedUserResponse = this._unitOfWork.UserResponseRepository.Create(userResponse);

                this._unitOfWork.SaveChanges();

                var addedUserResponseModel = userResponseModel;
                addedUserResponseModel.Id = addedUserResponse.Id;
                return addedUserResponseModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
    }
}
