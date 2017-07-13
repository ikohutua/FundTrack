using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Abstract
{
    public interface IGoodsCategoryRepository
    {
        /// <summary>
        /// Gets GoodsCategory by its name
        /// </summary>
        /// <param name="name">Name of goods category</param>
        /// <returns>GoodsCategory</returns>
        GoodsCategory GetGoodsCategoryByName(string name);

        /// <summary>
        /// Gets GoodsCategory by its id
        /// </summary>
        /// <param name="name">Id of goods category</param>
        /// <returns>GoodsCategory</returns>
        GoodsCategory GetGoodsCategoryById(int id);

        /// <summary>
        /// Creates goods category
        /// </summary>
        /// <param name="item">GoodsCategory</param>
        /// <returns>GoodsCategory</returns>
        GoodsCategory Create(GoodsCategory item);

        /// <summary>
        /// Unsurprisingly, deletes goods category =(
        /// </summary>
        /// <param name="id">Goods category id</param>
        void Delete(int id);

        /// <summary>
        /// Updates goods category
        /// </summary>
        /// <param name="item">GoodsCategory</param>
        /// <returns>GoodsCategory</returns>
        GoodsCategory Update(GoodsCategory item);

        /// <summary>
        /// Reads all goods categories
        /// </summary>
        /// <returns>Collection of goods categories</returns>
        IEnumerable<GoodsCategory> Read();
    }
}
