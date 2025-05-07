using Moq;
using System;
using System.Threading.Tasks;
using Taskly.Dashboard.App.Exceptions;
using Taskly.Dashboard.App.Repositories.Interfaces;
using Taskly.Dashboard.App.Services;
using Taskly.Users.Models;
using Xunit;

namespace Taskly.Dashboard.App.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepo.Object);
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
                PhoneNumber = "0902004525"
            };
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsUserId_IfValidUser()
        {
            // Arrange
            var user = GetSampleUser();
            _mockRepo.Setup(r => r.IsEmailExistsAsync(user.Email)).ReturnsAsync(false);
            _mockRepo.Setup(r => r.IsPhoneNumberExistsAsync(user.PhoneNumber ?? string.Empty)).ReturnsAsync(false);
            _mockRepo.Setup(r => r.RegisterUserAsync(It.IsAny<User>())).ReturnsAsync(1);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task RegisterUserAsync_ThrowsException_IfEmailExists()
        {
            // Arrange
            var user = GetSampleUser();
            _mockRepo.Setup(r => r.IsEmailExistsAsync(user.Email)).ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UserRegistrationException>(() => _userService.RegisterUserAsync(user));
            Assert.Equal("Email already exists.", ex.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ThrowsException_IfPhoneNumberExists()
        {
            // Arrange
            var user = GetSampleUser();
            _mockRepo.Setup(r => r.IsEmailExistsAsync(user.Email)).ReturnsAsync(false);
            _mockRepo.Setup(r => r.IsPhoneNumberExistsAsync(user.PhoneNumber ?? string.Empty)).ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UserRegistrationException>(() => _userService.RegisterUserAsync(user));
            Assert.Equal("Phone already exists.", ex.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ThrowsArgumentNullException_IfUserIsNull()
        {
            // Arrange
            User? user = null;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.RegisterUserAsync(user!));
            Assert.Equal("User model cannot be null. (Parameter 'model')", ex.Message);
        }
    }
}