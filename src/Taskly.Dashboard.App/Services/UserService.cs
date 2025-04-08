using Taskly.Dashboard.App.Exceptions;
using Taskly.Dashboard.App.Repositories.Interfaces;
using Taskly.Dashboard.App.Services.Interfaces;
using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<long?> RegisterUser(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "User model cannot be null.");
            }

            if (await _userRepository.IsEmailExistsAsync(model.Email))
            {
                throw new UserRegistrationException("Email already exists.");
            }

            if (await _userRepository.IsPhoneNumberExistsAsync(model.PhoneNumber))
            {
                throw new UserRegistrationException("Phone already exists.");
            }

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
            model.IsActive = true;
            model.EmailVerified = false;

            var createdUser = await _userRepository.RegisterUserAsync(model);

            return createdUser.Id;
        }
    }
}