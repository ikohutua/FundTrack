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
                Status status = this._unitOfWork.StatusRepository.GetStatusByName("new");
                RequestedItem requestedItem = new RequestedItem
                {
                    Name = requestedItemViewModel.Name,
                    OrganizationId = 1,
                    StatusId = status.Id,
                    Description = requestedItemViewModel.Description,
                    GoodsCategoryId = requestedItemViewModel.GoodsCategoryId
                };

                requestedItem = this._unitOfWork.RequestedItemRepository.Create(requestedItem);
                this._unitOfWork.SaveChanges();

                return requestedItemViewModel;
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
        public void DeleteRequestedItem(int itemId)
        {
            try
            {
                //delete image before deleting requested item
                this._unitOfWork.RequestedItemRepository.Delete(itemId);
                this._unitOfWork.SaveChanges();
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
                RequestedItem requestedItem = this._unitOfWork.RequestedItemRepository.Get(id);
                return new RequestedItemViewModel
                {
                    Id = requestedItem.Id,
                    Name = requestedItem.Name,
                    Description = requestedItem.Description,
                    Status = requestedItem.Status.StatusName,
                    GoodsCategoryId = requestedItem.GoodsCategoryId,
                    OrganizationId = requestedItem.OrganizationId,
                    GoodsCategory = requestedItem.GoodsCategory.Name,
                };
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Get organization requested item
        /// </summary>
        /// <param name="organizationId">Id of organization</param>
        /// <returns>List of requested items</returns>
        public List<RequestedItemViewModel> GetOrganizationRequestedId(int organizationId)
        {
            try
            {
                List<RequestedItemViewModel> requestedItems = this._unitOfWork.RequestedItemRepository
                    .GetOrganizationRequestedItems(organizationId)
                    .Select(item =>
                     new RequestedItemViewModel
                     {
                         Id = item.Id,
                         Name = item.Name,
                         Description = item.Description,
                         Status = item.Status?.StatusName,
                         GoodsCategory = item.GoodsCategory?.Name,
                         Images = this.GetImagesByRequestedId(item.Id)
                     })
                    .ToList();

                return requestedItems;
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
                RequestedItem requestedItem = new RequestedItem
                {
                    Id = requestedItemViewModel.Id,
                    Name = requestedItemViewModel.Name,
                    Description = requestedItemViewModel.Description,
                    GoodsCategoryId = requestedItemViewModel.GoodsCategoryId,
                    OrganizationId = requestedItemViewModel.OrganizationId,
                    StatusId = 1
                };

                this._unitOfWork.RequestedItemRepository.Update(requestedItem);
                this._unitOfWork.SaveChanges();
                //  RequestedItem requestedItem = requestedItemViewModel;
                // RequestedItemViewModel item = this._unitOfWork.RequestedItemRepository.Update(requestedItem);
                // this._unitOfWork.SaveChanges();

                // return item;
                return requestedItemViewModel;

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
                throw new Exception(ex.Message);
            }
        }
            
        /// <summary>
        /// Gets all goods type from database
        /// </summary>
        /// <returns>List of goods type</returns>
        public IEnumerable<GoodsTypeViewModel> GetAllGoodTypes()
        {
            try
            {
                var goodsCategories = this._unitOfWork.RequestedItemRepository.GetAllGoodTypes()
                    .Select(x => new GoodsTypeViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TypeCategories = x.GoodsCategories.Select(e => new GoodsCategoryViewModel
                        {
                            Id = e.Id,
                            Name = e.Name
                        })
                    });

                return goodsCategories;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets images by requested item id
        /// </summary>
        /// <param name="requestedItemId"></param>
        /// <returns></returns>
        public IEnumerable<RequestedImageViewModel> GetImagesByRequestedId(int requestedItemId)
        {
            try
            {
                var allImages = this._unitOfWork.RequestedItemRepository.GetAllImages(requestedItemId)
                    .Select(i => new RequestedImageViewModel
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        IsMain = i.IsMain,
                        RequestedItemId = i.RequestedItemId
                    });

                return allImages;
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
    }
}
