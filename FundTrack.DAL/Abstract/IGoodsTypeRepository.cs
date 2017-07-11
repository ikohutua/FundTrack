using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Abstract
{
    public interface IGoodsTypeRepository
    {
        IEnumerable<GoodsType> Read();
        GoodsType GetGoodsTypeById(int id);
        GoodsType GetGoodsTypeByName(string name);
    }
}
