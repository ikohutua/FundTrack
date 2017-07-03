using FundTrack.BLL.Concrete;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FundTrack.BLL.Tests
{
    /// <summary>
    /// Test class for BaseService
    /// </summary>
    public sealed class BaseServiceTests
    {
        private List<User> _users;

        public BaseServiceTests()
        {
            this._users = new List<User>();
            this._users.Add(new User() { Id = 1, FirstName = "Vasyl" });
            this._users.Add(new User() { Id = 2, FirstName = "Oksana" });
            this._users.Add(new User() { Id = 3, FirstName = "Viktoria" });
            this._users.Add(new User() { Id = 4, FirstName = "Bohdan" });
            this._users.Add(new User() { Id = 5, FirstName = "Taras" });
            this._users.Add(new User() { Id = 6, FirstName = "Serhii" });
            this._users.Add(new User() { Id = 7, FirstName = "Ihor" });
        }

        /// <summary>
        /// Gets the several users for display. Methodfor pagination.
        /// SCRIPT - Setup collection of users.
        /// RESULT - The result is collection collection of three users
        /// </summary>
        [Fact]
        public void GetPageItems_CollectionOFSevenUsers_GetPageItems_CollectionOFThreeUsers()
        {
            //Arrange
            var baseService = new BaseService();

            //Act
            var result = baseService.GetPageItems<User>(this._users, 3, 1);

            //Assert
            Assert.Equal("Vasyl", result.ElementAt(0).FirstName);
            Assert.Equal("Oksana", result.ElementAt(1).FirstName);
            Assert.Equal("Viktoria", result.ElementAt(2).FirstName);
        }
    }
}
