using FundTrack.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Concrete
{
   public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        FundTrackContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(FundTrackContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }     

        public IEnumerable<TEntity> Read()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        TEntity IRepository<TEntity>.Create(TEntity item)
        {
            _dbSet.Add(item);
            return item;
        }

        TEntity IRepository<TEntity>.Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;        
            return item;
        }

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }
    }
}
