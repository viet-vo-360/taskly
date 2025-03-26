using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool IsEmailExists(string email);
        bool IsPhoneNumberExists(string phoneNumber);
        void RegisterUser(User user);
    }
}
