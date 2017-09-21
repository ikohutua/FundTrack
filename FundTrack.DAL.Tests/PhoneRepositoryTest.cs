using FundTrack.DAL.Concrete;
using System.Linq;
using Xunit;

namespace FundTrack.DAL.Tests
{
    /// <summary>
    /// Test class for OrganizationsForFilteringRepository
    /// </summary>
    public sealed class PhoneRepositoryTest
    {
        private FundTrackContext _context;
        private FakeFundTrackDbContextBaseBuilder _fakeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneRepositoryTest"/> class.
        /// </summary>
        public PhoneRepositoryTest()
        {
            this._fakeBuilder = new FakeFundTrackDbContextBaseBuilder();
            this._fakeBuilder.SetPhones();
            this._context = this._fakeBuilder.GetFakeContext();
        }

        /// <summary>
        /// Tests GetPhoneByUserId method in the PhoneRepository
        /// SCRIPT - Setup fake data base context.
        /// RESULT - The result is instance of phone
        /// </summary>
        [Fact]
        public void GetPhoneByUserIdTest()
        {
            //Arrange
            var repository = new PhoneRepository(this._context);
            var expectedPhone = new Entities.Phone { Id = 1, Number = "0993108753", UserId = 1 };

            //Act
            var result = repository.GetPhoneByUserId(1);

            //Assert
            Assert.Equal( expectedPhone.Id, result.Id);
            Assert.Equal( expectedPhone.Number, result.Number);
            Assert.Equal(expectedPhone.UserId, result.UserId);

        }

        /// <summary>
        /// Tests Add method in the PhoneRepository
        /// SCRIPT - Setup fake data base context.Method add return added phone 
        /// RESULT - The result is instance of phone
        /// </summary>
        [Fact]
        public void AddTest()
        {
            //Arrange
            var repository = new PhoneRepository(this._context);
            var expectedPhone = new Entities.Phone { Id = 4, Number = "0993108444", UserId = 3 };

            //Act
            var result = repository.Add(expectedPhone);

            //Assert
            Assert.Equal(expectedPhone.Id, result.Id);
            Assert.Equal(expectedPhone.Number, result.Number);
            Assert.Equal(expectedPhone.UserId, result.UserId);
        }

        /// <summary>
        /// Tests Update method in the PhoneRepository
        /// SCRIPT - Setup fake data base context.Method add return added phone 
        /// RESULT - The result is instance of phone
        /// </summary>
        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var repository = new PhoneRepository(this._context);
            var expectedPhone = _context.Phones.Where(x=>x.Id == 1).FirstOrDefault();
            var editablePhone = new Entities.Phone { Number = "0991111111", Id = 1, UserId = 1 };

            //Act
            var result = repository.Update(editablePhone);

            //Assert
            Assert.Equal(expectedPhone.Id, result.Id);
            Assert.Equal(expectedPhone.Number, result.Number);
            Assert.Equal(expectedPhone.UserId, result.UserId);
        }
    }
}
