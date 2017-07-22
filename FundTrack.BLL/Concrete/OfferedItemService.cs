using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class that manages all interaction related to offer items and offer item images entities
    /// </summary>
    public class OfferedItemService : BaseService, IOfferedItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OfferedItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Creates new Offer Item
        /// </summary>
        /// <param name="model">base model for new offer item</param>
        /// <returns>OfferedItemViewModel</returns>
        public OfferedItemViewModel CreateOfferedItem(OfferedItemViewModel model)
        {
            const string initialStatus = "Активний";
            try
            {
                if (model != null)
                {
                    OfferedItem item = model;
                    item.User = this._unitOfWork.UsersRepository.GetUserById(model.UserId);
                    item.GoodsCategory = this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryById(model.GoodsCategoryId);
                    item.Status = this._unitOfWork.StatusRepository.GetStatusByName(initialStatus);
                    var createdItem = this._unitOfWork.OfferedItemRepository.Create(item);
                    this.SetNewPictures(model, createdItem);
                    this._unitOfWork.SaveChanges();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
        /// <summary>
        /// Edits offer item, that matches received offered item view model
        /// </summary>
        /// <param name="model">offer item view model, received from front end</param>
        /// <returns>Edited offer item view model</returns>
        public OfferedItemViewModel EditOfferedItem(OfferedItemViewModel model)
        {
            try
            {
                if (model != null)
                {
                    OfferedItem item = this._unitOfWork.OfferedItemRepository.Get(model.Id);
                    item.Description = model.Description;
                    item.Name = model.Name;
                    item.GoodsCategory = this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryById(model.GoodsCategoryId);
                    item.GoodsCategoryId = item.GoodsCategory.Id;
                    this.SetOfferedItemPictures(model.Image, item);
                    this._unitOfWork.SaveChanges();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
        /// <summary>
        /// Deletes offered item by its id
        /// </summary>
        /// <param name="id">id of the offer item entity</param>
        public void DeleteOfferedItem(int id)
        {
            try
            {
                this._unitOfWork.OfferedItemRepository.Delete(id);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
           
        }
        /// <summary>
        /// Gets all offered item view models
        /// </summary>
        /// <returns>Collection of all offered item view models</returns>
        public IEnumerable<OfferedItemViewModel> GetAllOfferedItemViewModels()
        {
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().OrderByDescending(a => a.Id))
            {
                list.Add(this.InitializeOfferedItemViewModel(item));
            }
            return list;
        }
        /// <summary>
        /// Gets offereditem viewModel by offeritem id
        /// </summary>
        /// <param name="id">offeritem id</param>
        /// <returns>OfferedItemViewModel</returns>
        public OfferedItemViewModel GetOfferedItemViewModel(int id)
        {
            var item = this._unitOfWork.OfferedItemRepository.Get(id);
            var model = new OfferedItemViewModel();
            model.Description = item.Description;
            model.Name = item.Name;
            model.Id = item.Id;
            model.StatusName = this._unitOfWork.StatusRepository.GetStatusById(item.StatusId).StatusName;
            model.UserId = item.UserId;
            model.GoodsCategoryName = this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryById(item.GoodsCategoryId).Name;
            model.GoodsTypeName = this._unitOfWork.GoodsTypeRepository.GetGoodsTypeById(item.GoodsCategory.GoodsTypeId).Name;
            model.GoodsCategoryId = this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryByName(model.GoodsCategoryName).Id;
            model.GoodsTypeId = this._unitOfWork.GoodsTypeRepository.GetGoodsTypeByName(model.GoodsTypeName).Id;
            model.Image = this.GetOfferedItemPictures(id).ToArray();
            return model;
        }
        /// <summary>
        /// Gets offered items of user by his id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>collection of OfferedItemViewModels</returns>
        public IEnumerable<OfferedItemViewModel> GetUserOfferedItems(int userId)
        {
            try
            {
                List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
                foreach (var item in this._unitOfWork.OfferedItemRepository.Read().Where(a => a.UserId == userId))
                {
                    list.Add(this.InitializeOfferedItemViewModel(item));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets user offered items, based on received parameters
        /// </summary>
        /// <param name="userId">Id of User entity</param>
        /// <param name="itemsToLoad">amount of items to be loaded</param>
        /// <param name="offset">Amount of items to skip, starting from first</param>
        /// <returns>Offered Item View Models</returns>
        public IEnumerable<OfferedItemViewModel> GetPagedUserOfferedItems(int userId, int itemsToLoad, int offset)
        {
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().Where(a => a.UserId == userId))
            {
                list.Add(this.InitializeOfferedItemViewModel(item));
            }
            return GetPageItems(list, itemsToLoad, offset);
        }

        /// <summary>
        /// Initializes offered item view model based on offer item entity
        /// </summary>
        /// <param name="item">offer item</param>
        /// <returns>OfferedItemViewModel</returns>
        public OfferedItemViewModel InitializeOfferedItemViewModel(OfferedItem item)
        {
            OfferedItemViewModel model = new OfferedItemViewModel()
            {
                Description = item.Description,
                Name = item.Name,
                Id = item.Id,
                GoodsCategoryName = item.GoodsCategory.Name,
                StatusName = item.Status.StatusName,
                UserId = item.UserId,
                GoodsTypeName = item.GoodsCategory.GoodsType.Name
            };
            model.Image = this._unitOfWork.OfferImagesRepository.Read().Where(a => a.OfferedItemId == model.Id)
                .Select(a => new OfferedItemImageViewModel
                {
                    ImageUrl = a.ImageUrl,
                    IsMain = a.IsMain,
                    OfferedItemId = model.Id,
                    Id = a.Id
                })
                .ToArray();
            return model;
        }

        /// <summary>
        /// Sets specified images of specified offer item
        /// </summary>
        /// <param name="model">Offer item view model</param>
        /// <param name="item">Offered item entity</param>
        /// <returns>List of offered item images</returns>
        public OfferedItemImageViewModel[] SetNewPictures(OfferedItemViewModel model, OfferedItem item)
        {
            foreach (var thing in model.Image)
            {
                var newImage = new OfferedItemImage
                {
                    ImageUrl = thing.ImageUrl,
                    IsMain = thing.IsMain,
                    OfferedItemId = item.Id
                };
                this._unitOfWork.OfferImagesRepository.Create(newImage);
            }
            return model.Image;
        }
        
        /// <summary>
        /// Gets offered item images of the specified item by it's id
        /// </summary>
        /// <param name="itemId">offered item id</param>
        /// <returns>offered item image view models of the specified offered item</returns>
        public IEnumerable<OfferedItemImageViewModel> GetOfferedItemPictures(int itemId)
        {
            return this.ConvertRequestItemImageModelList(this._unitOfWork.OfferImagesRepository.GetOfferedItemImageByOfferItemId(itemId));
        }
        /// <summary>
        /// Set pictures, received as param for specified offered item id
        /// </summary>
        /// <param name="images">IEnumerable of offered item image view models</param>
        /// <param name="item">offered item entity</param>
        /// <returns>Offered Item Image View models</returns>
        public IEnumerable<OfferedItemImageViewModel> SetOfferedItemPictures(IEnumerable<OfferedItemImageViewModel> images, OfferedItem item)
        {
            var mainImage = images.Where(a => a.IsMain == true).Take(1).ToArray();
            if (mainImage.Count() != 0)
            {
                this.ClearMainImageStatus(item);
                if (mainImage[0].Id != 0 && mainImage[0].IsMain == true)
                {
                    this._unitOfWork.OfferImagesRepository.Get(mainImage[0].Id).IsMain = true;
                }
            }
            IEnumerable<OfferedItemImage> newImages = images.Where(a => a.Id == 0).Select(
                image => new OfferedItemImage
                {
                    ImageUrl = image.ImageUrl,
                    IsMain = image.IsMain,
                    OfferedItemId = image.OfferedItemId,
                    OfferedItem = item
                }
                );
            IEnumerable<OfferedItemImage> incomingImages = images.Select(
                image => new OfferedItemImage
                {
                    Id = image.Id,
                    ImageUrl = image.ImageUrl,
                    IsMain = image.IsMain,
                    OfferedItemId = item.Id
                }
                ).ToList();
            IEnumerable<OfferedItemImage> missingImages = this._unitOfWork.OfferImagesRepository.GetOfferedItemImageByOfferItemId(item.Id).Except(incomingImages, new DAL.Comparers.OfferedItemImageComparator());
            foreach (var stuff in missingImages)
            {
                this._unitOfWork.OfferImagesRepository.Delete(stuff.Id);
            }
            foreach (var picture in newImages)
            {
                this._unitOfWork.OfferImagesRepository.Create(picture);
            }
            return images;
        }

        /// <summary>
        /// Returns offer item image view models fro offered item image entities
        /// </summary>
        /// <param name="imageList"></param>
        /// <returns></returns>
        private IEnumerable<OfferedItemImageViewModel> ConvertRequestItemImageModelList(IEnumerable<OfferedItemImage> imageList)
        {
            IEnumerable<OfferedItemImageViewModel> images = imageList
                    .Select(image => new OfferedItemImageViewModel
                    {
                        Id = image.Id,
                        IsMain = image.IsMain,
                        OfferedItemId = image.OfferedItemId,
                        ImageUrl = image.ImageUrl
                    });
            return images;
        }
        /// <summary>
        /// Removes isMain flag from all images of specified Offered item
        /// </summary>
        /// <param name="item"></param>
        private void ClearMainImageStatus(OfferedItem item)
        {
            var images = this._unitOfWork.OfferImagesRepository.GetOfferedItemImageByOfferItemId(item.Id);
            foreach (var thing in images)
            {
                thing.IsMain = false;
            }
        }

        /// <summary>
        /// Changes status of offered item to received in the view model
        /// </summary>
        /// <param name="model">View model</param>
        /// <returns>View model</returns>
        public OfferItemChangeStatusViewModel ChangeOfferItemStatus(OfferItemChangeStatusViewModel model)
        {
            try
            {
                var item = this._unitOfWork.OfferedItemRepository.Get(model.OfferItemId);
                if (item.UserId==model.UserId)
                {
                    item.Status = this._unitOfWork.StatusRepository.GetStatusByName(model.OfferItemStatus);
                    this._unitOfWork.SaveChanges();
                    return model;
                }
                else
                {
                    return new OfferItemChangeStatusViewModel { ErrorMessage = "Недостатньо прав" };
                }
                
            }
            catch (Exception e)
            {
                return new OfferItemChangeStatusViewModel
                {
                    ErrorMessage = e.Message
                };
            }
           
        }
    }
}
