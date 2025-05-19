namespace IdentityServer.Application.Interfaces
{
    using IdentityServer.Application.Results;
    using IdentityServer.Domain.Models;
    public interface IUserManager
    {
        Task<IdentityResult<User?>> FindByEmailAsync(string email);
        Task<IdentityResult<User>> CreateAsync(User user);
        Task<IdentityResult<User>> ValidateUserAsync(string username, string password);
        Task<IdentityResult<User?>> FindByIdAsync(int userId);
        Task<IdentityResult<User>> UpdateAsync(User user);
        Task<IdentityResult<bool>> DeleteAsync(int userId);
    }
}
