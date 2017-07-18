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
                    this._unitOfWork.OfferedItemRepository.Create(item);
                    this._unitOfWork.SaveChanges();
                    item = this._unitOfWork.OfferedItemRepository.Read().Where(a => a.Description == model.Description && a.Name == model.Name).FirstOrDefault();
                    item.OfferedItemImages = this.SetPictures(model, item);
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
                    this._unitOfWork.OfferImagesRepository.DeleteOfferedItemImagesByOfferedItemId(model.Id);
                    item.OfferedItemImages = this.SetPictures(model, item);
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
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().OrderByDescending(a=>a.Id))
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
            model.ImageUrl = GetPictures(item);
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
                list.Add(this.InitializeOfferedItemViewModel(item));
            }
            return list;
        }
        public IEnumerable<OfferedItemViewModel> GetPagedUserOfferedItems(int userId, int itemsToLoad, int itemsToSkip)
        {
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().Where(a => a.UserId == userId))
            {
                list.Add(this.InitializeOfferedItemViewModel(item));
            }
            return GetPageItems(list, itemsToLoad, itemsToSkip);
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
        /// <summary>
        /// Sets specified images of specified offer item
        /// </summary>
        /// <param name="model">Offer item view model</param>
        /// <param name="item">Offered item entity</param>
        /// <returns>List of offered item images</returns>
        public List<OfferedItemImage> SetPictures(OfferedItemViewModel model, OfferedItem item)
        {
            List<OfferedItemImage> picList = new List<OfferedItemImage>();
            for (int i = 0; i < model.ImageUrl.Length; i++)
            {
                OfferedItemImage image = new OfferedItemImage();
                image.ImageUrl = model.ImageUrl[i];
                image.OfferedItem = item;
                image.OfferedItemId = item.Id;
                if (i == 0)
                {
                    image.IsMain = true;
                }
                picList.Add(image);
            }
            return picList;
        }
        /// <summary>
        /// Gets pictures of specified offered item
        /// </summary>
        /// <param name="item">Offered item entity</param>
        /// <returns>Array of image urls</returns>
        public string[] GetPictures(OfferedItem item)
        {
            List<string> picList = new List<string>();
            var images = this._unitOfWork.OfferImagesRepository.Read().Where(a => a.OfferedItemId==item.Id);
            foreach (var thing in images)
            {
                picList.Add(thing.ImageUrl);
            }
            return picList.ToArray();
        }
    }
}
