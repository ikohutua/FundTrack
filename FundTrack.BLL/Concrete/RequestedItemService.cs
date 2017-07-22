using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.RequestedItemModel;
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
    public sealed class RequestedItemService : BaseService, IRequestedItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _requestedItemPerPage = 3;

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
            const string InitialStatusName = StuffStatus.New;
            try
            {
                if (requestedItemViewModel == null)
                {
                    throw new ArgumentNullException(nameof(requestedItemViewModel));
                }

                Status status = this._unitOfWork.StatusRepository.GetStatusByName(InitialStatusName);

                if (status == null)
                {
                    throw new BusinessLogicException($"Статус {InitialStatusName} не знайдено");
                }

                RequestedItem requestedItem = this.convertRequestedItem(requestedItemViewModel);
                requestedItem = this._unitOfWork.RequestedItemRepository.Create(requestedItem);
                var requestedImagesList = this.ConvertViewModelImageList(requestedItemViewModel.Images,
                    requestedItem.Id);
                this._unitOfWork.RequestedItemImageRepository.SaveListOfImages(requestedImagesList);
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
                this._unitOfWork.RequestedItemImageRepository.DeleteImagesByRequestedItemId(itemId);
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

                if (requestedItem == null)
                {
                    throw new BusinessLogicException($"Потреба з ідентифікатором {id} не знайдена");
                }

                IEnumerable<RequestedImageViewModel> imagesList = this.ConvertRequestItemImageModelList(requestedItem.RequestedItemImages,
                                                                  requestedItem.Id);

                RequestedItemViewModel itemViewModel = this.ConvertToRequestedItemViewModel(requestedItem, imagesList);

                return itemViewModel;
                
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
                     this.ConvertToRequestedItemViewModel(item, this.ConvertRequestItemImageModelList(item.RequestedItemImages, item.Id)))
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
            var imagesToUpdate = requestedItemViewModel.Images.Where(e => e.RequestedItemId == 0);
            IEnumerable<RequestedItemImage> imagesList = this.ConvertViewModelImageList(imagesToUpdate,
                                                         requestedItemViewModel.Id);

            try
            {
                RequestedItem requestedItem = this.convertRequestedItem(requestedItemViewModel);

                this._unitOfWork.RequestedItemRepository.Update(requestedItem);
                this._unitOfWork.RequestedItemImageRepository.SaveListOfImages(imagesList);

                this._unitOfWork.SaveChanges();

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
                return this._unitOfWork.RequestedItemRepository
                           .ReadAsQueryable()
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
                    RequestedItemId = userResponseModel.RequestedItemId,
                    StatusId = this._unitOfWork.StatusRepository.GetStatusByName(StuffStatus.New).Id
                };

                if (userResponseModel.UserId != 0)
                {
                    userResponse.UserId = userResponseModel.UserId;
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
                            Name = e.Name,
                            GoodsTypeId = x.Id
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
        /// Gets requested items of all organizations with additional information.
        /// </summary>
        /// <returns>Collection of ShowAllRequestedItemModel</returns>
        public IEnumerable<ShowAllRequestedItemModel> GetRequestedItemToShow()
        {
            var events = ((DbSet<RequestedItem>)_unitOfWork.RequestedItemRepository.ReadAsQueryable())
             .Select(c => new ShowAllRequestedItemModel()
             {
                 Id = c.Id,
                 GoodsCategory = c.GoodsCategory.Name,
                 GoodsType = c.GoodsCategory.GoodsType.Name,
                 CreateDate = DateTime.Now,
                 Organization = c.Organization.Name,
                 Status = c.Status.StatusName,
                 Name = c.Name,
                 Description = c.Description
             }).OrderBy(e => e.GoodsCategory);

            return events;
        }

        /// <summary>
        /// Gets  RequestedItem per page.
        /// </summary>
        /// <returns>Collection of ShowAllRequestedItemModel</returns>
        public IEnumerable<ShowAllRequestedItemModel> GetRequestedItemToShowPerPage(FilterPaginationViewModel filter)
        {
            var _showRequstedItems = filter.filterOptions != null ? this._unitOfWork.RequestedItemRepository.FilterRequestedItem(filter)
                : this._unitOfWork.RequestedItemRepository.ReadForPagination(filter.ItemsPerPage, filter.CurrentPage);

            return _showRequstedItems.Select(c => new ShowAllRequestedItemModel()
            {
                Id = c.Id,
                GoodsCategory = c.GoodsCategory.Name,
                GoodsType = c.GoodsCategory.GoodsType.Name,
                CreateDate = DateTime.Now,
                Organization = c.Organization.Name,
                Status = c.Status.StatusName,
                Name = c.Name,
                Description = c.Description
            })
            .OrderBy(e => e.GoodsCategory);
        }

        /// <summary>
        /// Gets Initial data for pagination
        /// </summary>
        /// <returns>Event Initial data</returns>
        public RequestedItemPaginationInitViewModel GetRequestedItemPaginationData()
        {
            return new RequestedItemPaginationInitViewModel
            {
                TotalItemsCount = _unitOfWork.RequestedItemRepository.ReadAsQueryable().Count(),
                ItemsPerPage = _requestedItemPerPage
            };
        }

        /// <summary>
        /// Gets the filter requsted item pagination data.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public RequestedItemPaginationInitViewModel GetFilterRequestedItemPaginationData(FilterRequstedViewModel filters)
        {
            return new RequestedItemPaginationInitViewModel
            {
                TotalItemsCount = _unitOfWork.RequestedItemRepository.FilterRequestedItem(filters).Count(),
                ItemsPerPage = _requestedItemPerPage
            };
        }

        /// <summary>
        /// Gets images by requested item id
        /// </summary>
        /// <param name="imagesList">List of images</param>
        /// <param name="requestedItemId">Id of requested item</param>
        private IEnumerable<RequestedItemImage> ConvertViewModelImageList(IEnumerable<RequestedImageViewModel> imagesList,
                                                 int requestedItemId)
        {
            IEnumerable<RequestedItemImage> images = imagesList
                    .Select(image => new RequestedItemImage
                    {
                        ImageUrl = image.ImageUrl,
                        IsMain = image.IsMain,
                        RequestedItemId = requestedItemId,
                    });

            return images;
        }

        /// <summary>
        /// Converts list of RequestedItemImage model to RequestedItemImageViewModel
        /// </summary>
        /// <param name="imageList">RequestedItemImage models list</param>
        /// <param name="requestedItemId">Id of requested item</param>
        /// <returns>List of Requested item image view model</returns>
        private IEnumerable<RequestedImageViewModel> ConvertRequestItemImageModelList(IEnumerable<RequestedItemImage> imageList,
                                                 int requestedItemId)
        {
            IEnumerable<RequestedImageViewModel> images = imageList
                    .Select(image => new RequestedImageViewModel
                    {
                        Id = image.Id,
                        IsMain = image.IsMain,
                        RequestedItemId = image.RequestedItemId,
                        ImageUrl = image.ImageUrl
                    });

            return images;
        }

        /// <summary>
        /// Delete current image from database
        /// </summary>
        /// <param name="currentImageId">Current image id</param>
        public void DeleteCurrentImage(int currentImageId)
        {
            try
            {
                this._unitOfWork.RequestedItemImageRepository.Delete(currentImageId);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets requested item per page
        /// </summary>
        /// <param name="id">Id of organization</param>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size number</param>
        /// <returns>List of requested items</returns>
        public IEnumerable<RequestedItemViewModel> GetRequestedItemPerPageByorganizationId(int id, int currentPage, int pageSize)
        {
            var resulList = this._unitOfWork.RequestedItemRepository.GetRequestedItemPerPageByorganizationId(id, currentPage, pageSize)
                .Select(item =>
                this.ConvertToRequestedItemViewModel(item, this.ConvertRequestItemImageModelList(item.RequestedItemImages, item.Id)));
            return resulList;
        }

        /// <summary>
        /// Gets requeted item initialize data
        /// </summary>
        /// <param name="organizationId">Id of organization</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemPaginationViewModel GetRequestedItemsInitData(int organizationId)
        {
            return new RequestedItemPaginationViewModel
            {
                RequestedItemsPerPage = 8,
                TotalRequestedItemsCount = this._unitOfWork.RequestedItemRepository.Read()
                .Where(r => r.OrganizationId == organizationId)
                .Count()
            };
        }

        /// <summary>
        /// Converts requested item to requested item view model
        /// </summary>
        /// <param name="requestedItem">Requested item model</param>
        /// <param name="imagesList">Images list</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel ConvertToRequestedItemViewModel(RequestedItem requestedItem, IEnumerable<RequestedImageViewModel> imagesList)
        {
            return new RequestedItemViewModel
            {
                Id = requestedItem.Id,
                Name = requestedItem.Name,
                Description = requestedItem.Description,
                Status = requestedItem.Status?.StatusName,
                GoodsCategoryId = requestedItem.GoodsCategoryId,
                OrganizationId = requestedItem.OrganizationId,
                GoodsCategory = requestedItem.GoodsCategory?.Name,
                GoodsTypeId = requestedItem.GoodsCategory.GoodsTypeId,
                Images = imagesList
            };
        }

        /// <summary>
        /// Gets all organizations from database
        /// </summary>
        /// <returns>List of </returns>
        public IEnumerable<OrganizationForFilteringViewModel> GetOrganizations()
        {
            try
            {
                var organizations = _unitOfWork.OrganizationRepository.Read()
              .Select(c => new OrganizationForFilteringViewModel()
              {
                  Id = c.Id,
                  Name = c.Name,
              }).OrderBy(e => e.Name);

                return organizations;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all goods categories from database
        /// </summary>
        /// <returns>List of goods categories</returns>
        public IEnumerable<GoodsCategoryForFilteringViewModel> GetCategories()
        {
            try
            {
                var categories = _unitOfWork.GoodsCategoryRepository.Read()
              .Select(c => new GoodsCategoryForFilteringViewModel()
              {
                  Id = c.Id,
                  Name = c.Name,
              }).OrderBy(e => e.Name);

                return categories;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all goods type from database
        /// </summary>
        /// <returns>List of goods type</returns>
        public IEnumerable<GoodsTypeForFilteringViewModel> GetTypes()
        {
            try
            {
                var types = _unitOfWork.GoodsTypeRepository.Read()
              .Select(c => new GoodsTypeForFilteringViewModel()
              {
                  Id = c.Id,
                  Name = c.Name,
              }).OrderBy(e => e.Name);

                return types;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all statuses of requested items from database
        /// </summary>
        /// <returns>List of statuses</returns>
        public IEnumerable<StatusForFilteringViewModel> GetStatuses()
        {
            try
            {
                var statuses = _unitOfWork.StatusRepository.Read()
              .Select(c => new StatusForFilteringViewModel()
              {
                  Id = c.Id,
                  Name = c.StatusName,
              }).OrderBy(e => e.Name);

                return statuses;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Converts requested item
        /// </summary>
        /// <param name="requestedItemViewModel"></param>
        /// <returns>Requested item view model</returns>
        private RequestedItem convertRequestedItem(RequestedItemViewModel requestedItemViewModel)
        {
            return new RequestedItem
            {
                Id = requestedItemViewModel.Id,
                Name = requestedItemViewModel.Name,
                Description = requestedItemViewModel.Description,
                StatusId = 1,
                OrganizationId = requestedItemViewModel.OrganizationId,
                GoodsCategoryId = requestedItemViewModel.GoodsCategoryId
            };
        }
    }
}
