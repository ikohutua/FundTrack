using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    public interface IGoodsService
    {
        IEnumerable<GoodsTypeViewModel> GetAllGoodsTypes();
        GoodsTypeViewModel GetGoodsTypeById(int id);
        GoodsTypeViewModel GetGoodsTypeByName(string name);
        IEnumerable<GoodsCategoryViewModel> GetAllGoodsCategories();
        GoodsCategoryViewModel GetGoodsCategoryById(int id);
        GoodsCategoryViewModel CreateGoodsCategory(GoodsCategoryViewModel model);
        GoodsCategoryViewModel UpdateGoodsCategory(GoodsCategoryViewModel model);
        void DeleteGoodsCategory(GoodsCategoryViewModel model);
    }   
}
