using Moq;
using Taskly.Dashboard.App.Services.Interfaces;
using Taskly.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using Home.Controllers;
using Taskly.Dashboard.App.Exceptions;

namespace Taskly.Dashboard.App.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockUserService.Object, _mockLogger.Object);
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
            var model = GetSampleUser();

            _controller.ModelState.AddModelError("Email", "Invalid email format");

            var result = await _controller.Register(model);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model); 
        }

        [Fact]
        public async Task Register_Post_ReturnsRedirectToAction_WhenUserIsRegisteredSuccessfully()
        {
            var model = GetSampleUser();

            _mockUserService.Setup(s => s.RegisterUserAsync(It.IsAny<User>())).ReturnsAsync(1);

            _controller.ModelState.Clear();

            var result = await _controller.Register(model);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Register", redirect.ActionName);
        }


        [Fact]
        public async Task Register_Post_ReturnsViewResult_WhenEmailIsAlreadyTaken()
        {
            var model = GetSampleUser();

            _mockUserService.Setup(service => service.RegisterUserAsync(It.IsAny<User>())).ThrowsAsync(new UserRegistrationException("Email already exists."));

            var result = await _controller.Register(model);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
            Assert.True(_controller.ModelState.ContainsKey("Email")); 
        }

        [Fact]
        public async Task Register_Post_ReturnsViewResult_WhenUnexpectedErrorOccurs()
        {
            var model = GetSampleUser();

            _mockUserService.Setup(service => service.RegisterUserAsync(It.IsAny<User>())).ThrowsAsync(new Exception("Unexpected error"));

            var result = await _controller.Register(model);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(_controller.ModelState.ContainsKey("Error")); 
        }
    }
}