using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // <summary>
        /// Checks whether a user with the specified email already exists in the system.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if the email exists; otherwise, false.</returns>
        Task<bool> IsEmailExistsAsync(string email);

        /// <summary>
        /// Checks whether a user with the specified phone number already exists in the system.
        /// </summary>
        /// <param name="phoneNumber">The phone number to check.</param>
        /// <returns>True if the phone number exists; otherwise, false.</returns>
        Task<bool> IsPhoneNumberExistsAsync(string phoneNumber);

        /// <summary>
        /// Adds a new user to the database and returns the ID of the newly created user.
        /// </summary>
        /// <param name="user">The user information to be registered.</param>
        /// <returns>The ID of the user after being saved to the database.</returns>
        Task<long> RegisterUserAsync(User user);
    }
}