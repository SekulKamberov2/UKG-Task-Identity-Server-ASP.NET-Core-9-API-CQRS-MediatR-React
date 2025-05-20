namespace IdentityServer.Application.Interfaces
{
    using IdentityServer.Domain.Models;
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user, string password); 
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> ResetPasswordAsync(int userId, string newPassword);
    }
}
