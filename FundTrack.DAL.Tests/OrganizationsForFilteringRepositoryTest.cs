//using FundTrack.DAL.Concrete;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace FundTrack.DAL.Tests
//{
//    public class OrganizationsForFilteringRepositoryTest
//    {
//        [Fact]
//        public void GetAll_FromContext_IEnumerable<Organization>()
//        {
//            //Arrange
//            var data = new List<Organization>()
//            {
//                new Organization(){ Id = 1, Name = "Name1"},
//                new Organization(){ Id = 2, Name = "Name2"}
//            }.AsQueryable();

//            var mockDbSet = new Mock<DbSet<Organization>>();
//            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Provider).Returns(data.Provider);
//            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.Expression).Returns(data.Expression);
//            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.ElementType).Returns(data.ElementType);
//            mockDbSet.As<IQueryable<Organization>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

//            var mockContext = new Mock<FundTrackContext>();
//            mockContext.Setup(x => x.Organizations).Returns(mockDbSet.Object);

//            var repository = new OrganizationsForFilteringRepository();

//            //Act
//            var resultOrganizations = (repository.GetAll as List<Organization>);

//            //Assert
//            Assert.Equal(2, resultOrganizations.Count);
//            Assert.Equal("Name1", resultOrganizations[0].Name);
//            Assert.Equal(1, resultOrganizations[0].Id);
//        }
//    }
//}
