using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Concrete
{
    public class GoodsCategoryRepository : IGoodsCategoryRepository
    {
        private readonly FundTrackContext _context;
        public GoodsCategoryRepository(FundTrackContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates goods category
        /// </summary>
        /// <param name="item">GoodsCategory</param>
        /// <returns>GoodsCategory</returns>
        public GoodsCategory Create(GoodsCategory item)
        {
            _context.GoodsCategories.Add(item);
            return item;
        }
        /// <summary>
        /// Unsurprisingly, deletes goods category =(
        /// </summary>
        /// <param name="id">Goods category id</param>
        public void Delete(int id)
        {
            GoodsCategory goodsCategory = _context.GoodsCategories.FirstOrDefault(x => x.Id == id);
            if (goodsCategory != null)
                _context.GoodsCategories.Remove(goodsCategory);
        }
        /// <summary>
        /// Gets GoodsCategory by its id
        /// </summary>
        /// <param name="name">Id of goods category</param>
        /// <returns>GoodsCategory</returns>
        public GoodsCategory GetGoodsCategoryById(int id)
        {
            return this._context.GoodsCategories.SingleOrDefault(g => g.Id == id);
        }
        /// <summary>
        /// Gets GoodsCategory by its name
        /// </summary>
        /// <param name="name">Name of goods category</param>
        /// <returns>GoodsCategory</returns>
        public GoodsCategory GetGoodsCategoryByName(string name)
        {
            return this._context.GoodsCategories.SingleOrDefault(g => g.Name == name);
        }
        /// <summary>
        /// Reads all goods categories
        /// </summary>
        /// <returns>Collection of goods categories</returns>
        public IEnumerable<GoodsCategory> Read()
        {
            return _context.GoodsCategories;
        }
        /// <summary>
        /// Updates goods category
        /// </summary>
        /// <param name="item">GoodsCategory</param>
        /// <returns>GoodsCategory</returns>
        public GoodsCategory Update(GoodsCategory item)
        {
            _context.Update(item);
            return item;
        }
    }
}
