using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class CurrencyRepository : IRepository<Currency>
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventImageRepository"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        public CurrencyRepository(FundTrackContext contextParam)
        {
            this._context = contextParam;
        }
        public Currency Create(Currency item)
        {
            var newItem=this._context.Currencies.Add(item);
            return newItem.Entity;
        }

        public void Delete(int id)
        {
            Currency currencyItem = _context.Currencies.FirstOrDefault(x => x.Id == id);
            if (currencyItem != null)
                _context.Currencies.Remove(currencyItem);
        }

        public Currency Get(int id)
        {
            return this._context.Currencies.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Currency> Read()
        {
            return this._context.Currencies;
        }

        public Currency Update(Currency item)
        {
            var updatedItem=this._context.Currencies.Update(item);
            return updatedItem.Entity;
        }
    }
}
