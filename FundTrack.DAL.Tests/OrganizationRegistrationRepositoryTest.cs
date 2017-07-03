using FundTrack.DAL.Concrete;
using System.Linq;
using Xunit;
namespace FundTrack.DAL.Tests
{
    public sealed class OrganizationRegistrationRepositoryTest
    {
        /// <summary>
        /// Creates new Record in Organization Repository
        /// Script - gets fake db context and setups repository
        /// Result - created record in Organization Repository
        /// </summary>
        [Fact]
        public void CreatesNewOrganization_FakeDbContext_CollectionOfOrganization()
        {
            using (var context = FakeDbContextBuilder.GetFakeContext())
            {
                //Arrange
                var repository = new OrganizationRepository(context);
                
                //Act
                var item = new Entities.Organization {Id=5, Name = "Soldair", Description = "Volunteer Organization" };
                var organization = repository.Create(item);
                context.SaveChanges();

                //Assert
                Assert.IsType<Entities.Organization>(organization);
                Assert.Equal("Volunteer Organization", organization.Description);
                Assert.True(context.Organizations.Count() == 4);                
            }
        }


    }
}
