using Microsoft.EntityFrameworkCore;
using Taskly.Dashboard.App.Data;
using Taskly.Dashboard.App.Repositories.Interfaces;
using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExistsAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

        public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber) => await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);

        public async Task<long> RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}