using System.Collections.Generic;
using System.Linq;

namespace FundTrack.Infrastructure.ExtensionMethods
{
    /// <summary>
    /// Class for Pagination Extension
    /// </summary>
    public static class PaginationExtension
    {
        /// <summary>
        /// Gets Items for Pagination
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="quary">Items in query</param>
        /// <param name="itemsOnPage">Items on page to take</param>
        /// <param name="currentPage">Current page</param>
        /// <returns>Items for specifice page</returns>
        public static IQueryable<Entity> GetItemsPerPage<Entity>(this IQueryable<Entity> quary, int itemsOnPage, int currentPage)
        {
            return quary.Skip((currentPage - 1) * itemsOnPage).Take(itemsOnPage);
        }

        /// <summary>
        /// Gets Items for Pagination
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="items">Items in query</param>
        /// <param name="itemsOnPage">Items on page to take</param>
        /// <param name="currentPage">Current page</param>
        /// <returns>Items for specifice page</returns>
        public static IEnumerable<Entity> GetItemsPerPage<Entity>(this IEnumerable<Entity> items, int itemsOnPage, int currentPage)
        {
            return items.Skip((currentPage - 1) * itemsOnPage).Take(itemsOnPage);
        }
    }
}
