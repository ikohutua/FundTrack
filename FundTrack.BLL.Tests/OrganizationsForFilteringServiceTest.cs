using FundTrack.BLL.Concrete;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FundTrack.BLL.Tests
{
    /// <summary>
    /// Test class for OrganizationsForFilteringService
    /// </summary>
    /// <para>
    /// Reference to article - Getting started with xUnit.net (.NET Core / ASP.NET Core)
    /// <seealso cref="http://xunit.github.io/docs/getting-started-dotnet-core"/>
    /// </para>
    /// <para>
    /// Reference to article - How to test exception in method
    /// <seealso cref="https://www.richard-banks.org/2015/07/stop-using-assertthrows-in-your-bdd.html"/>
    /// </para>
    public class OrganizationsForFilteringServiceTest
    {
        /// <summary>
        /// Tests GetAll method in the OrganizationsForFilteringService
        /// SCRIPT - Setup repository and unitOfWork. Repository returns collection
        /// RESULT - The result is collection of OrganizationsForFilteringViewModel
        /// </summary>
        [Fact]
        public void GetAll_SetupRepository_CollectionOfOrganizationsForFilteringViewModel()
        {
            //Arrange
            var reposirory = new Mock<IOrganizationsForFilteringRepository>();
            reposirory.Setup(x => x.GetAll)
                 .Returns(new List<Organization>()
                 {
                     new Organization(){ Id = 1, Name = "Name1"},
                     new Organization(){ Id = 2, Name = "Name2"}
                 });

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.OrganizationsForFilteringRepository)
                .Returns(reposirory.Object);

            var service = new OrganizationsForFilteringService(unitOfWork.Object);

            //Act
            var result = service.GetAll();

            //Assert
            Assert.Equal(1, result.ElementAt(0).Id);
            Assert.Equal("Name2", result.ElementAt(1).Name);
           
            reposirory.Verify(x => x.GetAll);
            unitOfWork.Verify(x => x.OrganizationsForFilteringRepository);
        }

        /// <summary>
        /// Tests GetAll method in OrganizationsForFilteringService
        /// SCRIPT - Repository.GetAll returns null
        /// RESULT - service.GetAll() throws BusinessLogicException
        /// </summary>
        [Fact]
        public void GetAll_Null_BuisnesLogicException()
        {
            //Arrange
            var reposirory = new Mock<IOrganizationsForFilteringRepository>();
            reposirory.Setup(x => x.GetAll).Returns(Enumerable.Empty<Organization>);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.OrganizationsForFilteringRepository)
                .Returns(reposirory.Object);

            var service = new OrganizationsForFilteringService(unitOfWork.Object);

            //Act
            var exception = Record.Exception(() => service.GetAll());

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<BusinessLogicException>(exception);
            //message
        }
    }
}
