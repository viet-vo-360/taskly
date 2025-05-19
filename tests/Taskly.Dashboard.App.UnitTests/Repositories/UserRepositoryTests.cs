using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskly.Dashboard.App.Data;
using Taskly.Dashboard.App.Repositories;
using Taskly.Users.Models;
using Xunit;

namespace Taskly.Dashboard.App.UnitTests.Repositories
{
    public class UserRepositoryTests : IDisposable
    {
        private AppDbContext _context;
        private UserRepository _repository;

        public UserRepositoryTests()
        {
            _context = GetInMemoryDbContext();
            _repository = new UserRepository(_context);
        }

        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TasklyDb_{Guid.NewGuid()}")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Test_IsEmailExistsAsync_ReturnsTrue_IfEmailExists()
        {
            // Arrange
            var user = new User
            {
                Email = "vohuyVu360@gmail.com",
                FirstName = "Vo",
                MiddleName = "Huy",
                LastName = "Vu",
                Password = "hashedpassword",
                Dob = new DateOnly(2002, 4, 18),
                PhoneNumber = "0902004525"
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var exists = await _repository.IsEmailExistsAsync("vohuyVu360@gmail.com");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Test_IsPhoneNumberExistsAsync_ReturnsTrue_IfPhoneNumberExists()
        {
            // Arrange
            var user = new User
            {
                Email = "vohuyvu360@gmail.com",
                FirstName = "Nguyen",
                MiddleName = "Van",
                LastName = "a",
                Password = "hashedpassword",
                Dob = new DateOnly(2002, 4, 18),
                PhoneNumber = "0902004525"
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var exists = await _repository.IsPhoneNumberExistsAsync("0902004525");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task Test_RegisterUserAsync_ReturnsUserId_IfValidUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);
            var user = new User
            {
                Email = "vohuyvu360@gmail.com",
                FirstName = "Nguyen",
                MiddleName = "Van",
                LastName = "a",
                Password = "hashedpassword",
                Dob = new DateOnly(2002, 4, 18),
                PhoneNumber = "0902004525"
            };

            // Act
            var userId = await repository.RegisterUserAsync(user);

            // Assert
            Assert.True(userId > 0);
            Assert.Equal(1, await context.Users.CountAsync());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}