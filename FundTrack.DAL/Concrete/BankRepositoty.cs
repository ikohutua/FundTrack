using System.Collections.Generic;
using System.Linq;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Concrete
{
    public class BankRepositoty : IBankRepository
    {
        private readonly FundTrackContext _context;

        public BankRepositoty(FundTrackContext context)
        {
            _context = context;
        }

        public IEnumerable<Bank> Read()
        {
            return _context.Banks;
        }

        public Bank Get(int bankId)
        {
            return _context.Banks
                .FirstOrDefault(b => b.Id == bankId);
        }

        public Bank Create(Bank item)
        {
            return _context.Banks.Add(item).Entity;
        }

        public Bank Update(Bank bank)
        {
            return _context.Update(bank).Entity;
        }

        public void Delete(int bankId)
        {
            var deletableBank = _context.Banks.FirstOrDefault(b => b.Id == bankId);
            _context.Banks.Remove(deletableBank);
        }
    }
}
