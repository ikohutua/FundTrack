using FundTrack.BLL.Concrete;
using FundTrack.DAL.Concrete;
using FundTrack.DAL.Tests;
using FundTrack.Infrastructure.ViewModel;
using Xunit;

namespace FundTrack.BLL.Tests
{
    /// <summary>
    /// Test class for EventDetailViewService
    /// </summary>
    public sealed class EventDetailViewServiceTests
    {
        private FundTrack.DAL.Concrete.FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDetailViewServiceTests"/> class.
        /// </summary>
        public EventDetailViewServiceTests()
        {
            this._context = FakeDbContextBuilder.GetFakeContext();
        }

        /// <summary>
        /// ReadById in EventDetailViewService. In the method makes join from 3 entities
        /// SCRIPT - Setup fake data base context with test data, reposirory and unitOfWork.
        /// RESULT - one item of EventDetailViewModel.
        /// </summary>
        [Fact]
        public void ReadById_SetupFakeDBContext_OneItemOfEventDetailViewModel()
        {
            //Arrange
            var reposirory = new EventRepository(this._context);
            var unitOfWork = new UnitOfWork(this._context, null, null, reposirory, null, null);
            var service = new EventDetailViewService(unitOfWork);

            //Act
            var result = service.ReadById(1);

            //Assert
            Assert.IsType<EventDetailViewModel>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Event Description", result.Description);
            Assert.Equal("EventId = 1", result.ImageUrl[0]);
        }
    }
}
