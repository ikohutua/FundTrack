using FundTrack.DAL.Concrete;
using System.Linq;
using Xunit;

namespace FundTrack.DAL.Tests
{
    /// <summary>
    /// Test class for OrganizationsForFilteringRepository
    /// </summary>
    public sealed class OrganizationsForFilteringRepositoryTest
    {
        /// <summary>
        /// Tests GetAll property in the OrganizationsForFilteringRepository
        /// SCRIPT - Setup fake data base context. GetAll returns collection of Organization
        /// RESULT - The result is collection of Organization
        /// </summary>
        [Fact]
        public void GetAll_SetupFakeDBContext_CollectionOfOrganization()
        {
            using (var context = FakeDbContextBuilder.GetFakeContext())
            {
                //Arrange
                var repository = new OrganizationsForFilteringRepository(context);

                //Act
                var result = repository.GetAll;

                //Assert
                Assert.IsType<Entities.Organization>(result.ElementAt(0));
                Assert.Equal("Name 1", result.ElementAt(0).Name);
                Assert.True(result.Count() == 3);
            }
        }
    }
}
