using Home.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Taskly.Dashboard.App.Exceptions;
using Taskly.Dashboard.App.Services.Interfaces;
using Taskly.Users.Models;
using Xunit;

namespace Taskly.Dashboard.App.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly HomeController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_mockUserService.Object, _mockLogger.Object);
        }

        private User GetSampleUser()
        {
            return new User
            {
                Email = "vohuyvu360@gmail.com",
                FirstName = "Vo",
                MiddleName = "Huy",
                LastName = "Vu",
                Password = "HashedPassword",
                Dob = new DateOnly(2002, 4, 18),
                PhoneNumber = "1234567890"
            };
        }

        [Fact]
        public async Task Register_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var model = GetSampleUser();
            _controller.ModelState.AddModelError("Email", "Invalid email format");

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public async Task Register_Post_ReturnsRedirectToAction_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var model = GetSampleUser();
            _mockUserService.Setup(s => s.RegisterUserAsync(It.IsAny<User>())).ReturnsAsync(1);
            _controller.ModelState.Clear();

            // Act
            var result = await _controller.Register(model);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Register", redirect.ActionName);
        }

        [Fact]
        public async Task Register_Post_ReturnsViewResult_WhenEmailIsAlreadyTaken()
        {
            // Arrange
            var model = GetSampleUser();
            _mockUserService.Setup(service => service.RegisterUserAsync(It.IsAny<User>())).ThrowsAsync(new UserRegistrationException("Email already exists."));

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
            Assert.True(_controller.ModelState.ContainsKey("Email"));
        }

        [Fact]
        public async Task Register_Post_ReturnsViewResult_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var model = GetSampleUser();
            _mockUserService.Setup(service => service.RegisterUserAsync(It.IsAny<User>())).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(_controller.ModelState.ContainsKey("Error"));
        }
    }
}