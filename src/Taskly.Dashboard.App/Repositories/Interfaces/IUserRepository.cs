using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsPhoneNumberExistsAsync(string phoneNumber);
        Task<User> RegisterUserAsync(User user);
    }
}
