using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for basic service functionality
    /// </summary>
    public class BaseService
    {               
        /// <summary>
        /// Gets items for page
        /// </summary>
        /// <param name="allItems">Items to paginate</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="currentPage">The current page number</param>
        /// <returns>Items for specifice page</returns>
        public IEnumerable<T> GetPageItems<T>(IEnumerable<T> allItems, int pageSize, int currentPage)
        {
            return allItems.Skip((currentPage - 1) * pageSize).Take(pageSize);
        }
    }
}
