using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Services.Interfaces
{
    public interface IUserService
    {
        Task<long?> RegisterUser(User model);
    }
}
