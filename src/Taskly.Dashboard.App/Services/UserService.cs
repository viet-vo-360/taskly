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

        public bool RegisterUser(User model)
        {
            if (_userRepository.IsEmailExists(model.Email) || _userRepository.IsPhoneNumberExists(model.PhoneNumber))
            {
                return false;
            }

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
            model.IsActive = true;
            model.EmailVerified = false;

            _userRepository.RegisterUser(model);
            return true;
        }
    }
}
