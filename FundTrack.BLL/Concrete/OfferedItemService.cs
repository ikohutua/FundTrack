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
                    var createdItem=this._unitOfWork.OfferedItemRepository.Create(item);
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
            this._unitOfWork.OfferedItemRepository.Delete(id);
            this._unitOfWork.SaveChanges();
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
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().Where(a => a.UserId == userId))
            {
                list.Add(this.InitializeOfferedItemViewModel2(item));
            }
            return list;
        }
        public IEnumerable<OfferedItemViewModel> GetPagedUserOfferedItems(int userId, int itemsToLoad, int offset)
        {
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().Where(a => a.UserId == userId))
            {
                list.Add(this.InitializeOfferedItemViewModel2(item));
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
            OfferedItemViewModel model = new OfferedItemViewModel();
            model.Description = item.Description;
            model.Name = item.Name;
            model.Id = item.Id;
            model.GoodsCategoryName = item.GoodsCategory.Name;
            model.StatusName = item.Status.StatusName;
            model.UserId = item.UserId;
            model.GoodsTypeName = item.GoodsCategory.GoodsType.Name;
            model.ImageUrl = item.OfferedItemImages.Select(a => a.ImageUrl).ToArray();
            return model;
        }

        public OfferedItemViewModel InitializeOfferedItemViewModel2(OfferedItem item)
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
                    ImageUrl=thing.ImageUrl,
                    IsMain=thing.IsMain,
                    OfferedItemId=item.Id
                };
                this._unitOfWork.OfferImagesRepository.Create(newImage);
            }
            return model.Image;
        }
        /// <summary>
        /// Gets pictures of specified offered item
        /// </summary>
        /// <param name="item">Offered item entity</param>
        /// <returns>Array of image urls</returns>
        public string[] GetPictures(OfferedItem item)
        {
            List<string> picList = new List<string>();
            var images = this._unitOfWork.OfferImagesRepository.Read().Where(a => a.OfferedItemId == item.Id);
            foreach (var thing in images)
            {
                picList.Add(thing.ImageUrl);
            }
            return picList.ToArray();
        }
        public IEnumerable<OfferedItemImageViewModel> GetOfferedItemPictures(int itemId)
        {
            return this.ConvertRequestItemImageModelList(this._unitOfWork.OfferImagesRepository.GetOfferedItemImageByOfferItemId(itemId));
        }
        public IEnumerable<OfferedItemImageViewModel> SetOfferedItemPictures(IEnumerable<OfferedItemImageViewModel> images, OfferedItem item)
        {
            var mainImage = images.Where(a => a.IsMain == true).Take(1);
            if (mainImage.Count()!=0)
            {
                this.ClearMainImageStatus(item);
            }
            var newImages = images.Where(a => a.Id == 0).Select(
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
            var missingImages = this._unitOfWork.OfferImagesRepository.GetOfferedItemImageByOfferItemId(item.Id).Except(incomingImages, new DAL.Comparers.OfferedItemImageComparator());
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
                        OfferedItemId=image.OfferedItemId,
                        ImageUrl=image.ImageUrl
                    });
                    return images;
        }
        /// <summary>
        /// Removes isMain flag from all images of specified Offer item
        /// </summary>
        /// <param name="item"></param>
        private void ClearMainImageStatus(OfferedItem item)
        {
            var images=this._unitOfWork.OfferImagesRepository.GetOfferedItemImageByOfferItemId(item.Id);
            foreach (var thing in images)
            {
                thing.IsMain = false;
            }
        }
        
    }
}
