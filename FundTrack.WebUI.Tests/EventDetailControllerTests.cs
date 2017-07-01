using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.WebUI.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FundTrack.WebUI.Tests
{
    /// <summary>
    /// Test class for EventDetailController
    /// </summary>
    public sealed class EventDetailControllerTests
    {
        /// <summary>
        /// Events the detail by identifier setup service one item of EventDetailViewModel.
        /// SCRIPT - Setup service. Service returns one item of of EventDetailViewModel
        /// RESULT - One item of of EventDetailViewModel
        /// </summary>
        [Fact]
        public void EventDetailById_SetupService_OneItemOfEventDetailViewModel()
        {
            //Arrange
            var serviceMock = new Mock<IViewService<EventDetailViewModel>>();
            serviceMock.Setup(c => c.ReadById(1)).Returns(new EventDetailViewModel()
            {
                Id = 1,
                OrganizationName = "OrganizationName1",
                Description = "Description1",
                OrganizationId = 1,
                CreateDate = DateTime.Today,
                ImageUrl = new List<string>() { "www1" }
            });

            var controller = new EventDetailController(serviceMock.Object);

            //Act
            var result = controller.EventDetailById(1);
            
            //Assert
            Assert.Equal(1, result.Id);
            Assert.True("OrganizationName1" == result.OrganizationName);
        }
    }
}
