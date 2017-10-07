using FundTrack.BLL.Abstract;
using FundTrack.BLL.Concrete;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Concrete;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FundTrack.BLL.Tests
{
    public sealed class TargetServiceTests
    {
        private List<Target> _testCollection;
        public TargetServiceTests()
        {
            _testCollection = new List<Target>()
            {
                new Target() { Id = 1, TargetName = "їжа", OrganizationId = 1 },
                new Target() { Id = 3, TargetName = "Одяг", OrganizationId = 1 },
                new Target() { Id = 8, TargetName = "спорядження", OrganizationId = 1 }
            };
        }
        [Fact]
        public void GetAllTargetById()
        {
            //Arrange
            var testTarget = _testCollection[0];
            var testId = testTarget.Id;
            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.GetTargetById(testId))
                 .Returns(testTarget);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var result = service.GetTargetById(testId);

            //Assert
            Assert.IsType<TargetViewModel>(result);
            Assert.True(testId == result.TargetId);

            unitOfWork.Verify(x => x.TargetRepository);
        }

        [Fact]
        public void GetAllTargetById_WrongId_BusinessLogicException()
        {
            //Arrange
            var testId = -1;
            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.GetTargetById(testId));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var exception = Record.Exception(() => service.GetTargetById(testId));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<BusinessLogicException>(exception);
            Assert.True(ErrorMessages.CantFindItem == exception.Message);

        }

        [Fact]
        public void GetTargetsByOrganizationId()
        {
            //Arrange
            var testOrgId = 1;
            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.GetTargetsByOrganizationId(testOrgId))
                 .Returns(_testCollection);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var result = service.GetTargetsByOrganizationId(testOrgId);

            //Assert
            Assert.IsType<List<TargetViewModel>>(result);
            foreach (var item in result)
            {
                Assert.True(testOrgId == item.OrganizationId);
            }
            reposirory.Verify(x => x.GetTargetsByOrganizationId(testOrgId));
            unitOfWork.Verify(x => x.TargetRepository);
        }

        [Fact]
        public void GetTargetsByOrganizationId_WrongId_BusinessLogicException()
        {
            //Arrange
            var testId = 1;
            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.GetTargetsByOrganizationId(testId));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var exception = Record.Exception(() => service.GetTargetsByOrganizationId(testId));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<BusinessLogicException>(exception);
            Assert.True(ErrorMessages.CantFindItem == exception.Message);
        }

        [Fact]
        public void EditTarget()
        {
            //Arrange
            var testTargetVm = new TargetViewModel() { TargetId = 5, Name = "техніка", OrganizationId = 2 };
            var testTarget = new Target() { Id = 5, TargetName = "техніка", OrganizationId = 2 };

            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.Update(It.IsAny<Target>()))
                 .Returns(testTarget);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var result = service.EditTarget(testTargetVm);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<TargetViewModel>(result);
            Assert.True(testTargetVm.TargetId == result.TargetId);
            Assert.True(testTargetVm.Name == result.Name);
            Assert.True(testTargetVm.OrganizationId == result.OrganizationId);
        }

        [Fact]
        public void EditTarget_ArgumentException()
        {
            //Arrange
            var testTargetVm = new TargetViewModel() { TargetId = 5, Name = "", OrganizationId = 2 };

            var reposirory = new Mock<ITargetRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var exception = Record.Exception(() => service.EditTarget(testTargetVm));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.True(ErrorMessages.RequiredFieldMessage == exception.Message);
        }

        [Fact]
        public void EditTarget_BusinessLogicException()
        {
            //Arrange
            var testTargetVm = new TargetViewModel() { TargetId = 5, Name = "zaz", OrganizationId = 2 };
            var testTarget = new Target() { Id = 5, TargetName = "техніка", OrganizationId = 2 };

            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.Update(testTarget));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var exception = Record.Exception(() => service.EditTarget(testTargetVm));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<BusinessLogicException>(exception);
            Assert.True(ErrorMessages.UpdateDataError == exception.Message);
        }

        [Fact]
        public void CreateTarget()
        {
            //Arrange
            var testTargetVm = new TargetViewModel() { Name = "техніка", OrganizationId = 2 };
            var testTargetA = new Target() { TargetName = "техніка", OrganizationId = 2 };
            var testTargetB = new Target() { Id = 5, TargetName = "техніка", OrganizationId = 2 };

            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.Create(It.IsAny<Target>()))
                 .Returns(testTargetB);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var result = service.CreateTarget(testTargetVm);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<TargetViewModel>(result);

            Assert.True(0 != result.TargetId);
            Assert.True(testTargetVm.Name == result.Name);
            Assert.True(testTargetVm.OrganizationId == result.OrganizationId);

            unitOfWork.Verify(x => x.TargetRepository);
        }


        [Fact]
        public void CreateTarget_ArgumentException()
        {
            //Arrange
            var testTargetVm = new TargetViewModel() { Name = "", OrganizationId = 2 };

            var reposirory = new Mock<ITargetRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var exception = Record.Exception(() => service.CreateTarget(testTargetVm));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.True(ErrorMessages.RequiredFieldMessage == exception.Message);
        }

        [Fact]
        public void CreateTarget_BusinessLogicException()
        {
            //Arrange
            var testTargetVm = new TargetViewModel() { TargetId = 5, Name = "zaz", OrganizationId = 2 };
            var testTarget = new Target() { Id = 5, TargetName = "техніка", OrganizationId = 2 };

            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.Create(testTarget));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);

            //Act
            var exception = Record.Exception(() => service.CreateTarget(testTargetVm));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<BusinessLogicException>(exception);
            Assert.True(ErrorMessages.CantCreatedItem == exception.Message);
        }

        [Fact]
        public void DeleteTarget_BusinessLogicException()
        {
            //Arrange
            var testId = -1;
            var reposirory = new Mock<ITargetRepository>();
            reposirory.Setup(x => x.Delete(testId)).Throws(new DataAccessException(ErrorMessages.DeleteDataError));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.TargetRepository)
                .Returns(reposirory.Object);

            var finOpService = new Mock<IFinOpService>();

            var service = new TargetService(unitOfWork.Object, finOpService.Object);
            //Act
            var exception = Record.Exception(() => service.DeleteTarget(testId));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<BusinessLogicException>(exception);
            Assert.True(ErrorMessages.DeleteDataError == exception.Message);
        }
    }
}
