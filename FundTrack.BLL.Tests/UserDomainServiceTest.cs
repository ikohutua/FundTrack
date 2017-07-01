using FundTrack.BLL.Concrete;
using FundTrack.BLL.DomainServices;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FundTrack.BLL.Tests
{
    public class UserDomainServiceTest
    {
        [Fact]
        public void GetAll_SetupRepository_CollectionOfUsers()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserResporitory>();
            userRepositoryMock.Setup(r => r.Read()).Returns(_getUsersTest);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UsersRepository).Returns(userRepositoryMock.Object);

            var userDomainService = new UserDomainService(unitOfWorkMock.Object);

            //Act
            var result = userDomainService.GetAllUsers();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Vasya", result[0].Login);
            Assert.Equal("Kogut", result[1].LastName);
            

            userRepositoryMock.Verify(r => r.Read());
            unitOfWorkMock.Verify(r => r.UsersRepository);
        }

        [Fact]
        public void CreateUser_SetupRepository_RegiterViewModel()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserResporitory>();

            userRepositoryMock.Setup(r => r.Create(It.IsAny<User>())).Returns(this._getUserModel());

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UsersRepository).Returns(userRepositoryMock.Object);

            var userDomainService = new UserDomainService(unitOfWorkMock.Object);

            //Act
            RegistrationViewModel result = userDomainService.CreateUser(this._getUserModel());

            //Assert
            Assert.Equal("Vasya", result.FirstName);
            Assert.Equal("Sypa", result.LastName);
        }

        [Fact]
        public void CreateUser_ExistedUser_ThrowsEscepton()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserResporitory>();

            userRepositoryMock.Setup(r => r.Create(It.IsAny<User>())).Returns(this._getUserModel());

            User user = this._getUserModel();
            userRepositoryMock.Setup(r => r.isUserExisted(user.Email, user.Login)).Returns(true);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UsersRepository).Returns(userRepositoryMock.Object);

            var userDomainService = new UserDomainService(unitOfWorkMock.Object);
            //Act
            Exception ex = Assert.Throws<BusinessLogicException>(() => userDomainService.CreateUser(this._getUserModel()));
            
            //Assert
            Assert.Equal(ErrorMessages.UserExistsMessage, ex.Message);
        }

        private List<User> _getUsersTest()
        {
            var users = new List<User>
            {
                new User { Login = "Vasya", LastName = "Sypa" },
                new User { Login = "Petya", LastName = "Kogut" }
            };

            return users;
        }

        private User _getUserModel()
        {
            User model = new User
            {
                FirstName = "Vasya",
                LastName = "Sypa",
                Email = "vasrya@gmail.com",
                Password = "1111111",
                Login = "vaska"
            };

            return model;
        }
    }
}
