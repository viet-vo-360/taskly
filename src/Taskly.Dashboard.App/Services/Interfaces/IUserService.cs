using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Services.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(User model);
    }
}
