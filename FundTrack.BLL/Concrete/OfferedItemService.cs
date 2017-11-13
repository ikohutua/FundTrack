using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class that manages all interaction related to offer items and offer item images entities
    /// </summary>
    public class OfferedItemService : BaseService, IOfferedItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManagementService _imgService;
        public OfferedItemService(IUnitOfWork unitOfWork, IImageManagementService imgService)
        {
            _unitOfWork = unitOfWork;
            _imgService = imgService;
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
                    item.User = _unitOfWork.UsersRepository.GetUserById(model.UserId);
                    item.GoodsCategory = _unitOfWork.GoodsCategoryRepository.GetGoodsCategoryById(model.GoodsCategoryId);
                    item.Status = _unitOfWork.StatusRepository.GetStatusByName(initialStatus);
                    item.OfferedItemImages = UploadImagesToStorage(model.Images, model.Id);
                    var createdItem = _unitOfWork.OfferedItemRepository.Create(item);

                    _unitOfWork.SaveChanges();

                    model.Images = ConvertOfferedItemImagesToViewModel(item.OfferedItemImages).ToArray();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Creating offered item error. " + ex.Message);
            }
        }

        /// <summary>
        /// Convert OfferedItemImage To OfferedItemImageViewModel
        /// </summary>
        /// <param name="offeredItemImages"></param>
        /// <returns></returns>
        private OfferedItemImageViewModel ConvertOfferedItemImageToViewModel(OfferedItemImage offeredItemImages)
        {
            return new OfferedItemImageViewModel()
            {
                Id = offeredItemImages.Id,
                IsMain = offeredItemImages.IsMain,
                OfferedItemId = offeredItemImages.OfferedItemId,
                ImageUrl = AzureStorageConfiguration.GetImageUrl(offeredItemImages.ImageUrl)
            };
        }
        /// <summary>
        /// Convert collection of OfferedItemImage To collection of OfferedItemImageViewModel
        /// </summary>
        /// <param name="offeredItemImages">Collection of OfferedItemImage</param>
        /// <returns>Collection of OfferedItemImageViewModel</returns>
        private IEnumerable<OfferedItemImageViewModel> ConvertOfferedItemImagesToViewModel(IEnumerable<OfferedItemImage> offeredItemImages)
        {
            return offeredItemImages.Select(ConvertOfferedItemImageToViewModel).ToList();
        }

        /// <summary>
        /// Convert OfferedItemImageViewModele To OfferedItemImage
        /// </summary>
        /// <param name="offeredItemImages"></param>
        /// <returns></returns>
        private OfferedItemImage ConvertFromOfferedItemImageViewModelToModel(OfferedItemImageViewModel offeredItemImages)
        {
            return new OfferedItemImage()
            {
                Id = offeredItemImages.Id,
                IsMain = offeredItemImages.IsMain,
                OfferedItemId = offeredItemImages.OfferedItemId,
                ImageUrl = AzureStorageConfiguration.GetImageNameFromUrl(offeredItemImages.ImageUrl)
            };
        }

        /// <summary>
        /// Convert collection of OfferedItemImageViewModel To collection of OfferedItemImage
        /// </summary>
        /// <param name="offeredItemImages">Collection of OfferedItemImageViewModel</param>
        /// <returns>Collection of OfferedItemImage</returns>
        private IEnumerable<OfferedItemImage> ConvertOfferedItemImagesVmToModels(IEnumerable<OfferedItemImageViewModel> offeredItemImages)
        {
            return offeredItemImages.Select(ConvertFromOfferedItemImageViewModelToModel).ToList();
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
                    SetOfferedItemPictures(model.Images, item);
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
                var item = _unitOfWork.OfferedItemRepository.Get(id);
                var images = item.OfferedItemImages?.Select(i => i.ImageUrl);

                this._unitOfWork.OfferedItemRepository.Delete(id);

                foreach (var imageName in images)
                {
                    _imgService.DeleteImageAsync(imageName);
                }
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Can't delete offered item. " + ex.Message);
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
            model.Images = this.GetOfferedItemPictures(id).ToArray();
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
            model.Images = this._unitOfWork.OfferImagesRepository.Read()
                .Where(a => a.OfferedItemId == model.Id)
                .Select(ConvertOfferedItemImageToViewModel)
                .ToArray();
            return model;
        }

        /// <summary>
        ///  Sets specified images of specified offer item. First image is Main
        /// </summary>
        /// <param name="images">Base64 code of images</param>
        /// <param name="offeredItemId">Offered item Id</param>
        /// <returns>Collection of offered item images</returns>
        public ICollection<OfferedItemImage> UploadImagesToStorage(IEnumerable<OfferedItemImageViewModel> images, int offeredItemId)
        {
            Dictionary<OfferedItemImage, Task<string>> imageTastDictionary = new Dictionary<OfferedItemImage, Task<string>>();

            foreach (var item in images)
            {
                var newImage = new OfferedItemImage()
                {
                    IsMain = item.IsMain,
                    OfferedItemId = offeredItemId
                };

                var t = _imgService.UploadImageAsync(Convert.FromBase64String(item.Base64Data), item.ImageExtension);
                imageTastDictionary.Add(newImage, t);
            }
            Task.WhenAll(imageTastDictionary.Values);

            foreach (var element in imageTastDictionary)
            {
                element.Key.ImageUrl = element.Value.Result;
            }

            return imageTastDictionary.Keys;
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
        /// <param name="incomingImages">IEnumerable of offered item image view models</param>
        /// <param name="offerItem">offered item entity</param>
        /// <returns>Offered Item Image View models</returns>
        public void SetOfferedItemPictures(IEnumerable<OfferedItemImageViewModel> incomingImages, OfferedItem offerItem)
        {
            //images in Db
            List<OfferedItemImage> storedImages = offerItem.OfferedItemImages.ToList();

            //new images user set
            List<OfferedItemImageViewModel> incomeNewImages = incomingImages.Select(i => i).Where(i => !String.IsNullOrEmpty(i.Base64Data)).ToList();
            if (incomeNewImages.Any(i=>i.IsMain))
            {
                storedImages.ForEach(i => i.IsMain = false);
            }

            //in case when only main image was changed
            storedImages.ForEach(si => si.IsMain = incomingImages.
                                                    Select(i => i).
                                                    Where(i => i.Id == si.Id).
                                                    FirstOrDefault()
                                                    ?.IsMain ?? si.IsMain);

            //old images stored in Db
            var incomeOldImages = incomingImages.Select(i => i).Where(i => String.IsNullOrEmpty(i.Base64Data));
            var incomeOldImagesModel = ConvertOfferedItemImagesVmToModels(incomeOldImages);

            //old images which we have removed from offerItem.Images
            var uslesImages = storedImages.Where(l2 => !incomeOldImagesModel.Any(l1 => l1.ImageUrl == l2.ImageUrl)).ToList();
            foreach (var stuff in uslesImages)
            {
                _unitOfWork.OfferImagesRepository.Delete(stuff.Id);
                storedImages.Remove(stuff);
                _imgService.DeleteImageAsync(AzureStorageConfiguration.GetImageNameFromUrl( stuff.ImageUrl));
            }

            //save new images
            var newImages = UploadImagesToStorage(incomeNewImages, offerItem.Id);
            foreach (var picture in newImages)
            {
                var newImg = _unitOfWork.OfferImagesRepository.Create(picture);
                storedImages.Add(newImg);
            }

            offerItem.OfferedItemImages = storedImages;
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
                        ImageUrl = AzureStorageConfiguration.GetImageUrl(image.ImageUrl)
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
                if (item.UserId == model.UserId)
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
