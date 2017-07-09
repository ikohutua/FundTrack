using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Concrete
{
    public class OfferedItemService : IOfferedItemService
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
            if (model!=null)
            {
                OfferedItem item = (OfferedItem)model;
                item.Status = this._unitOfWork.StatusRepository.GetStatusByName(model.StatusName);
                item.User = this._unitOfWork.UsersRepository.GetUserById(model.UserId);
                item.GoodsCategory = this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryByName(model.GoodsCategoryName);
                this._unitOfWork.OfferedItemRepository.Create(item);
                this._unitOfWork.SaveChanges();
            }
            return model;
        }
        /// <summary>
        /// Deletes offered item by its id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOfferedItem(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets all offered item view models
        /// </summary>
        /// <returns>Collection of all offered item view models</returns>
        public IEnumerable<OfferedItemViewModel> GetAllOfferedItemViewModels()
        {
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read())
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets offered items of user by his id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>collection of OfferedItemViewModels</returns>
        public IEnumerable<OfferedItemViewModel> GetUserOfferedItems(int userId)
        {
            List<OfferedItemViewModel> list = new List<OfferedItemViewModel>();
            foreach (var item in this._unitOfWork.OfferedItemRepository.Read().Where(a=>a.UserId==userId))
            {
                list.Add(this.InitializeOfferedItemViewModel(item));
            }
            return list;
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
        /// Updates new Offer Item
        /// </summary>
        /// <param name="model">base model for updating offer item</param>
        /// <returns>OfferedItemViewModel</returns>
        public OfferedItemViewModel UpdateOfferedItem(OfferedItemViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
