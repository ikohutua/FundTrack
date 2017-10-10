using FundTrack.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;
using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Concrete
{
    public class GoodsTypeRepository : IGoodsTypeRepository
    {
        private readonly FundTrackContext _context;
        public GoodsTypeRepository(FundTrackContext context)
        {
            _context = context;
        }

        public GoodsType GetGoodsTypeById(int id)
        {
            return this._context.GoodsTypes.FirstOrDefault(a => a.Id == id);
        }

        public GoodsType GetGoodsTypeByName(string name)
        {
            return this._context.GoodsTypes.FirstOrDefault(a => a.Name == name);
        }

        public IEnumerable<GoodsType> Read()
        {
            return this._context.GoodsTypes;
        }
    }
}
