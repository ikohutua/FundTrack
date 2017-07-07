using FundTrack.DAL.Concrete;
using System.Linq;
using Xunit;
namespace FundTrack.DAL.Tests
{
    public sealed class OrganizationRegistrationRepositoryTest
    {
        private FundTrackContext _context;
        private FakeFundTrackDbContextBaseBuilder _fakeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationRegistrationRepositoryTest"/> class.
        /// </summary>
        public OrganizationRegistrationRepositoryTest()
        {
            this._fakeBuilder = new FakeFundTrackDbContextBaseBuilder();
            this._fakeBuilder.SetOrganizations();
            this._context = this._fakeBuilder.GetFakeContext();
        }
        /// <summary>
        /// Creates new Record in Organization Repository
        /// Script - gets fake db context and setups repository
        /// Result - created record in Organization Repository
        /// </summary>
        [Fact]
        public void CreatesNewOrganization_FakeDbContext_CollectionOfOrganization()
        {
            //Arrange
            var repository = new OrganizationRepository(this._context);

            //Act
            var item = new Entities.Organization { Id = 5, Name = "Soldair", Description = "Volunteer Organization" };
            var organization = repository.Create(item);
            this._context.SaveChanges();

            //Assert
            Assert.IsType<Entities.Organization>(organization);
            Assert.Equal("Volunteer Organization", organization.Description);
            Assert.True(this._context.Organizations.Count() == 4);
        }
    }
}
