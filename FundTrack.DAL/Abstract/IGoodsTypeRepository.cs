using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface IGoodsTypeRepository
    {
        IEnumerable<GoodsType> Read();
        GoodsType GetGoodsTypeById(int id);
        GoodsType GetGoodsTypeByName(string name);
    }
}
