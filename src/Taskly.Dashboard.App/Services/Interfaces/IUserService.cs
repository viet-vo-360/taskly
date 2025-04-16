using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="model">The user information to register.</param>
        /// <returns>The ID of the newly registered user, or null if registration failed.</returns>
        Task<long?> RegisterUserAsync(User model);
    }
}