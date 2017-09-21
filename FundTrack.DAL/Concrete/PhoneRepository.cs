using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly FundTrackContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PhoneRepository(FundTrackContext context)
        {
            this.context = context;
        }

        public Phone Add(Phone phone)
        {
            var added = context.Phones.Add(phone);
            return added.Entity;
        }

        public Phone GetPhoneByUserId(int userId)
        {
            var phones = context.Phones.Where(x => x.UserId == userId);
            return phones.FirstOrDefault();
        }

        public Phone Update(Phone phone)
        {
            Phone editablePhone = context.Phones.Where(x => x.UserId == phone.UserId).FirstOrDefault();
            editablePhone.Number = phone.Number;
            return editablePhone;
        }
    }
}
