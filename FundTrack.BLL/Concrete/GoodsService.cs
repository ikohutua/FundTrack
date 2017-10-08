using FundTrack.BLL.Abstract;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Concrete
{
    public class GoodsService : IGoodsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GoodsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public GoodsCategoryViewModel CreateGoodsCategory(GoodsCategoryViewModel model)
        {
            GoodsCategory category = new GoodsCategory();
            category.Name = model.Name;
            category.GoodsTypeId = model.GoodsTypeId;
            this._unitOfWork.GoodsCategoryRepository.Create(category);
            this._unitOfWork.SaveChanges();
            return category;
        }

        public void DeleteGoodsCategory(GoodsCategoryViewModel model)
        {
            this._unitOfWork.GoodsCategoryRepository.Delete(model.Id);
            this._unitOfWork.SaveChanges();
        }

        public IEnumerable<GoodsCategoryViewModel> GetAllGoodsCategories()
        {
            List<GoodsCategoryViewModel> list = new List<GoodsCategoryViewModel>();
            foreach (var item in this._unitOfWork.GoodsCategoryRepository.Read())
            {
                GoodsCategoryViewModel category = new GoodsCategoryViewModel();
                category.GoodsTypeId = item.GoodsTypeId;
                category.Id = item.Id;
                category.Name = item.Name;
                list.Add(category);
            }
            return list;
        }

        public IEnumerable<GoodsTypeViewModel> GetAllGoodsTypes()
        {
            List<GoodsTypeViewModel> list = new List<GoodsTypeViewModel>();
            foreach (var item in this._unitOfWork.GoodsTypeRepository.Read())
            {
                GoodsTypeViewModel type = new GoodsTypeViewModel();
                type.Name = item.Name;
                type.Id = item.Id;
                list.Add(type);
            }
            return list;
        }

        public GoodsCategoryViewModel GetGoodsCategoryById(int id)
        {
            return this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryById(id);
        }

        public GoodsTypeViewModel GetGoodsTypeById(int id)
        {
            return this._unitOfWork.GoodsTypeRepository.GetGoodsTypeById(id);
        }

        public GoodsTypeViewModel GetGoodsTypeByName(string name)
        {
            return this._unitOfWork.GoodsTypeRepository.GetGoodsTypeByName(name);
        }

        public GoodsCategoryViewModel UpdateGoodsCategory(GoodsCategoryViewModel model)
        {
            GoodsCategory category = this._unitOfWork.GoodsCategoryRepository.GetGoodsCategoryById(model.Id);
            category.Name = model.Name;
            category.GoodsTypeId = model.GoodsTypeId;
            this._unitOfWork.SaveChanges();
            return category;
        }
        
    }
    
}
